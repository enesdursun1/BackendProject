using Business.Abstract;
using Business.Constants;
using Business.Dtos.Requests.User;
using Business.Dtos.Responses.User;
using Core.Attributes.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static Business.Constants.UsersOperationClaims;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : BaseController
{
    private IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
       
    }



    [HttpGet("{Id}")]
    [Authorization(new[] { Admin, Read })]
    public async Task<IActionResult> GetById([FromRoute] GetByIdUserRequest getByIdUserRequest)
    {
        GetByIdUserResponse result = await _userService.GetByIdAsync(getByIdUserRequest);
        return Ok(result);
    }

    [HttpGet("GetFromAuth")]
    public async Task<IActionResult> GetFromAuth()
    {
        GetByIdUserRequest getByIdUserRequest = new() { Id = getUserIdFromRequest() };
        GetByIdUserResponse result = await _userService.GetByIdAsync(getByIdUserRequest);
        return Ok(result);
    }

    [HttpGet]
    [Authorization(new[] { Admin, Read })]
    public async Task<IActionResult> GetList()
    {
        var data = await _userService.GetListAsync();

        return Ok(data);
    }

    [HttpPost]
  
    [Authorization(new[] { Admin, Write, Create })]
    public async Task<IActionResult> Add([FromBody] CreateUserRequest createUserRequest)
    {
        CreatedUserResponse result = await _userService.AddAsync(createUserRequest);
        return Created("",result);    
    }

    [HttpPut]

    [Authorization(new[] { Admin, Write, UsersOperationClaims.Update })]
    public async Task<IActionResult> Update([FromBody] UpdateUserRequest updateUserRequest)
    {
        UpdatedUserResponse result = await _userService.UpdateAsync(updateUserRequest);
        return Ok(result);
    }

    //[HttpPut("FromAuth")]
    //public async Task<IActionResult> UpdateFromAuth([FromBody] UpdateUserFromAuthCommand updateUserFromAuthCommand)
    //{
    //    updateUserFromAuthCommand.Id = getUserIdFromRequest();
    //    UpdatedUserFromAuthResponse result = await _userService.AddAsync(updateUserFromAuthCommand);
    //    return Ok(result);
    //}

    [HttpDelete]

    [Authorization(new[] { Admin, Write, UsersOperationClaims.Delete })]
    public async Task<IActionResult> Delete([FromBody] DeleteUserRequest deleteUserRequest)
    {
        DeletedUserResponse result = await _userService.DeleteAsync(deleteUserRequest);
        return Ok(result);
    }

}
