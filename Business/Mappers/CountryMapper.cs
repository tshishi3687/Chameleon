using Chameleon.Business.Dtos;
using Chameleon.DataAccess.Entity;

namespace Chameleon.Business.Mappers;

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

    public Country toEntity(CountryDto dto)
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