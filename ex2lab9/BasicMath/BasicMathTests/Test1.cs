using Microsoft.VisualStudio.TestTools.UnitTesting;
using BasicMath;

namespace BasicMathTests
{
    [TestClass]
    public class BasicMathsTests
    {
        // ── ADD ──────────────────────────────────────────────────────────────
        [DataTestMethod]
        [DataRow(1, 1, 2.0)]          // EP: Positive numbers
        [DataRow(-1, -1, -2.0)]         // EP: Negative numbers
        [DataRow(0, 0, 0.0)]          // EP: Zero
        [DataRow(int.MaxValue, 1, (double)int.MaxValue + 1)]  // BVA: Upper
        [DataRow(int.MinValue, -1, (double)int.MinValue - 1)]  // BVA: Lower
        public void Test_Add(int a, int b, double expected)
        {
            BasicMaths bm = new BasicMaths();
            double actual = bm.Add(a, b);
            Assert.AreEqual(expected, actual);
        }

        // ── SUBTRACT ─────────────────────────────────────────────────────────
        [DataTestMethod]
        [DataRow(5, 3, 2.0)]          // EP: Positive numbers
        [DataRow(-5, -3, -2.0)]         // EP: Negative numbers
        [DataRow(0, 0, 0.0)]          // EP: Zero
        [DataRow(int.MaxValue, 1, (double)int.MaxValue - 1)]  // BVA: Upper
        [DataRow(int.MinValue, -1, (double)int.MinValue + 1)]  // BVA: Lower
        public void Test_Subtract(int a, int b, double expected)
        {
            BasicMaths bm = new BasicMaths();
            double actual = bm.Subtract(a, b);
            Assert.AreEqual(expected, actual);
        }

        // ── MULTIPLY ─────────────────────────────────────────────────────────
        [DataTestMethod]
        [DataRow(2, 3, 6.0)]         // EP: Positive numbers
        [DataRow(-2, -3, 6.0)]         // EP: Negative × Negative
        [DataRow(0, 5, 0.0)]         // EP: Zero
        [DataRow(int.MaxValue, 1, (double)int.MaxValue)]  // BVA: Upper
        [DataRow(int.MinValue, 1, (double)int.MinValue)]  // BVA: Lower
        public void Test_Multiply(int a, int b, double expected)
        {
            BasicMaths bm = new BasicMaths();
            double actual = bm.Multiply(a, b);
            Assert.AreEqual(expected, actual);
        }

        // ── DIVIDE ───────────────────────────────────────────────────────────
        [DataTestMethod]
        [DataRow(6, 3, 2.0)]         // EP: Positive numbers
        [DataRow(-6, -3, 2.0)]         // EP: Negative ÷ Negative
        [DataRow(0, 1, 0.0)]         // EP: Zero numerator
        [DataRow(int.MaxValue, 1, (double)int.MaxValue)]  // BVA: Upper
        public void Test_Divide(int a, int b, double expected)
        {
            BasicMaths bm = new BasicMaths();
            double actual = bm.Divide(a, b);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_Divide_ByZero_ThrowsException()
        {
            BasicMaths bm = new BasicMaths();
            Assert.Throws<DivideByZeroException>(() => bm.Divide(10, 0));
        }
    }
}