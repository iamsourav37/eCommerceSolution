using eCommerce.API.Utility;
using eCommerce.Core.DTOs.OrderDTO;
using eCommerce.Core.Interfaces.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private ApiResponse _response;

        public OrderController(IOrderService orderService)
        {
            this._orderService = orderService;
            _response = new ApiResponse();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderCreateDto orderCreateDto)
        {
            var orderDto = await _orderService.CreateOrder(orderCreateDto);
            _response.SetResponse(true, 200, orderDto, null);
            return Ok(_response);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Guid customerId, [FromQuery] Guid? orderId)
        {
            if (customerId == Guid.Empty)
            {
                _response.SetResponse(false, 400, null,["CustomerId is required." ]);
                return BadRequest(_response);
            }

            if (orderId.HasValue) // Fetch a specific order
            {
                var orderDto = await _orderService.GetOrderById(customerId, orderId.Value);

                if (orderDto == null)
                {
                    _response.SetResponse(false, 404, null, ["Order not found." ]);
                    return NotFound(_response);
                }

                _response.SetResponse(true, 200, orderDto, null);
                return Ok(_response);
            }

            // Fetch all orders for the customer
            var orderDtoList = await _orderService.GetAllOrders(customerId);

            if (orderDtoList == null || !orderDtoList.Any())
            {
                _response.SetResponse(false, 404, null, ["No orders found for the specified customer."]);
                return NotFound(_response);
            }

            _response.SetResponse(true, 200, orderDtoList, null);
            return Ok(_response);
        }

    }
}
