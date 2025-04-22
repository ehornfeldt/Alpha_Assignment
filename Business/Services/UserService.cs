using Business.Models;
using Data.Entities;
using Data.Repositories;
using Domain.Extensions;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
//using Microsoft.AspNetCore.Identity;

namespace Business.Services;

public interface IUserService
{
    Task<UserResult> GetUsersAsync();
    Task<UserResult> CreateUserAsync(SignUpFormData formData);
}

public class UserService(IUserRepository userRepository, UserManager<UserEntity> userManager) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly UserManager<UserEntity> _userManager = userManager;
    //private readonly RoleManager<IdentityRole> _roleManager;    //2.20 i tips och trick backend men detta är VG

    public async Task<UserResult> GetUsersAsync()
    {
        var result = await _userRepository.GetAllAsync();
        return result.MapTo<UserResult>();
    }

    public async Task<UserResult> CreateUserAsync(SignUpFormData formData)
    {
        if (formData == null)
        {
            return new UserResult { Succeeded = false, StatusCode = 400, Error = "Not all required field are supplied." };
        }
        var existsResult = await _userRepository.ExistsAsync(x => x.Email == formData.Email);
        if (existsResult.Succeeded)
        {
            return new UserResult { Succeeded = false, StatusCode = 409, Error = "User with same email already exists." };
        }
        try
        {
            var userEntity = formData.MapTo<UserEntity>();
            var result = await _userManager.CreateAsync(userEntity, formData.Password);  //3.06.58 i filmen

            return result.Succeeded
                ? new UserResult { Succeeded = true, StatusCode = 201 }
                : new UserResult { Succeeded = false, StatusCode = 500, Error = "Unable to create user" };
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new UserResult { Succeeded = false, StatusCode = 500, Error = ex.Message };
        }
    }
}
