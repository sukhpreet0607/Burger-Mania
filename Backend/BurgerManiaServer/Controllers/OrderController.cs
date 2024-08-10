using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BurgerManiaServer.Data;
using BurgerManiaServer.Models;
using System.Data.Common;

namespace BurgerManiaServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly BurgerManiaContext _context;

        public OrderController(BurgerManiaContext context)
        {
            _context = context;
        }

        // GET: api/Order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> Index()
        {
            return await _context.Orders.ToListAsync();
        }

        // GET: api/Order/burger/category
        [HttpGet("{burger}/{category}")]
        public async Task<IActionResult> GetOrder(string burger, string category)
        {
            try
            {
                var Order = await _context.Orders.
                        FirstOrDefaultAsync(item => item.Burger == burger && item.Category == category);
                if (Order == null)
                {
                    return NotFound();
                }

                return Ok(Order);
            }
            catch (DbException err)
            {

                throw err;
            }
        }

        // POST: api/Order/AddOrder
        [HttpPost("AddOrder")]
        public async Task<ActionResult<Order>> AddOrder(Order Order)
        {
            try
            {
                switch (Order.Category)
                {
                    case "Veg":
                        Order.Price = 100;
                        break;
                    case "Egg":
                        Order.Price = 150;
                        break;
                    case "Chicken":
                        Order.Price = 200;
                        break;
                    default:
                        Order.Price = null;
                        break;
                }

                Order.TotalPrice = Convert.ToInt32(Order.Price) * Order.Quantity;
                _context.Orders.Add(Order);
                await _context.SaveChangesAsync();
            }
            catch (DbException err)
            {
                throw err;
            }

            return CreatedAtAction("AddOrder", new { id = Order.ItemId }, Order);
        }

        // PUT: api/Order/burger/category
        [HttpPut("{burger}/{category}")]
        public async Task<ActionResult> UpdateOrder(string burger, string category, Order updatedItem)
        {
            try
            {
                var item = await _context.Orders.FirstOrDefaultAsync(
                    item => item.Burger==burger && item.Category==category);
                if (item != null)
                {
                    updatedItem.TotalPrice = updatedItem.Price * updatedItem.Quantity;
                    _context.Orders.Entry(item).State = EntityState.Detached;
                    _context.Orders.Entry(updatedItem).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return Ok(updatedItem);
                }
                else
                {
                    return NotFound();
                }
                
            }
            catch (DbException err)
            {
                throw err;
            }
        }


        // DELETE: api/Order/burger/category
        [HttpDelete("{burger}/{category}")]
        public async Task<IActionResult> RemoveOrder(string burger, string category)
        {
            var item = await _context.Orders
                .FirstOrDefaultAsync(item => item.Burger == burger && item.Category == category);
            if (item == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(item);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}