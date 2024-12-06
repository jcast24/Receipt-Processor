namespace Receipt_Processor.Models;

public class Receipt
{
    public string Retailer { get; set; }
    public string PurchaseDate { get; set; }
    public string PurchaseTime { get; set; }
    public List<Item> Items { get; set; }
    public int Total { get; set; }
}

public class Item
{
    public string ShortDescription { get; set; }
    public decimal Price { get; set; }
}