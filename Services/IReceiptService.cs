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
    public decimal checkDescription(Receipt item);
    
    // 6 points if the day in the purchase date is odd
    // 10 points if the time of the purchase is between 2:00 pm and before 4:00pm (convert to rest of world time)
    public bool checkDateAndTime(Receipt item);
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
        if (!decimal.TryParse(item.total, out decimal convertedTotal))
        {
            throw new ArgumentException("Invalid");
        }

        const decimal multiplier = 0.25m;

        if (convertedTotal % 1 == 0)
        {
            return 50;
        }
        else if (convertedTotal % multiplier == 0)
        {
            return 25;
        }

        return 0;
    }


    public int checkItemsCount(Receipt item)
    {
        int count = (item.items.Count / 2) * 5;
        return count;
    }
    
    // check if the description is a multiple of 3
    // if so multiply the price by 0.2 and round up to nearest integer
    public decimal checkDescription(Receipt item)
    {
        decimal result = 0m;
        decimal finalResult = 0m;
        decimal roundedResult = 0m;
        decimal roundedResultUp = 0m;
        foreach(var i in item.items)
        {
            string description = i.shortDescription.Trim();
            int descriptionLength = description.Length;
            decimal price = i.price;
            decimal multiplier = 0.2m;
            
            if (descriptionLength % 3 == 0)
            {
                result = price * multiplier;
                roundedResult = Math.Round(result, 2);
                roundedResultUp = Math.Ceiling(roundedResult);

            }
            finalResult += roundedResultUp;
        }
        return finalResult;
    }

    public bool checkDateAndTime(Receipt item)
    {
        throw new NotImplementedException();
    }
}


