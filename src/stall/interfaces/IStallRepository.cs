using FoodPool.stall.dtos;
using FoodPool.stall.entities;

namespace FoodPool.stall.interfaces;

public interface IStallRepository
{
    Task<List<Stall>> GetAll();
    Task<Stall> GetById(int id);
    void Insert(Stall stall);
    void Update(UpdateStallDto updateStallDto, int id);
    void Delete(int id);
    void Save();
    bool ExistById(int id);
}
