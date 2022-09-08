namespace HowToTestYourCsharpWebApi.Api
{
    public class TestAccount
    {
        public long AccountId { get; set; }
        public decimal Balance { get; set; }
        public bool Active { get; set; }

        public TestAccount()
        {

        }
        public TestAccount(long accountId, decimal balance, bool active)
        {
            this.AccountId = accountId;
            this.Balance = balance;
            this.Active = active;
        }
    }
   
}
