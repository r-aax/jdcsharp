// Author: Alexey Rybakov

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

using Sea.Tools;
using Sea.Core.Books;
using Lib.DataStruct;
using Lib.GUI.WindowsForms;

namespace Sea.Forms
{
    /// <summary>
    /// Edit categories form.
    /// </summary>
    public partial class EditCategoriesTreeForm : Form
    {
        /// <summary>
        /// Root.
        /// </summary>
        private MPTTTree Root = null;

        /// <summary>
        /// Books.
        /// </summary>
        private BooksList Books = null;

        /// <summary>
        /// Captured node.
        /// </summary>
        private TreeNode HoldedNode = null;

        /// <summary>
        /// Accept button flag.
        /// </summary>
        public bool IsAccepted = false;

        /// <summary>
        /// Find <c>MPTT</c> tree.
        /// </summary>
        /// <param name="node">node</param>
        /// <returns><c>MPTT</c> tree</returns>
        private MPTTTree FindMPTTTree(TreeNode node)
        {
            if (node == null)
            {
                return null;
            }

            List<int> path = new List<int>();
            TreeNode walk_node = node;

            // Put path into stach.
            path.Insert(0, walk_node.Index);
            while (walk_node.Parent != null)
            {
                walk_node = walk_node.Parent;
                path.Insert(0, walk_node.Index);
            }

            // Analogue in recursive tree.
            MPTTTree tree = Root;
            for (int i = 0; i < path.Count; i++)
            {
                tree = tree.Children[path[i]];
            }

            return tree;
        }

        /// <summary>
        /// Get current level.
        /// </summary>
        /// <param name="node">node</param>
        /// <returns>current level nodes</returns>
        TreeNodeCollection GetLevelNodes(TreeNode node)
        {
            return (node.Parent == null)
                   ? CategoriesTreeTV.Nodes
                   : node.Parent.Nodes;
        }

        /// <summary>
        /// Free holded node.
        /// </summary>
        private void ResetHoldedNode()
        {
            if (HoldedNode != null)
            {
                HoldedNode.BackColor = Color.Empty;
                HoldedNode = null;
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sea">sea</param>
        public EditCategoriesTreeForm(Core.Sea sea)
        {
            InitializeComponent();

            Books = sea.Books;
            Root = sea.CategoryRoot;
        }

        /// <summary>
        /// Open form.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void EditCategoriesTreeForm_Shown(object sender, EventArgs e)
        {
            if (Root.IsLeaf)
            {
                // Write that we create new categories tree.
                Text = "Create new categories tree (no categories file is found)";
            }
            else
            {
                Root.ToTreeView(CategoriesTreeTV);
            }

            SetButtonsEnable();

            if (CategoriesTreeTV.Nodes.Count > 0)
            {
                CategoriesTreeTV.Nodes[0].ExpandAll();
            }
        }

        /// <summary>
        /// Enabled buttons.
        /// </summary>
        private void SetButtonsEnable()
        {
            TreeNode sel_node = CategoriesTreeTV.SelectedNode;
            bool is_selected = (sel_node != null);
            bool is_holded = (HoldedNode != null);
            bool is_sel_first = is_selected
                                && (sel_node.Index == 0);
            bool is_sel_last = is_selected
                               && (sel_node.Index == GetLevelNodes(sel_node).Count - 1);

            // We can always add categories.
            NewCategoryB.Enabled = true;

            // We can add only near holded node.
            AddBeforeB.Enabled = is_selected;
            AddAfterB.Enabled = is_selected;
            AddUnderB.Enabled = is_selected;

            // We can replace only holded node and only in allowed ranges.
            MoveUpB.Enabled = is_selected && !is_sel_first;
            MoveDownB.Enabled = is_selected && !is_sel_last;

            // We can capture only selected node.
            HoldB.Enabled = is_selected;

            // We need captured and selected nodes for replace.
            ReplaceBeforeB.Enabled = is_selected && is_holded;
            ReplaceAfterB.Enabled = is_selected && is_holded;
            ReplaceUnderB.Enabled = is_selected && is_holded;

            // We can rename only selected node.
            RenameB.Enabled = is_selected;

            // We can delete only selected node.
            DeleteB.Enabled = is_selected;

            // Enabled at all time.
            AcceptB.Enabled = true;

            // We can cancel always.
            CancelB.Enabled = true;
        }

        /// <summary>
        /// New category.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void NewCategoryB_Click(object sender, EventArgs e)
        {
            string name = IO.InputString(null, "Ввод имени новой категории");

            if ((name == null) || (name == ""))
            {
                return;
            }

            // Change tree.
            Root.AddChild(new MPTTTree(name, Root.MaxId() + 1));

            // Change view.
            TreeNode node = CategoriesTreeTV.Nodes.Add(name);
            CategoriesTreeTV.SelectedNode = node;
            CategoriesTreeTV.Select();

            SetButtonsEnable();
        }

        /// <summary>
        /// Add subtree before given.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void AddBeforeB_Click(object sender, EventArgs e)
        {
            string name = IO.InputString(null, "Ввод имени новой подкатегории");

            if ((name == null) || (name == ""))
            {
                return;
            }

            TreeNode node = CategoriesTreeTV.SelectedNode;
            Debug.Assert(node != null);
            MPTTTree tree = FindMPTTTree(node);

            // Change tree.
            tree.Parent.AddChildBefore(new MPTTTree(name, Root.MaxId() + 1), node.Index);

            // Change view.
            CategoriesTreeTV.SelectedNode = GetLevelNodes(node).Insert(node.Index, name);
            CategoriesTreeTV.Select();

            SetButtonsEnable();
        }

        /// <summary>
        /// Add subtree after given.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void AddAfterB_Click(object sender, EventArgs e)
        {
            string name = IO.InputString(null, "Ввод имени новой подкатегории");

            if ((name == null) || (name == ""))
            {
                return;
            }

            TreeNode node = CategoriesTreeTV.SelectedNode;
            Debug.Assert(node != null);
            MPTTTree tree = FindMPTTTree(node);

            // Change tree.
            tree.Parent.AddChildAfter(new MPTTTree(name, Root.MaxId() + 1), node.Index);

            // Change view.
            CategoriesTreeTV.SelectedNode = GetLevelNodes(node).Insert(node.Index + 1, name);
            CategoriesTreeTV.Select();

            SetButtonsEnable();
        }

        /// <summary>
        /// Add subtree under given.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void AddUnderB_Click(object sender, EventArgs e)
        {
            string name = IO.InputString(null, "Ввод имени новой подкатегории");

            if ((name == null) || (name == ""))
            {
                return;
            }

            TreeNode node = CategoriesTreeTV.SelectedNode;
            Debug.Assert(node != null);
            MPTTTree tree = FindMPTTTree(node);

            // Change tree.
            tree.AddChild(new MPTTTree(name, Root.MaxId() + 1));

            // Change view.
            CategoriesTreeTV.SelectedNode = node.Nodes.Add(name);
            node.Expand();
            CategoriesTreeTV.Select();

            SetButtonsEnable();
        }

        /// <summary>
        /// Move up.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void MoveUpB_Click(object sender, EventArgs e)
        {
            TreeNode node = CategoriesTreeTV.SelectedNode;
            Debug.Assert(node != null);
            TreeNodeCollection nodes = GetLevelNodes(node);
            MPTTTree tree = FindMPTTTree(node);

            // There is no way to move up.
            if (node.Index == 0)
            {
                return;
            }

            // Change tree.
            if (!tree.Parent.ReplaceSubtreeBefore(tree, node.Index - 1))
            {
                MessageBox.Show("Can not replace subtree.");

                return;
            }

            // Change view.
            int index = node.Index;
            node.Remove();
            nodes.Insert(index - 1, node);
            CategoriesTreeTV.SelectedNode = node;
            CategoriesTreeTV.Select();

            SetButtonsEnable();
        }

        /// <summary>
        /// Move down.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void MoveDownB_Click(object sender, EventArgs e)
        {
            TreeNode node = CategoriesTreeTV.SelectedNode;
            Debug.Assert(node != null);
            TreeNodeCollection nodes = GetLevelNodes(node);
            MPTTTree tree = FindMPTTTree(node);

            // Already in the bottom.
            if (node.Index == nodes.Count - 1)
            {
                return;
            }

            // Change tree.
            if (!tree.Parent.ReplaceSubtreeAfter(tree, node.Index))
            {
                MessageBox.Show("Can not replace subtree.");

                return;
            }

            // Change view.
            int index = node.Index;
            node.Remove();
            nodes.Insert(index + 1, node);
            CategoriesTreeTV.SelectedNode = node;
            CategoriesTreeTV.Select();

            SetButtonsEnable();
        }

        /// <summary>
        /// Hold it.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void HoldB_Click(object sender, EventArgs e)
        {
            TreeNode node = CategoriesTreeTV.SelectedNode;
            Debug.Assert(node != null);

            ResetHoldedNode();
            HoldedNode = node;
            HoldedNode.BackColor = Parameters.SelectColor;

            SetButtonsEnable();
        }

        /// <summary>
        /// Replace before.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void ReplaceBeforeB_Click(object sender, EventArgs e)
        {
            TreeNode sel_node = CategoriesTreeTV.SelectedNode;
            Debug.Assert(sel_node != null);
            Debug.Assert(HoldedNode != null);
            MPTTTree sel_tree = FindMPTTTree(sel_node);
            MPTTTree hold_tree = FindMPTTTree(HoldedNode);

            // Change tree.
            if (!sel_tree.Parent.ReplaceSubtreeBefore(hold_tree, sel_node.Index))
            {
                MessageBox.Show("Can not replace subtree.");

                return;
            }

            // Change view.
            HoldedNode.Remove();
            GetLevelNodes(sel_node).Insert(sel_node.Index, HoldedNode);
            CategoriesTreeTV.SelectedNode = HoldedNode;
            CategoriesTreeTV.Select();

            ResetHoldedNode();

            SetButtonsEnable();
        }

        /// <summary>
        /// Replace after.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void ReplaceAfterB_Click(object sender, EventArgs e)
        {
            TreeNode sel_node = CategoriesTreeTV.SelectedNode;
            Debug.Assert(sel_node != null);
            Debug.Assert(HoldedNode != null);
            MPTTTree sel_tree = FindMPTTTree(sel_node);
            MPTTTree hold_tree = FindMPTTTree(HoldedNode);

            // Change tree.
            if (!sel_tree.Parent.ReplaceSubtreeAfter(hold_tree, sel_node.Index))
            {
                MessageBox.Show("Can not replace subtree.");

                return;
            }

            // Change view.
            HoldedNode.Remove();
            GetLevelNodes(sel_node).Insert(sel_node.Index + 1, HoldedNode);
            CategoriesTreeTV.SelectedNode = HoldedNode;
            CategoriesTreeTV.Select();

            ResetHoldedNode();

            SetButtonsEnable();
        }

        /// <summary>
        /// Replace under.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void ReplaceUnderB_Click(object sender, EventArgs e)
        {
            TreeNode sel_node = CategoriesTreeTV.SelectedNode;
            Debug.Assert(sel_node != null);
            Debug.Assert(HoldedNode != null);
            MPTTTree sel_tree = FindMPTTTree(sel_node);
            MPTTTree hold_tree = FindMPTTTree(HoldedNode);

            // Change tree.
            if (!sel_tree.ReplaceSubtree(hold_tree))
            {
                MessageBox.Show("Can not replace subtree.");

                return;
            }

            // Change view.
            HoldedNode.Remove();
            sel_node.Nodes.Add(HoldedNode);
            CategoriesTreeTV.SelectedNode = HoldedNode;
            sel_node.Expand();
            CategoriesTreeTV.Select();

            ResetHoldedNode();

            SetButtonsEnable();
        }

        /// <summary>
        /// Rename.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void RenameB_Click(object sender, EventArgs e)
        {
            TreeNode sel_node = CategoriesTreeTV.SelectedNode;
            Debug.Assert(sel_node != null);
            MPTTTree sel_tree = FindMPTTTree(sel_node);

            string name = IO.InputString(sel_tree.GetDataString(),
                                         "Редактирование названия категории");

            if ((name == null) || (name == ""))
            {
                return;
            }

            sel_node.Text = name;
            sel_tree.SetDataString(name);

            SetButtonsEnable();
        }

        /// <summary>
        /// Delete.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void DeleteB_Click(object sender, EventArgs e)
        {
            TreeNode sel_node = CategoriesTreeTV.SelectedNode;
            Debug.Assert(sel_node != null);
            MPTTTree sel_tree = FindMPTTTree(sel_node);

            // Change tree.
            sel_tree.Remove();

            // Change view.
            sel_node.Remove();

            SetButtonsEnable();
        }

        /// <summary>
        /// Accept.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void AcceptB_Click(object sender, EventArgs e)
        {
            // Close form.
            IsAccepted = true;
            Close();
        }

        /// <summary>
        /// Cancel.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void CancelB_Click(object sender, EventArgs e)
        {
            // Cancel changes end exit.
            IsAccepted = false;
            Close();
        }

        /// <summary>
        /// Select node.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void CategoriesTreeTV_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SetButtonsEnable();
        }

        /// <summary>
        /// Resresh counters.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void FunctionsRefreshCountersMI_Click(object sender, EventArgs e)
        {
            // First reset Is.
            Root.ResetI();

            foreach (Book b in Books.Items)
            {
                // First propagate counters.
                foreach (MPTTTree t in b.Categories.Items)
                {
                    t.ImpulseB();
                }

                // Now from root node we increment I for all nodes where B = true.
                Root.IncI();
                Root.ResetB();
            }

            if (Root != null)
            {
                Root.ToTreeView(CategoriesTreeTV);
                SetButtonsEnable();

                if (CategoriesTreeTV.Nodes.Count > 0)
                {
                    CategoriesTreeTV.Nodes[0].ExpandAll();
                }
            }
        }

        /// <summary>
        /// Hide counters.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void FunctionsHideCountersMI_Click(object sender, EventArgs e)
        {
            Root.ResetI();

            if (Root != null)
            {
                Root.ToTreeView(CategoriesTreeTV);
                SetButtonsEnable();

                if (CategoriesTreeTV.Nodes.Count > 0)
                {
                    CategoriesTreeTV.Nodes[0].ExpandAll();
                }
            }
        }

        /// <summary>
        /// Check identifiers.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">parameters</param>
        private void DiagnosticsCheckIdsMI_Click(object sender, EventArgs e)
        {
            int[] ids = new int[Root.NodesCount];

            // Zero all.
            for (int i = 0; i < ids.Length; i++)
            {
                ids[i] = 0;
            }

            // Write ids.
            Root.WriteIdsToArray(ids, 0);
            Array.Sort(ids);

            // Check all identifiers.
            for (int i = 0; i < ids.Length; i++)
            {
                if (ids[i] != i - 1)
                {
                    MessageBox.Show(String.Format("Check if fault: position = {0}, value = {1} (right value is {2}).",
                                                  i,
                                                  ids[i],
                                                  i - 1),
                                    "Identifiers checking");

                    return;
                }
            }

            MessageBox.Show("Check is complete: no errors are found.", "Identifiers checking");
        }
    }
}
