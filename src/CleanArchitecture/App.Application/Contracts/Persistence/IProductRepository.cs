using App.Domain.Entities;

namespace App.Application.Contracts.Persistence;

public interface IProductRepository : IGenericRepository<Product, int> {
    Task<List<Product>> GetTopPriceProductAsync(int count);
}