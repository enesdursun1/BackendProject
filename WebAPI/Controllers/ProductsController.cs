using Business.Abstract;
using Business.Constants;
using Business.Dtos.Requests.Product;
using Core.Attributes.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Business.Constants.ProductsOperationClaims;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet]
        [Authorization(new[] { Admin, Read })]
        public async Task<IActionResult> GetList()
        {
            var data = await _productService.GetListAsync();

            return Ok(data);

        }

        [HttpPost]
    
        [Authorization(new[] { Admin, Write, Create })]
        public async Task<IActionResult> Add([FromBody]CreateProductRequest createProductRequest)
        {
            var data = await _productService.AddAsync(createProductRequest);

            return Ok(data);

        }
        [HttpPut]

        [Authorization(new[] { Admin, Write, ProductsOperationClaims.Update })]
        public async Task<IActionResult> Update([FromBody] UpdateProductRequest updateProductRequest)
        {
            var data = await _productService.UpdateAsync(updateProductRequest);

            return Ok(data);

        }
        [HttpDelete]
      
        [Authorization(new[] { Admin, Write , ProductsOperationClaims.Delete })]
        public async Task<IActionResult> Delete([FromBody] DeleteProductRequest deleteProductRequest)
        {
            var data = await _productService.DeleteAsync(deleteProductRequest);

            return Ok(data);

        }
        [HttpGet("{Id}")]
        [Authorization(new[] { Admin, Read })]
        public async Task<IActionResult> GetById([FromRoute] GetByIdProductRequest getByIdProductRequest) 
        {
            var data = await _productService.GetByIdAsync(getByIdProductRequest);

            return Ok(data);

        }
    }
}
