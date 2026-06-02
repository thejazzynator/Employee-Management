using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

[TestFixture]
public class EmployeeApiTests : PlaywrightTest
{
    private WebApplicationFactory<Program> _factory = null!;
    private HttpClient _client = null!;

    [OneTimeSetUp]
    public void StartApi()
    {
        _factory = new WebApplicationFactory<Program>();
        _client = _factory.CreateClient();
    }

    [OneTimeTearDown]
    public void StopApi()
    {
        _client.Dispose();
        _factory.Dispose();
    }

    [Test]
    public async Task GetEmployees_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/employees");
        Assert.That((int)response.StatusCode, Is.EqualTo(200));
    }
}