using Chameleon.Application.CompanySetting.Business.Dtos;
using Chameleon.Application.CompanySetting.DataAccess.Entities;
using Chameleon.Application.HumanSetting.Business.Mappers;

namespace Chameleon.Application.CompanySetting.Business.Mappers;

public class CardMapper: Mappers<CardDto, Card>
{
    private readonly AbsentMapper _absentMapper = new();
    private readonly MemoryMapper _memoryMapper = new();
    private readonly TaskOrEventMapper _taskOrEventMapper = new();

    public CardDto ToDto(Card entity)
    {
        var cardDto = new CardDto
        {
            DateTime = entity.DateTime,
            IsEnd = entity.IsEnd!.Value,
            IsMade = entity.IsMadeIt!.Value
        };

        if (!entity.AbsentDetailsId.Equals(Guid.Empty))
        {
                cardDto.AbsentDetails = _absentMapper.ToDto(entity.AbsentDetails);
        }

        if (!entity.MemoryDetailsId.Equals(Guid.Empty))
        {
                cardDto.MemoryDetails = _memoryMapper.ToDto(entity.MemoryDetails);
        }

        if (!entity.TaskOrEventDetailsId.Equals(Guid.Empty))
        {
                cardDto.TaskOrEventDetails = _taskOrEventMapper.ToDto(entity.TaskOrEventDetails);
        }

        return cardDto;
    }

    public Card ToEntity(CardDto dto)
    {
        throw new NotImplementedException();
    }

    public ICollection<CardDto> ToDtos(ICollection<Card> entities)
    {
        throw new NotImplementedException();
    }
}