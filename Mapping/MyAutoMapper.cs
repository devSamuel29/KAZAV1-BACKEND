using kazariobranco_backend.Models;
using kazariobranco_backend.Request;

using AutoMapper;
using Microsoft.Extensions.Options;
using kazariobranco_backend.Response;

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
        CreateMap<UserModel, UserResponse>()
            .ForMember(
                p => p.Name,
                options => options.MapFrom(p => $"{p.Firstname} {p.Lastname}")
            );

        CreateMap<ContactRequest, ContactModel>();

        CreateMap<AddNewAddressRequest, AddressModel>();
    }
}
