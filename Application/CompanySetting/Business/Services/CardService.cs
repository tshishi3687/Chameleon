using Chameleon.Application.CompanySetting.Business.Dtos;
using Chameleon.Application.CompanySetting.DataAccess.Entities;

namespace Chameleon.Application.CompanySetting.Business.Services;

public class CardService(Context context)
{
    private readonly AbsentService _absentService = new(context);
    private readonly MemoryService _memoryService = new(context);
    private readonly TaskOrEventService _taskOrEventService = new(context);

    public Card CreateEntity(Company company, CardDto dto)
    {
        CheckForm(dto);
        var card = context.Cards.Add(new Card
        {
            CompanyIGuid = company.Id,
            DateTime = dto.DateTime,
            IsEnd = false,
            IsMadeIt = false
        }).Entity;
        
        CardImplementations(card, dto);

        context.CompanyCards.Add(new CompanyCard
        {
            Company = company,
            CompanyGuid = company.Id,
            Card = card,
            CardGuid = card.Id
        });
        
        context.SaveChanges();
        return card;
    }

    public Card ReadEntity(Company company, Guid cardGuid)
    {
        var card = company.CompanyCards().First(cc => cc.CardGuid.Equals(cardGuid)).Card;
        if (card == null) throw new Exception("Card not found!");
        return card;
    }

    public ICollection<Card> ReadAllEntity(Company company)
    {
        return company.CompanyCards().Select(cc => cc.Card).ToList();
    }

    public Card UpdateEntity(Company company, CardDto dto, Guid cardGuid)
    {
        DeleteCardAndCompanyCard(company, cardGuid);
        return CreateEntity(company, dto);
    }

    public void DeleteEntity(Company company, Guid cardGuid)
    {
        DeleteCardAndCompanyCard(company, cardGuid);
        context.SaveChanges();
    }

    private void DeleteCardAndCompanyCard(Company company, Guid cardGuid)
    {
        var companyCard = company.CompanyCards().First(cc => cc.CardGuid.Equals(cardGuid));
        if (companyCard == null) throw new Exception("Card not found");
        context.Remove(companyCard);
        context.Cards.Remove(companyCard.Card);
    }

    private void CardImplementations(Card card, CardDto dto)
    {
        switch (dto)
        {
            case { AbsentDetails: not null }:
                var absent =
                    _absentService.CreateEntity(
                        context.User.FirstOrDefault(u => u.Id.Equals(dto.AbsentDetails!.MadeBy.Id))!);
                card.AbsentDetails = absent;
                card.AbsentDetailsId = absent.Id;
                break;
            case { MemoryDetails: not null }:
                var memory = _memoryService.CreateEntity(dto.MemoryDetails!);
                card.MemoryDetails = memory;
                card.MemoryDetailsId = memory.Id;
                break;
            default: // TaskOrEventDetails: not null
                var taskOrEvent = _taskOrEventService.CreateEntity(dto.TaskOrEventDetails!);
                card.TaskOrEventDetails = taskOrEvent;
                card.TaskOrEventDetailsId = taskOrEvent.Id;
                
                break;
        }
    }

    private static void CheckForm(CardDto dto)
    {
        // My map can only contain one entity at a time. But ALWAYS one. they can't all be null.
        switch (dto)
        {
            case { AbsentDetails: null, TaskOrEventDetails: null, MemoryDetails: null }:
                throw new Exception("Card not complete!");
            case { AbsentDetails: not null, MemoryDetails: not null }:
                throw new Exception("Card not conform!");
            case { AbsentDetails: not null, TaskOrEventDetails: not null }:
                throw new Exception("Card not conform!");
            case { MemoryDetails: not null, TaskOrEventDetails: not null }:
                throw new Exception("Card not conform!");
        }
    }
}