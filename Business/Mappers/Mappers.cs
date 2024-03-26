namespace Chameleon.Business.Mappers;

public interface Mappers<Dto, Entity>
{
    Dto ToDto(Entity entity);
    Entity toEntity(Dto dto);

    ICollection<Dto> toDtos(ICollection<Entity> entities);
}