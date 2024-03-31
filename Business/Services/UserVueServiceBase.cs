using Chameleon.Business.Dtos;
using Chameleon.Business.Mappers;

namespace Chameleon.Business.Services;

public class UserVueServiceBase(Context context) : IContext(context), IService<UserDto, Guid>
{
    private readonly UserMapper _userMapper = new();
    private readonly ContactDetailsMapper _contactDetailsMapper = new();
    
    public UserDto CreateEntity1(UserDto dto)
    {
        throw new NotImplementedException();
    }

    public UserDto ReadEntity(Guid guid)
    {
        var user = Context.User.SingleOrDefault(u => u.Id.Equals(guid));
        if (user == null)
        {
            throw new KeyNotFoundException("Unable to find entity with this key");
        }

        var contactDetailsForUser = Context.UsersContactDetails
            .Where(uc => uc.UserId == user.Id)
            .Select(uc => uc.ContactDetails)
            .Select(c => _contactDetailsMapper.ToDto(c))
            .ToList();
        UserDto dto = _userMapper.ToDto(user);
        dto.ContactDetails = contactDetailsForUser;
        return dto;
    }

    public ICollection<UserDto> ReadAllEntity()
    {
        return Context.User.Select(u => _userMapper.ToDto(u)).ToList();
    }

    public UserDto UpdateEntity(UserDto dto, Guid guid)
    {
        throw new NotImplementedException();
    }

    public void DeleteEntity(Guid guid)
    {
        throw new NotImplementedException();
    }
}