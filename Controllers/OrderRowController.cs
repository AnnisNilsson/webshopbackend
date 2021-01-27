using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

 

namespace dotnetwebshop.Controllers
{

 

    [ApiController]
    [Route("[controller]")]
    public class OrderRowController : ControllerBase
    {
        private readonly ProductContext _context;
        private readonly IMapper _mapper;

 

        public OrderRowController(ProductContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

 
        [HttpGet]
        public async Task<ActionResult> GetOrderRows()
        {
            List<OrderRow> orderRows = await _context.OrderRows.Include(or => or.Order).Include(or => or.Product).ToListAsync();
            List<OrderRowDTO> orderRowDTOs = _mapper.Map<List<OrderRowDTO>>(orderRows);

            return Ok(orderRowDTOs);
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrderRow(OrderRowDTO newOrderRowDTO)
        {
            OrderRow newOrderRow = _mapper.Map<OrderRow>(newOrderRowDTO);

            _context.OrderRows.Add(newOrderRow);
            await _context.SaveChangesAsync();

            return CreatedAtAction("CreateOrderRow", newOrderRow);
        }
    }
}