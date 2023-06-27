using kazariobranco_backend.Models;
using kazariobranco_backend.Request;

using AutoMapper;
using Microsoft.Extensions.Options;

namespace kazariobranco_backend.Mapping;

public class MyAutoMapper : Profile
{
    public MyAutoMapper()
    {
        CreateMap<RegisterRequest, UserModel>()
            .ForMember(
                p => p.Password,
                options =>
                    options.MapFrom(p => BCrypt.Net.BCrypt.HashPassword(p.Password))
            );

        CreateMap<ContactRequest, ContactModel>();
    }
}
