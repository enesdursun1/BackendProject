using Core.CrossCuttingConcerns.Exceptions;
using Core.CrossCuttingConcerns.Exceptions.Types;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Rules;

public class ProductBusinessRules
{

    private readonly IProductDal _productDal;

    public ProductBusinessRules(IProductDal productDal)
    {
        _productDal = productDal;
    }

    public void ProductShouldExistWhenSelected(Product product)
    {

        if (product == null)
            throw new Exception("Product not exists");
    }
    public async Task ProductNameCanNotBeDuplicatedWhenInserted(string name)
    {
        Product? result = await _productDal.GetAsync(p => p.Name.ToLower() == name.ToLower());
        if (result != null)
            throw new BusinessException("Product name exists");
    }
    public async Task ProductNameCanNotBeDuplicatedWhenUpdated(Product product)
    {
        Product? result = await _productDal.GetAsync(x => x.Id != product.Id && x.Name.ToLower() == product.Name.ToLower());
        if (result != null)
            throw new BusinessException("Product name exists");

    }
}
