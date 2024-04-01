using Chameleon.Application.Common.DataAccess.Entities;
using Chameleon.Application.HumanSetting.Business.Dtos;
using Chameleon.Application.HumanSetting.DataAccess.Entities;

namespace Chameleon.Application.HumanSetting.Business.Mappers;

public class ContactDetailsMapper: Mappers<ContactDetailsDto, ContactDetails>
{

    public Mappers<LocalityDto, Locality> LocalityMappers = new LocalityMapper();
    public Mappers<CountryDto, Country> CountryMappers = new CountryMapper();
    
    public ContactDetailsDto ToDto(ContactDetails entity)
    {
        return new ContactDetailsDto
        {
            Id = entity.Id,
            Address = entity.Address,
            Number = entity.Number,
            Locality = LocalityMappers.ToDto(entity.Locality),
            Country = CountryMappers.ToDto(entity.Country)
        };
    }

    public ContactDetails toEntity(ContactDetailsDto dto)
    {
        return new ContactDetails
        {
            Id = dto.Id,
            Address = dto.Address,
            Number = dto.Number,
        };
    }

    public ICollection<ContactDetailsDto> toDtos(ICollection<ContactDetails> entities)
    {
        return entities.Select(entity => ToDto(entity)).ToList();
    }
}