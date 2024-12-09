namespace Receipt_Processor.Models;

public class Receipt
{
    public Guid id { get; set; }
    public string retailer { get; set; }
    public string purchaseDate { get; set; }
    public string purchaseTime { get; set; }
    public List<Item> items { get; set; }
    public string total { get; set; }
}

public class Item
{
    public string shortDescription { get; set; }
    public decimal price { get; set; }
}
