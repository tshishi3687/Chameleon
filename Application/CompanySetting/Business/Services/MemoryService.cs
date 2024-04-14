using Chameleon.Application.CompanySetting.Business.Dtos;
using Chameleon.Application.CompanySetting.DataAccess.Entities;
using Chameleon.Application.HumanSetting.DataAccess.Entities;

namespace Chameleon.Application.CompanySetting.Business.Services;

public class MemoryService(Context context)
{
    
    public Memory CreateEntity(MemoryDto dto)
    {
        CheckDto(dto);
        var user = context.User.FirstOrDefault(u => u.Id.Equals(dto.MadeBy.Id));
        return context.Memories.Add(new Memory
        {
            MadeBy = user!,
            MadeById = user!.Id,
            Title = dto.Title,
            Description = dto.Description
        }).Entity;
    }

    public Memory ReadEntity(Guid guid)
    {
        var memory = context.Memories.FirstOrDefault(m => m.Id.Equals(guid));
        if (memory == null) throw new Exception($"Memory with id : {guid} not found");
        return memory;
    }

    public ICollection<Memory> ReadAllEntity()
    {
        return context.Memories.ToList();
    }

    public Memory UpdateEntity(MemoryDto dto, Guid guid)
    {
        var memory = context.Memories.FirstOrDefault(m => m.Id.Equals(guid));
        if (memory == null) throw new Exception($"Memory with id : {guid} not found");
        context.Memories.Remove(memory);
        return CreateEntity(dto);
    }

    public void DeleteEntiy(Guid guid)
    {
        var memory = context.Memories.FirstOrDefault(m => m.Id.Equals(guid));
        if (memory == null) throw new Exception($"Memory with id : {guid} not found");
        context.Memories.Remove(memory);
        context.SaveChanges();
    }

    private void CheckDto(MemoryDto dto)
    {
        if (dto == null) throw new Exception("Dto cannot be null!");
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Memory name's cannot be null or empty!");
        if (string.IsNullOrEmpty(dto.Description)) throw new Exception("Memory description's cannot be null or empty!");
        var user = context.User.FirstOrDefault(u => u.Id.Equals(dto.MadeBy.Id));
        if (user == null) throw new Exception("Memory madeBy not found!");
    }
}