using Chameleon.Application.HumanSetting.DataAccess.Entities;

namespace Chameleon.Application.CompanySetting.Common.Interface;

public interface ICardType
{
    Guid Id { get; }
    Users MadeBy { get; }
}