using Chameleon.Business.Dtos;
using Chameleon.DataAccess.Entity;

namespace Chameleon.Business.Mappers;

public class UserMapper : Mappers<UserDto, User>
{
    
    public UserDto ToDto(User entity)
    {

        return new UserDto
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            BursDateTime = entity.BursDateTime,
            ContactDetails = new List<ContactDetailsDto>()
        };
    }

    public User toEntity(UserDto dto)
    {
        throw new NotImplementedException();
    }

    public ICollection<UserDto> toDtos(ICollection<User> entities)
    {
        return entities.Select(ToDto).ToList();
    }
}