using System.Net;
using System.Security.Claims;
using Chameleon.Application.HumanSetting.Business.Dtos;
using Chameleon.Application.HumanSetting.Business.Mappers;
using Chameleon.Application.Securities;

namespace Chameleon.Application.HumanSetting.Business.Services;

public class UserVueServiceBase(Context context) : IContext(context), IService<UserVueDto, Guid>
{
    private readonly UserVueMapper _userVueMapper = new();
    private readonly ContactDetailsMapper _contactDetailsMapper = new();
    private readonly Constantes _constantes = new(context);
    
    public UserVueDto CreateEntity(UserVueDto vueDto)
    {
        throw new NotImplementedException();
    }

    public HttpResponseMessage Login(LoggerDto dto)
    {
        if (CheckLogin(dto).StatusCode == HttpStatusCode.BadRequest)
        {
            return CheckLogger(dto);
        }
        
        List<Claim> claims = [new Claim(ClaimTypes.Email, dto.Identification)];

        var response = new HttpResponseMessage(HttpStatusCode.Accepted);
        response.Headers.Add("Authorization", "Bearer " + _constantes.GenerateToken(claims));
        
        return response;
    }

    private HttpResponseMessage CheckLogin(LoggerDto dto)
    {
        if (CheckLogger(dto).StatusCode == HttpStatusCode.BadRequest)
        {
            return CheckLogger(dto);
        }
        
        if (CheckAuthentication(dto).StatusCode == HttpStatusCode.BadRequest)
        {
            new CancellationTokenSource().CancelAfter(TimeSpan.FromSeconds(5));
            return CheckAuthentication(dto);
        }

        if (CheckLogger(dto).StatusCode == HttpStatusCode.BadRequest)
        {
            return CheckLogger(dto);
        }
        return new HttpResponseMessage(HttpStatusCode.Accepted);
    }

    private HttpResponseMessage CheckAuthentication(LoggerDto dto)
    {
        var user = Context.User.SingleOrDefault(u => u.Email.Equals(dto.Identification) || u.Phone.Equals(dto.Identification));
        if (user == null)
        {
            return new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent("User or Password not match!")
            };
        }
                           
        if (new MdpCrypte().Compart(user.PassWord, dto.Password))
        {
            return new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent("User or Password not match!")
            };
        }
        return new HttpResponseMessage(HttpStatusCode.Accepted);
    }

    private HttpResponseMessage CheckLogger(LoggerDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Identification))
        {
            return new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent("Identification can't be null!")
            };
        }
        
        if (string.IsNullOrWhiteSpace(dto.Password))
        {
            return new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent("Password can't be null!")
            };
        }
        
        return new HttpResponseMessage(HttpStatusCode.Accepted);
    }
    
    public UserVueDto ReadEntity(Guid guid)
    {
        var user = Context.User.SingleOrDefault(u => u.Id.Equals(guid));
        if (user == null)
        {
            throw new KeyNotFoundException("Unable to find entity with this key");
        }

        var contactDetailsForUser = Context.UsersContactDetails
            .Where(uc => uc.UserId == user.Id)
            .Select(uc => uc.ContactDetails)
            .Select(c => _contactDetailsMapper.ToDto(c))
            .ToList();
        UserVueDto vueDto = _userVueMapper.ToDto(user);
        vueDto.ContactDetails = contactDetailsForUser;
        return vueDto;
    }

    public ICollection<UserVueDto> ReadAllEntity()
    {
        return Context.User.Select(u => _userVueMapper.ToDto(u)).ToList();
    }

    public UserVueDto UpdateEntity(UserVueDto vueDto, Guid guid)
    {
        throw new NotImplementedException();
    }

    public void DeleteEntity(Guid guid)
    {
        throw new NotImplementedException();
    }
}