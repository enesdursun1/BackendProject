using Application.Services.Repositories;
using Core.DataAccess.Repositories;
using Core.Security.Entities;
using DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete;

public class EfRefreshTokenDal: EfRepositoryBase<RefreshToken, int, BaseDbContext> ,IRefreshTokenDal
{
    public EfRefreshTokenDal(BaseDbContext context) : base(context)
    {
    }

    public async Task<List<RefreshToken>> GetOldRefreshTokensAsync(int userId, int refreshTokenTTL)
    {
        List<RefreshToken> tokens = await Query()
            .AsNoTracking()
            .Where(

            r =>

              r.UserId == userId
            && r.Revoked == null
            && r.Expires >= DateTime.UtcNow
            && r.CreatedDate.AddDays(refreshTokenTTL) <= DateTime.UtcNow

            )
            .ToListAsync();

        return tokens;
    }
}
