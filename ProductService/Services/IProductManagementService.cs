using Common.Dtos.Response;
using ProductService.Models;

namespace ProductService.Services;

public interface IProductManagementService
{
    public Task<ServiceResponse<List<Product>>> GetProducts();
    public Task<ServiceResponse<Product>> GetProductById(int productId);
    public Task<ServiceResponse<Product>> CreateProduct(Product product);
    public Task<ServiceResponse<Product>> UpdateProduct(Product product);
}