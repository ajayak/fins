using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FINS.Features.Login;
using FINS.Models;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.PlatformAbstractions;
using OpenIddict.Core;
using OpenIddict.Models;
using Xunit;

namespace FINS.UnitTest.Features.Login
{
    public class AuthorizationControllerShould
    {
        private TestServer _server;
        private HttpClient _client;

        public AuthorizationControllerShould()
        {
            var path = PlatformServices.Default.Application.ApplicationBasePath;
            var setDir = Path.GetFullPath(Path.Combine(path, "../../../../src/FINS"));

            _server = new TestServer(new WebHostBuilder()
                .UseContentRoot(setDir)
                .UseEnvironment("Test")
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async void ReturnOkStatus()
        {
            var result = await _client.GetAsync("/api/values");
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
