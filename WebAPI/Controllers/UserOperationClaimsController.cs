using Business.Abstract;
using Business.Constants;
using Business.Dtos.Requests.UserOperationClaim;
using Business.Dtos.Responses.UserOperationClaim;
using Core.Attributes.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static Business.Constants.UserOperationClaimsOperationClaims;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserOperationClaimsController : BaseController
{
    private readonly IUserOperationClaimService _userOperationClaimService;

    public UserOperationClaimsController(IUserOperationClaimService userOperationClaimService)
    {
        _userOperationClaimService = userOperationClaimService;
    }

    [HttpGet("{Id}")]
    [Authorization(new[] { Admin, Read })]
    public async Task<IActionResult> GetById([FromRoute] GetByIdUserOperationClaimRequest getByIdUserOperationClaimRequest)
    {
        GetByIdUserOperationClaimResponse result = await _userOperationClaimService.GetByIdAsync(getByIdUserOperationClaimRequest);
        return Ok(result);
    }

    [HttpGet]
    [Authorization(new[] { Admin, Read })]
    public async Task<IActionResult> GetList()
    {

        var data = await _userOperationClaimService.GetListAsync();
        return Ok(data);
    }

    [HttpPost]

    [Authorization(new[] { Admin, Write, Create })]
    public async Task<IActionResult> Add([FromBody] CreateUserOperationClaimRequest createUserOperationClaimRequest)
    {
        CreatedUserOperationClaimResponse result = await _userOperationClaimService.AddAsync(createUserOperationClaimRequest);
        return Created(uri: "", result);
    }

    [HttpPut]

    [Authorization(new[] { Admin, Write, UserOperationClaimsOperationClaims.Update })]
    public async Task<IActionResult> Update([FromBody] UpdateUserOperationClaimRequest updateUserOperationClaimRequest)
    {
        UpdatedUserOperationClaimResponse result = await _userOperationClaimService.UpdateAsync(updateUserOperationClaimRequest);
        return Ok(result);
    }

    [HttpDelete]

    [Authorization(new[] { Admin, Write , UserOperationClaimsOperationClaims.Delete })]
    public async Task<IActionResult> Delete([FromBody] DeleteUserOperationClaimRequest deleteUserOperationClaimRequest)
    {
        DeletedUserOperationClaimResponse result = await _userOperationClaimService.DeleteAsync(deleteUserOperationClaimRequest);
        return Ok(result);
    }

}
