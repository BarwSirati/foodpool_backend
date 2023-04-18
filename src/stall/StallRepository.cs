using FoodPool.data;
using FoodPool.stall.dtos;
using FoodPool.stall.entities;
using FoodPool.stall.interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodPool.stall;

public class StallRepository : IStallRepository
{
    private readonly FoodpoolDbContext _dbContext;

    public StallRepository(FoodpoolDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Delete(int id)
    {
        var stall = GetById(id).Result;
        _dbContext.Stall.Remove(stall);
    }

    public async Task<List<Stall>> GetAll()
    {
        var stalls = await _dbContext.Stall.ToListAsync();
        return stalls;
    }

    public async Task<Stall> GetById(int id)
    {
        var stall = await _dbContext.Stall.FirstOrDefaultAsync(s => s.Id == id);
        return stall!;
    }

    public void Insert(Stall stall)
    {
        _dbContext.Stall.AddAsync(stall);
    }

    public void Save()
    {
        _dbContext.SaveChanges();
    }

    public void Update(UpdateStallDto updateStallDto, int id)
    {
        var stall = GetById(id).Result;
        stall.Name = updateStallDto.Name;
    }
}
