using Genie.Core.Entities;
using Genie.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GenieAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly GenieContext _context;
        public ProductsController(GenieContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task <ActionResult<List<Product>>> GetProducts()
        {
           var products = await _context.Products.ToListAsync();

           return products;
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return await _context.Products.FindAsync(id);
        }
    }
}