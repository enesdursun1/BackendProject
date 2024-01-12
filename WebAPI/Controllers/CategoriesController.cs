using Business.Abstract;
using Business.Dtos.Requests.Category;
using Business.Dtos.Requests.Product;
using Core.CrossCuttingConcerns.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var data = await _categoryService.GetListAsync();

        return Ok(data);

    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateCategoryRequest createCategoryRequest)
    {
        var data = await _categoryService.AddAsync(createCategoryRequest);

        return Ok(data);

    }
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateCategoryRequest updateCategoryRequest)
    {
        var data = await _categoryService.UpdateAsync(updateCategoryRequest);

        return Ok(data);

    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteCategoryRequest deleteCategoryRequest)
    {
        var data = await _categoryService.DeleteAsync(deleteCategoryRequest);

        return Ok(data);

    }
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdCategoryRequest getByIdCategoryRequest)
    {
        var data = await _categoryService.GetByIdAsync(getByIdCategoryRequest);

        return Ok(data);

    }

}
