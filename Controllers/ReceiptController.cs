using Microsoft.AspNetCore.Mvc;
using Receipt_Processor.Models;

namespace Receipt_Processor.Controllers;

[ApiController]
[Route("[controller]")]
public class ReceiptController() : ControllerBase
{
    [HttpPost]
    [Route("process")]
    public ActionResult<Guid> CreateIdFromReceipt([FromBody] Receipt receipt)
    {
        receipt.id = Guid.NewGuid();
        ReceiptDatabase.ReceiptDB.Add(receipt);

        return Ok(receipt.id);
    }
    
    [HttpGet("{id}")]
    public ActionResult<List<Receipt>> GetAllReceipts(Guid id)
    {
        var item = ReceiptDatabase.ReceiptDB.FirstOrDefault(i => i.id == id);

        if (item == null)
        {
            return NotFound($"Item with ID {id} not found");
        }

        return Ok(item);
    }
}