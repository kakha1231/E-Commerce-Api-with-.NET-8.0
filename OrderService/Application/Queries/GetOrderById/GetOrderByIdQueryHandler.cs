using ErrorOr;
using MediatR;
using OrderService.Domain.Agregates;
using OrderService.Domain.Errors;
using OrderService.Infrastructure.Data;

namespace OrderService.Application.Queries.GetOrderById;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, ErrorOr<Order>>
{
    
    private readonly IOrderRepository _orderRepository;

    public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<ErrorOr<Order>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
       var order = await _orderRepository.GetOrderById(request.OrderId);

       if (order == null)
       {
           return Errors.OrderNotFound;
       }
       
       return order;
    }
}