using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lib.Tests.Maths.Bits
{
    /// <summary>
    /// Tests for Bits.
    /// </summary>
    [TestClass]
    public class BitsTest
    {
        /// <summary>
        /// Basic tests.
        /// </summary>
        [TestMethod]
        public void TestBasic()
        {
            Lib.Maths.Bits.Bits b = new Lib.Maths.Bits.Bits(10);

            b.Set(true);
            Assert.IsTrue(b.IsAllTrue);
            b.Set(false);
            Assert.IsTrue(b.IsAllFalse);

            for (int i = 0; i < 10; i++)
            {
                b.SetRandomSet(5);
                Assert.AreEqual(b.TrueCount, 5);
            }
        }
    }
}
