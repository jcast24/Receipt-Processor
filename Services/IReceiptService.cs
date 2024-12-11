using Microsoft.AspNetCore.Mvc.Infrastructure;
using Receipt_Processor.Models;

namespace Receipt_Processor.Services;

public interface IReceiptService
{
    public int CheckAlphanumeric(Receipt item);
    public int CheckTotal(Receipt item);
    public int CheckItemsCount(Receipt item);
    public decimal CheckDescription(Receipt item);
    public int CheckDate(Receipt item);
    public int CheckTime(Receipt item);
}

/*
 * Each method here processes the logic behind getting the points for each JSON payload. aaaaa
 */
public class ReceiptService : IReceiptService
{
    // Check to see if each character is a letter or a string in item.Retailer
    public int CheckAlphanumeric(Receipt item)
    {
        string retailerName = item.Retailer;
        int alphaCount = retailerName.Count(char.IsLetterOrDigit);
        return alphaCount;
    }
    
    // Check if the total is a multiple of 0.25, a whole number, or both
    public int CheckTotal(Receipt item)
    {
        if (!decimal.TryParse(item.Total, out decimal convertedTotal))
        {
            throw new ArgumentException("Invalid");
        }

        bool isWholeNumber = convertedTotal % 1 == 0;
        bool isMulitpleOfQuarter = convertedTotal % 0.25m == 0;
        
        if (isWholeNumber && isMulitpleOfQuarter)
        {
            return 75;
        }
        else if (isWholeNumber)
        {
            return 50;
        }
        else if (isMulitpleOfQuarter)
        {
            return 25;
        }
        return 0;
    }
    
    // Check the amount of items in a receipt
    public int CheckItemsCount(Receipt item)
    {
        int count = (item.Items.Count / 2) * 5;
        return count;
    }
    
    /*
     * Check the length of the description of each item in the receipt.
     * If the length is a multiple of 3, multiply by 0.2 and add the total together
     */
    public decimal CheckDescription(Receipt item)
    {
        decimal result = 0m;
        decimal finalResult = 0m;
        decimal roundedResult = 0m;
        decimal roundedResultUp = 0m;
        foreach(var individualItem in item.Items)
        {
            string description = individualItem.ShortDescription.Trim();
            int descriptionLength = description.Length;
            decimal price = individualItem.Price;
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
    
    // check if the date is an odd number
    public int CheckDate(Receipt item)
    {
        string itemPurchaseDate = item.PurchaseDate;
        DateTime date = DateTime.ParseExact(itemPurchaseDate, "yyyy-MM-dd", null);

        int day = date.Day;

        if (day % 2 == 1)
        {
            return 6;
        }
        return 0;
    }

    // check if the time is between 2:00 and 4:00, if so award points. 
    public int CheckTime(Receipt item)
    {
        string itemPurchaseTime = item.PurchaseTime;
        TimeSpan startTime = TimeSpan.Parse("14:00");
        TimeSpan endTime = TimeSpan.Parse("16:00");
        TimeSpan convertPurchaseTime = TimeSpan.Parse(itemPurchaseTime);

        if (convertPurchaseTime >= startTime && convertPurchaseTime <= endTime)
        {
            return 10;
        }
        return 0;
        
    }
}


