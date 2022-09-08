using HowToTestYourCsharpWebApi.Api;
using HowToTestYourCsharpWebApi.Api.Ports;
using System.Reflection.Metadata.Ecma335;

namespace HowToTestYourCsharpWebApi.Tests.Fixtures
{
    public class POSoperationConfigStub : IPOSoperationConfigService
    {
        public bool BalanceCheck() => true;

        public bool AccountCheck(long accountId)
        { 
            return true; 
        }

        public bool TransactionCheck()
        {
            return true;
        }

        public bool ApplyCommRate(TestAccount testAccount, decimal payment, string origin)
        {
            decimal currentBalance = testAccount.Balance;

            // apply % 1 for visa %2 for MASTER to payment

            // update testAccount.Balance with (currentBalance-appliedBalance)
            return true;
        }

        public bool AddTransaction()
        {
            return true;
        }
    }
}
