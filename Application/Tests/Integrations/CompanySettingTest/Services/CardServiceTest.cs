// using Chameleon.Application.CompanySetting.Business.Dtos;
// using Chameleon.Application.CompanySetting.Business.Services;
// using Chameleon.Application.HumanSetting.Business.Mappers;
// using Chameleon.Application.HumanSetting.Business.Services;
// using Chameleon.Application.Securities;
// using Xunit;
//
// namespace Chameleon.Application.Tests.Integrations.CompanySettingTest.Services;
//
// public class CardServiceTest: BaseModelsForTests
// {
//     private readonly Context _context;
//     private readonly Constantes _constantes;
//
//     public CardServiceTest()
//     {
//         _context = new MockContext();
//     }
//     
//     [Fact]
//     public void CreateEntity()
//     {
//         var cardService = new CardService(_context);
//         var userService = new UserService(_context);
//         
//         var company = userService.CreateCompanyAndUser(AddValidCreationCompanyAndUserDto(), _constantes);
//         Assert.NotNull(company);
//         
//         companyService.AddUserInCompany(NewCreationUserDto(), company);
//         
//         var userMadeBy = new SimpleUserMapper().ToDto(_context.User.FirstOrDefault(u =>
//             u.Email.Equals(AddValidCreationCompanyAndUserDto())));
//         Assert.NotNull(userMadeBy);
//         
//         var addSimplifedUser = new SimpleUserMapper().ToDto(_context.User.First(u =>
//             u.Email.Equals(NewCreationUserDto().Email)));
//         Assert.NotNull(addSimplifedUser);
//
//         Assert.Throws<Exception>(() => cardService.CreateEntity(company, GetBadCardWithAbsentMemory(userMadeBy)));
//         Assert.Throws<Exception>(() => cardService.CreateEntity(company, GetBadCardWithAbsentTaskOrEvent(userMadeBy, new SimpleUserMapper().ToDto(company.Tutor))));
//         Assert.Throws<Exception>(() => cardService.CreateEntity(company, GetBadCardWithAbsentMemoryAndTaskOrEvent(userMadeBy, new SimpleUserMapper().ToDto(company.Tutor))));
//         Assert.Throws<Exception>(() => cardService.CreateEntity(company, GetBadCardWithEmptyAbsentMemoryAndTaskOrEvent()));
//
//         var cardDtoWithAbsent = GetValidCardWithAbsent(userMadeBy);
//         var cardWithAbsent = cardService.CreateEntity(company, cardDtoWithAbsent);
//         Assert.NotNull(cardWithAbsent);
//         Assert.False(cardWithAbsent.AbsentDetailsId.Equals(Guid.Empty));
//         Assert.True(cardWithAbsent.MemoryDetailsId.Equals(Guid.Empty));
//         Assert.Null(cardWithAbsent.MemoryDetails);
//         Assert.True(cardWithAbsent.TaskOrEventDetailsId.Equals(Guid.Empty));
//         Assert.Null(cardWithAbsent.TaskOrEventDetails);
//         Assert.False(cardWithAbsent.IsEnd);
//         Assert.False(cardWithAbsent.IsMadeIt);
//         Assert.Equal(cardWithAbsent.DateTime, cardDtoWithAbsent.DateTime);
//         
//         
//         var cardDtoWithMemory = GetValidCardWithMemory(userMadeBy);
//         var cardWithMemoryt = cardService.CreateEntity(company, cardDtoWithMemory);
//         Assert.NotNull(cardWithMemoryt);
//         Assert.False(cardWithMemoryt.MemoryDetailsId.Equals(Guid.Empty));
//         Assert.NotNull(cardWithMemoryt.MemoryDetails);
//         Assert.True(cardWithMemoryt.TaskOrEventDetailsId.Equals(Guid.Empty));
//         Assert.Null(cardWithMemoryt.TaskOrEventDetails);
//         Assert.True(cardWithMemoryt.AbsentDetailsId.Equals(Guid.Empty));
//         Assert.Null(cardWithMemoryt.AbsentDetails);
//         Assert.False(cardWithMemoryt.IsEnd);
//         Assert.False(cardWithMemoryt.IsMadeIt);
//         Assert.Equal(cardWithMemoryt.DateTime, cardDtoWithMemory.DateTime);
//         Assert.Equal(cardDtoWithMemory.MemoryDetails.Title, cardDtoWithMemory.MemoryDetails.Title);
//         Assert.Equal(cardDtoWithMemory.MemoryDetails.Description, cardDtoWithMemory.MemoryDetails.Description);
//         Assert.Equal(cardWithMemoryt.MemoryDetails.MadeBy.Id, userMadeBy.Id);
//         
//         
//         var cardDtoWithTaskOrEvent = GetValidCardWithTaskOrEvent(userMadeBy, new SimpleUserMapper().ToDto(company.Tutor));
//         var cardWithTaskOrEvent = cardService.CreateEntity(company, cardDtoWithTaskOrEvent);
//         Assert.NotNull(cardWithTaskOrEvent);
//         Assert.True(cardWithTaskOrEvent.MemoryDetailsId.Equals(Guid.Empty));
//         Assert.Null(cardWithTaskOrEvent.MemoryDetails);
//         Assert.False(cardWithTaskOrEvent.TaskOrEventDetailsId.Equals(Guid.Empty));
//         Assert.NotNull(cardWithTaskOrEvent.TaskOrEventDetails);
//         Assert.True(cardWithTaskOrEvent.AbsentDetailsId.Equals(Guid.Empty));
//         Assert.Null(cardWithTaskOrEvent.AbsentDetails);
//         Assert.False(cardWithTaskOrEvent.IsEnd);
//         Assert.False(cardWithTaskOrEvent.IsMadeIt);
//         Assert.Equal(cardWithTaskOrEvent.DateTime, cardDtoWithTaskOrEvent.DateTime);
//         Assert.Equal(cardWithTaskOrEvent.TaskOrEventDetails.Title, cardDtoWithTaskOrEvent.TaskOrEventDetails.Title);
//         Assert.Equal(cardWithTaskOrEvent.TaskOrEventDetails.Description, cardDtoWithTaskOrEvent.TaskOrEventDetails.Description);
//         Assert.Equal(cardWithTaskOrEvent.TaskOrEventDetails.MadeBy.Id, userMadeBy.Id);
//     }
// }