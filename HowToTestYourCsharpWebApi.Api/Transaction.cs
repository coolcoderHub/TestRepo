namespace HowToTestYourCsharpWebApi.Api
{
    public class Transaction
    {
        public string MessageType { get; set; }   //(PAYMENT | ADJUSTMENT) interface olmalı mı? PaymentType gibi
        public string TransactionId
        {
            get
            {
                var guid = Guid.NewGuid().ToString();
                return guid;
            }
        }

        public long AccountId { get; set; }
        public string Origin { get; set; }        // (VISA | MASTER) interface CardType olmalı mı? american express de gelebilir
        public decimal Amount { get; set; }
    }
}
