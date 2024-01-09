using System.Text;
using Chameleon.Business.Dto;
using Chameleon.DataAccess.Entity;
using Chameleon.Securities;

namespace Chameleon.Business.Mappers;

public class UserMapper: Mappers<UserCreationDto, User>
{
    private MdpCrypte _crypte = new MdpCrypte();


    public UserCreationDto ToDto(User entity)
    {
        throw new NotImplementedException();
    }

    public User toEntity(UserCreationDto dto)
    {
        return new User(
            dto.FirstName,
            dto.LastName,
            dto.BursDateTime,
            _crypte.CryptMdp(dto.Email),
            EncodeEmail(dto.Email),
            EncodePhone(dto.Phone),
            _crypte.CryptMdp(dto.Phone), 
            _crypte.CryptMdp(dto.PassWord)
        );
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