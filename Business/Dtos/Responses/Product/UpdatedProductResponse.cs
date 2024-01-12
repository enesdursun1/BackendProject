namespace Business.Dtos.Responses.Product;

public class UpdatedProductResponse
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public decimal UnitPrice { get; set; }
    public int Stock { get; set; }

}

