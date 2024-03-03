using Business.Abstract;
using Business.Constants;
using Business.Dtos.Requests.OperationClaim;
using Business.Dtos.Responses.OperationClaim;
using Core.Attributes.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static Business.Constants.OperationClaimsOperationClaims;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OperationClaimsController : BaseController
{
    private readonly IOperationClaimService _operationClaimService;

    public OperationClaimsController(IOperationClaimService operationClaimService)
    {
        _operationClaimService = operationClaimService;
    }

    [HttpGet("{Id}")]
    [Authorization(new[] { Admin, Read })]
    public async Task<IActionResult> GetById([FromRoute] GetByIdOperationClaimRequest getByIdOperationClaimRequest)
    {
        GetByIdOperationClaimResponse result = await _operationClaimService.GetByIdAsync(getByIdOperationClaimRequest);
        return Ok(result);
    }

    [HttpGet]
    [Authorization(new[] { Admin, Read })]
    public async Task<IActionResult> GetList()
    {

        var data = await _operationClaimService.GetListAsync();
        return Ok(data);
    }

    [HttpPost]
    [Authorization(new[] { Admin, Write, Create })]
    public async Task<IActionResult> Add([FromBody] CreateOperationClaimRequest createOperationClaimRequest)
    {
        CreatedOperationClaimResponse result = await _operationClaimService.AddAsync(createOperationClaimRequest);
        return Created(uri: "", result);
    }

    [HttpPut]
    [Authorization(new[] { Admin, Write, OperationClaimsOperationClaims.Update })]
    public async Task<IActionResult> Update([FromBody] UpdateOperationClaimRequest updateOperationClaimRequest)
    {
        UpdatedOperationClaimResponse result = await _operationClaimService.UpdateAsync(updateOperationClaimRequest);
        return Ok(result);
    }

    [HttpDelete]
    [Authorization(new[] { Admin, Write, OperationClaimsOperationClaims.Delete })]
    public async Task<IActionResult> Delete([FromBody] DeleteOperationClaimRequest deleteOperationClaimRequest)
    {
        DeletedOperationClaimResponse result = await _operationClaimService.DeleteAsync(deleteOperationClaimRequest);
        return Ok(result);
    }

}
