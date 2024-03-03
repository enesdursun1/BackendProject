using AutoMapper;
using Azure.Core;
using Business.Abstract;
using Business.Dtos.Requests.Product;
using Business.Dtos.Requests.User;
using Business.Dtos.Responses.Product;
using Business.Dtos.Responses.User;
using Business.Rules;
using Core.Security.Entities;
using Core.Security.Hashing;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Concrete;

public class UserManager : IUserService
{


    private readonly IUserDal _userDal;
    private readonly IMapper _mapper;


    public UserManager(IUserDal userDal, IMapper mapper)
    {
        _userDal = userDal;
        _mapper = mapper;

    }


    public async Task<CreatedUserResponse> AddAsync(CreateUserRequest createUserRequest)
    {
        User user = _mapper.Map<User>(createUserRequest);

        HashingHelper.CreatePasswordHash(
              createUserRequest.Password,
              passwordHash: out byte[] passwordHash,
              passwordSalt: out byte[] passwordSalt
          );
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        User createdUser = await _userDal.AddAsync(user);

        CreatedUserResponse response = _mapper.Map<CreatedUserResponse>(createdUser);
        return response;


    }
    public async Task<UpdatedUserResponse> UpdateAsync(UpdateUserRequest updateUserRequest)
    {
        User? user = await _userDal.GetAsync(predicate: u => u.Id == updateUserRequest.Id);


        user = _mapper.Map(updateUserRequest, user);

        HashingHelper.CreatePasswordHash(
            updateUserRequest.Password,
            passwordHash: out byte[] passwordHash,
            passwordSalt: out byte[] passwordSalt
        );
        user!.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        await _userDal.UpdateAsync(user);

        UpdatedUserResponse response = _mapper.Map<UpdatedUserResponse>(user);
        return response;
    }

    public async Task<DeletedUserResponse> DeleteAsync(DeleteUserRequest deleteUserRequest)
    {
        User? user = await _userDal.GetAsync(predicate: u => u.Id == deleteUserRequest.Id);


        await _userDal.DeleteAsync(user!);

        DeletedUserResponse response = _mapper.Map<DeletedUserResponse>(user);
        return response;
    }

    public async Task<GetByIdUserResponse> GetByIdAsync(GetByIdUserRequest getByIdUserRequest)
    {
        User? user = await _userDal.GetAsync(predicate: b => b.Id == getByIdUserRequest.Id);

        GetByIdUserResponse response = _mapper.Map<GetByIdUserResponse>(user);
        return response;
    }

    public async Task<IList<GetListUserResponse>> GetListAsync()
    {
        var data = await _userDal.GetListAsync();
        IList<GetListUserResponse> getListUserResponse = _mapper.Map<IList<GetListUserResponse>>(data);
        return getListUserResponse;
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        User? user = await _userDal.GetAsync(predicate: b => b.Email == email);



        return user;



    }
}
