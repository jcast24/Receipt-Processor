using Microsoft.AspNetCore.Mvc;
using Receipt_Processor.Models;
using Receipt_Processor.Services;

namespace Receipt_Processor.Controllers;

[ApiController]
[Route("[controller]")]
public class ReceiptController(IReceiptService receiptService) : ControllerBase
{
    
    /*
     * POST
     * Create Id when passing in JSON receipt object
     * Response: JSON object giving the Id for the receipt
     */
    
    [HttpPost]
    [Route("process")]
    public ActionResult<Guid> CreateIdFromReceipt([FromBody] Receipt receipt)
    {
        receipt.Id = Guid.NewGuid();
        ReceiptDatabase.ReceiptDB.Add(receipt);

        return Ok(new { id = receipt.Id});
    }
    
    /*
     * GET 
     * Process points via Id
     * Response: JSON object named "points"
     */
    
    [HttpGet("{id}/points")]
    public ActionResult<List<Receipt>> GetPointsById(Guid id)
    {
        var item = ReceiptDatabase.ReceiptDB.FirstOrDefault(i => i.Id == id);
        
        if (item == null)
        {
            return NotFound($"Item with ID {id} not found");
        }

        var checkAlphaResult = receiptService.CheckAlphanumeric(item);
        var checkTotalResult = receiptService.CheckTotal(item);
        var checkItemsResult = receiptService.CheckItemsCount(item);
        var checkItemsDescription = receiptService.CheckDescription(item);
        var checkPurchaseDate = receiptService.CheckDate(item);
        var checkPurchaseTime = receiptService.CheckTime(item);
        decimal total = checkTotalResult + checkAlphaResult + checkItemsResult + 
                        checkItemsDescription + checkPurchaseDate + checkPurchaseTime;

        return Ok(new {points = total});
    }
    
}
