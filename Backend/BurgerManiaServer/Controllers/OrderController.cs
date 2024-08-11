using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BurgerManiaServer.Data;
using BurgerManiaServer.Models;
using System.Data.Common;
using BurgerManiaServer.Utilities;

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

        // PUT: api/Order/UpdateOrder
        [HttpPut("UpdateOrder")]
        public async Task<IActionResult> UpdateOrder(UpdateOrderParam updateOrderParam)
        {
            try
            {
                string burger = updateOrderParam.Burger;
                string category = updateOrderParam.Category;
                Order UpdatedItem = updateOrderParam.UpdatedItem; ;

                var item = await _context.Orders.FirstOrDefaultAsync(
                    item => item.Burger == burger && item.Category == category && item.UserId == UpdatedItem.UserId && !item.IsCheckout);
                if (item != null)
                {
                    Console.WriteLine("Into the if of if ");
                    UpdatedItem.TotalPrice = UpdatedItem.Price * UpdatedItem.Quantity;
                    _context.Orders.Entry(item).State = EntityState.Detached;
                    _context.Orders.Entry(UpdatedItem).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return Ok(UpdatedItem);
                }
                else
                {
                    Console.WriteLine("Into the else of else ");
                    return NotFound();
                }
                
            }
            catch (DbException err)
            {
                throw err;
            }
        }


        // DELETE: api/Order/RemoveOrder
        [HttpDelete("RemoveOrder")]
        public async Task<IActionResult> RemoveOrder(RemoveOrderParam removeOrderParam)
        {
            Console.WriteLine("Delete api call params: ",removeOrderParam);
            var item = await _context.Orders
                .FirstOrDefaultAsync(item => item.Burger == removeOrderParam.Burger && item.Category == removeOrderParam.Category && item.UserId == removeOrderParam.UserId && item.IsCheckout==false);
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