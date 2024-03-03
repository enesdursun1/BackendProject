using AutoMapper;
using Azure.Core;
using Business.Abstract;
using Business.Dtos.Requests.OperationClaim;
using Business.Dtos.Responses.OperationClaim;
using Business.Dtos.Responses.User;
using Core.Security.Entities;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Business.Concrete;

public class OperationClaimManager : IOperationClaimService
{
    private readonly IOperationClaimDal _operationClaimDal;
    private readonly IMapper _mapper;


    public OperationClaimManager(IOperationClaimDal operationClaimDal, IMapper mapper)
    {
        _operationClaimDal = operationClaimDal;
        _mapper = mapper;

    }




    public async Task<CreatedOperationClaimResponse> AddAsync(CreateOperationClaimRequest createOperationClaimRequest)
    {
        OperationClaim mappedOperationClaim = _mapper.Map<OperationClaim>(createOperationClaimRequest);

        OperationClaim createdOperationClaim = await _operationClaimDal.AddAsync(mappedOperationClaim);

        CreatedOperationClaimResponse response = _mapper.Map<CreatedOperationClaimResponse>(createdOperationClaim);
        return response;
    }
    public async Task<UpdatedOperationClaimResponse> UpdateAsync(UpdateOperationClaimRequest updateOperationClaimRequest)
    {
        OperationClaim? operationClaim = await _operationClaimDal.GetAsync(
               predicate: oc => oc.Id == updateOperationClaimRequest.Id
              );
   
        OperationClaim mappedOperationClaim = _mapper.Map(updateOperationClaimRequest, destination: operationClaim!);

        OperationClaim updatedOperationClaim = await _operationClaimDal.UpdateAsync(mappedOperationClaim);

        UpdatedOperationClaimResponse response = _mapper.Map<UpdatedOperationClaimResponse>(updatedOperationClaim);
        return response;
    }
    public async Task<DeletedOperationClaimResponse> DeleteAsync(DeleteOperationClaimRequest deleteOperationClaimRequest)
    {
        OperationClaim? operationClaim = await _operationClaimDal.GetAsync(
                 predicate: oc => oc.Id == deleteOperationClaimRequest.Id,
                 include: q => q.Include(oc => oc.UserOperationClaims)
                
             );


        await _operationClaimDal.DeleteAsync(entity: operationClaim!);

        DeletedOperationClaimResponse response = _mapper.Map<DeletedOperationClaimResponse>(operationClaim);
        return response;
    }

    public async Task<GetByIdOperationClaimResponse> GetByIdAsync(GetByIdOperationClaimRequest getByIdOperationClaimRequest)
    {
        OperationClaim? operationClaim = await _operationClaimDal.GetAsync(
               predicate: b => b.Id == getByIdOperationClaimRequest.Id,
               include: q => q.Include(oc => oc.UserOperationClaims)
              
           );
      
        GetByIdOperationClaimResponse response = _mapper.Map<GetByIdOperationClaimResponse>(operationClaim);
        return response;
    }

    public async Task<IList<GetListOperationClaimResponse>> GetListAsync()
    {
      
            var data = await _operationClaimDal.GetListAsync();
            IList<GetListOperationClaimResponse> getListOperationClaimResponse = _mapper.Map<IList<GetListOperationClaimResponse>>(data);
            return getListOperationClaimResponse;
        

    }


}
