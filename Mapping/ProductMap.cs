using Microsoft.EntityFrameworkCore;

using kazariobranco_backend.Models;

namespace kazariobranco_backend.Mapping;

public class ProductMap : IEntityTypeConfiguration<ProductModel>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ProductModel> builder)
    {
        throw new NotImplementedException();
    }
}
