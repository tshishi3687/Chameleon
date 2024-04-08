using System.Net;
using System.Runtime;
using Chameleon.Application.CompanySetting.Business.Dtos;
using Chameleon.Application.CompanySetting.Business.Services;
using Chameleon.Application.HumanSetting;
using Chameleon.Application.HumanSetting.Business.Dtos;
using Chameleon.Application.HumanSetting.Business.Services;
using Chameleon.Application.Securities;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Chameleon.Application.Tests.Integrations.CompanySettingTest.Services;

public class CompanyServiceBaseTest : BaseModelsForTests
{
    [Fact]
    public void CrudService()
    {
        var context = CreateDbContext();
        var createCompanyService = new CompanyServiceBase(context);
        var badEntityNameCreated = AddBadCreationCompanyAndUserDtoName();
        var badEntityBusinessNumberCreated = AddBadCreationCompanyAndUserDtoBusinessNumber();
        var badEntityUserIdAndTutorCreated = AddBadCreationCompanyAndUserDtoUserIdAndTutor();
        var validEntityCreated = AddValidCreationCompanyAndUserDto();

        Assert.Equal(HttpStatusCode.BadRequest, createCompanyService.CreateEntity(badEntityNameCreated).StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, createCompanyService.CreateEntity(badEntityBusinessNumberCreated).StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, createCompanyService.CreateEntity(badEntityUserIdAndTutorCreated).StatusCode);
        
        // Check Company created
        Assert.Equal(HttpStatusCode.Created, createCompanyService.CreateEntity(validEntityCreated).StatusCode);
        var companyCreated = context.Companies.Last();
        Assert.NotNull(companyCreated);
        Assert.True(companyCreated.Id != Guid.Empty);
        Assert.Equal(companyCreated.Name, validEntityCreated.Name);
        Assert.Equal(companyCreated.BusinessNumber, validEntityCreated.BusinessNumber);
        Assert.Equal(companyCreated.ContactDetails.Address, validEntityCreated.ContactDetail.Address);
        Assert.Equal(companyCreated.ContactDetails.Number, validEntityCreated.ContactDetail.Number);
        Assert.Equal(companyCreated.ContactDetails.Locality.Name, validEntityCreated.ContactDetail.Locality.Name.ToUpper());
        Assert.Equal(companyCreated.ContactDetails.Country.Name, validEntityCreated.ContactDetail.Country.Name.ToUpper());

        var tutorCreated = context.CompanyUsers.Where(c => c.CompanyId.Equals(companyCreated.Id)).Select(c => c.User).FirstOrDefault();
        
        // check user created
        Assert.NotNull(tutorCreated);
        Assert.Equal(tutorCreated.FirstName, validEntityCreated.AddCompanyUser.CreationUserDto!.FirstName);
        Assert.Equal(tutorCreated.LastName, validEntityCreated.AddCompanyUser.CreationUserDto.LastName);
        Assert.Equal(tutorCreated.BursDateTime, validEntityCreated.AddCompanyUser.CreationUserDto.BursDateTime);
        Assert.NotNull(tutorCreated.UserRoles());
        Assert.Equal(tutorCreated.UserRoles().FirstOrDefault()!.Roles.Company.Id, companyCreated.Id);
        Assert.Equal(tutorCreated.UserRoles().FirstOrDefault()!.Roles.Name, EnumUsersRoles.SUPER_ADMIN.ToString());
        Assert.NotNull(tutorCreated.UserContactDetails());
        Assert.Equal(tutorCreated.UserContactDetails().FirstOrDefault()!.ContactDetails.Address, validEntityCreated.AddCompanyUser.CreationUserDto.ContactDetails.FirstOrDefault()!.Address);
        Assert.Equal(tutorCreated.UserContactDetails().FirstOrDefault()!.ContactDetails.Number, validEntityCreated.AddCompanyUser.CreationUserDto.ContactDetails.FirstOrDefault()!.Number);
        Assert.Equal(tutorCreated.UserContactDetails().FirstOrDefault()!.ContactDetails.Locality.Name, validEntityCreated.AddCompanyUser.CreationUserDto.ContactDetails.FirstOrDefault()!.Locality.Name.ToUpper());
        Assert.Equal(tutorCreated.UserContactDetails().FirstOrDefault()!.ContactDetails.Country.Name, validEntityCreated.AddCompanyUser.CreationUserDto.ContactDetails.FirstOrDefault()!.Country.Name.ToUpper());
    }

    [Fact]
    public void AddUserInCompanyTest()
    {
        var context = CreateDbContext();
        Assert.NotNull(new CompanyServiceBase(context).CreateEntity(AddValidCreationCompanyAndUserDto()));
        var companyDto = context.CompanyUsers.FirstOrDefault(cu =>
            cu.Company.BusinessNumber.Equals(AddValidCreationCompanyAndUserDto().BusinessNumber));
        Assert.NotNull(companyDto);
        var _constente = new Constantes(context);
        _constente.Connected = context.User.FirstOrDefault(u => u.Email.Equals(AddCreationUserDto().Email));
        Assert.Equal(HttpStatusCode.Unauthorized, new CompanyServiceBase(context).AddUserInCompany(AddCompanyUser(companyDto.CompanyId), new Guid()).StatusCode);
    }
}