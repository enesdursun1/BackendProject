using AutoMapper;
using Business.Abstract;
using Business.Dtos.Requests.Product;
using Business.Dtos.Responses.Product;
using Business.Rules;
using Business.ValidationRules.FluentValidation.Product;
using Core.CrossCuttingConcerns.Validation.FluentValidation;
using Core.CrossCuttingConcerns.Authorization;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Business.Concrete;

public class ProductManager : IProductService
{
    private readonly IProductDal _productDal;
    private readonly IMapper _mapper;
    private readonly ProductBusinessRules _productBusinessRules;

    public ProductManager(IProductDal productDal, IMapper mapper, ProductBusinessRules productBusinessRules)
    {
        _productDal = productDal;
        _mapper = mapper;
        _productBusinessRules = productBusinessRules;
    }

    public async Task<CreatedProductResponse> AddAsync(CreateProductRequest createProductRequest)
    {
        await ValidationManager.ValidateAsync(typeof(CreateProductValidator), createProductRequest);

        await _productBusinessRules.ProductNameCanNotBeDuplicatedWhenInserted(createProductRequest.Name);

        Product product = _mapper.Map<Product>(createProductRequest);

        Product createdProduct = await _productDal.AddAsync(product);

        CreatedProductResponse createdProductResponse = _mapper.Map<CreatedProductResponse>(createdProduct);

        return createdProductResponse;
    }

    public async Task<IList<GetListProductResponse>> GetListAsync()
    {
        var data = await _productDal.GetListAsync(include: p => p.Include(p => p.Category));
        IList<GetListProductResponse> getListProductResponses = _mapper.Map<IList<GetListProductResponse>>(data);
        return getListProductResponses;
    }

    public async Task<GetByIdProductResponse> GetByIdAsync(GetByIdProductRequest getByIdProductRequest)
    {
        Product? product = await _productDal.GetAsync(p => p.Id == getByIdProductRequest.Id, include: p => p.Include(p => p.Category));

        _productBusinessRules.ProductShouldExistWhenSelected(product);

        GetByIdProductResponse getByIdProductResponse = _mapper.Map<GetByIdProductResponse>(product);

        return getByIdProductResponse;
    }

    public async Task<UpdatedProductResponse> UpdateAsync(UpdateProductRequest updateProductRequest)
    {
        await ValidationManager.ValidateAsync(typeof(UpdateProductValidator), updateProductRequest);

        Product? product = await _productDal.GetAsync(p => p.Id == updateProductRequest.Id);

        _productBusinessRules.ProductShouldExistWhenSelected(product);

        _mapper.Map(updateProductRequest, product);

        await _productBusinessRules.ProductNameCanNotBeDuplicatedWhenUpdated(product);

        Product? updatedProduct = await _productDal.UpdateAsync(product);

        UpdatedProductResponse mappedProduct = _mapper.Map<UpdatedProductResponse>(updatedProduct);

        return mappedProduct;
    }
    public async Task<DeletedProductResponse> DeleteAsync(DeleteProductRequest deleteProductRequest)
    {
        Product? product = await _productDal.GetAsync(p => p.Id == deleteProductRequest.Id);

        _productBusinessRules.ProductShouldExistWhenSelected(product);

        Product? deletedProduct = await _productDal.DeleteAsync(product);

        DeletedProductResponse mappedProduct = _mapper.Map<DeletedProductResponse>(deletedProduct);

        return mappedProduct;

    }
}

