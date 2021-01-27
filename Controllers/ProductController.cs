using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Logging;

namespace webshopbackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductContext _context;
        private readonly IMapper _mapper;

        private List<Product> products = new List<Product>();

        public ProductController(ProductContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

[HttpGet]
        public async Task<ActionResult> GetProducts() {
            List<Product> products = await _context.Products.ToListAsync();
            List<ProductDTO> productDTOs = _mapper.Map<List<ProductDTO>>(products);

            return Ok(productDTOs);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            Product found = await _context.Products.FindAsync(id);

            if(found == null) {
                return NotFound();
            }

            return Ok(_mapper.Map<ProductDTO>(found));
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct(ProductDTO newProductDTO)
        {
            Product newProduct = _mapper.Map<Product>(newProductDTO);
            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("CreateProduct", newProduct);
        }
    }
}
