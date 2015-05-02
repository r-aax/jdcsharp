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
        /// Angles test.
        /// </summary>
        [TestMethod]
        public void AngleMeasUnitsTest()
        {
            double e = 1.0e-6;
            Angle a = new Angle(155.0, AngleMeasUnit.Degree);
            a.Convert(AngleMeasUnit.Radian);
            a.Convert(AngleMeasUnit.Degree);

            Assert.IsTrue(Math.Abs(a - 155.0) < e);
        }
    }
}
