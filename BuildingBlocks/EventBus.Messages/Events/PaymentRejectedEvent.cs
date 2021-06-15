namespace EventBus.Messages.Events
{
    public class PaymentRejectedEvent
    {
        public int OrderId { get; set; }
        public string Reason { get; set; }
    }
}
