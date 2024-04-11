using Chameleon.Application.CompanySetting.Business.Dtos;
using Chameleon.Application.CompanySetting.Business.Services;
using Chameleon.Application.CompanySetting.DataAccess.Entities;
using Chameleon.Application.HumanSetting;
using Xunit;

namespace Chameleon.Application.Tests.Integrations.CompanySettingTest.Services;

public class CompanyServiceTest : BaseModelsForTests
{

    private Context _context;
    private CompanyService _service;
    
    [Fact]
    public void AddingCompanyTest()
    {
        var company = GetCompany();

        Assert.NotNull(company);
        Assert.True(company.Id != Guid.Empty);
        Assert.Equal(company.Name, AddValidCreationCompanyAndUserDto().Name);
        Assert.Equal(company.BusinessNumber, AddValidCreationCompanyAndUserDto().BusinessNumber);
        Assert.Equal(company.ContactDetails.Address, AddValidCreationCompanyAndUserDto().ContactDetail.Address);
        Assert.Equal(company.ContactDetails.Number, AddValidCreationCompanyAndUserDto().ContactDetail.Number);
        Assert.Equal(company.ContactDetails.Locality.Name.ToUpper(),
            AddValidCreationCompanyAndUserDto().ContactDetail.Locality.Name.ToUpper());
        Assert.Equal(company.ContactDetails.Country.Name.ToUpper(),
            AddValidCreationCompanyAndUserDto().ContactDetail.Country.Name.ToUpper());
    }

    [Fact]
    public void AddUserTest()
    {
        var company = GetCompany();
        Assert.Equal(company.Tutor.FirstName,
            AddValidCreationCompanyAndUserDto().AddCompanyUser.CreationUserDto!.FirstName);
        Assert.Equal(company.Tutor.LastName,
            AddValidCreationCompanyAndUserDto().AddCompanyUser.CreationUserDto.LastName);
        Assert.Equal(company.Tutor.BursDateTime,
            AddValidCreationCompanyAndUserDto().AddCompanyUser.CreationUserDto.BursDateTime);
        Assert.NotNull(company.Tutor.UserRoles());
        Assert.Equal(company.Tutor.UserRoles().FirstOrDefault()!.Roles.Company.Id, company.Id);
        Assert.Equal(company.Tutor.UserRoles().FirstOrDefault()!.Roles.Name, EnumUsersRoles.SUPER_ADMIN.ToString());
        Assert.NotNull(company.Tutor.UserContactDetails());
        Assert.Equal(company.Tutor.UserContactDetails().First().ContactDetails.Address,
            AddValidCreationCompanyAndUserDto().AddCompanyUser.CreationUserDto.ContactDetails.FirstOrDefault()!
                .Address);
        Assert.Equal(company.Tutor.UserContactDetails().First().ContactDetails.Number,
            AddValidCreationCompanyAndUserDto().AddCompanyUser.CreationUserDto.ContactDetails.FirstOrDefault()!.Number);
        Assert.Equal(company.Tutor.UserContactDetails().First().ContactDetails.Locality.Name.ToUpper(),
            AddValidCreationCompanyAndUserDto().AddCompanyUser.CreationUserDto.ContactDetails.FirstOrDefault()!.Locality
                .Name.ToUpper());
        Assert.Equal(company.Tutor.UserContactDetails().First().ContactDetails.Country.Name.ToUpper(),
            AddValidCreationCompanyAndUserDto().AddCompanyUser.CreationUserDto.ContactDetails.FirstOrDefault()!.Country
                .Name.ToUpper());
    }
    
    [Fact]
    public void AddingUserInCompany()
    {
        var context = CreateDbContext();
        var company = GetCompany();
        
        _service.AddUserInCompany(new AddCompanyUser
        {
            CreationUserDto = NewCreationUserDto()
        }, company.Id);

        var companyImplemented = context.Companies.FirstOrDefault(c => c.Id.Equals(company.Id));
        Assert.NotNull(companyImplemented);
        Assert.NotNull(companyImplemented.CompanyUser());
        Assert.True(companyImplemented.CompanyUser().Count == 2);
        var companyUser = companyImplemented.CompanyUser();
        Assert.NotNull(companyUser);
        // Assert.Equal(
        //     companyUser.FirstOrDefault(cu => cu.User.Email.Equals(NewCreationUserDto().Email)).User
        //         .FirstName, NewCreationUserDto().FirstName);
        // Assert.Equal(
        //     companyUser.FirstOrDefault(cu => cu.User.Email.Equals(NewCreationUserDto().Email)).User
        //         .LastName, NewCreationUserDto().LastName);
        // Assert.Equal(
        //     companyUser.FirstOrDefault(cu => cu.User.Email.Equals(NewCreationUserDto().Email)).User
        //         .BursDateTime, NewCreationUserDto().BursDateTime);
        // Assert.Equal(
        //     companyUser.FirstOrDefault(cu => cu.User.Email.Equals(NewCreationUserDto().Email)).User
        //         .Email, NewCreationUserDto().Email);
        // Assert.Equal(
        //     companyUser.FirstOrDefault(cu => cu.User.Email.Equals(NewCreationUserDto().Email)).User
        //         .Phone, NewCreationUserDto().Phone);
        // Assert.Equal(
        //     companyUser.FirstOrDefault(cu => cu.User.Email.Equals(NewCreationUserDto().Email)).User
        //         .UserContactDetails().First().ContactDetails.Address,
        //     NewCreationUserDto().ContactDetails.First().Address);
        // Assert.Equal(
        //     companyUser.FirstOrDefault(cu => cu.User.Email.Equals(NewCreationUserDto().Email)).User
        //         .UserContactDetails().First().ContactDetails.Number,
        //     NewCreationUserDto().ContactDetails.First().Number);
        // Assert.Equal(
        //     companyUser.FirstOrDefault(cu => cu.User.Email.Equals(NewCreationUserDto().Email)).User
        //         .UserContactDetails().First().ContactDetails.Locality.Name,
        //     NewCreationUserDto().ContactDetails.First().Locality.Name);
        // Assert.Equal(
        //     companyUser.FirstOrDefault(cu => cu.User.Email.Equals(NewCreationUserDto().Email)).User
        //         .UserContactDetails().First().ContactDetails.Country.Name,
        //     NewCreationUserDto().ContactDetails.First().Country.Name);
    }

    [Fact]
    public void CatchErrorTest()
    {
        var context = CreateDbContext();
        var service = new CompanyService(context);

        Assert.Throws<Exception>(() =>
            service.CreateCompanyAndUser(AddBadCreationCompanyAndUserDtoUserIdAndTutor()));
        Assert.Throws<ArgumentException>(() =>
            service.CreateCompanyAndUser(AddBadCreationCompanyAndUserDtoName()));
        Assert.Throws<ArgumentException>(() =>
            service.CreateCompanyAndUser(AddBadCreationCompanyAndUserDtoBusinessNumber()));
    }

    [Fact]
    public void CatchErrorTestWithBusinessNumberIsEmpty()
    {
        var context = CreateDbContext();
        var service = new CompanyService(context);
        
        Assert.Throws<ArgumentException>(() =>
            service.CreateCompanyAndUser(AddBadCreationCompanyAndUserDtoBusinessNumber()));
    }

    [Fact]
    public void CatchErrorTestWithTutorIsEmpty()
    {
        var context = CreateDbContext();
        var service = new CompanyService(context);

        Assert.Throws<Exception>(() =>
            service.CreateCompanyAndUser(AddBadCreationCompanyAndUserDtoUserIdAndTutor()));
    }

    [Fact]
    public void CatchErrorTestWithCompanyNameIsEmpty()
    {
        var context = CreateDbContext();
        var service = new CompanyService(context);
        Assert.Throws<ArgumentException>(() =>
            service.CreateCompanyAndUser(AddBadCreationCompanyAndUserDtoName()));
    }

    private Company GetCompany()
    {
        _context = CreateDbContext();
        _service = new CompanyService(_context);

        return _service.CreateCompanyAndUser(AddValidCreationCompanyAndUserDto());
    }
}