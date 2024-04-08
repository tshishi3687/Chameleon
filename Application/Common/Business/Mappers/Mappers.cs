namespace Chameleon.Application.HumanSetting.Business.Mappers;

public interface Mappers<Dto, Entity>
{
    Dto ToDto(Entity entity);
    Entity ToEntity(Dto dto);

    ICollection<Dto> toDtos(ICollection<Entity> entities);
}