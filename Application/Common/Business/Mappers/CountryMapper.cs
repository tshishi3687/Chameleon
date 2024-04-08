using Chameleon.Application.Common.DataAccess.Entities;
using Chameleon.Application.HumanSetting.Business.Dtos;
using Chameleon.Application.HumanSetting.Business.Mappers;
using Chameleon.Application.HumanSetting.DataAccess.Entities;

namespace Chameleon.Application.Common.Business.Mappers;

public class CountryMapper: Mappers<CountryDto, Country>
{
    public CountryDto ToDto(Country entity)
    {
        return new CountryDto
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }

    public Country ToEntity(CountryDto dto)
    {
        return new Country
        {
            Name = dto.Name.ToUpper(),
            ContactDetailsList = new List<ContactDetails>()
        };
    }

    public ICollection<CountryDto> toDtos(ICollection<Country> entities)
    {
        return entities.Select(entity => ToDto(entity)).ToList();
    }
}