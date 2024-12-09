﻿using Microsoft.AspNetCore.Mvc.Infrastructure;
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
    public int checkDate(Receipt item);
    public int checkTime(Receipt item);
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

    /*
     * 6 points if the purchase date is odd
     * 10 points if the time of the purchase is between 2:00pm and 4:00pm (convert to military time)
     * The format is YYYY-MM-DD
     */
    public int checkDate(Receipt item)
    {
        string itemPurchaseDate = item.purchaseDate;
        DateTime date = DateTime.ParseExact(itemPurchaseDate, "yyyy-MM-dd", null);

        int day = date.Day;

        if (day % 2 == 1)
        {
            return 6;
        }
        return 0;
    }

    public int checkTime(Receipt item)
    {
        string itemPurchaseTime = item.purchaseTime;
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


