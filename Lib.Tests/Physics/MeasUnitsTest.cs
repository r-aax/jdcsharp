// Author: Alexey Rybakov

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lib.Physics.MeasUnits;

namespace Lib.Tests.Physics
{
    /// <summary>
    /// Measurements units tests.
    /// </summary>
    [TestClass]
    public class MeasUnitsTest
    {
        /// <summary>
        /// Epsilon.
        /// </summary>
        private double E = 1.0e-5;

        /// <summary>
        /// Angles test.
        /// </summary>
        [TestMethod]
        public void AngleMeasUnitsTest()
        {
            double ini = 155.0;
            Angle a = new Angle(ini, AngleMeasUnit.Degree);

            a.Convert(AngleMeasUnit.Radian);
            a.Convert(AngleMeasUnit.Degree);

            Assert.IsTrue(Math.Abs(a - ini) < E);
        }

        /// <summary>
        /// Linear test.
        /// </summary>
        [TestMethod]
        public void LinearMeasUnitsTest()
        {
            double ini = 1495.5;
            Linear l = new Linear(ini, LinearMeasUnit.Foot);

            l.Convert(LinearMeasUnit.Meter);
            l.Convert(LinearMeasUnit.NauticalMile);
            l.Convert(LinearMeasUnit.Foot);

            Assert.IsTrue(Math.Abs(l - ini) < E);
        }
    }
}
