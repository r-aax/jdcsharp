using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lib.MathMod.SolidGrid;
using Lib.Maths.Geometry;
using Lib.Maths.Geometry.Geometry3D;
using Vector3D = Lib.Maths.Geometry.Geometry3D.Vector;

namespace Lib.MathMod.Solver
{
    /// <summary>
    /// Godunov's method 1 order of precision.
    /// </summary>
    public class Godunov1
    {
        /// <summary>
        /// Grid.
        /// </summary>
        private SolidGrid.SolidGrid Grid;

        /// <summary>
        /// Time step.
        /// </summary>
        private double Dt;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="grid">grid</param>
        /// <param name="dt">time step</param>
        public Godunov1(SolidGrid.SolidGrid grid, double dt)
        {
            Grid = grid;
            Dt = dt;            
        }

        /// <summary>
        /// Do several iterations of Godunov's method.
        /// </summary>
        /// <param name="n">iterations count</param>
        public void Iters(int n)
        {
            for (int i = 0; i < n; i++)
            {
                Iter();
            }
        }

        /// <summary>
        /// Solid grid iteration.
        /// </summary>
        public void Iter()
        {
            U ru;
            D flow;
            double k = Grid.CellS * Dt;

            Grid.UtoD();

            // X faces
            for (int xi = 0; xi < Grid.NX - 1; xi++)
            {
                for (int yi = 0; yi < Grid.NY; yi++)
                {
                    for (int zi = 0; zi < Grid.NZ; zi++)
                    {
                        Cell left = Grid.Cells[xi, yi, zi];
                        Cell right = Grid.Cells[xi + 1, yi, zi];
                        ru = RiemannToro.X(left.U, right.U);
                        flow = ru.FlowX * k;
                        left.D -= flow;
                        right.D += flow;
                    }
                }
            }

            // X borders.
            for (int yi = 0; yi < Grid.NY; yi++)
            {
                for (int zi = 0; zi < Grid.NZ; zi++)
                {
                    Cell cell;
                        
                    cell = Grid.Cells[0, yi, zi];
                    ru = RiemannToro.X(cell.U.MirrorX, cell.U);
                    flow = ru.FlowX * k;
                    cell.D += flow;

                    cell = Grid.Cells[Grid.NX - 1, yi, zi];
                    ru = RiemannToro.X(cell.U, cell.U.MirrorX);
                    flow = ru.FlowX * k;
                    cell.D -= flow;
                }
            }

            // Y faces.
            for (int xi = 0; xi < Grid.NX; xi++)
            {
                for (int yi = 0; yi < Grid.NY - 1; yi++)
                {
                    for (int zi = 0; zi < Grid.NZ; zi++)
                    {
                        Cell left = Grid.Cells[xi, yi, zi];
                        Cell right = Grid.Cells[xi, yi + 1, zi];
                        ru = RiemannToro.Y(left.U, right.U);
                        flow = ru.FlowY * k;
                        left.D -= flow;
                        right.D += flow;
                    }
                }
            }

            // Y borders.
            for (int xi = 0; xi < Grid.NX; xi++)
            {
                for (int zi = 0; zi < Grid.NZ; zi++)
                {
                    Cell cell;

                    cell = Grid.Cells[xi, 0, zi];
                    ru = RiemannToro.Y(cell.U.MirrorY, cell.U);
                    flow = ru.FlowY * k;
                    cell.D += flow;

                    cell = Grid.Cells[xi, Grid.NY - 1, zi];
                    ru = RiemannToro.Y(cell.U, cell.U.MirrorY);
                    flow = ru.FlowY * k;
                    cell.D -= flow;
                }
            }

            Grid.DtoU();
        }
    }
}
