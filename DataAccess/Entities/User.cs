using System.Collections.ObjectModel;
using System.Text;

namespace Chameleon.DataAccess.Entity;

public class User
{
    public Guid Id { get; }
    public Guid ReferenceCode { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BursDateTime { get; set; }
    public string Email { get; set; }
    public string EmailEncoding { get;}
    public string Phone { get; set; }
    public string PassWord { get; set; }
    public Collection<Roles> Roles { get; set; }

    public User(string firstName, string lastName, DateTime bursDateTime, string email, string emailEncoding, string phone, string passWord)
    {
        Id = Guid.NewGuid();
        ReferenceCode = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        BursDateTime = bursDateTime;
        Email = email;
        EmailEncoding = emailEncoding;
        Phone = phone;
        PassWord = passWord;
        Roles = new Collection<Roles>();
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

}