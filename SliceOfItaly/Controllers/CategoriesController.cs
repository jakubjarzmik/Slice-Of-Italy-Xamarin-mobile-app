using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SliceOfItalyAPI.Controllers.Abstract;
using SliceOfItalyAPI.Data;
using SliceOfItalyAPI.Models;

namespace SliceOfItalyAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : BaseController<Category>
{
    public CategoriesController(SliceOfItalyContext context) : base(context)
    {
    }
}