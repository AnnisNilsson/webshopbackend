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
    public class OrderController : ControllerBase
    {
        private readonly ProductContext _context;
        private readonly IMapper _mapper;


        private List<Order> orders = new List<Order>();

        public OrderController(ProductContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult> GetOrders()
        {
            List<Order> orders = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderRows)
                .ToListAsync();
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
            Customer cust = await CreateCustomer(newOrderDTO.CustomerDTO);
            CustomerDTO custD = _mapper.Map<CustomerDTO>(cust);
           
            Order newOrder = _mapper.Map<Order>(newOrderDTO);
            newOrder.Created = DateTime.Now;
            newOrder.CustomerId = cust.Id;
            newOrder.OrderRows = new List<OrderRow>();
           

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

        OrderDTO newOrderD = _mapper.Map<OrderDTO>(newOrder);
        newOrderD.CustomerDTO = custD;
            List<OrderRow> or = await CreateOrderRow(newOrderDTO.OrderRows, newOrder.Id);
            List<OrderRowDTO> orderRowD = _mapper.Map<List<OrderRowDTO>>(or);
            newOrderD.OrderRows = orderRowD;
            return CreatedAtAction("CreateOrder", newOrderD);
        }

        public async Task<List<OrderRow>>CreateOrderRow(ICollection<OrderRowDTO> newOrderRowDTO, int OrderId)
        {
            List<OrderRow> newlyCreatedOrderRows = new List<OrderRow>();
            {
                foreach(OrderRowDTO orDto in newOrderRowDTO){
                    OrderRow newOrderRow = new OrderRow()
                    {
                    OrderId = OrderId,
                    ProductId = orDto.ProductId
                };

            _context.OrderRows.Add(newOrderRow);
            await _context.SaveChangesAsync();
            newlyCreatedOrderRows.Add(newOrderRow);
        } 
            return newlyCreatedOrderRows;
        }
        }
        //änding slut

        public async Task<Customer>CreateCustomer(CustomerDTO newCustomerDTO)
        {
            Customer newCustomer = new Customer()
            {
                Name = newCustomerDTO.Name,
                Adress = newCustomerDTO.Adress,
                City = newCustomerDTO.City
            };
            _context.Customers.Add(newCustomer);
            await _context.SaveChangesAsync();
            return newCustomer;
        }
    }
}
