using Receipt_Processor.Models;

namespace Receipt_Processor.Services;

public interface IReceiptService
{
    // 1 point for every alphanumeric character in the retailer name
    public int checkAlphanumeric(Receipt item);
    
    // 50 points if the total is a round number amount with no cents
    // 25 points if the total is a multiple of 0.25
    public int checkTotal(Receipt item);
    
    // 5 points for every two items on the receipt
    public int checkItemsCount(Receipt item);

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
        string retailerName = item.retailer;
        int alphaCount = retailerName.Count(char.IsLetterOrDigit);
        return alphaCount;
    }
    
    public int checkTotal(Receipt item)
    {
        // convert the total prop from a string to a float 
        decimal total = decimal.Parse(item.total);

        bool isRounded = total % 1 == 0;

        if (isRounded)
        {
            return 50;
        }
        return 25;
    }


    public int checkItemsCount(Receipt item)
    {
        int count = (item.items.Count / 2) * 5;
        return count;
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


