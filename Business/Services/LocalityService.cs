using System.Data;
using Chameleon.Business.Dtos;
using Chameleon.Business.Mappers;
using Chameleon.DataAccess.Entity;

namespace Chameleon.Business.Services;

public class LocalityService : IService<LocalityDto, Guid>
{
    public Context Context;
    public Mappers<LocalityDto, Locality> LocalityMappers = new LocalityMapper();

    public LocalityService(Context context)
    {
        Context = context;
    }

    public LocalityDto CreatEntity(LocalityDto dto)
    {
        Locality locality = Context.Localities.SingleOrDefault(lo => lo.Name.ToUpper() == dto.Name.ToUpper());

        if (locality != null)
        {
            return LocalityMappers.ToDto(locality);
        }

        Locality data = LocalityMappers.toEntity(dto);
        Context.Localities.Add(data);

        int affectedRows = Context.SaveChanges();

        if (affectedRows == 1)
        {
            return LocalityMappers.ToDto(data);
        }

        throw new DataException("An error occurred while saving in the DB");
    }

    public LocalityDto ReadEntity(Guid guid)
    {
        Locality locality = Context.Localities.SingleOrDefault(lo => lo.Id == guid);

        if (locality != null)
        {
            return LocalityMappers.ToDto(locality);
        }

        throw new KeyNotFoundException("Unable to find entity with this key");
    }

    public ICollection<LocalityDto> ReadAllEntity()
    {
        return Context.Localities.Select(locality => LocalityMappers.ToDto(locality)).ToList();
    }

    public LocalityDto updateEntity(LocalityDto dto, Guid guid)
    {
        if (Context.Localities.SingleOrDefault(lo => lo.Id == guid) == null)
        {
            throw new DllNotFoundException("Entity not found!");
        }

        Context.Localities.Remove(Context.Localities.FirstOrDefault(lo => lo.Id.Equals(guid)));
        Context.Localities.Add(LocalityMappers.toEntity(dto));
        int i = Context.SaveChanges();

        if (i != 2)
        {
            throw new DataMisalignedException();
        }

        return LocalityMappers.ToDto(Context.Localities.FirstOrDefault(lo => lo.Name.Equals(dto)));
    }

    public void delete(Guid guid)
    {
        Context.Localities.Remove(Context.Localities.FirstOrDefault(lo => lo.Id.Equals(guid)));
    }
}