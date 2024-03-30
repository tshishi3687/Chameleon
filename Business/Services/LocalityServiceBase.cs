using System.Data;
using System.Runtime;
using Chameleon.Business.Dtos;
using Chameleon.Business.Mappers;

namespace Chameleon.Business.Services;

public class LocalityServiceBase(Context context) : IContext(context), IService<LocalityDto, Guid>
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
        Context.SaveChanges();

        return _localityMappers.ToDto(Context.Localities.Last());
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

    public LocalityDto UpdateEntity(LocalityDto dto, Guid guid)
    {
        var localityToRemove = Context.Localities.FirstOrDefault(l => l.Id.Equals(guid));

        if (localityToRemove == null)
        {
            throw new KeyNotFoundException("Entity not found!");
        }

        Context.Localities.Remove(localityToRemove);
        Context.SaveChanges();
        return CreateEntity(dto);
    }

    public void DeleteEntity(Guid guid)
    {
        var entityToDelete = Context.Localities.SingleOrDefault(l => l.Id.Equals(guid));
        if (entityToDelete != null)
        {
            Context.Localities.Remove(entityToDelete);
            Context.SaveChanges();
        }
    }
}