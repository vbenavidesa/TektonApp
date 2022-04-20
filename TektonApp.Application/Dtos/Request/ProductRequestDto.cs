namespace TektonApp.Application.Dtos.Request
{
    public class ProductRequestDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public decimal Stock { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
