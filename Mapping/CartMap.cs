using Microsoft.EntityFrameworkCore;

using kazariobranco_backend.Models;

namespace kazariobranco_backend.Mapping;

public class CartMap : IEntityTypeConfiguration<CartModel>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CartModel> builder)
    {
        throw new NotImplementedException();
    }
}
