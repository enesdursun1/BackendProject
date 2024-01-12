using AutoMapper;
using Business.Abstract;
using Business.Dtos.Requests.Category;
using Business.Dtos.Requests.Product;
using Business.Dtos.Responses.Category;
using Business.Rules;
using Business.ValidationRules.FluentValidation.Category;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Validation.FluentValidation;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete;

public class CategoryManager : ICategoryService
{
    private readonly ICategoryDal _categoryDal;
    private readonly IMapper _mapper;
    private readonly CategoryBusinessRules _categoryBusinessRules;

    public CategoryManager(ICategoryDal categoryDal, IMapper mapper, CategoryBusinessRules categoryBusinessRules)
    {
        _categoryDal = categoryDal;
        _mapper = mapper;
        _categoryBusinessRules = categoryBusinessRules;
    }


    public async Task<CreatedCategoryResponse> AddAsync(CreateCategoryRequest createCategoryRequest)
    {
        await ValidationManager.ValidateAsync(typeof(CreateCategoryValidator), createCategoryRequest);

        await _categoryBusinessRules.CategoryNameCanNotBeDuplicatedWhenInserted(createCategoryRequest.Name);

        Category category = _mapper.Map<Category>(createCategoryRequest);

        Category createdCategory = await _categoryDal.AddAsync(category);

        CreatedCategoryResponse createdCategoryResponse = _mapper.Map<CreatedCategoryResponse>(createdCategory);

        return createdCategoryResponse;
    }

    public async Task<DeletedCategoryResponse> DeleteAsync(DeleteCategoryRequest deleteCategoryRequest)
    {
        Category? category = await _categoryDal.GetAsync(p => p.Id == deleteCategoryRequest.Id);

        _categoryBusinessRules.CategoryShouldExistWhenSelected(category);

        Category? deletedCategory = await _categoryDal.DeleteAsync(category);

        DeletedCategoryResponse mappedCategory = _mapper.Map<DeletedCategoryResponse>(deletedCategory);

        return mappedCategory;
    }

    public async Task<GetByIdCategoryResponse> GetByIdAsync(GetByIdCategoryRequest getByIdCategoryRequest)
    {
        Category? category = await _categoryDal.GetAsync(p => p.Id == getByIdCategoryRequest.Id);

        _categoryBusinessRules.CategoryShouldExistWhenSelected(category);

        GetByIdCategoryResponse getByIdCategoryResponse = _mapper.Map<GetByIdCategoryResponse>(category);

        return getByIdCategoryResponse;
    }

    public async Task<IList<GetListCategoryResponse>> GetListAsync()
    {
        var data = await _categoryDal.GetListAsync();
        IList<GetListCategoryResponse> getListCategoryResponse = _mapper.Map<IList<GetListCategoryResponse>>(data);
        return getListCategoryResponse;
    }

    public async Task<UpdatedCategoryResponse> UpdateAsync(UpdateCategoryRequest updateCategoryRequest)
    {
        await ValidationManager.ValidateAsync(typeof(UpdateCategoryValidator), updateCategoryRequest);

        Category? category = await _categoryDal.GetAsync(p => p.Id == updateCategoryRequest.Id);

        _categoryBusinessRules.CategoryShouldExistWhenSelected(category);

        _mapper.Map(updateCategoryRequest, category);

        await _categoryBusinessRules.CategoryNameCanNotBeDuplicatedWhenUpdated(category);

        Category? updatedCategory = await _categoryDal.UpdateAsync(category);


        UpdatedCategoryResponse mappedCategory = _mapper.Map<UpdatedCategoryResponse>(updatedCategory);

        return mappedCategory;
    }
}

