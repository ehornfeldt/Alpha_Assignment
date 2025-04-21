using Business.Models;
using Data.Entities;
using Data.Repositories;
using Domain.Extensions;
using Domain.Models;
//using Microsoft.AspNetCore.Identity;

namespace Business.Services;

public interface IUserService
{
    Task<UserResult> GetUsersAsync();
}

public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    //private readonly UserManager<UserEntity> _userManager;
    //private readonly RoleManager<IdentityRole> _roleManager;    //2.20 i tips och trick backend men detta är VG

    public async Task<UserResult> GetUsersAsync()
    {
        var result = await _userRepository.GetAllAsync();
        return result.MapTo<UserResult>();
    }

    //public async Task<UserResult> CreateUserAsync(SignUpFormData formData)
    //{
    //    if (formData == null)
    //    {
    //        return new UserResult { Succeeded = false, StatusCode = 400, Error = "Not all required field are supplied." };
    //    }
    //    var existsResult = await _userRepository.ExistsAsync(x => x.Email == formData.Email);
    //    if (existsResult.Succeeded)
    //    {
    //        return new UserResult { Succeeded = false, StatusCode = 409, Error = "User with same email already exists." };
    //    }
    //    try
    //    {
    //        var userEntity = formData.MapTo<UserEntity>();
    //        var result = await _user
    //    } 
    //    catch(Exception ex)
    //    {
    //        return new UserResult { Succeeded = false, StatusCode = 500, Error = ex.Message };
    //    }
}
