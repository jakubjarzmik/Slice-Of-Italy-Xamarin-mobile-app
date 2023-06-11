using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SliceOfItalyAPI.Controllers.Abstract;
using SliceOfItalyAPI.Data;
using SliceOfItalyAPI.Models;

namespace SliceOfItalyAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AddressesController : BaseController<Address>
{
    public AddressesController(SliceOfItalyContext context) : base(context)
    {
    }
}