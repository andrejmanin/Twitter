using Microsoft.EntityFrameworkCore;
using Database;
using Database.DTO;
using Database.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Server.Services.Interfaces;

namespace Server.Services;

public class UserService : IUserService
{
    private readonly TwitterDbContext _context;
    
    public UserService(TwitterDbContext context) => _context = context;

    private Task<UserDto> CreateUserDto(User user)
    {
        var postDtos = user.Posts
            .Select(p => new PostDto
            {
                Id = p.Id,
                UserNickname = p.User.Nickname,
                Title = p.Title,
                Content = p.Content,
                Created = p.Created,
                Likes = p.Likes,
                Dislikes = p.Likes,
                Comments = p.Comments,
            })
            .ToList();
        return Task.FromResult(new UserDto
        {
            Gmail = user.Gmail,
            Password = user.Password,
            Nickname = user.Nickname,
            Name = user.Name,
            Surname = user.Surname,
            PhoneNumber = user.PhoneNumber,
            Country = new CountryDto
            {
                Name = user.Country.Name, 
                Code = user.Country.PhoneCode
            },
            City = new CityDto
            {
                Name = user.City.Name
            },
            Status = user.Status,
            Description = user.Description,
            CreatedAt = user.Created,
            Posts = postDtos,
            Followers = user.Followers,
            Following = user.Following
        });
    }

    public async Task<List<UserDto>> GetUsersAsync()
    {
        List<User> users = await _context.Users
            .Include(u => u.Country)
            .Include(u => u.City)
            .Include(u => u.Posts)
            .Include(u => u.Followers)
            .Include(u => u.Following)
            .AsNoTracking()
            .ToListAsync();
        List<UserDto> userDtos = new List<UserDto>();
        for (int i = 0; i < users.Count; i++)
        {
            userDtos.Add(await CreateUserDto(users[i]));
        }
        return userDtos;
    }

    public async Task<UserDto?> GetUserAsync(Guid id)
    {
        User? user = await _context.Users
            .Include(u => u.Country)
            .Include(u => u.City)
            .Include(u => u.Posts)
            .Include(u => u.Followers)
            .Include(u => u.Following)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if(user is null) return null;
        return await CreateUserDto(user);
    }

    public async Task<UserDto?> GetUserAsync(string email)
    {
        User? user = await _context.Users
            .Include(u => u.Country)
            .Include(u => u.City)
            .Include(u => u.Posts)
            .Include(u => u.Followers)
            .Include(u => u.Following)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Gmail == email);
        
        if(user is null) return null;
        return await CreateUserDto(user);
    }

    public async Task<UserDto?> GetUserByNicknameAsync(string nickname)
    {
         User? user = await _context.Users
            .Include(u => u.Country)
            .Include(u => u.City)
            .Include(u => u.Posts)
            .Include(u => u.Followers)
            .Include(u => u.Following)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Nickname == nickname);
         
         if(user is null) return null;
         return await CreateUserDto(user);
    }

    public async Task<string> CreateUserAsync(CreateUserDto userDto)
    {
        if (await _context.Users.AnyAsync(u => u.Gmail == userDto.Gmail))
            return "Gmail is already taken"; 

        if (await _context.Users.AnyAsync(u => u.Nickname == userDto.Nickname))
            return "Nickname is already taken";
        
        var countryId = await _context.Countries
            .FirstOrDefaultAsync(c => c.Name == userDto.CountryName);
        if (countryId is null) return "Country is not valid";
        
        var cityId = await _context.Cities
            .FirstOrDefaultAsync(c => c.Name == userDto.CityName);
        if (cityId is null) return "City is not valid";
        
        User newUser = new User
        {
            Gmail = userDto.Gmail,
            Password = userDto.Password,
            Nickname = userDto.Nickname,
            CountryId = countryId.Id,
            CityId = cityId.Id,
            Created = DateTime.Today
        };
        
        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();
        return "User was created";
    }

    public async Task<string> UpdateUserAsync(UpdateUserDto userDto)
    {
        var usr = await _context.Users.FirstOrDefaultAsync(u => u.Gmail == userDto.Gmail);
        if (usr == null) return "User with this email not found";
        
        var country = await _context.Countries.FirstOrDefaultAsync(c => c.Name == userDto.Country);
        if (country == null) return "Country not found";

        var city = await _context.Cities.FirstOrDefaultAsync(c => c.Name == userDto.City);
        if (city == null) return "City not found";

        usr.Password     = userDto.Password;
        usr.Nickname     = userDto.Nickname;
        usr.Name         = userDto.Name;
        usr.Surname      = userDto.Surname;
        usr.PhoneNumber  = userDto.PhoneNumber;
        usr.Status       = userDto.Status;
        usr.Description  = userDto.Description;
        usr.CountryId    = country.Id;
        usr.CityId       = city.Id;

        await _context.SaveChangesAsync();
        return "User was updated";
    }

    public async Task<bool> DeleteUserAsync(Guid id)
    {
        User? user = await _context.Users
            .Include(u => u.Posts)
            .Include(u => u.Followers)
            .Include(u => u.Following)
            .FirstOrDefaultAsync(u => u.Id == id);
        
        if(user is null) return false;
        return await DeleteUserAsync(user);
    }

    public async Task<bool> DeleteUserAsync(string email)
    {
        User? user = await _context.Users
            .Include(u => u.Posts)
            .Include(u => u.Followers)
            .Include(u => u.Following)
            .FirstOrDefaultAsync(u => u.Gmail == email);
        
        if(user is null) return false;
        return await DeleteUserAsync(user);
    }

    private async Task<bool> DeleteUserAsync(User user)
    {
        var posts = await _context.Posts
            .Where(p => p.UserId == user.Id)
            .ToListAsync();
        
        if (posts.Any())
        {
            _context.Posts.RemoveRange(posts);
        }

        if (user.Followers.Any())
        {
            _context.Followers.RemoveRange(user.Followers);
        }

        if (user.Following.Any())
        {
            _context.Followers.RemoveRange(user.Following);
        }
        
        var comments = await _context.Comments.Where(u => u.UserId == user.Id).ToListAsync();
        if (comments.Any())
        {
            _context.Comments.RemoveRange(comments);
        }
        
        _context.Users.Remove(user);
        
        await _context.SaveChangesAsync();
        return true;
    }
}