using Allure.Commons;
using Allure.Net.Commons;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using CsvHelper;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Globalization;

namespace LoginAutomation
{
    [TestFixture]
    [AllureNUnit]
    [Allure.NUnit.Attributes.AllureSuite("Login Tests")]
    [Allure.NUnit.Attributes.AllureFeature("Login Page")]
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
            // Take screenshot on failure and attach to Allure report
            if (TestContext.CurrentContext.Result.Outcome.Status ==
                NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                var screenshot = ((ITakesScreenshot)driver!).GetScreenshot();
                var screenshotBytes = screenshot.AsByteArray;
                AllureApi.AddAttachment("Screenshot on Failure", "image/png", screenshotBytes);
            }

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
        [Allure.NUnit.Attributes.AllureSubSuite("Valid Credentials")]
        [Allure.NUnit.Attributes.AllureStory("User can login with valid credentials")]
        [Allure.NUnit.Attributes.AllureTag("Login", "Selenium", "CSV")]
        public void Login_WithValidCredentials_ShouldSucceed(
            string username,
            string password)
        {
            AllureApi.Step($"Navigate to login page");
            driver!.Navigate().GoToUrl(loginPageUrl);

            AllureApi.Step($"Enter username: {username}");
            driver.FindElement(By.Id("username")).SendKeys(username);

            AllureApi.Step($"Enter password: {password}");
            driver.FindElement(By.Id("password")).SendKeys(password);

            AllureApi.Step("Click login button");
            driver.FindElement(By.Id("loginButton")).Click();

            System.Threading.Thread.Sleep(2000);

            AllureApi.Step("Verify login successful message");
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