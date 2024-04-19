using System.Runtime;
using Chameleon.Application.HumanSetting.Business.Dtos;
using Chameleon.Application.HumanSetting.DataAccess.Entities;

namespace Chameleon.Application.Common.Business.Services;

public class CountryService(Context context) : CheckServiceBase(context)
{
    public Country AddOrCreateCountry(CountryDto dto)
    {
        if (dto == null) throw new Exception(); //TODO


        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            throw new AmbiguousImplementationException("Dto name's can't be null!");
        }

        var country = context.Countries.FirstOrDefault(l => l.Name.ToUpper().Equals(dto.Name.ToUpper()));

        if (country != null)
        {
            return country;
        }

        return context.Countries.Add(new Country
        {
            Name = dto.Name
        }).Entity;
    }
}