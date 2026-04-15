using Microsoft.VisualStudio.TestTools.UnitTesting;
using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using BasicMath;

namespace BasicMathTests
{
    [TestClass]
    public class CalculatorCsvTests
    {
        // Helper method to read test data from a CSV file
        private static IEnumerable<object[]> GetTestData(string fileName)
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<TestData>();
                foreach (var record in records)
                {
                    yield return new object[] { record.A, record.B, record.Expected };
                }
            }
        }

        // ── ADD ──────────────────────────────────────────────────────────────
        public static IEnumerable<object[]> AddTestData =>
            GetTestData("add_testdata.csv");

        [DataTestMethod]
        [DynamicData(nameof(AddTestData), DynamicDataSourceType.Property)]
        public void Add_CsvData_ReturnsCorrectSum(int a, int b, double expected)
        {
            BasicMaths bm = new BasicMaths();
            double actual = bm.Add(a, b);
            Assert.AreEqual(expected, actual);
        }

        // ── SUBTRACT ─────────────────────────────────────────────────────────
        public static IEnumerable<object[]> SubtractTestData =>
            GetTestData("subtract_testdata.csv");

        [DataTestMethod]
        [DynamicData(nameof(SubtractTestData), DynamicDataSourceType.Property)]
        public void Subtract_CsvData_ReturnsCorrectDifference(int a, int b, double expected)
        {
            BasicMaths bm = new BasicMaths();
            double actual = bm.Subtract(a, b);
            Assert.AreEqual(expected, actual);
        }

        // ── MULTIPLY ─────────────────────────────────────────────────────────
        public static IEnumerable<object[]> MultiplyTestData =>
            GetTestData("multiply_testdata.csv");

        [DataTestMethod]
        [DynamicData(nameof(MultiplyTestData), DynamicDataSourceType.Property)]
        public void Multiply_CsvData_ReturnsCorrectProduct(int a, int b, double expected)
        {
            BasicMaths bm = new BasicMaths();
            double actual = bm.Multiply(a, b);
            Assert.AreEqual(expected, actual);
        }

        // ── DIVIDE ───────────────────────────────────────────────────────────
        public static IEnumerable<object[]> DivideTestData =>
            GetTestData("divide_testdata.csv");

        [DataTestMethod]
        [DynamicData(nameof(DivideTestData), DynamicDataSourceType.Property)]
        public void Divide_CsvData_ReturnsCorrectQuotient(int a, int b, double expected)
        {
            BasicMaths bm = new BasicMaths();
            double actual = bm.Divide(a, b);
            Assert.AreEqual(expected, actual);
        }

        // Class to represent CSV row structure
        public class TestData
        {
            public int A { get; set; }
            public int B { get; set; }
            public double Expected { get; set; }
        }
    }
}