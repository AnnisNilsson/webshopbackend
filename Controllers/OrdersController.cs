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
    public class OrdersController : ControllerBase
    {
        private readonly ProductContext _context;
        private readonly IMapper _mapper;


        private List<Order> orders = new List<Order>();

        public OrdersController(ProductContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetOrders()
        {
            List<Order> orders = await _context.Orders.Include(o => o.OrderRows).ToListAsync();
            List<OrderDTO> orderDTOs = _mapper.Map<List<OrderDTO>>(orders);
            return Ok(orderDTOs);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            Order found = await _context.Orders.FindAsync(id);

            if (found == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<OrderDTO>(found));
        }

        //skapar created
        [HttpPost]
        public async Task<ActionResult> CreateOrder(OrderDTO newOrderDTO)
        {
            int CustId = await CreateCustomer(newOrderDTO.CustomerDTO);
            Order newOrder = _mapper.Map<Order>(newOrderDTO);
            newOrder.Created = DateTime.Now;
            newOrder.CustomerId = CustId.ToString();
            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("CreateOrder", newOrder);
        }

        public async Task<int>CreateCustomer(CustomerDTO newCustomerDTO)
        {
            Customer newCustomer = new Customer()
            {
                Name = newCustomerDTO.Name
            };
            _context.Customers.Add(newCustomer);
            await _context.SaveChangesAsync();
            return newCustomer.Id;
        }
        //ny
        // [HttpPost]
        //  public async Task<ActionResult> CreateCustomer(CustomerDTO newCustomerDTO) {
        //      Customer newCustomer = _mapper.Map<Customer>(newCustomerDTO);

        //     _context.Customers.Add(newCustomer);
        // await _context.SaveChangesAsync();

        //     return CreatedAtAction("CreateCustomer", newCustomer);
        // }
        //ny slut
    }
}

