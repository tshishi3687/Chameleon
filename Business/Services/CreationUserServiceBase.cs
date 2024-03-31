using Chameleon.Business.Dto;
using Chameleon.Business.Dtos;
using Chameleon.Business.Mappers;
using Chameleon.DataAccess.Entity;

namespace Chameleon.Business.Services;

public class CreationUserServiceBase(Context context) : IContext(context),IService<CreationUserDto, Guid>
{
    private readonly UserMapper _userMapper = new();
    private readonly CreationUserMapper _creationUserMapper = new();
    private readonly ContactDetailsMapper _contactDetailsMapper = new();

    private readonly ContactDetailsServiceBase _contactDetailsServiceBase = new(context);

    public UserDto CreateEntity1(CreationUserDto dto)
    {
        PasswordMatch(dto);
        UniqueUser(dto);

        Context.User.Add(_creationUserMapper.toEntity(dto));
        Context.SaveChanges();
        var user = Context.User.FirstOrDefault(u => u.Email.Equals(dto.Email));
        if (user == null)
        {
            throw new Exception(); // TODO
        }

        var uc = new UsersContactDetails();
        uc.UserId = user.Id;
        foreach (var contactDetailsDto in dto.ContactDetails)
        {
            var contactDetails = _contactDetailsServiceBase.CreateEntity1(contactDetailsDto);
            uc.ContactDetailsId = contactDetails.Id;
        }

        Context.UsersContactDetails.Add(uc);

        Context.SaveChanges();
        
        var lastUserCreated = Context.User.Last();
        var userDto = _userMapper.ToDto(lastUserCreated);
        foreach (var usersContactDetails in lastUserCreated.ContactDetails)
        {
            Context.ContactDetails.FirstOrDefault(c => c.Id.Equals(usersContactDetails.ContactDetailsId));
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

    public UserDto UpdateEntity(CreationUserDto dto, Guid guid)
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