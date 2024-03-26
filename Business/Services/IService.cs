namespace Chameleon.Business.Services;

public interface IService<Dto, Guid>
{
    Dto CreatEntity(Dto dto);
    Dto ReadEntity(Guid guid);
    ICollection<Dto> ReadAllEntity();
    Dto updateEntity(Dto dto, Guid guid);
    void delete(Guid guid);
}