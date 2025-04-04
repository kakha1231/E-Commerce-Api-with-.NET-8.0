using ErrorOr;

namespace OrderService.Domain.Errors;

public static class Errors
{
        public static Error OrderNotFound => Error.NotFound(description: "Order Not Found");
}