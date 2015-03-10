// Author: Alexey Rybakov

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lib.DataStruct;

namespace Lib.Tests.DataStruct
{
    /// <summary>
    /// Test for <c>StringsList</c>.
    /// </summary>
    [TestClass]
    public class StringListTest
    {
        /// <summary>
        /// Test method.
        /// </summary>
        [TestMethod]
        public void TestCommon()
        {
            StringsList sl = new StringsList();

            sl.Add("str1");
            sl.Add("str2");

            Assert.AreEqual(sl.Count, 2);
            Assert.AreEqual(sl[0], "str1");
            Assert.AreEqual(sl[1], "str2");
        }
    }
}
