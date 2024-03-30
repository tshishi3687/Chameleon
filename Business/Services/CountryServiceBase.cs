using System.Runtime;
using Chameleon.Business.Dtos;
using Chameleon.Business.Mappers;

namespace Chameleon.Business.Services;

public class CountryServiceBase(Context context) : IContext(context), IService<CountryDto, Guid>
{
    private readonly CountryMapper _countryMapper = new();

    public CountryDto CreateEntity(CountryDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            throw new AmbiguousImplementationException("Dto name's can't be null!");
        }

        var country = Context.Countries.FirstOrDefault(l => l.Name.ToUpper().Equals(dto.Name.ToUpper()));

        if (country != null)
        {
            return _countryMapper.ToDto(country);
        }

        var data = _countryMapper.toEntity(dto);
        Context.Countries.Add(data);
        Context.SaveChanges();

        return _countryMapper.ToDto(Context.Countries.Last());
    }

    public CountryDto ReadEntity(Guid guid)
    {
        var country = Context.Countries.SingleOrDefault(l => l.Id.Equals(guid));

        if (country == null)
        {
            throw new KeyNotFoundException("Unable to find entity with this key");
        }

        return _countryMapper.ToDto(country);
    }

    public ICollection<CountryDto> ReadAllEntity()
    {
        return Context.Countries.Select(c => _countryMapper.ToDto(c)).ToList();
    }

    public CountryDto UpdateEntity(CountryDto dto, Guid guid)
    {
        var countryToRemove = Context.Countries.FirstOrDefault(c => c.Id.Equals(guid));

        if (countryToRemove == null)
        {
            throw new DllNotFoundException("Entity not found!");
        }

        Context.Countries.Remove(countryToRemove);
        Context.SaveChanges();
        return CreateEntity(dto);
    }

    public void DeleteEntity(Guid guid)
    {
        var entityToDelete = Context.Countries.SingleOrDefault(c => c.Id.Equals(guid));
        if (entityToDelete != null)
        {
            Context.Countries.Remove(entityToDelete);
            Context.SaveChanges();
        }
    }
}