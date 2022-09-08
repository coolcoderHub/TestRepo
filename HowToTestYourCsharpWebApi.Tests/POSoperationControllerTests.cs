using FluentAssertions;
using HowToTestYourCsharpWebApi.Api;
using HowToTestYourCsharpWebApi.Api.Ports;
using HowToTestYourCsharpWebApi.Tests.Fixtures;
using HowToTestYourCsharpWebApi.Tests.Utils;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace HowToTestYourCsharpWebApi.Tests
{
    public class POSoperationControllerTests : IntegrationTest
    {
        public POSoperationControllerTests(ApiWebApplicationFactory fixture)
            : base(fixture) { }

        [Fact]
        public async Task GET_retrieves_pos_operation()
        {
            var posOp = await _client.GetAndDeserialize<POSoperation[]>("/POSoperation");
            posOp.Should().HaveCount(1);
        }

        [Fact]
        public async Task GET_with_invalid_config_results_in_a_bad_request()
        {
            var clientWithInvalidConfig = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddTransient<IPOSoperationConfigService, InvalidPOSoperationConfigStub>();
                });
            })
            .CreateClient();

            var response = await clientWithInvalidConfig.GetAsync("/POSoperation");
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        public class InvalidPOSoperationConfigStub : IPOSoperationConfigService
        {
            public bool AccountCheck(long accountId) => true;

            public bool ApplyCommRate(TestAccount testAccount, decimal payment, string origin)
            {
                decimal currentBalance = testAccount.Balance;

                // apply % 1 for visa %2 for MASTER to payment

                // update testAccount.Balance with (currentBalance-appliedBalance)
                return true;
            }

            public bool BalanceCheck() => true;
            public bool TransactionCheck() => true;
            public bool AddTransaction()
            {
                return true;
            }
            

        }
    }
}
