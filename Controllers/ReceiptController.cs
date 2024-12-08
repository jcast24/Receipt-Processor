﻿using Microsoft.AspNetCore.Mvc;
using Receipt_Processor.Models;
using Receipt_Processor.Services;

namespace Receipt_Processor.Controllers;

[ApiController]
[Route("[controller]")]
public class ReceiptController(IReceiptService receiptService) : ControllerBase
{
    // DI ReceiptService class
    private readonly IReceiptService _receiptService = receiptService;
    
    [HttpPost]
    [Route("process")]
    public ActionResult<Guid> CreateIdFromReceipt([FromBody] Receipt receipt)
    {
        receipt.id = Guid.NewGuid();
        ReceiptDatabase.ReceiptDB.Add(receipt);

        return Ok(receipt.id);
    }
    
    // create the logic to process the points
    // response is going to be a json object called "points"
    [HttpGet("{id}/points")]
    public ActionResult<List<Receipt>> GetPointsById(Guid id)
    {
        var item = ReceiptDatabase.ReceiptDB.FirstOrDefault(i => i.id == id);
        
        if (item == null)
        {
            return NotFound($"Item with ID {id} not found");
        }

        var checkAlphaResult = _receiptService.checkAlphanumeric(item);
        var checkTotalResult = _receiptService.checkTotal(item);
        var checkItemsResult = _receiptService.checkItemsCount(item);
        var checkItemsDescription = _receiptService.checkDescription(item);

        decimal total = checkTotalResult + checkAlphaResult + checkItemsResult + checkItemsDescription;

        return Ok(new {points = total});
    }
    
}
