using System.Security.Claims;
using Chameleon.Application.HumanSetting.DataAccess.Entities;

namespace Chameleon.Application.Securities;

public interface IConstente
{
    User Connected { get; set; }
    string GenerateToken(List<Claim> claims);
    void UseThisUserConnected(string accessToken);
}