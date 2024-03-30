using Chameleon.Business.Dto;
using Chameleon.DataAccess.Entity;
using Chameleon.Securities;

namespace Chameleon.Business.Mappers;

public class CreationUserMapper: Mappers<CreationUserDto, User>
{
    private MdpCrypte Crypte = new MdpCrypte();

    public CreationUserDto ToDto(User entity)
    {
        return new CreationUserDto();
    }

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
            PassWord = Crypte.CryptMdp(dto.PassWord),
            Roles = new List<UsersRoles>(),
            ContactDetails = new List<UsersContactDetails>()
        };
    }

    public ICollection<CreationUserDto> toDtos(ICollection<User> entities)
    {
        return entities.Select(entity => ToDto(entity)).ToList();
    }
}