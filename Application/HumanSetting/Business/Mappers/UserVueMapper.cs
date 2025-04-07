using Chameleon.Application.Common.Business.Mappers;
using Chameleon.Application.CompanySetting.Business.Mappers;
using Chameleon.Application.HumanSetting.Business.Dtos;
using Chameleon.Application.HumanSetting.DataAccess.Entities;

namespace Chameleon.Application.HumanSetting.Business.Mappers;

public class UserVueMapper : Mappers<UserVueDto, Users>
{
    
    public UserVueDto ToDto(Users entity)
    {
        return null;
    }

    public Users ToEntity(UserVueDto vueDto)
    {
        throw new NotImplementedException();
    }

    public ICollection<UserVueDto> ToDtos(ICollection<Users> entities)
    {
        return entities.Select(ToDto).ToList();
    }
}