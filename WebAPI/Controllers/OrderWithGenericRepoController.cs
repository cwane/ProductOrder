using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Entity;
using WebAPI.Repository;
using WebAPI.ViewModel;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderWithGenericRepoController : ControllerBase
    {
        private readonly IRepository<Order> _orderRespository;
        public OrderWithGenericRepoController(IRepository<Order> orderRespository)
        {
            _orderRespository = orderRespository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var orders = await _orderRespository.GetAllAsync();
            return Ok(orders);

        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetById(int id)
        {
            var order = await _orderRespository.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderRequest order)

        {
            var orderEntity = new Order()
            {
                ProductId = order.ProductId,
                Orderdate = order.Orderdate,
               
            };
            var createdProductResponse = await _orderRespository.AddAsync(orderEntity);
            return CreatedAtAction(nameof(GetById), new { id = createdProductResponse.OrderId }, createdProductResponse.Orderdate);

        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] OrderRequest order)
        {
            var orderEntity = await _orderRespository.GetByIdAsync(id);
            if (orderEntity == null)
            {
                return NotFound();
            }
            orderEntity.Orderdate = order.Orderdate;
           
            await _orderRespository.UpdateAsync(orderEntity);
            return NoContent();
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _orderRespository.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            await _orderRespository.UpdateAsync(order);
            return NoContent();
        }
    }
}

