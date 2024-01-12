using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityConfigurations;

public class CategoryConfiguration:IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
{
    builder.ToTable("Categories").HasKey(p => p.Id);

    builder.Property(p => p.Id).HasColumnName("Id").IsRequired();
    builder.Property(p => p.Name).HasColumnName("Name").IsRequired();
   
    builder.HasMany(p => p.Products);


}
}
