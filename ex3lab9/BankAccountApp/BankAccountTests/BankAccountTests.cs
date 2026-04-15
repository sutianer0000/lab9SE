using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankAccountApp;

namespace BankAccountTests
{
    [TestClass]
    public class BankAccountTests
    {
        // ── DEBIT ─────────────────────────────────────────────────────────────
        [DataTestMethod]
        [DataRow("John Doe", 1000.0, 200.0, 800.0)]
        [DataRow("Jane Smith", 500.0, 100.0, 400.0)]
        [DataRow("Alice", 300.0, 50.0, 250.0)]
        [DataRow("Bob", 0.0, 0.0, 0.0)]
        [DataRow("Charlie", 1000.0, 1.0, 999.0)]
        [DataRow("Dave", 1000.0, 1000.0, 0.0)]
        public void Debit_ValidAmount_UpdatesBalance(
            string customerName,
            double initialBalance,
            double debitAmount,
            double expectedBalance)
        {
            var account = new BankAccount(customerName, (decimal)initialBalance);
            account.Debit((decimal)debitAmount);
            Assert.AreEqual((decimal)expectedBalance, account.Balance);
        }

        [DataTestMethod]
        [DataRow("Bob Brown", 100.0, 150.0)]
        [DataRow("Eve", 0.0, 1.0)]
        public void Debit_InsufficientFunds_ThrowsException(
            string customerName,
            double initialBalance,
            double debitAmount)
        {
            var account = new BankAccount(customerName, (decimal)initialBalance);
            Assert.ThrowsException<InvalidOperationException>(
                () => account.Debit((decimal)debitAmount));
        }

        // ── CREDIT ────────────────────────────────────────────────────────────
        [DataTestMethod]
        [DataRow("John Doe", 1000.0, 200.0, 1200.0)]
        [DataRow("Jane Smith", 500.0, 100.0, 600.0)]
        [DataRow("Alice", 300.0, 50.0, 350.0)]
        [DataRow("Bob", 0.0, 100.0, 100.0)]
        [DataRow("Charlie", 1000.0, 0.0, 1000.0)]
        public void Credit_ValidAmount_UpdatesBalance(
            string customerName,
            double initialBalance,
            double creditAmount,
            double expectedBalance)
        {
            var account = new BankAccount(customerName, (decimal)initialBalance);
            account.Credit((decimal)creditAmount);
            Assert.AreEqual((decimal)expectedBalance, account.Balance);
        }

        // ── WITHDRAW ──────────────────────────────────────────────────────────
        [DataTestMethod]
        [DataRow("John Doe", 1000.0, 200.0, 800.0)]
        [DataRow("Jane Smith", 500.0, 100.0, 400.0)]
        [DataRow("Alice", 300.0, 50.0, 250.0)]
        [DataRow("Bob", 1000.0, 1000.0, 0.0)]
        public void Withdraw_ValidAmount_UpdatesBalance(
            string customerName,
            double initialBalance,
            double withdrawAmount,
            double expectedBalance)
        {
            var account = new BankAccount(customerName, (decimal)initialBalance);
            account.Withdraw((decimal)withdrawAmount);
            Assert.AreEqual((decimal)expectedBalance, account.Balance);
        }

        [DataTestMethod]
        [DataRow("Bob Brown", 100.0, 150.0)]
        public void Withdraw_InsufficientFunds_ThrowsException(
            string customerName,
            double initialBalance,
            double withdrawAmount)
        {
            var account = new BankAccount(customerName, (decimal)initialBalance);
            Assert.ThrowsException<InvalidOperationException>(
                () => account.Withdraw((decimal)withdrawAmount));
        }
    }
}