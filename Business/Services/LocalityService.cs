using System.Data;
using System.Runtime;
using Chameleon.Business.Dtos;
using Chameleon.Business.Mappers;

namespace Chameleon.Business.Services;

public class LocalityService(Context context) : IContext(context), IService<LocalityDto, Guid>
{
    private readonly LocalityMapper _localityMappers = new();

    public LocalityDto CreateEntity(LocalityDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            throw new AmbiguousImplementationException("Dto name's can't be null!");
        }
        
        var locality = Context.Localities.SingleOrDefault(l => l.Name.ToUpper().Equals(dto.Name.ToUpper()));

        if (locality != null)
        {
            return _localityMappers.ToDto(locality);
        }

        var data = _localityMappers.toEntity(dto);
        Context.Localities.Add(data);

        var affectedRows = Context.SaveChanges();

        if (affectedRows != 1)
        {
            throw new DataException("An error occurred while saving in the DB");
        }

        return _localityMappers.ToDto(data);
    }

    public LocalityDto ReadEntity(Guid guid)
    {
        var locality = Context.Localities.SingleOrDefault(l => l.Id.Equals(guid));

        if (locality == null)
        {
            throw new KeyNotFoundException("Unable to find entity with this key");
        }

        return _localityMappers.ToDto(locality);
    }

    public ICollection<LocalityDto> ReadAllEntity()
    {
        return Context.Localities.Select(l => _localityMappers.ToDto(l)).ToList();
    }

    public LocalityDto updateEntity(LocalityDto dto, Guid guid)
    {
        var localityToRemove = Context.Localities.FirstOrDefault(l => l.Id.Equals(guid));

        if (localityToRemove == null)
        {
            throw new DllNotFoundException("Entity not found!");
        }

        Context.Localities.Remove(localityToRemove);
        Context.Localities.Add(_localityMappers.toEntity(dto));
        
        var i = Context.SaveChanges();
        if (i != 2)
        {
            throw new DataMisalignedException();
        }

        var localitySaved = Context.Localities.SingleOrDefault(l => l.Name.Equals(dto.Name.ToUpper()));
        if (localitySaved == null)
        {
            throw new DataException();
        }

        return _localityMappers.ToDto(localitySaved);
    }

    public void delete(Guid guid)
    {
        var entityToDelete = Context.Localities.SingleOrDefault(l => l.Id.Equals(guid));
        if (entityToDelete != null)
        {
            Context.Localities.Remove(entityToDelete);
            Context.SaveChanges();
        }
    }
}