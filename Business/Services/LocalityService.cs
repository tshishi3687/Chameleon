using System.Data;
using Chameleon.Business.Dtos;
using Chameleon.Business.Mappers;

namespace Chameleon.Business.Services;

public class LocalityService(Context context) : BaseContext(context), IService<LocalityDto, Guid>
{
    private readonly LocalityMapper _localityMappers = new();

    public LocalityDto CreateEntity(LocalityDto dto)
    {
        var locality = Context.Localities.SingleOrDefault(lo => lo.Name.ToUpper().Equals(dto.Name.ToUpper()));

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
        var locality = Context.Localities.SingleOrDefault(lo => lo.Id.Equals(guid));

        if (locality == null)
        {
            throw new KeyNotFoundException("Unable to find entity with this key");
        }

        return _localityMappers.ToDto(locality);
    }

    public ICollection<LocalityDto> ReadAllEntity()
    {
        return Context.Localities.Select(locality => _localityMappers.ToDto(locality)).ToList();
    }

    public LocalityDto updateEntity(LocalityDto dto, Guid guid)
    {
        var localityToRemove = Context.Localities.FirstOrDefault(p => p.Id.Equals(guid));

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

        var localitySaved = Context.Localities.SingleOrDefault(lo => lo.Name.Equals(dto.Name.ToUpper()));
        if (localitySaved == null)
        {
            throw new DataException();
        }

        return _localityMappers.ToDto(localitySaved);
    }

    public void delete(Guid guid)
    {
        var entityToDelete = Context.Localities.SingleOrDefault(lo => lo.Id.Equals(guid));
        if (entityToDelete != null)
        {
            Context.Localities.Remove(entityToDelete);
            Context.SaveChanges();
        }
    }
}