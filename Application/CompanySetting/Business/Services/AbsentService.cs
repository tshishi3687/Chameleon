using Chameleon.Application.Common.Business.Services;
using Chameleon.Application.CompanySetting.DataAccess.Entities;
using Chameleon.Application.HumanSetting.DataAccess.Entities;

namespace Chameleon.Application.CompanySetting.Business.Services;

public class AbsentService(Context context) : CheckServiceBase(context)
{
    public Absent CreateEntity(User user)
    {
        return context.Absents.Add(new Absent
        {
            MadeBy = user,
            MadeById = user.Id
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

    public Absent UpdateEntity(Guid guid, User user)
    {
        var absent = context.Absents.First(a => a.Id.Equals(guid));
        if (absent == null) throw new FileNotFoundException($"Absent with id: {guid} not found");
        context.Absents.Remove(absent);
        return CreateEntity(user);
    }

    public void DeleteEntity(Guid guid)
    {
        var absent = context.Absents.First(a => a.Id.Equals(guid));
        if (absent == null) throw new FileNotFoundException($"Absent with id: {guid} not found");
        context.Absents.Remove(absent);
    }
}