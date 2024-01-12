using Business.Dtos.Requests.Product;
using Business.Dtos.Responses.Product;

namespace Business.Abstract;

public interface IProductService
{
    public Task<CreatedProductResponse> AddAsync(CreateProductRequest createProductRequest);
    public Task<IList<GetListProductResponse>> GetListAsync();
    public Task<GetByIdProductResponse> GetByIdAsync(GetByIdProductRequest getByIdProductRequest);
    public Task<UpdatedProductResponse> UpdateAsync(UpdateProductRequest updateProductRequest);
    public Task<DeletedProductResponse> DeleteAsync(DeleteProductRequest deleteProductRequest);




}
