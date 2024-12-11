using Microsoft.AspNetCore.Mvc;
using Receipt_Processor.Models;
using Receipt_Processor.Services;

namespace Receipt_Processor.Controllers;

[ApiController]
[Route("[controller]")]
public class ReceiptController(IReceiptService receiptService) : ControllerBase
{

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

        var checkAlphaResult = receiptService.checkAlphanumeric(item);
        var checkTotalResult = receiptService.checkTotal(item);
        var checkItemsResult = receiptService.checkItemsCount(item);
        var checkItemsDescription = receiptService.checkDescription(item);
        var checkPurchaseDate = receiptService.checkDate(item);
        var checkPurchaseTime = receiptService.checkTime(item);
        decimal total = checkTotalResult + checkAlphaResult + checkItemsResult + 
                        checkItemsDescription + checkPurchaseDate + checkPurchaseTime;

        return Ok(new {points = total});
    }
    
}
