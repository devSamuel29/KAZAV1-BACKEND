using kazariobranco_backend.Models;
using kazariobranco_backend.Response;
using kazariobranco_backend.Request.Auth;
using kazariobranco_backend.Request.Contact;

using AutoMapper;
using kazariobranco_backend.Request.User;

namespace kazariobranco_backend.Mapping;

public class MyAutoMapper : Profile
{
    public MyAutoMapper()
    {
        CreateMap<RegisterRequest, LoginRequest>();
        CreateMap<ChangePasswordRequest, LoginRequest>()
            .ForMember(
                p => p.Password,
                options =>
                    options.MapFrom(p => BCrypt.Net.BCrypt.HashPassword(p.NewPassword))
            );

        CreateMap<RegisterRequest, UserModel>()
            .ForMember(
                p => p.Password,
                options =>
                    options.MapFrom(p => BCrypt.Net.BCrypt.HashPassword(p.Password))
            )
            .ForMember(
                p => p.Cpf,
                options => options.MapFrom(p => p.Cpf.Replace(".", "").Replace("-", ""))
            )
            .ForMember(
                p => p.Phone,
                options =>
                    options.MapFrom(
                        p =>
                            p.Phone
                                .Replace("(", "")
                                .Replace(")", "")
                                .Replace(" ", "")
                                .Replace("-", "")
                    )
            );

        CreateMap<UserModel, UserResponse>()
            .ForMember(
                p => p.Name,
                options => options.MapFrom(p => $"{p.Firstname} {p.Lastname}")
            );

        CreateMap<ContactRequest, ContactModel>();
        CreateMap<ContactModel, ContactResponse>()
            .ForMember(
                p => p.IsFinished,
                options => options.MapFrom(p => p.EndedAt != DateTime.MinValue)
            );

        CreateMap<CreteAddressRequest, AddressModel>();
        CreateMap<AddressModel, AddressResponse>();
    }
}
