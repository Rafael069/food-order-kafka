using Microsoft.AspNetCore.Mvc;

namespace FoodOrder.API.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly OrderService _service;

        public OrdersController(OrderService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequest request)
        {
            var order = await _service.CreateOrder(request.Items, request.Customer);

            return Ok(order);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var order = _service.GetById(id);

            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            Console.WriteLine("Entrou no cancel");
            var success = await _service.CancelOrder(id);

            if (!success)
                return NotFound();

            return NoContent();
        }

        //[HttpPost("{id}/cancel")]
        //public IActionResult Cancel(Guid id)
        //{
        //    var success = _service.CancelOrder(id);

        //    if (success)
        //        return NotFound();

        //    return NoContent();
        //}

        public class CreateOrderRequest
        {
            public List<string> Items { get; set; } = new();
            public string Customer { get; set; } = string.Empty;
        }
    }
}
