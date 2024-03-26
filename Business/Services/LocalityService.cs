using System.Data;
using Chameleon.Business.Dtos;
using Chameleon.Business.Mappers;

namespace Chameleon.Business.Services;

public class LocalityService(Context context) : IService<LocalityDto, Guid>
{
    private readonly Context _context = context ?? throw new ArgumentNullException(nameof(context));
    private readonly LocalityMapper _localityMappers = new();

    public LocalityDto CreatEntity(LocalityDto dto)
    {
        var locality = _context.Localities.SingleOrDefault(lo => lo.Name.ToUpper().Equals(dto.Name.ToUpper()));

        if (locality != null)
        {
            return _localityMappers.ToDto(locality);
        }

        var data = _localityMappers.toEntity(dto);
        _context.Localities.Add(data);

        var affectedRows = _context.SaveChanges();

        if (affectedRows != 1)
        {
            throw new DataException("An error occurred while saving in the DB");
        }

        return _localityMappers.ToDto(data);
    }

    public LocalityDto ReadEntity(Guid guid)
    {
        var locality = _context.Localities.SingleOrDefault(lo => lo.Id.Equals(guid));

        if (locality == null)
        {
            throw new KeyNotFoundException("Unable to find entity with this key");
        }

        return _localityMappers.ToDto(locality);
    }

    public ICollection<LocalityDto> ReadAllEntity()
    {
        return _context.Localities.Select(locality => _localityMappers.ToDto(locality)).ToList();
    }

    public LocalityDto updateEntity(LocalityDto dto, Guid guid)
    {
        var localityToRemove = _context.Localities.FirstOrDefault(p => p.Id.Equals(guid));

        if (localityToRemove == null)
        {
            throw new DllNotFoundException("Entity not found!");
        }

        _context.Localities.Remove(localityToRemove);
        _context.Localities.Add(_localityMappers.toEntity(dto));
        
        var i = _context.SaveChanges();
        if (i != 2)
        {
            throw new DataMisalignedException();
        }

        var localitySaved = _context.Localities.SingleOrDefault(lo => lo.Name.Equals(dto));
        if (localitySaved == null)
        {
            throw new DataException();
        }

        return _localityMappers.ToDto(localitySaved);
    }

    public void delete(Guid guid)
    {
        var entityToDelete = _context.Localities.SingleOrDefault(lo => lo.Id.Equals(guid));
        if (entityToDelete != null)
        {
            _context.Localities.Remove(entityToDelete);
        }
    }
}