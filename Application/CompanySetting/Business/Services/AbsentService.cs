using Chameleon.Application.Common.Business.Services;
using Chameleon.Application.CompanySetting.DataAccess.Entities;
using Chameleon.Application.HumanSetting.DataAccess.Entities;

namespace Chameleon.Application.CompanySetting.Business.Services;

public class AbsentService(Context context) : CheckServiceBase(context)
{
    public Absent CreateEntity(User user)
    {
        return Context.Absents.Add(new Absent
        {
            MadeBy = user,
            MadeById = user.Id
        }).Entity;
    }

    public Absent ReadEntity(Guid guid)
    {
        var absent = Context.Absents.First(a => a.Id.Equals(guid));
        if (absent == null) throw new Exception($"Absent with Id : {guid} not found!");
        return absent;
    }

    public ICollection<Absent> ReadAllEntity()
    {
        return Context.Absents.ToList();
    }

    public Absent UpdateEntity(Guid guid, User user)
    {
        var absent = Context.Absents.First(a => a.Id.Equals(guid));
        if (absent == null) throw new FileNotFoundException($"Absent with id: {guid} not found");
        Context.Absents.Remove(absent);
        return CreateEntity(user);
    }

    public void DeleteEntity(Guid guid)
    {
        var absent = Context.Absents.First(a => a.Id.Equals(guid));
        if (absent == null) throw new FileNotFoundException($"Absent with id: {guid} not found");
        Context.Absents.Remove(absent);
    }
}