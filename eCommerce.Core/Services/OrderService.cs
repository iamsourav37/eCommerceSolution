using AutoMapper;
using eCommerce.Core.Domain;
using eCommerce.Core.DTOs.OrderDTO;
using eCommerce.Core.Interfaces.RepositoryContracts;
using eCommerce.Core.Interfaces.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<OrderDto> CreateOrder(OrderCreateDto orderCreateDto)
        {

            #region Check Product exists or not

            var productIds = orderCreateDto.LineItems.Select(li => li.ProductId).ToList();
            var productDictionary = new Dictionary<Guid, double>();

            foreach (var productId in productIds)
            {
                var productDto = await _unitOfWork.Products.GetByIdAsync(productId);
                if (productDto == null)
                {
                    throw new Exception($"Invalid Product Id ('{productId}').");
                }
                productDictionary.Add(productId, productDto.Price);
            }

            #endregion

            Order order = new Order()
            {
                CustomerId = orderCreateDto.CustomerId,
                OrderDate = DateTime.UtcNow,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                Status = OrderStatus.Pending,
                ShippingAddressId = orderCreateDto.ShippingAddressId,
                PaymentStatus = PaymentStatus.Unpaid,
                LineItems = orderCreateDto.LineItems.Select(lineItem => new LineItem() { ProductId = lineItem.ProductId, Quantity = lineItem.Quantity, CreatedDate = DateTime.UtcNow, UpdatedDate = DateTime.UtcNow, }).ToList(),
                TotalAmount = orderCreateDto.LineItems.Sum(li => productDictionary[li.ProductId] * li.Quantity)
                
            };

            await _unitOfWork.Orders.AddAsync(order);
            var result = await _unitOfWork.SaveChangesAsync();

            if (result > 0)
            {
                return _mapper.Map<OrderDto>(order);
            }
            return null;
        }

        public async Task<List<OrderDto>> GetAllOrders(Guid customerId)
        {
            var orderList = await _unitOfWork.Orders.GetOrderByCustomerIdAsync(customerId);

            return _mapper.Map<List<OrderDto>>(orderList);
        }

        public async Task<OrderDto> GetOrderById(Guid customerId, Guid orderId)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId, "LineItems");

            if (order == null)
            {
                return null;
            }
            return _mapper.Map<OrderDto>(order);
        }
    }
}
