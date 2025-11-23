using Microsoft.AspNetCore.Mvc;

namespace API.Infra.Http.Controllers;
[Route("api/v1/products")]
[ApiController]
public class ProductController : ControllerBase
{
    public ProductController()
    {
        
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(new { Message="Ok" });
    }


    [HttpGet]
    [Route("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        return Ok(new { Message="Ok" });
    }
}
