using API_First.Data.DAL;
using API_First.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_First.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Product product = _context.Products.FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {
            List<Product> products = _context.Products.ToList();

            return StatusCode(201, products);

        }

        [HttpPost("")]
        public IActionResult Create(Product prod)
        {
           
            _context.Products.Add(prod);
            _context.SaveChanges();
            return StatusCode(201, prod);

        }

        [HttpPut("")]
        public IActionResult Update(Product product)
        {
            Product exist = _context.Products.FirstOrDefault(e => e.Id == product.Id);
            if (exist == null)
            {
                return NotFound();
            }
            exist.Name = product.Name;
            exist.Price = product.Price;
            exist.CategoryId = product.CategoryId;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Product product = _context.Products.FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
