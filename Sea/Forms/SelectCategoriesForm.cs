// Author: Alexey Rybakov

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

using Lib.DataStruct;
using Sea.Tools;

namespace Sea.Forms
{
    /// <summary>
    /// Select categories form.
    /// </summary>
    public partial class SelectCategoriesForm : Form
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public SelectCategoriesForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Categories tree root.
        /// </summary>
        private MPTTTree Root = null;

        /// <summary>
        /// List of categories identifiers.
        /// </summary>
        public List<int> CategoriesIds = null;

        /// <summary>
        /// Find <c>MPTT</c> tree by node.
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

            // Put full path (list of indices) into the stack.
            path.Insert(0, walk_node.Index);
            while (walk_node.Parent != null)
            {
                walk_node = walk_node.Parent;
                path.Insert(0, walk_node.Index);
            }

            // Fund analogue in recursive tree.
            MPTTTree tree = Root;
            for (int i = 0; i < path.Count; i++)
            {
                tree = tree.Children[path[i]];
            }

            return tree;
        }

        /// <summary>
        /// Find node of view by categories subtree.
        /// </summary>
        /// <param name="tree">subtree</param>
        /// <returns>node</returns>
        private TreeNode FindTreeNode(MPTTTree tree)
        {
            if (tree == null)
            {
                return null;
            }

            List<int> path = new List<int>();
            MPTTTree walk_tree = tree;

            // Put full path (list of indices) into the stack.
            while (walk_tree.Parent != null)
            {
                int index = walk_tree.Parent.Children.IndexOf(walk_tree);

                path.Insert(0, index);
                walk_tree = walk_tree.Parent;
            }

            // Find analogue in the view.
            TreeNode node = CategoriesTreeTV.Nodes[path[0]];
            for (int i = 1; i < path.Count; i++)
            {
                node = node.Nodes[path[i]];
            }

            return node;
        }

        /// <summary>
        /// Select categories.
        /// </summary>
        /// <param name="ids">categories identifiers</param>
        private void SelectGivenCategories(List<int> ids)
        {
            if (ids == null)
            {
                return;
            }

            foreach (int id in ids)
            {
                MPTTTree tree = Root.FindById(id);

                Debug.Assert(tree != null);

                TreeNode node = FindTreeNode(tree);

                Debug.Assert(node != null);

                node.BackColor = Parameters.SelectColor;
            }
        }

        /// <summary>
        /// Open form event.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void SelectCategoriesForm_Shown(object sender, EventArgs e)
        {
            Root = MPTTTree.XmlDeserialize(Parameters.CategoriesTreeXMLFullFilename);

            if (Root != null)
            {
                Root.ToTreeView(CategoriesTreeTV);
            }

            SelectGivenCategories(CategoriesIds);
        }

        /// <summary>
        /// Check if parent node is selected.
        /// </summary>
        /// <param name="node">node</param>
        /// <returns>
        /// <c>true</c> - if parent node is selected, 
        /// <c>false</c> - otherwise
        /// </returns>
        private bool IsParentNodeSelected(TreeNode node)
        {
            TreeNode parent = node.Parent;

            if (parent == null)
            {
                return false;
            }

            return (parent.BackColor == Parameters.SelectColor)
                   || IsParentNodeSelected(parent);
        }

        /// <summary>
        /// Clear selection.
        /// </summary>
        /// <param name="nodes">nodes</param>
        private void DeselectAll(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                node.BackColor = Color.Empty;
                DeselectAll(node.Nodes);
            }
        }

        /// <summary>
        /// Select category.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void SelectB_Click(object sender, EventArgs e)
        {
            TreeNode node = CategoriesTreeTV.SelectedNode;

            if (node == null)
            {
                // Node is not selected.
                return;
            }

            if (node.BackColor == Parameters.SelectColor)
            {
                // Cancel select.
                node.BackColor = Color.Empty;
            }
            else
            {
                // Select only if no parent selected.
                if (IsParentNodeSelected(node))
                {
                    MessageBox.Show("Parent category is already selected.");
                }
                else
                {
                    node.BackColor = Parameters.SelectColor;

                    // Clear selection for children.
                    DeselectAll(node.Nodes);
                }
            }
        }

        /// <summary>
        /// Clear selection.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void DeselectAllB_Click(object sender, EventArgs e)
        {
            DeselectAll(CategoriesTreeTV.Nodes);
        }

        /// <summary>
        /// Add categories identifiers to the list.
        /// </summary>
        /// <param name="nodes">nodes list</param>
        /// <param name="ids">identifiers list</param>
        private void AddCategoriesToIdsList(TreeNodeCollection nodes, List<int> ids)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.BackColor == Parameters.SelectColor)
                {
                    MPTTTree tree = FindMPTTTree(node);

                    ids.Add(tree.Id);
                }
                else
                {
                    AddCategoriesToIdsList(node.Nodes, ids);
                }
            }
        }

        /// <summary>
        /// Accept selection.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void AcceptB_Click(object sender, EventArgs e)
        {
            // Rebuild tree.
            CategoriesIds = new List<int>();
            AddCategoriesToIdsList(CategoriesTreeTV.Nodes, CategoriesIds);

            Close();
        }

        /// <summary>
        /// Cancel selection.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void CancelB_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
