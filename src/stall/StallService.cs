using AutoMapper;
using FluentResults;
using FoodPool.stall.dtos;
using FoodPool.stall.entities;
using FoodPool.stall.interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FoodPool.stall;

public class StallService : IStallService
{
    private readonly IStallRepository _stallRepository;
    private readonly IMapper _mapper;
    public StallService(IStallRepository stallRepository, IMapper mapper)
    {
        _stallRepository = stallRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<GetStallDto>>> GetAll()
    {
        var stalls = await _stallRepository.GetAll();
        return Result.Ok(stalls.Select(stall => _mapper.Map<GetStallDto>(stall)).ToList());
    }

    public async Task<Result<GetStallDto>> GetById(int id)
    {
        var stall = await _stallRepository.GetById(id);
        return Result.Ok(_mapper.Map<GetStallDto>(stall));
    }

    public async Task<Result> Create(CreateStallDto createStallDto)
    {
        try
        {
            var stall = _mapper.Map<Stall>(createStallDto);
            _stallRepository.Insert(stall);
            _stallRepository.Save();
            return Result.Ok();
        }
        catch (Exception)
        {
            return Result.Fail(new Error("400"));
        }
    }

    public Task<Result<GetStallDto>> Update(UpdateStallDto updateStallDto, int id)
    {
        throw new NotImplementedException();
    }

    public Task<Result> Delete(int id)
    {
        throw new NotImplementedException();
    }
}