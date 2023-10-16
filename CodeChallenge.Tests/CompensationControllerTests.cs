
using System.Net;
using System.Net.Http;
using System.Text;

using CodeChallenge.Models;

using CodeCodeChallenge.Tests.Integration.Extensions;
using CodeCodeChallenge.Tests.Integration.Helpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeCodeChallenge.Tests.Integration
{
    [TestClass]
    public class CompensationControllerTests
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        // Attribute ClassInitialize requires this signature
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer();
            _httpClient = _testServer.NewClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }

        [TestMethod]
        public void CreateCompensation_Returns_CreatedAndGetCompensationById_Returns_Ok()
        {
            //Create Compensation Test
            // Arrange
            var Compensation = new Compensation()
            {
                Employee = new Employee()
                {
                    EmployeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f"
                },
                Salary = 100000,
                EffectiveDate = "01/01/24",
            };

            var requestContent = new JsonSerialization().ToJson(Compensation);

            // Execute
            var postRequestTask = _httpClient.PostAsync("api/compensation",
               new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var newCompensation = response.DeserializeContent<Compensation>();
            Assert.IsNotNull(newCompensation.Employee.EmployeeId);
            Assert.AreEqual(Compensation.Salary, newCompensation.Salary);
            Assert.AreEqual(Compensation.EffectiveDate, newCompensation.EffectiveDate);

            //Get Compensation Test
            // Arrange
            var employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";
            var expectedFirstName = "John";
            var expectedLastName = "Lennon";

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/compensation/{employeeId}");
            var response2 = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response2.StatusCode);
            var Compensation2 = response2.DeserializeContent<Compensation>();
            Assert.AreEqual(expectedFirstName, Compensation2.Employee.FirstName);
            Assert.AreEqual(expectedLastName, Compensation2.Employee.LastName);
            Assert.AreEqual(expectedLastName, Compensation2.Employee.LastName);
        }
    }
}
