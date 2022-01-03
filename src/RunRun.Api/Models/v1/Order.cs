namespace RunRun.Api.Models.v1
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public Customer Customer { get; set; }
    }
}
