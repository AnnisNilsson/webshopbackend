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
            //anropar customer metod, den som skapar en ny kund
            Customer cust = await CreateCustomer(newOrderDTO.Customer);
            //hämtar in objektet från createCustomer
            CustomerDTO custD = _mapper.Map<CustomerDTO>(cust);
            //automappar objekten newOrderDto -> newOrdet 
            Order newOrder = _mapper.Map<Order>(newOrderDTO);
            //Hämtar det nuvarande datumet och tiden när orden skapas
            newOrder.Created = DateTime.Now;
            //lägger till Id från cust i newOrders CustomerId.
            newOrder.CustomerId = cust.Id;
            //gör en ny lista i newOrder med propertyn Orderrows
            newOrder.OrderRows = new List<OrderRow>();
            
            //sparar ner new order och lägger till i databasen
            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();


         
        //gör det till en lista med orderrows och skickar in orderId
            List<OrderRow> or = await CreateOrderRow(newOrderDTO.OrderRows, newOrder.Id);
            //mappar listan till orderRowD
            List<OrderRowDTO> orderRowD = _mapper.Map<List<OrderRowDTO>>(or);

            //***total price
            int totalPrice = 0;
            foreach (OrderRow orderrow in newOrder.OrderRows) {
            int price  = (await _context.Products.FirstAsync(p => p.Id == orderrow.ProductId)).Price;
            totalPrice = totalPrice + price;
            }
            //uppdaterar total price
             newOrder.TotalPrice = totalPrice;

       //mappar objektet newOrder till newOrderD
        OrderDTO newOrderD = _mapper.Map<OrderDTO>(newOrder);
            
            // sparar ner totalPrice
            await _context.SaveChangesAsync();

            //returnerar newOrderD
            return CreatedAtAction("CreateOrder", newOrderD);
        }

        private async Task<List<OrderRow>>CreateOrderRow(ICollection<OrderRowDTO> newOrderRowDTO, int OrderId)
        {
            //gör en lista med datatypen OrderRow
            List<OrderRow> newlyCreatedOrderRows = new List<OrderRow>();
            {
            //loopar igenom listan med objekt av datatypen OrderRowDTO i newOrderRowDTO
                foreach(OrderRowDTO orDto in newOrderRowDTO){
                    //skapar en ny orderrow
                    OrderRow newOrderRow = new OrderRow()
                    {
                    //lägger till OrderId propertyn i OrderRow
                    OrderId = OrderId,
                    //lägger till ProductId av produkter som laggts in
                    ProductId = orDto.ProductId
                };
             //sparar ner neworderRow och lägger till i databasen
            _context.OrderRows.Add(newOrderRow);
            await _context.SaveChangesAsync();
            // i listan newlyCreatedOrderRows läggs newOrderRow till
            newlyCreatedOrderRows.Add(newOrderRow);
        } 
        //returnerar newlyCreatedOrderRows
            return newlyCreatedOrderRows;
        }
        }
        private async Task<Customer>CreateCustomer(CustomerDTO newCustomerDTO)
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
