using Chameleon.Application.HumanSetting.Business.Dtos;
using Chameleon.Application.HumanSetting.Business.Mappers;

namespace Chameleon.Application.HumanSetting.Business.Services;

public class UserVueServiceBase(Context context) : IContext(context), IService<UserVueDto, Guid>
{
    private readonly UserMapper _userMapper = new();
    private readonly ContactDetailsMapper _contactDetailsMapper = new();
    
    public UserVueDto CreateEntity1(UserVueDto vueDto)
    {
        throw new NotImplementedException();
    }

    public UserVueDto ReadEntity(Guid guid)
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
        UserVueDto vueDto = _userMapper.ToDto(user);
        vueDto.ContactDetails = contactDetailsForUser;
        return vueDto;
    }

    public ICollection<UserVueDto> ReadAllEntity()
    {
        return Context.User.Select(u => _userMapper.ToDto(u)).ToList();
    }

    public UserVueDto UpdateEntity(UserVueDto vueDto, Guid guid)
    {
        throw new NotImplementedException();
    }

    public void DeleteEntity(Guid guid)
    {
        throw new NotImplementedException();
    }
}