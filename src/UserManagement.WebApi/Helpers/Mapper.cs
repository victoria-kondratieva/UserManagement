using AutoMapper;
using UserManagement.Domain.Entities;
using UserManagement.Domain.DTOs.User;

namespace UserManagement.WebApi.Helpers;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<UserRequest, User>();
    }
}