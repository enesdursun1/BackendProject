namespace Business.Dtos.Responses.OperationClaim;

public class GetByIdOperationClaimResponse 
{
    public int Id { get; set; }
    public string Name { get; set; }

    public GetByIdOperationClaimResponse()
    {
        Name = string.Empty;
    }

    public GetByIdOperationClaimResponse(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
