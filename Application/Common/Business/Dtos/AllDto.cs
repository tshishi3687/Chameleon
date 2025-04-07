using Chameleon.Application.CompanySetting.Business.Dtos;
using Chameleon.Application.CompanySetting.DataAccess.Entities;
using Chameleon.Application.HumanSetting.Business.Dtos;

namespace Chameleon.Application.Common.Business.Dtos;

public class AllDto
{
    public ContactDetailsDto? ContactDetails { get; set; }
    public CountryDto? Country { get; set; }
    public CreationUserDto? CreationUser { get; set; }
    public LocalityDto? Locality { get; set; }
    public Absent? Absent { get; set; }
    public AddCompanyUser? AddCompanyUser { get; set; }
    public CardDto? Card { get; set; }
    public CompanyEasyVueDto? CompanyEasyVue { get; set; }
    public CreationCompanyAndUserDto? CreationCompanyAndUser { get; set; }
    public MemoryDto? Memory { get; set; }
    public TasDto? Task { get; set; }
    public LoggerDto? Logger { get; set; }
    public RolesDto? Roles { get; set; }
    public SimpleUserDto? SimpleUser { get; set; }
    public UserVueDto? UserVue { get; set; }
    public Passport? Data { get; set; }
}