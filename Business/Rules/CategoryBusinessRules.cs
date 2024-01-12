using Core.CrossCuttingConcerns.Exceptions;
using Core.CrossCuttingConcerns.Exceptions.Types;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Rules;

public class CategoryBusinessRules
{
    private readonly ICategoryDal _categoryDal;

    public CategoryBusinessRules(ICategoryDal categoryDal)
    {
        _categoryDal = categoryDal;
    }

    public void CategoryShouldExistWhenSelected(Category category)
    {

        if (category == null)
            throw new Exception("Category not exists");
    }
    public async Task CategoryNameCanNotBeDuplicatedWhenInserted(string name)
    {
       Category? result = await _categoryDal.GetAsync(p => p.Name.ToLower() == name.ToLower());
       if (result!=null)
            throw new BusinessException("Category name exists");
    }
    public async Task CategoryNameCanNotBeDuplicatedWhenUpdated(Category category)
    {
        Category? result = await _categoryDal.GetAsync(x => x.Id != category.Id && x.Name.ToLower() == category.Name.ToLower());
        if (result != null)
            throw new BusinessException("Category name exists");

    }
}
