using App.Application.Contracts.Persistence;
using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Categories;

public class CategoryRepository(AppDbContext context) : GenericRepository<Category, int>(context), ICategoryRepository {
    public Task<Category?> GetCategoryWithProductAsync(int id) {
        return Context.Categories.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<List<Category>> GetCategoryWithProductsAsync() {
        return Context.Categories.Include(x => x.Products).ToListAsync();
    }
}