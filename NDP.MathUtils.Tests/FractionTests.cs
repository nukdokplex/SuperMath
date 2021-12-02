using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using NDP.MathUtils;

namespace NDP.MathUtils.Tests
{
    [TestClass]
    public class FractionTests
    {
        [TestMethod]
        public void Minimize_2and4_1and2returned()
        {
            //arrange
            CommonFraction actual = new CommonFraction(2, -4);

            CommonFraction expected = new CommonFraction(-1, 2);
            
            //act
            actual.Minimize();

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Operations()
        {
            //arrange
            CommonFraction x1 = new CommonFraction(3, 4);
            CommonFraction x2 = new CommonFraction(6, -7);
            int x3 = -3;
            //act

            //+
            var a1 = x1 + x2;
            var a2 = x2 + x3;
            var e1 = new CommonFraction(-3, 28);
            var e2 = new CommonFraction(-27, 7);
            //-
            var a3 = x1 - x2;
            var a4 = x2 - x3;
            var e3 = new CommonFraction(45, 28);
            var e4 = new CommonFraction(15, 7);
            //*
            var a5 = (x1 * x2).Minimized();
            var a6 = (x2 * x3).Minimized();
            var e5 = new CommonFraction(-9, 14);
            var e6 = new CommonFraction(18, 7);
            // /
            var a7 = (x1 / x2).Minimized(); // 7/8
            var a8 = (x2 / x3).Minimized(); // 3/7
            var e7 = new CommonFraction(-7, 8);
            var e8 = new CommonFraction(2, 7);

            //Negotiation
            var a9 = -x2;
            var e9 = new CommonFraction(6, 7);

            //assert
            Assert.AreEqual(e1, a1);
            Assert.AreEqual(e2, a2);
            Assert.AreEqual(e3, a3);
            Assert.AreEqual(e4, a4);
            Assert.AreEqual(e5, a5);
            Assert.AreEqual(e6, a6);
            Assert.AreEqual(e7, a7);
            Assert.AreEqual(e8, a8);
            Assert.AreEqual(e9, a9);
        }

        [TestMethod]
        public void EqualTest()
        {
            Assert.IsTrue(new CommonFraction(1, 2) == new CommonFraction(1, 2));
            Assert.IsFalse(new CommonFraction(1, 2) == new CommonFraction(-1, 2));
            Assert.IsFalse(new CommonFraction(1, 2) == 0.5f);
        }

        
    }
}
