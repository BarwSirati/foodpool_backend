using AutoMapper;
using FoodPool.stall.dtos;
using FoodPool.stall.entities;

namespace FoodPool.stall.mapper;

public class StallMapper : Profile
{
    public StallMapper()
    {
        CreateMap<Stall, GetStallDto>();
        CreateMap<CreateStallDto, Stall>();
        CreateMap<UpdateStallDto, Stall>();
    }
}