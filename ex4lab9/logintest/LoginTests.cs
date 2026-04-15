using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using CsvHelper;
using System.Globalization;
using NUnit.Framework;

namespace LoginAutomation
{
    [TestFixture]
    public class LoginTests
    {
        private IWebDriver? driver;
        private string loginPageUrl = string.Empty;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();

            var loginPagePath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "login.html");
            loginPageUrl = $"file:///{loginPagePath.Replace("\\", "/")}";
        }

        [TearDown]
        public void TearDown()
        {
            driver?.Quit();
            driver?.Dispose();
        }

        public static IEnumerable<object[]> GetTestData()
        {
            var filePath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "LoginTestData.csv");

            var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null,
                MissingFieldFound = null,
                PrepareHeaderForMatch = args => args.Header.ToLower()
            };

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, config))
            {
                var records = csv.GetRecords<TestData>().ToList();
                foreach (var record in records)
                {
                    yield return new object[]
                    {
                        record.Username,
                        record.Password
                    };
                }
            }
        }

        [Test]
        [TestCaseSource(nameof(GetTestData))]
        public void Login_WithValidCredentials_ShouldSucceed(
            string username,
            string password)
        {
            driver!.Navigate().GoToUrl(loginPageUrl);

            driver.FindElement(By.Id("username")).SendKeys(username);
            driver.FindElement(By.Id("password")).SendKeys(password);
            driver.FindElement(By.Id("loginButton")).Click();

            System.Threading.Thread.Sleep(2000);

            var message = driver.FindElement(By.Id("successMessage")).Text;
            Assert.That(message, Is.EqualTo("Login successful"));
        }

        public class TestData
        {
            public string Username { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }
    }
}