using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntityConfigurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens").HasKey(r => r.Id);

        builder.Property(r => r.Id).HasColumnName("Id").IsRequired();
        builder.Property(r => r.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(r => r.Token).HasColumnName("Token").IsRequired();
        builder.Property(r => r.Expires).HasColumnName("Expires").IsRequired();
        builder.Property(r => r.CreatedByIp).HasColumnName("CreatedByIp").IsRequired();
        builder.Property(r => r.Revoked).HasColumnName("Revoked");
        builder.Property(r => r.RevokedByIp).HasColumnName("RevokedByIp");
        builder.Property(r => r.ReplacedByToken).HasColumnName("ReplacedByToken");
        builder.Property(r => r.ReasonRevoked).HasColumnName("ReasonRevoked");
        builder.Property(r => r.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(r => r.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(r => r.DeletedDate).HasColumnName("DeletedDate");


        builder.HasQueryFilter(r => !r.DeletedDate.HasValue);


        builder.HasOne(r => r.User);

    }
}
