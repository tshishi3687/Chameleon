using Chameleon.Application.Common.Business.Services;
using Chameleon.Application.CompanySetting.Common.Enumeration;
using Chameleon.Application.CompanySetting.DataAccess.Entities;
using Chameleon.Application.HumanSetting.DataAccess.Entities;

namespace Chameleon.Application.CompanySetting.Business.Services;

public class AbsentService(Context context) : CheckServiceBase(context)
{
    public Absent CreateEntity(Users users)
    {
        return context.Absents.Add(new Absent
        {
            AcceptedBy = users,
            AcceptedById = users.Id,
            Status = EAbsentStatus.PENDING
        }).Entity;
    }

    public Absent ReadEntity(Guid guid)
    {
        var absent = context.Absents.First(a => a.Id.Equals(guid));
        if (absent == null) throw new Exception($"Absent with Id : {guid} not found!");
        return absent;
    }

    public ICollection<Absent> ReadAllEntity()
    {
        return context.Absents.ToList();
    }

    public Absent UpdateEntity(Guid guid, Users users)
    {
        var absent = context.Absents.First(a => a.Id.Equals(guid));
        if (absent == null) throw new FileNotFoundException($"Absent with id: {guid} not found");
        context.Absents.Remove(absent);
        return CreateEntity(users);
    }

    public void DeleteEntity(Guid guid)
    {
        var absent = context.Absents.First(a => a.Id.Equals(guid));
        if (absent == null) throw new FileNotFoundException($"Absent with id: {guid} not found");
        context.Absents.Remove(absent);
    }
}