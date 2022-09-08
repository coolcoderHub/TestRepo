using HowToTestYourCsharpWebApi.Api.Ports;

namespace HowToTestYourCsharpWebApi.Api.Adapters
{
    public class POSoperationConfigService : IPOSoperationConfigService
    {
        public bool AccountCheck(long accountId) 
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
        public bool BalanceCheck() => true;
        public bool TransactionCheck() => true;

        public bool AddTransaction()
        {
            return true;
        }

    }
}