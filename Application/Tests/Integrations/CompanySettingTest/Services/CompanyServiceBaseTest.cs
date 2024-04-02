using System.Runtime;
using Chameleon.Application.CompanySetting.Business.Services;
using Chameleon.Application.HumanSetting;
using Xunit;

namespace Chameleon.Application.Tests.Integrations.CompanySettingTest.Services;

public class CompanyServiceBaseTest: BaseModelsForTests
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

        Assert.Throws<AmbiguousImplementationException>(() => createCompanyService.CreateEntity(badEntityNameCreated));
        Assert.Throws<AmbiguousImplementationException>(() => createCompanyService.CreateEntity(badEntityBusinessNumberCreated));
        Assert.Throws<AmbiguousImplementationException>(() => createCompanyService.CreateEntity(badEntityUserIdAndTutorCreated));
        
        // Check Company created
        var companyCreated = context.Companies.Find(createCompanyService.CreateEntity(validEntityCreated).Id);
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
        Assert.Equal(tutorCreated.FirstName, validEntityCreated.Tutor!.FirstName);
        Assert.Equal(tutorCreated.LastName, validEntityCreated.Tutor.LastName);
        Assert.Equal(tutorCreated.BursDateTime, validEntityCreated.Tutor.BursDateTime);
        Assert.NotNull(tutorCreated.UserRoles());
        Assert.Equal(tutorCreated.UserRoles().FirstOrDefault()!.Roles.Name, EnumUsersRoles.SUPER_ADMIN.ToString());
        Assert.NotNull(tutorCreated.UserContactDetails());
        Assert.Equal(tutorCreated.UserContactDetails().FirstOrDefault()!.ContactDetails.Address, validEntityCreated.Tutor.ContactDetails.FirstOrDefault()!.Address);
        Assert.Equal(tutorCreated.UserContactDetails().FirstOrDefault()!.ContactDetails.Number, validEntityCreated.Tutor.ContactDetails.FirstOrDefault()!.Number);
        Assert.Equal(tutorCreated.UserContactDetails().FirstOrDefault()!.ContactDetails.Locality.Name, validEntityCreated.Tutor.ContactDetails.FirstOrDefault()!.Locality.Name.ToUpper());
        Assert.Equal(tutorCreated.UserContactDetails().FirstOrDefault()!.ContactDetails.Country.Name, validEntityCreated.Tutor.ContactDetails.FirstOrDefault()!.Country.Name.ToUpper());
    }
}