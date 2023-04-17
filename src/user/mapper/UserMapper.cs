using AutoMapper;
using FoodPool.user.dtos;
using FoodPool.user.entities;

namespace FoodPool.user.mapper;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<User, GetUserDto>();
        CreateMap<CreateUserDto, User>();
        CreateMap<UpdateUserDto, User>();
    }
}