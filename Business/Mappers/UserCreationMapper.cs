using System.Text;
using Chameleon.Business.Dto;
using Chameleon.DataAccess.Entity;
using Chameleon.Securities;

namespace Chameleon.Business.Mappers;

public class UserCreationMapper: Mappers<UserCreationDto, User>
{
    private MdpCrypte Crypte;

    public UserCreationDto ToDto(User entity)
    {
        throw new NotImplementedException();
    }

    public User toEntity(UserCreationDto dto)
    {
        return new User
        {
            ReferenceCode = Guid.NewGuid(),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            BursDateTime = dto.BursDateTime,
            Email = Crypte.CryptMdp(dto.Email),
            Phone = Crypte.CryptMdp(dto.Phone),
            PassWord = Crypte.CryptMdp(dto.PassWord),
            Roles = null,
            ContactDetails = null
        };
    }

    public ICollection<UserCreationDto> toDtos(ICollection<User> entities)
    {
        return entities.Select(entity => ToDto(entity)).ToList();
    }
}