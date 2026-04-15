using BankAccountApp;
using CsvHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;

namespace BankAccountTests
{
    [TestClass]
    public class BankAccountCsvTests
    {
        // Helper to read CSV data
        private static IEnumerable<object[]> GetTestData()
        {
            var filePath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "BankAccountTestData.csv");

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<TestData>().ToList();
                foreach (var record in records)
                {
                    yield return new object[]
                    {
                        record.CustomerName,
                        record.InitialBalance,
                        record.DebitAmount,
                        record.ExpectedBalance
                    };
                }
            }
        }

        public static IEnumerable<object[]> CsvTestData => GetTestData();

        [DataTestMethod]
        [DynamicData(nameof(CsvTestData), DynamicDataSourceType.Property)]
        public void Debit_CsvData_UpdatesBalance(
            string customerName,
            decimal initialBalance,
            decimal debitAmount,
            string expectedBalance)
        {
            var account = new BankAccount(customerName, initialBalance);

            if (expectedBalance == "Insufficient funds")
            {
                Assert.ThrowsException<InvalidOperationException>(
                    () => account.Debit(debitAmount));
            }
            else
            {
                account.Debit(debitAmount);
                Assert.AreEqual(decimal.Parse(expectedBalance), account.Balance);
            }
        }

        // Class to map CSV columns
        public class TestData
        {
            public string CustomerName { get; set; } = string.Empty;
            public decimal InitialBalance { get; set; }
            public decimal DebitAmount { get; set; }
            public string ExpectedBalance { get; set; } = string.Empty;
        }
    }
}