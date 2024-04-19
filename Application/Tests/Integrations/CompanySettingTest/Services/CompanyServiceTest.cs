using Chameleon.Application.CompanySetting.Business.Dtos;
using Chameleon.Application.CompanySetting.Business.Services;
using Xunit;

namespace Chameleon.Application.Tests.Integrations.CompanySettingTest.Services;

public class CompanyServiceTest : BaseModelsForTests, IDisposable
{
    private Context _context;

    public CompanyServiceTest()
    {
        _context = new MockContext();
    }
    
    public void Dispose()
    {
        _context = null;
    }
    
    [Fact]
    public void AddingCompanyTest()
    {
        var service = new CompanyService(_context);
        var companyAndUser = AddValidCreationCompanyAndUserDto();
        var company = service.CreateCompanyAndUser(companyAndUser);

        Assert.NotNull(company);
        Assert.True(company.Id != Guid.Empty);
        Assert.Equal(company.Name, companyAndUser.Name);
        Assert.Equal(company.BusinessNumber, companyAndUser.BusinessNumber);
        Assert.Equal(company.ContactDetails.Address, companyAndUser.ContactDetail.Address);
        Assert.Equal(company.ContactDetails.Number, companyAndUser.ContactDetail.Number);
        Assert.Equal(company.ContactDetails.Locality.Name.ToUpper(),
            companyAndUser.ContactDetail.Locality.Name.ToUpper());
        Assert.Equal(company.ContactDetails.Country.Name.ToUpper(),
            companyAndUser.ContactDetail.Country.Name.ToUpper());
    }

    [Fact]
    public void AddCompanyAndUserTest()
    {
        var service = new CompanyService(_context);
        var companyAndUser = AddValidCreationCompanyAndUserDto();
        var company = service.CreateCompanyAndUser(companyAndUser);
        
        Assert.Equal(company.Tutor.FirstName,
            companyAndUser.AddCompanyUser.CreationUserDto!.FirstName);
        Assert.Equal(company.Tutor.LastName,
            companyAndUser.AddCompanyUser.CreationUserDto.LastName);
        Assert.Equal(company.Tutor.BursDateTime,
            companyAndUser.AddCompanyUser.CreationUserDto.BursDateTime);
        Assert.NotNull(company.Tutor.UserRoles());
        Assert.Equal(company.Tutor.UserRoles().FirstOrDefault()!.Roles.Company.Id, company.Id);
        Assert.NotNull(company.Tutor.UserContactDetails());
        Assert.Equal(company.Tutor.UserContactDetails().First().ContactDetails.Address,
            companyAndUser.AddCompanyUser.CreationUserDto.ContactDetails.FirstOrDefault()!
                .Address);
        Assert.Equal(company.Tutor.UserContactDetails().First().ContactDetails.Number,
            companyAndUser.AddCompanyUser.CreationUserDto.ContactDetails.FirstOrDefault()!.Number);
        Assert.Equal(company.Tutor.UserContactDetails().First().ContactDetails.Locality.Name.ToUpper(),
            companyAndUser.AddCompanyUser.CreationUserDto.ContactDetails.FirstOrDefault()!.Locality
                .Name.ToUpper());
        Assert.Equal(company.Tutor.UserContactDetails().First().ContactDetails.Country.Name.ToUpper(),
            companyAndUser.AddCompanyUser.CreationUserDto.ContactDetails.FirstOrDefault()!.Country
                .Name.ToUpper());

        var isActiveUser = _context.IsActiveUserInCompanies
            .Where(i => i.CompanyId.Equals(company.Id) && i.UserId.Equals(company.Tutor.Id)).First();
        Assert.NotNull(isActiveUser);
        Assert.True(isActiveUser.IsActive);
    }
    
    [Fact]
    public void AddingUserInCompany()
    {
        var service = new CompanyService(_context);
        var company = service.CreateCompanyAndUser(AddValidCreationCompanyAndUserDto());
        service.AddUserInCompany(new AddCompanyUser
        {
            CreationUserDto = NewCreationUserDto()
        }, company);

        var companyImplemented = _context.Companies.FirstOrDefault(c => c.Id.Equals(company.Id));
        Assert.NotNull(companyImplemented);
        Assert.NotNull(companyImplemented.CompanyUser());
        Assert.True(companyImplemented.CompanyUser().Count == 2);
        var isActiveInCompany = _context.User.FirstOrDefault(u => u.Id.Equals(companyImplemented.CompanyUser().Last().UserId)).IsActiveInCompanies(company.Id);
        Assert.NotNull(isActiveInCompany);
        Assert.True(isActiveInCompany.First().IsActive);
        

    }

    [Fact]
    public void CatchErrorTest()
    {
        var service = new CompanyService(_context);

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
        var service = new CompanyService(_context);
        
        Assert.Throws<ArgumentException>(() =>
            service.CreateCompanyAndUser(AddBadCreationCompanyAndUserDtoBusinessNumber()));
    }

    [Fact]
    public void CatchErrorTestWithTutorIsEmpty()
    {
        var service = new CompanyService(_context);

        Assert.Throws<Exception>(() =>
            service.CreateCompanyAndUser(AddBadCreationCompanyAndUserDtoUserIdAndTutor()));
    }

    [Fact]
    public void CatchErrorTestWithCompanyNameIsEmpty()
    {
        var service = new CompanyService(_context);
        Assert.Throws<ArgumentException>(() =>
            service.CreateCompanyAndUser(AddBadCreationCompanyAndUserDtoName()));
    }

    
}