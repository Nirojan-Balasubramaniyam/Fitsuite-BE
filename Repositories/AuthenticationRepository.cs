using GYMFeeManagement_System_BE.Database;
using GYMFeeManagement_System_BE.DTOs.Request;
using GYMFeeManagement_System_BE.Entities;
using GYMFeeManagement_System_BE.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace GYMFeeManagement_System_BE.Repositories
{
    public class AuthenticationRepository: IAunthenticationRepository
    { 

    private readonly GymDbContext _dBContext;

    public AuthenticationRepository(GymDbContext dBContext)
    {
        _dBContext = dBContext;
    }

    public async Task<Staff> AddUser(Staff user)
    {
        var userDetails = await _dBContext.Staffs.AddAsync(user);
        await _dBContext.SaveChangesAsync();
        return userDetails.Entity;
    }

    public async Task<Staff> GetUserByEmail(string email)
    {
        var user = await _dBContext.Staffs.SingleOrDefaultAsync(u => u.Email == email);
        return user!;
    }

    public async Task<Member> GetMemberByEmail(string email)
    {
        return await _dBContext.Members.SingleOrDefaultAsync(m => m.Email == email);
    }

    public async Task<Staff> Login(LoginRequestDTO request)
    {
        var user = await GetUserByEmail(request.Email);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        var isLogin = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
        if (isLogin)
        {
            return user;
        }
        else
        {
            throw new Exception("Invalid password");
        }
    }

    }

}
