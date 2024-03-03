using Business.Abstract;
using static Business.Constants.CategoriesOperationClaims;
using Business.Dtos.Requests.Category;
using Business.Dtos.Requests.Product;
using Core.CrossCuttingConcerns.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Business.Constants;
using Core.Attributes.Authorization;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : BaseController
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    [Authorization(new[] { Admin, Read })]
    public async Task<IActionResult> GetList()
    {
        var data = await _categoryService.GetListAsync();

        return Ok(data);

    }

    [HttpPost]
    [Authorization(new[] { Admin, Write, Create })]
    public async Task<IActionResult> Add([FromBody] CreateCategoryRequest createCategoryRequest)
    {
        var data = await _categoryService.AddAsync(createCategoryRequest);

        return Ok(data);

    }
    [HttpPut]
    [Authorization(new[] { Admin, Write, CategoriesOperationClaims.Update })]
    public async Task<IActionResult> Update([FromBody] UpdateCategoryRequest updateCategoryRequest)
    {
        var data = await _categoryService.UpdateAsync(updateCategoryRequest);

        return Ok(data);

    }

    [HttpDelete]
    [Authorization(new[] { Admin, Write, CategoriesOperationClaims.Delete })]
    public async Task<IActionResult> Delete([FromBody] DeleteCategoryRequest deleteCategoryRequest)
    {
        var data = await _categoryService.DeleteAsync(deleteCategoryRequest);

        return Ok(data);

    }
    [HttpGet("{Id}")]
    [Authorization(new[] { Admin, Read })]
    public async Task<IActionResult> GetById([FromRoute] GetByIdCategoryRequest getByIdCategoryRequest)
    {
        var data = await _categoryService.GetByIdAsync(getByIdCategoryRequest);

        return Ok(data);

    }

}
