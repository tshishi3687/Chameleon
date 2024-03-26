using Chameleon.Business.Dtos;
using Chameleon.DataAccess.Entity;

namespace Chameleon.Business.Mappers;

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
            Address = dto.Address,
            Number = dto.Number,
            Locality = null,
            Country = null
        };
    }

    public ICollection<ContactDetailsDto> toDtos(ICollection<ContactDetails> entities)
    {
        return entities.Select(entity => ToDto(entity)).ToList();
    }
}