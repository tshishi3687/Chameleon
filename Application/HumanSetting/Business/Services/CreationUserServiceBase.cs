using Chameleon.Application.HumanSetting.Business.Dtos;
using Chameleon.Application.HumanSetting.Business.Mappers;
using Chameleon.Application.HumanSetting.DataAccess.Entities;

namespace Chameleon.Application.HumanSetting.Business.Services;

public class CreationUserServiceBase(Context context) : IContext(context), IService<CreationUserDto, Guid>
{
    private readonly UserVueMapper _userVueMapper = new();
    private readonly CreationUserMapper _creationUserMapper = new(context);
    private readonly ContactDetailsMapper _contactDetailsMapper = new();

    private readonly ContactDetailsServiceBase _contactDetailsServiceBase = new(context);

    [Obsolete("Obsolete")]
    public UserVueDto CreateEntity1(CreationUserDto dto)
    {
        PasswordMatch(dto);
        UniqueUser(dto);

        var userEntity = Context.User.Add(_creationUserMapper.toEntity(dto)).Entity;
        var uc = new UsersContactDetails();
        uc.UserId = userEntity.Id;

        foreach (var contactDetailsDto in dto.ContactDetails)
        {
            uc.ContactDetailsId = _contactDetailsServiceBase.CreateEntity1(contactDetailsDto).Id;
            Context.UsersContactDetails.Add(uc);
        }

        Context.SaveChanges();

        var userDto = _userVueMapper.ToDto(userEntity);
        foreach (var usersContactDetails in userEntity.UserContactDetails())
        {
            userDto.ContactDetails.Add(_contactDetailsMapper.ToDto(usersContactDetails.ContactDetails));
        }
        
        return userDto;
    }

    public CreationUserDto ReadEntity(Guid guid)
    {
        throw new NotImplementedException();
    }

    public ICollection<CreationUserDto> ReadAllEntity()
    {
        throw new NotImplementedException();
    }

    CreationUserDto IService<CreationUserDto, Guid>.UpdateEntity(CreationUserDto dto, Guid guid)
    {
        throw new NotImplementedException();
    }

    public void DeleteEntity(Guid guid)
    {
        throw new NotImplementedException();
    }

    CreationUserDto IService<CreationUserDto, Guid>.CreateEntity1(CreationUserDto dto)
    {
        throw new NotImplementedException();
    }

    public UserVueDto UpdateEntity(CreationUserDto dto, Guid guid)
    {
        var userRemove = Context.User.FirstOrDefault(u => u.Id.Equals(guid));
        if (userRemove == null)
        {
            throw new DllNotFoundException("Entity not found!");
        }

        Context.User.Remove(userRemove);
        Context.SaveChanges();
        return CreateEntity1(dto);
    }

    private void UniqueUser(CreationUserDto dto)
    {
        if (Context.User.Any(u => u.Email.Equals(dto.Email) || u.Phone.Equals(dto.Phone)))
        {
            throw new Exception($"There already exists a user with this email: {dto.Email} or this phone {dto.Phone}!");
        }
    }

    private static void PasswordMatch(CreationUserDto dto)
    {
        if (!dto.PassWord.Equals(dto.PassWordCheck))
        {
            throw new ArgumentException("Passwords do not match.");
        }
    }
}