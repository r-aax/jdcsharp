// Author: Alexey Rybakov

using System.Diagnostics;

using Lib.DataStruct.Graph;
using Lib.Maths.Geometry;

namespace GraphMaster.Tools
{
    /// <summary>
    /// <c>GUI</c> constrol.
    /// </summary>
    public class GUIProcessor
    {
        /// <summary>
        /// State.
        /// </summary>
        public GUIState State = GUIState.Common;

        /// <summary>
        /// Base point (click point).
        /// </summary>
        private Point BasePoint = null;

        /// <summary>
        /// Captured node.
        /// </summary>
        public Node Node = null;

        /// <summary>
        /// Begin coordinates of captured node.
        /// </summary>
        private Point NodePoint = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        public GUIProcessor()
        {
        }

        /// <summary>
        /// Set new state.
        /// </summary>
        /// <param name="state">state</param>
        public void SetState(GUIState state)
        {
            Node = null;
            State = state;
        }

        /// <summary>
        /// Check if node captured.
        /// </summary>
        public bool IsNodeCaptured
        {
            get
            {
                return Node != null;
            }
        }

        /// <summary>
        /// Try to capture node.
        /// </summary>
        /// <param name="g">graph</param>
        /// <param name="p">point</param>
        /// <returns>
        /// <c>true</c> - if node was captured, 
        /// <c>false</c> - otherwise
        /// </returns>
        public bool TryToCaptureNode(Graph g, Point p)
        {
            Debug.Assert((State == GUIState.Common)
                         && (BasePoint == null)
                         && (Node == null)
                         && (NodePoint == null));

            Node n = g.FindNearestNode(p);

            if (n == null)
            {
                return false;
            }

            BasePoint = p;
            Node = n;
            NodePoint = n.P.Clone() as Point;

            return true;
        }

        /// <summary>
        /// Move captured node.
        /// </summary>
        /// <param name="p">current position</param>
        public void MoveCapturedNode(Point p)
        {
            if (!IsNodeCaptured)
            {
                // No node captured.
                return;
            }

            Debug.Assert((BasePoint != null)
                         && (NodePoint != null));

            // Shift.
            Vector v = p - BasePoint;

            // Move node.
            Node.P = NodePoint + v;
        }

        /// <summary>
        /// Node moving end.
        /// </summary>
        /// <param name="p">point</param>
        public void FinishNodeDrag(Point p)
        {
            if (!IsNodeCaptured)
            {
                // No node captured, nothing to do.
                return;
            }

            Debug.Assert((BasePoint != null)
                         && (NodePoint != null));

            BasePoint = null;
            Node = null;
            NodePoint = null;
        }

        /// <summary>
        /// Cancel of moving.
        /// </summary>
        public void CancelNodeDrag()
        {
            if (!IsNodeCaptured)
            {
                // Nothing to cancel.
                return;
            }

            Debug.Assert((BasePoint != null)
                         && (NodePoint != null));

            // Set old coordinates back.
            Node.P = NodePoint;
            BasePoint = null;
            Node = null;
            NodePoint = null;
        }
    }
}
