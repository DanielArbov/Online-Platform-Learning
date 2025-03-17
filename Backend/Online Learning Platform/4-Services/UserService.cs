using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Talent;


public class UserService:IDisposable
{
    private readonly LearningPlatformContext _db;
    private readonly IMapper _mapper;


    public UserService(LearningPlatformContext db ,IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    // Registers a new user, hashes their password, saves them to the database, and returns a JWT token
    public string Register(User user)
    {
        user.Email = user.Email.ToLower();
        user.Password = Cyber.HashPassowrd(user.Password);
        _db.Users.Add(user);
        _db.SaveChanges();
        return JwtHelper.GetNewToken(user);
    }

    // Logs in a user by checking credentials and returns a JWT token if successful
    public string? LogIn(Credentials credentials)
    {

        credentials.Email = credentials.Email.ToLower();
        credentials.Password = Cyber.HashPassowrd(credentials.Password);
        User? user = _db.Users.AsNoTracking().SingleOrDefault(u => u.Email == credentials.Email && u.Password == credentials.Password);
        if (user == null) return null;
        return JwtHelper.GetNewToken(user);
    }

    // Checks if an email is already registered in the database
    public bool IsEmailTaken(string email)
    {
        return _db.Users.Any(u => u.Email == email);
    }



    // Retrieves all users and maps them to UserDto objects
    public List<UserDto> GetAllUsers()
    {
        return _db.Users.AsNoTracking().Select(u=>_mapper.Map<UserDto>(u)).ToList();
    }




    // Disposes of the database context to free resources
    public void Dispose()
    {
        _db.Dispose();
    }
}
