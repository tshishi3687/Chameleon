// using Chameleon.Application.CompanySetting.Business.Services;
// using Xunit;
//
// namespace Chameleon.Application.Tests.Integrations.CompanySettingTest.Services;
//
// public class CompanyServiceTest : BaseModelsForTests, IDisposable
// {
//     private Context _context;
//
//     public CompanyServiceTest()
//     {
//         _context = new MockContext();
//     }
//     
//     public void Dispose()
//     {
//         _context = null;
//     }
//     
//     [Fact]
//     public void AddingCompanyTest()
//     {
//         var service = new CompanyService(_context);
//         var companyAndUser = AddValidCreationCompanyAndUserDto();
//         var company = service.CreateCompanyAndUser(companyAndUser);
//
//         Assert.NotNull(company);
//         Assert.True(company.Id != Guid.Empty);
//         Assert.Equal(company.Name, companyAndUser.Name);
//     }
//
//     [Fact]
//     public void AddCompanyAndUserTest()
//     {
//         var service = new CompanyService(_context);
//         var companyAndUser = AddValidCreationCompanyAndUserDto();
//         var company = service.CreateCompanyAndUser(companyAndUser);
//         
//         Assert.Equal(company.Tutor.FirstName,
//             companyAndUser.UserDto!.FirstName);
//         Assert.Equal(company.Tutor.LastName,
//             companyAndUser.UserDto.LastName);
//         Assert.Equal(company.Tutor.BursDateTime,
//             companyAndUser.UserDto.BursDateTime);
//         Assert.NotNull(company.Tutor.UserRoles());
//         Assert.Equal(company.Tutor.UserRoles().FirstOrDefault()!.Roles.Company.Id, company.Id);
//         Assert.NotNull(company.Tutor.UserContactDetails());
//         Assert.Equal(company.Tutor.UserContactDetails().First().ContactDetails.Address,
//             companyAndUser.UserDto.ContactDetails.FirstOrDefault()!
//                 .Address);
//         Assert.Equal(company.Tutor.UserContactDetails().First().ContactDetails.Number,
//             companyAndUser.UserDto.ContactDetails.FirstOrDefault()!.Number);
//         Assert.Equal(company.Tutor.UserContactDetails().First().ContactDetails.Locality.Name.ToUpper(),
//             companyAndUser.UserDto.ContactDetails.FirstOrDefault()!.Locality
//                 .Name.ToUpper());
//         Assert.Equal(company.Tutor.UserContactDetails().First().ContactDetails.Country.Name.ToUpper(),
//             companyAndUser.UserDto.ContactDetails.FirstOrDefault()!.Country
//                 .Name.ToUpper());
//     }
//     
//     [Fact]
//     public void AddingUserInCompany()
//     {
//         var service = new CompanyService(_context);
//         var company = service.CreateCompanyAndUser(AddValidCreationCompanyAndUserDto());
//         service.AddUserInCompany(NewCreationUserDto(), company);
//
//         var companyImplemented = _context.Companies.FirstOrDefault(c => c.Id.Equals(company.Id));
//         Assert.NotNull(companyImplemented);
//         Assert.NotNull(companyImplemented.CompanyUser());
//         Assert.True(companyImplemented.CompanyUser().Count == 2);
//     }
//
//     [Fact]
//     public void CatchErrorTest()
//     {
//         var service = new CompanyService(_context);
//
//         Assert.Throws<NullReferenceException>(() =>
//             service.CreateCompanyAndUser(AddBadCreationCompanyAndUserDtoUserIdAndTutor()));
//         Assert.Throws<ArgumentException>(() =>
//             service.CreateCompanyAndUser(AddBadCreationCompanyAndUserDtoName()));
//         Assert.Throws<ArgumentException>(() =>
//             service.CreateCompanyAndUser(AddBadCreationCompanyAndUserDtoBusinessNumber()));
//     }
//
//     [Fact]
//     public void CatchErrorTestWithBusinessNumberIsEmpty()
//     {
//         var service = new CompanyService(_context);
//         
//         Assert.Throws<ArgumentException>(() =>
//             service.CreateCompanyAndUser(AddBadCreationCompanyAndUserDtoBusinessNumber()));
//     }
//
//     [Fact]
//     public void CatchErrorTestWithTutorIsEmpty()
//     {
//         var service = new CompanyService(_context);
//
//         Assert.Throws<NullReferenceException>(() =>
//             service.CreateCompanyAndUser(AddBadCreationCompanyAndUserDtoUserIdAndTutor()));
//     }
//
//     [Fact]
//     public void CatchErrorTestWithCompanyNameIsEmpty()
//     {
//         var service = new CompanyService(_context);
//         Assert.Throws<ArgumentException>(() =>
//             service.CreateCompanyAndUser(AddBadCreationCompanyAndUserDtoName()));
//     }
//
//     
// }