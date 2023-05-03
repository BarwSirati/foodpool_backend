using FluentResults;
using FoodPool.stall.dtos;

namespace FoodPool.stall.interfaces;

public interface IStallService
{
    Task<Result<List<GetStallDto>>> GetAll();
    Task<Result<GetStallDto>> GetById(int id);
    Task<Result> Create(CreateStallDto createStallDto);
    Task<Result<GetStallDto>> Update(UpdateStallDto updateStallDto, int id);
    Task<Result> Delete(int id);
}