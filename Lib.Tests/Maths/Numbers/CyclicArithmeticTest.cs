// Author: Alexey Rybakov

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lib.Maths.Numbers;

namespace Lib.Tests.Maths.Numbers
{
    /// <summary>
    /// Test for <c>CyclicArithmetic</c>.
    /// </summary>
    [TestClass]
    public class CyclicArithmeticTest
    {
        /// <summary>
        /// Common test method.
        /// </summary>
        [TestMethod]
        public void TestCommon()
        {
            CyclicArithmetic arith = new CyclicArithmetic(23);

            Assert.AreEqual(arith.Pow(3, 0), 1);
            Assert.AreEqual(arith.Pow(3, 1), 3);
            Assert.AreEqual(arith.Pow(3, 22), 1);
        }
    }
}
