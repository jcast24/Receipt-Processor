namespace Receipt_Processor.Models;

public class Receipt
{
    public Guid Id { get; set; }
    public string Retailer { get; set; } = string.Empty;
    public string PurchaseDate { get; set; } = string.Empty;
    public string PurchaseTime { get; set; } = string.Empty;
    public List<Item>? Items { get; set; }
    public string Total { get; set; } = string.Empty;
}

public class Item
{
    public string ShortDescription { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
