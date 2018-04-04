using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Lib.MathMod.Grid.DescartesObjects;

namespace Lib.Tests.MathMod.Grid.DescartesObjects
{
    /// <summary>
    /// Test for DescartesObject3D.
    /// </summary>
    [TestClass]
    public class DescartesObject3DTest
    {
        /// <summary>
        /// Cut and trunc test.
        /// </summary>
        [TestMethod]
        public void TestStringConstructor()
        {
            DescartesObject3D d = new DescartesObject3D("[1 - 10] x [5 - 15] x [20 - 100]");

            Assert.AreEqual(d.ToString(), "[1 - 10] x [5 - 15] x [20 - 100]");
        }
    }
}
