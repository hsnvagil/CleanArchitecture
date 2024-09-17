using App.API.Filters;
using App.Application.Features.Products;
using App.Application.Features.Products.Create;
using App.Application.Features.Products.Update;
using App.Application.Features.Products.UpdateStock;
using App.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;

public class ProductsController(IProductService productService) : CustomBaseController {
    [HttpGet]
    public async Task<IActionResult> GetAll() {
        return CreateActionResult(await productService.GetAllListAsync());
    }

    [HttpGet("{pageNumber:int}/{pageSize:int}")]
    public async Task<IActionResult> GetPagedAll(int pageNumber, int pageSize) {
        return CreateActionResult(await productService.GetPagedAllListAsync(pageNumber, pageSize));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id) {
        return CreateActionResult(await productService.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductRequest request) {
        return CreateActionResult(await productService.CreateAsync(request));
    }

    [ServiceFilter(typeof(NotFoundFilter<Product, int>))]
    [HttpPut]
    public async Task<IActionResult> Update(int id, UpdateProductRequest request) {
        return CreateActionResult(await productService.UpdateAsync(id, request));
    }

    [ServiceFilter(typeof(NotFoundFilter<Product, int>))]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) {
        return CreateActionResult(await productService.DeleteAsync(id));
    }

    [HttpPut("UpdateStock")]
    public async Task<IActionResult> UpdateStock(UpdateProductStockRequest request) {
        return CreateActionResult(await productService.UpdateStockAsync(request));
    }
}