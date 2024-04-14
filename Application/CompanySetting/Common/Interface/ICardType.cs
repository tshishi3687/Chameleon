using Chameleon.Application.HumanSetting.DataAccess.Entities;

namespace Chameleon.Application.CompanySetting.Common.Interface;

public interface ICardType
{
    Guid Id { get; }
    User MadeBy { get; }
}