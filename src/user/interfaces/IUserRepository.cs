using FoodPool.user.dtos;
using FoodPool.user.entities;

namespace FoodPool.user.interfaces;

public interface IUserRepository
{
    Task<List<User>> GetAll();
    Task<User> GetById(int id);
    Task<User> GetByUsername(string username);
    void Insert(User user);

    void AddPoint(int userId);

    void RemovePoint(int userId);
    void Update(UpdateUserDto updateUserDto, int id);
    void Delete(int id);
    bool Exist(string username);
    bool ExistById(int id);
    void Save();
}