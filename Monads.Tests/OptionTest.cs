using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monads;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;

namespace Monads.Tests {
    [TestClass]
    public class OptionTest {
        public class Parent { }

        public class Child : Parent { }

        [TestMethod]
        public void TestMapSome() {
            var mx = Option.Some(1);
            var mxi = mx.Select(x => x + 1);

            Assert.AreEqual(2, mxi.Value);
        }

        [TestMethod]
        public void TestMapNone() {
            var mx = Option.Empty<int>();
            var mxi = mx.Select(x => x + 1);

            Assert.IsFalse(mxi.HasValue);
        }

        [TestMethod]
        public void TestLinqSome() {
            var mx = Option.Some(1);
            var my = Option.Some(2);
            var sum = from x in mx
                      from y in my
                      select x + y;
            Assert.AreEqual(3, sum.Value);
        }

        [TestMethod]
        public void TestLinqNone() {
            var mx = Option.Some(1);
            var my = Option.Empty<int>();

            var sum = from x in mx
                      from y in my
                      select x + y;

            Assert.IsFalse(sum.HasValue);
        }

        [TestMethod]
        public void TestFilterSome() {
            var mx = Option.Some(1);
            var inc = from x in mx where x > 0 select x + 1;

            Assert.AreEqual(2, inc.Value);
        }

        [TestMethod]
        public void TestFilterNone() {
            var mx = Option.Some(2);
            var inc = from x in mx where x > 10 select x + 1;

            Assert.IsFalse(inc.HasValue);
        }

        [TestMethod]
        public void TestVariance() {
            IOption<object> mx = Option.Some("option");
        }

        [TestMethod]
        public void TestVariance2() {
            // IOption<string> mx = Option.Empty<object>();
        }

    }
}
