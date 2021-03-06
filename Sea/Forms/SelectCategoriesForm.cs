﻿// Author: Alexey Rybakov

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
        public List<MPTTTree> Categories = null;

        /// <summary>
        /// Accept flag.
        /// </summary>
        public bool IsAccepted = false;

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
        /// <param name="categories">categories list</param>
        private void SelectGivenCategories(List<MPTTTree> categories)
        {
            if (categories == null)
            {
                return;
            }

            foreach (MPTTTree category in categories)
            {
                // Bad way.
                // We have to remake it back to categories ids.
                // And on edit book form we should keep whole categories tree.

                MPTTTree tree = Root.FindById(category.Id);

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

            SelectGivenCategories(Categories);

            for (int i = 0; i < CategoriesTreeTV.Nodes.Count; i++)
            {
                CategoriesTreeTV.Nodes[i].ExpandAll();
            }
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
        /// Add categories to the list.
        /// </summary>
        /// <param name="nodes">nodes list</param>
        /// <param name="categories">categories list</param>
        private void AddCategoriesToList(TreeNodeCollection nodes, List<MPTTTree> categories)
        {
            /*
             * We have autocorrection of wrong categories configuration here.
             * Suppose we have the following categories configuration:
             *   cat Math [marked]
             *     cat Geom [marked]
             * And both of them are marked.
             * When we encounter Math category we add it to categories list and do not process Geom subcategory.
             * It is correct behaviour, it is not needed to be fixed.
             */

            foreach (TreeNode node in nodes)
            {
                if (node.BackColor == Parameters.SelectColor)
                {
                    MPTTTree category = FindMPTTTree(node);

                    Debug.Assert(category != null);

                    categories.Add(category);
                }
                else
                {
                    AddCategoriesToList(node.Nodes, categories);
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
            Categories = new List<MPTTTree>();
            AddCategoriesToList(CategoriesTreeTV.Nodes, Categories);
            IsAccepted = true;

            Close();
        }

        /// <summary>
        /// Cancel selection.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">parameters</param>
        private void CancelB_Click(object sender, EventArgs e)
        {
            IsAccepted = false;
            Close();
        }
    }
}
