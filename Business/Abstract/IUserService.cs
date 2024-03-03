using Business.Dtos.Requests.User;
using Business.Dtos.Responses.User;
using Core.Security.Entities;

namespace Business.Abstract;

public interface IUserService
{
    public Task<CreatedUserResponse> AddAsync(CreateUserRequest createUserRequest);
    public Task<IList<GetListUserResponse>> GetListAsync();
    public Task<GetByIdUserResponse> GetByIdAsync(GetByIdUserRequest getByIdUserRequest);
    public Task<User> GetByEmailAsync(string email);
    public Task<UpdatedUserResponse> UpdateAsync(UpdateUserRequest updateUserRequest);
    public Task<DeletedUserResponse> DeleteAsync(DeleteUserRequest deleteUserRequest);


}
