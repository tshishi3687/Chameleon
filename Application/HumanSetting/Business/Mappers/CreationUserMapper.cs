using Chameleon.Application.HumanSetting.Business.Dtos;
using Chameleon.Application.HumanSetting.DataAccess.Entities;
using Chameleon.Application.Securities;

namespace Chameleon.Application.HumanSetting.Business.Mappers;

public class CreationUserMapper(Context context): Mappers<CreationUserDto, User>
{
    private readonly MdpCrypte _crypto = new();

    public CreationUserDto ToDto(User entity)
    {
        return new CreationUserDto();
    }

    [Obsolete("Obsolete")]
    public User ToEntity(CreationUserDto dto)
    {
        return new User(context)
        {
            ValidationCode = Guid.NewGuid(),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            BursDateTime = dto.BursDateTime,
            Email = dto.Email,
            Phone = dto.Phone,
            PassWord = _crypto.CryptMdp(dto.PassWord)
        };
    }

    public ICollection<CreationUserDto> ToDtos(ICollection<User> entities)
    {
        return entities.Select(entity => ToDto(entity)).ToList();
    }
}