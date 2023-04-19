using AutoMapper;
using FoodPool.post.dtos;
using FoodPool.post.entities;
namespace FoodPool.post.mapper;


public class PostMapper : Profile
{
    public PostMapper()
    {
        CreateMap<Post,GetPostDto>();
        CreateMap<GetPostDto,Post>();
        CreateMap<GetPostDto,Post>();
        
    }
    
}