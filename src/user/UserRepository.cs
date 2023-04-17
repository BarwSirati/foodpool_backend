using FoodPool.data;
using FoodPool.user.dtos;
using FoodPool.user.entities;
using FoodPool.user.interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodPool.user;

public class UserRepository : IUserRepository
{
    private readonly FoodpoolDbContext _dbContext;

    public UserRepository(FoodpoolDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<User>> GetAll()
    {
        var users = await _dbContext.User.ToListAsync();
        return users;
    }

    public async Task<User> GetById(int id)
    {
        var user = await _dbContext.User.FirstOrDefaultAsync(u => u.Id == id);
        return user!;
    }

    public Task<User> GetByUsername(string username)
    {
        throw new NotImplementedException();
    }

    public void Insert(User user)
    {
        throw new NotImplementedException();
    }

    public void Update(UpdateUserDto updateUserDto, int id)
    {
        var user = GetById(id).Result;
        user.Name = updateUserDto.Name;
        user.Lastname = updateUserDto.Lastname;
        user.Username = updateUserDto.Username;
        user.Password = updateUserDto.Password;
        user.Line = updateUserDto.Line;
        user.Tel = updateUserDto.Tel;
    }

    public void Delete(int id)
    {
        var user = GetById(id).Result;
        _dbContext.User.Remove(user);
    }

    public bool Exist(string username)
    {
        return _dbContext.User.Any(u => u.Username == username);
    }

    public bool ExistById(int id)
    {
        return _dbContext.User.Any(u => u.Id == id);
    }

    public void Save()
    {
        _dbContext.SaveChanges();
    }
}