using System.Net;

namespace Overlord.IntegrationTests.Externsions
{
    public static class HttpResponseMessageExtensions
    {
        public static void AssertStatusCodeMatches(this HttpResponseMessage? response, HttpStatusCode statusCode)
        {
            Assert.NotNull(response);
            Assert.Equal(statusCode, response.StatusCode);
        }

        public static void AssertStatusCodeSuccess(this HttpResponseMessage response) =>
            Assert.True(response.IsSuccessStatusCode);
    }
}
