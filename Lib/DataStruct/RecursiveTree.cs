// Author: Alexey Rybakov

using System.Collections.Generic;
using System;
using System.IO;
using System.Xml.Serialization;

namespace Lib.DataStruct
{
    /// <summary>
    /// Recursive tree is a tree that has a root and
    /// collection of children (trees too).
    /// </summary>
    public class RecursiveTree
    {
        /// <summary>
        /// User data.
        /// </summary>
        public object Data;

        /// <summary>
        /// Children.
        /// </summary>
        private List<RecursiveTree> _Children;

        /// <summary>
        /// Children access.
        /// </summary>
        public List<RecursiveTree> Children
        {
            get
            {
                return _Children;
            }
        }

        /// <summary>
        /// Direct children count.
        /// </summary>
        public int ChildrenCount
        {
            get
            {
                return Children.Count;
            }
        }

        /// <summary>
        /// Parent.
        /// </summary>
        private RecursiveTree Parent;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public RecursiveTree()
        {
            Data = null;
            _Children = new List<RecursiveTree>();
            Parent = null;
        }

        /// <summary>
        /// Constructor by string label.
        /// </summary>
        /// <param name="label">string</param>
        public RecursiveTree(string label)
            : this()
        {
            Data = label as object;
        }

        /// <summary>
        /// Add child.
        /// </summary>
        /// <param name="child">child</param>
        public void AddChild(RecursiveTree child)
        {
            Children.Add(child);
            child.Parent = this;
        }

        /// <summary>
        /// Delete children.
        /// </summary>
        public void DeleteChildren()
        {
            _Children.Clear();
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
        /// Deserialization.
        /// </summary>
        /// <param name="file_name">name of file</param>
        /// <returns>tree</returns>
        static public RecursiveTree XmlDeserialize(string file_name)
        {
            try
            {
                TextReader reader = new StreamReader(file_name);
                XmlSerializer serializer = new XmlSerializer(typeof(RecursiveTree));
                RecursiveTree tree = serializer.Deserialize(reader) as RecursiveTree;

                reader.Close();

                return tree;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
