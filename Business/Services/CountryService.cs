using System.Data;
using System.Runtime;
using Chameleon.Business.Dtos;
using Chameleon.Business.Mappers;

namespace Chameleon.Business.Services;

public class CountryService(Context context) : IContext(context), IService<CountryDto, Guid>
{

    private readonly CountryMapper _countryMapper = new();
    
    public CountryDto CreateEntity(CountryDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            throw new AmbiguousImplementationException("Dto name's can't be null!");
        }
        
        var country = Context.Countries.SingleOrDefault(l => l.Name.ToUpper().Equals(dto.Name.ToUpper()));

        if (country != null)
        {
            return _countryMapper.ToDto(country);
        }

        var data = _countryMapper.toEntity(dto);
        Context.Countries.Add(data);

        var affectedRows = Context.SaveChanges();

        if (affectedRows != 1)
        {
            throw new DataException("An error occurred while saving in the DB");
        }

        return _countryMapper.ToDto(data);
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

    public CountryDto updateEntity(CountryDto dto, Guid guid)
    {
        var countryToRemove = Context.Countries.FirstOrDefault(c => c.Id.Equals(guid));

        if (countryToRemove == null)
        {
            throw new DllNotFoundException("Entity not found!");
        }

        Context.Countries.Remove(countryToRemove);
        Context.Countries.Add(_countryMapper.toEntity(dto));
        
        var i = Context.SaveChanges();
        if (i != 2)
        {
            throw new DataMisalignedException();
        }

        var countrySaved = Context.Countries.SingleOrDefault(c => c.Name.Equals(dto.Name.ToUpper()));
        if (countrySaved == null)
        {
            throw new DataException();
        }

        return _countryMapper.ToDto(countrySaved);
    }

    public void delete(Guid guid)
    {
        var entityToDelete = Context.Countries.SingleOrDefault(c => c.Id.Equals(guid));
        if (entityToDelete != null)
        {
            Context.Countries.Remove(entityToDelete);
            Context.SaveChanges();
        }
    }
}