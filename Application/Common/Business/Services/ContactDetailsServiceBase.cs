using System.Runtime;
using Chameleon.Application.Common.Business.Mappers;
using Chameleon.Application.Common.DataAccess.Entities;
using Chameleon.Application.HumanSetting.Business.Dtos;
using Chameleon.Application.HumanSetting.Business.Mappers;
using Chameleon.Application.HumanSetting.Business.Services;

namespace Chameleon.Application.Common.Business.Services;

public class ContactDetailsServiceBase(Context context) : IContext(context), IService<ContactDetailsDto, Guid>
{
    private readonly ContactDetailsMapper _contactDetailsMapper = new();
    private readonly LocalityMapper _localityMapper = new();
    private readonly CountryMapper _countryMapper = new();
    private readonly LocalityServiceBase _localityServiceBase = new(context);
    private readonly CountryServiceBase _countryServiceBase = new(context);

    public ContactDetailsDto CreateEntity(ContactDetailsDto dto)
    {
        CheckAddressAndNumber(dto);
        var contactDetails = CreateContactDetails(dto);

        Context.ContactDetails.Add(contactDetails);
        Context.SaveChanges();

        return _contactDetailsMapper.ToDto(Context.ContactDetails.OrderByDescending(cd => cd.CreatedAt).First());
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
        return CreateEntity(dto);
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
        var contactDetails = _contactDetailsMapper.ToEntity(dto);
        contactDetails.Locality = context.Localities.FirstOrDefault(l => l.Id.Equals(_localityServiceBase.CreateEntity(dto.Locality).Id));
        contactDetails.Country = context.Countries.FirstOrDefault(c => c.Id.Equals(_countryServiceBase.CreateEntity(dto.Country).Id));
        return contactDetails;
    }
}