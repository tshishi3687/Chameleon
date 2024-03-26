using Chameleon.Business.Dtos;
using Chameleon.DataAccess.Entity;

namespace Chameleon.Business.Mappers;

public class UserMapper : Mappers<UserDto, User>
{
    private Context Context = new Context();
    private RoleMapper RoleMapper = new RoleMapper();
    
    public UserDto ToDto(User entity)
    {
        List<UsersRoles> ListUR = Context.UsersRoles.Where(pr => pr.UserId == entity.Id).ToList();
        List<Roles> ListR = Context.Roles.Where(r => ListUR.Any(ur => ur.RoleId == r.Id)).ToList();

        return new UserDto
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            BursDateTime = entity.BursDateTime,
            Roles = ListR.Select(r => RoleMapper.ToDto(r)).ToList()
        };
    }

    public User toEntity(UserDto dto)
    {
        throw new NotImplementedException();
    }

    public ICollection<UserDto> toDtos(ICollection<User> entities)
    {
        return entities.Select(entity => ToDto(entity)).ToList();
    }
}