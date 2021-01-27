using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace webshopbackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ProductContext _context;
        private readonly IMapper _mapper;

        private List<Customer> customers = new List<Customer>();

        public CustomerController(ProductContext context, IMapper mapper){
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetCustomers(){
            List<Customer> customers = await  _context.Customers.Include(c => c.Orders).ToListAsync();
            List<CustomerDTO> customerDTOs = _mapper.Map<List<CustomerDTO>>(customers);
            return Ok(customerDTOs);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetById(int id){
            Customer found = await _context.Customers.FindAsync(id);

            if(found == null){
                return NotFound();
            }
            return Ok(_mapper.Map<CustomerDTO>(found));
        }
        [HttpPost]

         public async Task<ActionResult> CreateCustomer(CustomerDTO newCustomerDTO) {
             Customer newCustomer = _mapper.Map<Customer>(newCustomerDTO);

            _context.Customers.Add(newCustomer);
        await _context.SaveChangesAsync();

            return CreatedAtAction("CreateCustomer", newCustomer);
        }
    }
}