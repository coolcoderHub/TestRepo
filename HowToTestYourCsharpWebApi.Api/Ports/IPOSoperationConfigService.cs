namespace HowToTestYourCsharpWebApi.Api.Ports
{
    public interface IPOSoperationConfigService
    {
        bool AccountCheck(long accountId);
        bool ApplyCommRate(TestAccount testAccount, decimal payment, string origin); 
        bool BalanceCheck();
        bool TransactionCheck();
        bool AddTransaction();

    }
}
