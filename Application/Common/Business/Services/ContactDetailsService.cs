using Chameleon.Application.Common.DataAccess.Entities;
using Chameleon.Application.HumanSetting.Business.Dtos;

namespace Chameleon.Application.Common.Business.Services;

public class ContactDetailsService(Context context) : CheckServiceBase(context)
{
    private readonly CountryService _countryService = new(context);
    private readonly LocalityService _localityService = new(context);

    public ContactDetails CreateEntity(ContactDetailsDto dto)
    {
        if (dto == null) throw new Exception(); //TODO

        CheckAddressAndNumber(dto);
        return context.ContactDetails.Add(new ContactDetails
        {
            Address = dto.Address,
            Number = dto.Number,
            Locality = _localityService.AddOrCreateLocality(dto.Locality),
            Country = _countryService.AddOrCreateCountry(dto.Country)
        }).Entity;
    }
}