using Business.Dtos.Requests.Category;
using Business.Dtos.Responses.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract;

public interface ICategoryService
{
    public Task<CreatedCategoryResponse> AddAsync(CreateCategoryRequest createCategoryRequest);
    public Task<IList<GetListCategoryResponse>> GetListAsync();
    public Task<GetByIdCategoryResponse> GetByIdAsync(GetByIdCategoryRequest getByIdCategoryRequest);
    public Task<UpdatedCategoryResponse> UpdateAsync(UpdateCategoryRequest updateCategoryRequest);
    public Task<DeletedCategoryResponse> DeleteAsync(DeleteCategoryRequest deleteCategoryRequest);
}
