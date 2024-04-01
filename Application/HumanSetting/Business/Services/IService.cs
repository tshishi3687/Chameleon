namespace Chameleon.Business.Services;

public interface IService<Dto, Guid>
{
    Dto CreateEntity1(Dto dto);
    Dto ReadEntity(Guid guid);
    ICollection<Dto> ReadAllEntity();
    Dto UpdateEntity(Dto dto, Guid guid);
    void DeleteEntity(Guid guid);
}