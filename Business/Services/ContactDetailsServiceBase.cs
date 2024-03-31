using System.Runtime;
using Chameleon.Business.Dtos;
using Chameleon.Business.Mappers;
using Chameleon.DataAccess.Entity;

namespace Chameleon.Business.Services;

public class ContactDetailsServiceBase(Context context) : IContext(context), IService<ContactDetailsDto, Guid>
{
    private readonly ContactDetailsMapper _contactDetailsMapper = new();
    private readonly LocalityMapper _localityMapper = new();
    private readonly CountryMapper _countryMapper = new();
    private readonly LocalityServiceBase _localityServiceBase = new(context);
    private readonly CountryServiceBase _countryServiceBase = new(context);

    public ContactDetailsDto CreateEntity1(ContactDetailsDto dto)
    {
        CheckAddressAndNumber(dto);
        var contactDetails = CreateContactDetails(dto);

        Context.ContactDetails.Add(contactDetails);
        Context.SaveChanges();

        return _contactDetailsMapper.ToDto(Context.ContactDetails.Last());
    }

    public ContactDetailsDto ReadEntity(Guid guid)
    {
        var contactDetails = Context.ContactDetails.SingleOrDefault(cd => cd.Id.Equals(guid));
        if (contactDetails == null)
        {
            throw new KeyNotFoundException("Unable to find entity with this key");
        }

        return _contactDetailsMapper.ToDto(contactDetails);
    }

    public ICollection<ContactDetailsDto> ReadAllEntity()
    {
        return Context.ContactDetails.Select(cd => _contactDetailsMapper.ToDto(cd)).ToList();
    }

    public ContactDetailsDto UpdateEntity(ContactDetailsDto dto, Guid guid)
    {
        var contactDetails = Context.ContactDetails.SingleOrDefault(cd => cd.Id.Equals(guid));
        if (contactDetails == null)
        {
            throw new KeyNotFoundException("Entity not found");
        }

        Context.ContactDetails.Remove(contactDetails);
        Context.SaveChanges();
        return CreateEntity1(dto);
    }

    public void DeleteEntity(Guid guid)
    {
        var contactDetails = Context.ContactDetails.SingleOrDefault(cd => cd.Id.Equals(guid));

        if (contactDetails != null)
        {
            Context.ContactDetails.Remove(contactDetails);
            Context.SaveChanges();
        }
    }

    private void CheckAddressAndNumber(ContactDetailsDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Address))
        {
            throw new AmbiguousImplementationException("Dto address's can't be null!");
        }

        if (string.IsNullOrWhiteSpace(dto.Number))
        {
            throw new AmbiguousImplementationException("Dto address's can't be null!");
        }
    }

    private ContactDetails CreateContactDetails(ContactDetailsDto dto)
    {
        var contactDetails = _contactDetailsMapper.toEntity(dto);
        contactDetails.Locality = _localityMapper.toEntity(_localityServiceBase.CreateEntity1(dto.Locality));
        contactDetails.Country = _countryMapper.toEntity(_countryServiceBase.CreateEntity1(dto.Country));
        return contactDetails;
    }
}