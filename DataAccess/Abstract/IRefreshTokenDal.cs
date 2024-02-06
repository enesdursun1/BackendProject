using Core.DataAccess.Repositories;
using Core.Security.Entities;

namespace Application.Services.Repositories;

public interface IRefreshTokenDal : IAsyncRepository<RefreshToken, int>, IRepository<RefreshToken, int>
{

    Task<List<RefreshToken>> GetOldRefreshTokensAsync(int userId,int refreshTokenTTL);
}
