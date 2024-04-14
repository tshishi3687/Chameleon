using Chameleon.Application.Common.DataAccess.Entities;
using Chameleon.Application.HumanSetting.Business.Dtos;
using Chameleon.Application.HumanSetting.Business.Mappers;

namespace Chameleon.Application.Common.Business.Mappers;

public class ContactDetailsMapper: Mappers<ContactDetailsDto, ContactDetails>
{
    private readonly LocalityMapper _localityMappers = new();
    private readonly CountryMapper _countryMappers = new();
    
    public ContactDetailsDto ToDto(ContactDetails entity)
    {
        if (entity == null) return null;
        
        return new ContactDetailsDto
        {
            Id = entity.Id,
            Address = entity.Address,
            Number = entity.Number,
            Locality = _localityMappers.ToDto(entity.Locality),
            Country = _countryMappers.ToDto(entity.Country)
        };
    }

    public ContactDetails ToEntity(ContactDetailsDto dto)
    {
        return new ContactDetails
        {
            Id = dto.Id,
            Address = dto.Address,
            Number = dto.Number,
        };
    }

    public ICollection<ContactDetailsDto> ToDtos(ICollection<ContactDetails> entities)
    {
        return entities.Select(entity => ToDto(entity)).ToList();
    }
}