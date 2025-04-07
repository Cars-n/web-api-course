using System.Net.Http.Json;
using Alba;

namespace SoftwareCenter.Tests.Vendors;
public class AddingAVendor
{
    [Fact]
    public async Task CanAddAVendorAsync()
    {
        var host = await AlbaHost.For<Program>();

        // Syatem Tests are "scenarios"
        await host.Scenario(api =>
        {
            api.Post.Json(new { }).ToUrl("/vendors");
            api.StatusCodeShouldBeOk();
        });

        var client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:1337");

        var response = await client.PostAsJsonAsync("vendors", new { });

        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
    }
}
