using Chameleon.Application.CompanySetting.Business.Dtos;
using Chameleon.Application.CompanySetting.DataAccess.Entities;
using Chameleon.Application.HumanSetting.Business.Mappers;

namespace Chameleon.Application.CompanySetting.Business.Mappers;

public class CompanyEasyVueMapper: Mappers<CompanyEasyVueDto, Company>
{
    public CompanyEasyVueDto ToDto(Company entity)
    {
        return new CompanyEasyVueDto
        {
            Id = entity.Id,
            Name = entity.Name,
            BusinessNumber = entity.BusinessNumber,
            FileName = entity.FileName,
            ContentType = entity.ContentType,
            FileContent = entity.FileContent,
        };
    }

    public Company ToEntity(CompanyEasyVueDto dto)
    {
        throw new NotImplementedException();
    }

    public ICollection<CompanyEasyVueDto> ToDtos(ICollection<Company> entities)
    {
        return entities.Select(ToDto).ToList();
    }
}