using App.API.Filters;
using App.Application.Features.Categories;
using App.Application.Features.Categories.Create;
using App.Application.Features.Categories.Update;
using App.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

public class CategoriesController(ICategoryService categoryService) : CustomBaseController {
    [HttpGet]
    public async Task<IActionResult> GetCategories() {
        return CreateActionResult(await categoryService.GetAllListAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCategory(int id) {
        return CreateActionResult(await categoryService.GetByIdAsync(id));
    }

    [HttpGet("{id}/products")]
    public async Task<IActionResult> GetCategoryWithProducts(int id) {
        return CreateActionResult(await categoryService.GetCategoryWithProductsAsync(id));
    }

    [HttpGet("products")]
    public async Task<IActionResult> GetCategoryWithProducts() {
        return CreateActionResult(await categoryService.GetCategoryWithProductsAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryRequest request) {
        return CreateActionResult(await categoryService.CreateAsync(request));
    }

    [ServiceFilter(typeof(NotFoundFilter<Category, int>))]
    [HttpPut]
    public async Task<IActionResult> Update(int id, UpdateCategoryRequest request) {
        return CreateActionResult(await categoryService.UpdateAsync(id, request));
    }

    [ServiceFilter(typeof(NotFoundFilter<Category, int>))]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) {
        return CreateActionResult(await categoryService.DeleteAsync(id));
    }
}