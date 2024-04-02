using Chameleon.Application.HumanSetting.Business.Dtos;
using Chameleon.Application.HumanSetting.DataAccess.Entities;
using Chameleon.Application.Securities;

namespace Chameleon.Application.HumanSetting.Business.Mappers;

public class CreationUserMapper: Mappers<CreationUserDto, User>
{
    private readonly MdpCrypte _crypto = new();

    public CreationUserDto ToDto(User entity)
    {
        return new CreationUserDto();
    }

    [Obsolete("Obsolete")]
    public User toEntity(CreationUserDto dto)
    {
        return new User
        {
            ReferenceCode = Guid.NewGuid(),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            BursDateTime = dto.BursDateTime,
            Email = dto.Email,
            Phone = dto.Phone,
            PassWord = _crypto.CryptMdp(dto.PassWord),
            Roles = new List<UsersRoles>(),
            ContactDetails = new List<UsersContactDetails>()
        };
    }

    public ICollection<CreationUserDto> toDtos(ICollection<User> entities)
    {
        return entities.Select(entity => ToDto(entity)).ToList();
    }
}