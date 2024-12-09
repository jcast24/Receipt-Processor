using Receipt_Processor.Models;

namespace Receipt_Processor.Services;

public interface IReceiptService
{
    // 1 point for every alphanumeric character in the retailer name
    public int checkAlphanumeric(Receipt item);
    
    // 50 points if the total is a round number amount with no cents
    // 25 points if the total is a multiple of 0.25
    public bool checkTotal();
    
    // 5 points for every two items on the receipt
    public bool checkItemsCount();

    // if the trimmed length of the item description is a multiple of 3, multiply the price by 0.2
    // and round up to the nearest integer. Result should be number of points earned. 
    public bool checkDescription();
    
    // 6 points if the day in the purchase date is odd
    // 10 points if the time of the purchase is between 2:00 pm and before 4:00pm (convert to rest of world time)
    public bool checkDateAndTime();
}

public class ReceiptService : IReceiptService
{
    public int checkAlphanumeric(Receipt item)
    {
        int points = 0;
        string retailerName = item.retailer;
        bool isAlpha = retailerName.All(char.IsLetterOrDigit);

        if (isAlpha)
        {
            points += retailerName.Length;
        }

        return points;
    }


    public bool checkTotal()
    {
        throw new NotImplementedException();
    }


    public bool checkItemsCount()
    {
        throw new NotImplementedException();
    }

    public bool checkDescription()
    {
        throw new NotImplementedException();
    }

    public bool checkDateAndTime()
    {
        throw new NotImplementedException();
    }
}


