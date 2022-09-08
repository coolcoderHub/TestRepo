namespace HowToTestYourCsharpWebApi.Api
{
    public class POSoperation
    {
        public string MessageType { get; set; }   //(PAYMENT | ADJUSTMENT)
        public string TransactionId { get; set; } //(Guid)
        public long AccountId { get; set; }
        public string Origin { get; set; }        // (VISA | MASTER)
        public decimal Amount { get; set; }

        public enum OriginType 
        {
            VISA,
            MASTER
        }

        public POSoperation()
        {

        }

        public POSoperation(string messageType, string transactionId, long accountId, string origin, decimal amount)
        {
            MessageType = messageType;
            TransactionId = transactionId;
            AccountId = accountId;
            Origin = origin;
            Amount = amount;
        }
    }
}
