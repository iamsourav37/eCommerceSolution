using eCommerce.Core.DTOs.OrderDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Interfaces.ServiceContracts
{
    public interface IOrderService
    {
        Task<List<OrderDto>> GetAllOrders(Guid customerId);
        Task<OrderDto> GetOrderById(Guid customerId, Guid orderId);
        Task<OrderDto> CreateOrder(OrderCreateDto orderCreateDto);
    }
}
