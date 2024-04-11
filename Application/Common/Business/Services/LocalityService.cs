using System.Runtime;
using Chameleon.Application.HumanSetting.Business.Dtos;
using Chameleon.Application.HumanSetting.DataAccess.Entities;

namespace Chameleon.Application.Common.Business.Services;

public class LocalityService(Context context) : CheckServiceBase(context)
{
    public Locality AddOrCreateLocality(LocalityDto dto)
    {
        if (dto == null) throw new Exception(); //TODO

        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            throw new AmbiguousImplementationException("Dto name's can't be null!");
        }

        var locality = Context.Localities.FirstOrDefault(l => l.Name.ToUpper().Equals(dto.Name.ToUpper()));

        if (locality != null)
        {
            return locality;
        }

        return Context.Localities.Add(new Locality
        {
            Name = dto.Name
        }).Entity;
    }
}