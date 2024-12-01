using WireMock.Server;
using WireMock.Settings;

namespace Overlord.IntegrationTests.Infrastructure
{
    public class WireMockServerFixture : IAsyncLifetime
    {
        private readonly WireMockServer _wireMockServer;

        public WireMockServerFixture()
        {
            _wireMockServer = WireMockServer.Start(new WireMockServerSettings()
            {
                UseSSL = true
            });
        }

        public string GetWireMockServerUrl()
        {
            return _wireMockServer.Urls[0];
        }

        public WireMockServer GetWireMockServer()
        {
            return _wireMockServer;
        }

        public Task InitializeAsync()
        {
            return Task.CompletedTask;
        }

        public Task DisposeAsync()
        {
            _wireMockServer.Dispose();

            return Task.CompletedTask;
        }
    }
}
