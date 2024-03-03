namespace Business.Dtos.Responses.OperationClaim;

public class GetListOperationClaimResponse
{
    public int Id { get; set; }
    public string Name { get; set; }

    public GetListOperationClaimResponse()
    {
        Name = string.Empty;
    }

    public GetListOperationClaimResponse(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
