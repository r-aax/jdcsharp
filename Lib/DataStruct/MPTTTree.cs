// Author: Alexey Rybakov

using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;

namespace Lib.DataStruct
{
    /// <summary>
    /// Recursive tree, with nodes numeration according to <c>MPTT</c> - Modified Preorder Tree Traversal.
    /// </summary>
    public class MPTTTree : ICloneable
    {
        /// <summary>
        /// Data.
        /// </summary>
        public Object Data { get; set; }

        /// <summary>
        /// Counter.
        /// </summary>
        [XmlIgnore]
        public int I { get; set; }

        /// <summary>
        /// Boolean flag.
        /// </summary>
        [XmlIgnore]
        public bool B { get; set; }

        /// <summary>
        /// Parent.
        /// </summary>
        [XmlIgnore]
        public MPTTTree Parent { get; private set; }

        /// <summary>
        /// Children.
        /// </summary>
        public List<MPTTTree> Children { get; set; }

        /// <summary>
        /// Children count.
        /// </summary>
        [XmlIgnore]
        public int ChildrenCount
        {
            get
            {
                return Children.Count;
            }
        }

        /// <summary>
        /// Node left number.
        /// </summary>
        [XmlAttribute]
        public int LNum { get; set; }

        /// <summary>
        /// Node right number.
        /// </summary>
        [XmlAttribute]
        public int RNum { get; set; }

        /// <summary>
        /// Identifier.
        /// </summary>
        [XmlAttribute]
        public int Id { get; set; }

        /// <summary>
        /// Nodes count.
        /// </summary>
        [XmlIgnore]
        public int NodesCount
        {
            get
            {
                return (RNum - LNum + 1) / 2;
            }
        }

        /// <summary>
        /// Check if node is root.
        /// </summary>
        [XmlIgnore]
        public bool IsRoot
        {
            get
            {
                return Parent == null;
            }
        }

        /// <summary>
        /// Check if node is leaf.
        /// </summary>
        [XmlIgnore]
        public bool IsLeaf
        {
            get
            {
                return RNum == LNum + 1;
            }
        }

        /// <summary>
        /// Current level of tree nodes.
        /// For root returns <c>null</c>.
        /// </summary>
        [XmlIgnore]
        public List<MPTTTree> LevelTrees
        {
            get
            {
                return IsRoot ? null : Parent.Children;
            }
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MPTTTree()
        {
            Data = null;
            I = 0;
            B = false;
            Parent = null;
            Children = new List<MPTTTree>();

            // Root numbers.
            LNum = 0;
            RNum = 1;

            // Not valid id.
            Id = -1;
        }

        /// <summary>
        /// Constructor by string.
        /// </summary>
        /// <param name="data">string data</param>
        public MPTTTree(string data)
            : this()
        {
            SetDataString(data);
        }

        /// <summary>
        /// Constructor by string and identifier.
        /// </summary>
        /// <param name="data">string data</param>
        /// <param name="id">identifier</param>
        public MPTTTree(string data, int id)
            : this(data)
        {
            Id = id;
        }

        /// <summary>
        /// Get root of tree.
        /// </summary>
        [XmlIgnore]
        public MPTTTree Root
        {
            get
            {
                if (IsRoot)
                {
                    return this;
                }

                return Parent.Root;
            }
        }

        /// <summary>
        /// Shift numbers of nodes.
        /// </summary>
        /// <param name="from">first shifted number</param>
        /// <param name="to">last shifted number</param>
        /// <param name="shift">shift value</param>
        /// <remarks>simple not optimal realization</remarks>
        private void ShiftNumbers(int from, int to, int shift)
        {
            if ((LNum > to) || (RNum < from))
            {
                // There is nothing to shift.
                return;
            }

            if (LNum >= from)
            {
                LNum += shift;
            }

            if (RNum <= to)
            {
                RNum += shift;
            }

            foreach (MPTTTree node in Children)
            {
                node.ShiftNumbers(from, to, shift);
            }
        }

        /// <summary>
        /// Shift numbers greater than given value.
        /// </summary>
        /// <param name="from">first shift number</param>
        /// <param name="shift">shift value</param>
        private void ShiftTailNumbers(int from, int shift)
        {
            ShiftNumbers(from, RNum, shift);
        }

        /// <summary>
        /// Set numbers starting with given value.
        /// </summary>
        /// <param name="start_num">start number</param>
        /// <returns>count of set numbers</returns>
        private int SetNumbers(int start_num)
        {
            int inner_start = start_num;

            LNum = inner_start++;

            for (int i = 0; i < ChildrenCount; i++)
            {
                inner_start += Children[i].SetNumbers(inner_start);
            }

            RNum = inner_start++;

            return inner_start - start_num;
        }

        /// <summary>
        /// Add child before child with given number.
        /// </summary>
        /// <param name="child">new child</param>
        /// <param name="before_num">child for insert before</param>
        /// <returns>added child</returns>
        public MPTTTree AddChildBefore(MPTTTree child, int before_num)
        {
            // 0 - to begin of list,
            // ChildrenCount - to end of list.
            Debug.Assert((before_num >= 0) && (before_num <= ChildrenCount));

            // Check if tree already contains element with given id.
            if (Root.ContainsId(child.Id))
            {
                throw new Exception("The outer tree already contains the given identifier.");
            }

            // In general case we can insert not only single child but a tree.
            int add_nodes_count = child.NodesCount;

            // Start number for added subtree numbers.
            // If there is no children or insert to the end of children list - current node right number.
            // In another case - next child left number.
            int start_num = (before_num == ChildrenCount)
                            ? RNum
                            : Children[before_num].LNum;

            // Change numbers.
            child.SetNumbers(start_num);
            Root.ShiftTailNumbers(start_num, 2 * add_nodes_count);

            // Insert subtree.
            Children.Insert(before_num, child);
            child.Parent = this;

            return child;
        }

        /// <summary>
        /// Add new child after child with given number.
        /// </summary>
        /// <param name="child">new child</param>
        /// <param name="after_num">child number for insert after</param>
        /// <returns>added child</returns>
        public MPTTTree AddChildAfter(MPTTTree child, int after_num)
        {
            return AddChildBefore(child, after_num + 1);
        }

        /// <summary>
        /// Adding child to the end of children list.
        /// </summary>
        /// <param name="child">child</param>
        /// <returns>added child</returns>
        public MPTTTree AddChild(MPTTTree child)
        {
            return AddChildAfter(child, ChildrenCount - 1);
        }

        /// <summary>
        /// Delete given subtree from general tree.
        /// </summary>
        public void Remove()
        {
            // It is impossible to delete the root.
            Debug.Assert(!IsRoot);

            // Delete subtree and shift numbers.
            Parent.Children.Remove(this);
            Root.ShiftTailNumbers(RNum + 1, -(2 * NodesCount));
        }

        /// <summary>
        /// Check if tree is outer of given tree.
        /// </summary>
        /// <param name="tree">tree</param>
        /// <returns><c>true</c> - if current tree is outer, <c>false</c> - in another case</returns>
        public bool IsOuter(MPTTTree tree)
        {
            return (LNum <= tree.LNum) && (RNum >= tree.RNum);
        }

        /// <summary>
        /// Check if tree is inner of given tree.
        /// </summary>
        /// <param name="tree">tree</param>
        /// <returns><c>true</c> - if current tree is inner, <c>false</c> - in another case</returns>
        public bool IsInner(MPTTTree tree)
        {
            return (LNum >= tree.LNum) && (RNum <= tree.RNum);
        }

        /// <summary>
        /// Check two trees intersection.
        /// </summary>
        /// <param name="tree">tree</param>
        /// <returns><c>true</c> - is trees intersect, <c>false</c> - otherwise</returns>
        public bool IsIntersection(MPTTTree tree)
        {
            return IsOuter(tree) || IsInner(tree);
        }

        /// <summary>
        /// Replace subtree before given child.
        /// </summary>
        /// <param name="subtree">subtree</param>
        /// <param name="before_num">number of child to replace before</param>
        /// <returns><c>true</c> - if replacement is possible, <c>false</c> - if replacement is inpossible</returns>
        public bool ReplaceSubtreeBefore(MPTTTree subtree, int before_num)
        {
            bool is_possible = !subtree.IsOuter(this);

            if (is_possible)
            {
                subtree.Remove();
                AddChildBefore(subtree, before_num);
            }

            return is_possible;
        }

        /// <summary>
        /// Replace subtree after given child
        /// </summary>
        /// <param name="subtree">subtree</param>
        /// <param name="before_num">number of child to replace after</param>
        /// <returns><c>true</c> - if replacement is possible, <c>false</c> - if replacement is inpossible</returns>
        public bool ReplaceSubtreeAfter(MPTTTree subtree, int before_num)
        {
            return ReplaceSubtreeBefore(subtree, before_num + 1);
        }

        /// <summary>
        /// Replace to the end of children list.
        /// </summary>
        /// <param name="subtree">subtree</param>
        /// <returns><c>true</c> - if replacement is possible, <c>false</c> - if replacement is inpossible</returns>
        public bool ReplaceSubtree(MPTTTree subtree)
        {
            return ReplaceSubtreeAfter(subtree, ChildrenCount - 1);
        }

        /// <summary>
        /// Get data as string.
        /// </summary>
        /// <returns>string</returns>
        public string GetDataString()
        {
            return Data as string;
        }

        /// <summary>
        /// Get data as string with I detalization.
        /// </summary>
        /// <returns></returns>
        public string GetDataStringWithI()
        {
            return String.Format("{0} ({1})", Data as string, I);
        }

        /// <summary>
        /// Set string data.
        /// </summary>
        /// <param name="data">string</param>
        public void SetDataString(string data)
        {
            Data = data;
        }

        /// <summary>
        /// Add children to <c>TreeNodesCollection</c>.
        /// </summary>
        /// <param name="coll">collection</param>
        /// <remarks>only for trees with string data</remarks>
        private void AddChildrenToTreeViewNodes(TreeNodeCollection coll, bool is_counters_to_show)
        {
            for (int i = 0; i < ChildrenCount; i++)
            {
                MPTTTree child = Children[i];

                coll.Add(is_counters_to_show ? child.GetDataStringWithI() : child.GetDataString());
                child.AddChildrenToTreeViewNodes(coll[i].Nodes, is_counters_to_show);
            }
        }

        /// <summary>
        /// Show in <c>TreeView</c>.
        /// </summary>
        /// <param name="tv"><c>TreeView</c> object</param>
        /// <remarks>only for trees with string data</remarks>
        public void ToTreeView(TreeView tv)
        {
            bool is_counters_to_show = (I > 0);

            // Clear component.
            tv.Nodes.Clear();

            // Add subtrees.
            AddChildrenToTreeViewNodes(tv.Nodes, is_counters_to_show);
        }

        /// <summary>
        /// Serialization.
        /// </summary>
        /// <param name="file_name">name of file</param>
        public void XmlSerialize(string file_name)
        {
            XmlSerializer serializer = new XmlSerializer(GetType());
            TextWriter writer = new StreamWriter(file_name);

            serializer.Serialize(writer, this);
            writer.Flush();
            writer.Close();
        }

        /// <summary>
        /// Restore links to parent.
        /// </summary>
        public void RestoreParent()
        {
            Children.ForEach(tree =>
            {
                tree.Parent = this;
                tree.RestoreParent();
            });
        }

        /// <summary>
        /// Deserialization.
        /// </summary>
        /// <param name="file_name">name of file</param>
        /// <returns><c>MPTT</c> tree</returns>
        static public MPTTTree XmlDeserialize(string file_name)
        {
            try
            {
                TextReader reader = new StreamReader(file_name);
                XmlSerializer serializer = new XmlSerializer(typeof(MPTTTree));
                MPTTTree mptt_tree = serializer.Deserialize(reader) as MPTTTree;

                reader.Close();

                mptt_tree.RestoreParent();

                return mptt_tree;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Cast to string.
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return (Data is string) ? (Data as string) : "MPTT";
        }

        /// <summary>
        /// Maximum identifier.
        /// </summary>
        public int MaxId
        {
            get
            {
                if (IsLeaf)
                {
                    return Id;
                }
                else
                {
                    return Children.Max(child => child.MaxId);
                }
            }
        }

        /// <summary>
        /// Find subtree.
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns>subtree</returns>
        public MPTTTree FindById(int id)
        {
            Debug.Assert(id >= 0);

            if (Id == id)
            {
                return this;
            }

            foreach (MPTTTree tree in Children)
            {
                MPTTTree found_tree = tree.FindById(id);

                if (found_tree != null)
                {
                    return found_tree;
                }
            }

            return null;
        }

        /// <summary>
        /// Check if tree contains identifier.
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns><c>true</c> - if tree contains identifier, <c>false</c> - otherwise.</returns>
        public bool ContainsId(int id)
        {
            if (Id == id)
            {
                return true;
            }

            foreach (MPTTTree t in Children)
            {
                if (t.ContainsId(id))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Clone.
        /// </summary>
        /// <returns>copy of object</returns>
        public object Clone()
        {
            MPTTTree tree = new MPTTTree();
            
            // Clone only references.
            tree.Data = Data;
            tree.I = I;
            tree.B = B;
            tree.Parent = Parent;
            tree.Children = Children;
            tree.LNum = LNum;
            tree.RNum = RNum;
            tree.Id = Id;

            return tree;
        }

        /// <summary>
        /// Find subtree by identifier.
        /// </summary>
        /// <param name="id">identifier</param>
        /// <returns>subtree or null</returns>
        public MPTTTree Find(int id)
        {
            if (Id == id)
            {
                return this;
            }

            foreach (MPTTTree child in Children)
            {
                MPTTTree subtree = child.Find(id);

                if (subtree != null)
                {
                    return subtree;
                }
            }

            return null;
        }

        /// <summary>
        /// Reset all Is.
        /// </summary>
        public void ResetI()
        {
            I = 0;

            foreach (MPTTTree t in Children)
            {
                t.ResetI();
            }
        }

        /// <summary>
        /// Reset all Bs.
        /// </summary>
        public void ResetB()
        {
            B = false;

            foreach (MPTTTree t in Children)
            {
                t.ResetB();
            }
        }

        /// <summary>
        /// Send impulse to all the tree up till the root.
        /// </summary>
        public void ImpulseB()
        {
            B = true;

            if (!IsRoot)
            {
                Parent.ImpulseB();
            }
        }

        /// <summary>
        /// Increment all subnodes for which B = true.
        /// </summary>
        public void IncI()
        {
            if (B)
            {
                I++;
            }

            foreach (MPTTTree t in Children)
            {
                t.IncI();
            }
        }

        /// <summary>
        /// Write all MPTT tree identifiers to array.
        /// </summary>
        /// <param name="arr">array of integers</param>
        /// <param name="pos">array position</param>
        public void WriteIdsToArray(int[] arr, int pos)
        {
            if (pos >= arr.Length)
            {
                throw new Exception("Array out if range.");
            }

            arr[pos] = Id;
            pos++;

            for (int i = 0; i < ChildrenCount; i++)
            {
                Children[i].WriteIdsToArray(arr, pos);
                pos += Children[i].NodesCount;
            }
        }
    }
}
