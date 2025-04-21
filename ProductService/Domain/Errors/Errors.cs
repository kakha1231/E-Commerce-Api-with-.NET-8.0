using ErrorOr;

namespace ProductService.Domain.Errors;

public static class Errors {
    public static Error ProductNotFound => Error.NotFound(description: "Product Not Found");
}