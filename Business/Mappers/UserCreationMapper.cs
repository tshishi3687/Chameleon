using System.Text;
using Chameleon.Business.Dto;
using Chameleon.DataAccess.Entity;
using Chameleon.Securities;

namespace Chameleon.Business.Mappers;

public class UserCreationMapper: Mappers<UserCreationDto, User>
{
    private MdpCrypte Crypte;
    private Context Context;
    private RoleMapper RoleMapper;

    public UserCreationDto ToDto(User entity)
    {
        throw new NotImplementedException();
    }

    public User toEntity(UserCreationDto dto)
    {
        List<UsersRoles> ListUR = Context.UsersRoles
            .Where(ur => dto.Roles.Any(r => r.Id == ur.RoleId))
            .ToList();

        List<Roles> ListR = Context.Roles
            .Where(r => ListUR.Any(ur => ur.RoleId == r.Id))
            .ToList();

        ICollection<UsersRoles> userRoles = ListR.Select(role => new UsersRoles { RoleId = role.Id }).ToList();

        return new User
        {
            ReferenceCode = Guid.NewGuid(),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            BursDateTime = dto.BursDateTime,
            Email = Crypte.CryptMdp(dto.Email),
            Phone = Crypte.CryptMdp(dto.Phone),
            PassWord = Crypte.CryptMdp(dto.PassWord),
            Roles = userRoles
        };
    }

    
    private string EncodeEmail(string email)
    {
        StringBuilder newEmail = new StringBuilder(email.Length);

        for (int i = 0; i < email.Length; i++)
        {
            if (i == 0 || i == email.Length - 1 || email[i] == '@')
            {
                newEmail.Append(email[i]);
            }
            else
            {
                newEmail.Append("*");
            }
        }

        return newEmail.ToString();
    }

    private string EncodePhone(string phone)
    {
        StringBuilder newPhone = new StringBuilder(phone.Length);
        for (int i = 0; i < phone.Length; i++)
        {
            if (i > 3 || i < phone.Length - 2)
            {
                newPhone.Append("*");
            }
            else
            {
                newPhone.Append(phone[i]);
            }
        }
        return newPhone.ToString();
    }
}