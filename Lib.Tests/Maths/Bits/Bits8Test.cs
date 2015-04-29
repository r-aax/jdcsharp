// Author: Alexey Rybakov

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lib.Maths.Bits;

namespace Lib.Tests.Maths.Bits
{
    /// <summary>
    /// Test for <c>Bits8</c>.
    /// </summary>
    [TestClass]
    public class Bits8Test
    {
        /// <summary>
        /// Test for <c>Popcnt</c>.
        /// </summary>
        [TestMethod]
        public void TestPopcnt()
        {
            Bits8 b1 = new Bits8(0x0);
            Bits8 b2 = new Bits8(0x1);
            Bits8 b3 = new Bits8(0x8);
            Bits8 b4 = new Bits8(25);
            Bits8 b5 = new Bits8(50);
            Bits8 b6 = new Bits8(63);
            Bits8 b7 = new Bits8(100);
            Bits8 b8 = new Bits8(0xFF);

            Assert.AreEqual(b1.Popcnt(), 0);
            Assert.AreEqual(b2.Popcnt(), 1);
            Assert.AreEqual(b3.Popcnt(), 1);
            Assert.AreEqual(b4.Popcnt(), 3);
            Assert.AreEqual(b5.Popcnt(), 3);
            Assert.AreEqual(b6.Popcnt(), 6);
            Assert.AreEqual(b7.Popcnt(), 3);
            Assert.AreEqual(b8.Popcnt(), 8);
        }
    }
}
