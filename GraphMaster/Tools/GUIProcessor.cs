// Author: Alexey Rybakov

using System.Diagnostics;

using Lib.DataStruct.Graph;
using Vector2D = Lib.Maths.Geometry.Geometry2D.Vector;
using Point2D = Lib.Maths.Geometry.Geometry2D.Point;

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
        private Point2D BasePoint = null;

        /// <summary>
        /// Captured node.
        /// </summary>
        private Node CapturedNode = null;

        /// <summary>
        /// Begin coordinates of captured node.
        /// </summary>
        private Point2D CapturedNodePoint = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        public GUIProcessor()
        {
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
        public bool TryToCaptureNode(Graph g, Point2D p)
        {
            Debug.Assert((State == GUIState.Common)
                         && (BasePoint == null)
                         && (CapturedNode == null)
                         && (CapturedNodePoint == null));

            Node n = g.FindNearestNode(p);

            if (n == null)
            {
                return false;
            }

            BasePoint = p;
            CapturedNode = n;
            CapturedNodePoint = n.Point2D.Clone() as Point2D;
            State = GUIState.SingleNodeCaptured;

            return true;
        }

        /// <summary>
        /// Move captured node.
        /// </summary>
        /// <param name="p">current position</param>
        public void MoveCapturedNode(Point2D p)
        {
            Debug.Assert((State == GUIState.SingleNodeCaptured)
                         && (BasePoint != null)
                         && (CapturedNode != null)
                         && (CapturedNodePoint != null));

            // Shift.
            Vector2D v = p - BasePoint;

            // Move node.
            CapturedNode.Point2D = CapturedNodePoint + v;
        }

        /// <summary>
        /// Node moving end.
        /// </summary>
        /// <param name="p">point</param>
        public void FinishNodeDrag(Point2D p)
        {
            Debug.Assert((State == GUIState.SingleNodeCaptured)
                         && (BasePoint != null)
                         && (CapturedNode != null)
                         && (CapturedNodePoint != null));

            BasePoint = null;
            CapturedNode = null;
            CapturedNodePoint = null;

            State = GUIState.Common;
        }

        /// <summary>
        /// Cancel of moving.
        /// </summary>
        public void CancelNodeDrag()
        {
            Debug.Assert((State == GUIState.SingleNodeCaptured)
                         && (BasePoint != null)
                         && (CapturedNode != null)
                         && (CapturedNodePoint != null));

            // Set old coordinates back.
            CapturedNode.Point2D = CapturedNodePoint;
            BasePoint = null;
            CapturedNode = null;
            CapturedNodePoint = null;

            State = GUIState.Common;
        }
    }
}
