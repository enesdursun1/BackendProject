namespace Business.Dtos.Requests.OperationClaim;

public class UpdateOperationClaimRequest
{
    public int Id { get; set; }
    public string Name { get; set; }

    public UpdateOperationClaimRequest()
    {
        Name = string.Empty;
    }

    public UpdateOperationClaimRequest(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
