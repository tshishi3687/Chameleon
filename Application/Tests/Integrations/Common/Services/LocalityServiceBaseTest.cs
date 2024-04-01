using System.Runtime;
using Chameleon.Application.HumanSetting.Business.Dtos;
using Xunit;

namespace Chameleon.Application.Tests.Integrations.Common.Services
{
    public class LocalityServiceBaseTest: BaseModelsForTests
    {
        [Fact]
        public void CrudServiceTest()
        {
            var localityService = new HumanSetting.Business.Services.LocalityServiceBase(CreateDbContext());

            var badDto1 = new LocalityDto();
            Assert.Throws<AmbiguousImplementationException>(() => localityService.CreateEntity1(badDto1));

            var badDto2 = new LocalityDto { Name = ""};
            Assert.Throws<AmbiguousImplementationException>(() => localityService.CreateEntity1(badDto2));

            var badDto3 = new LocalityDto { Name = " "};
            Assert.Throws<AmbiguousImplementationException>(() => localityService.CreateEntity1(badDto3));
            
            var localityDto = AddLocalityDto();

            // CreateEntity
            var createdLocalityDto = localityService.CreateEntity1(localityDto);
            Assert.NotNull(createdLocalityDto);
            Assert.Equal(createdLocalityDto.Name, localityDto.Name.ToUpper());
            
            // ReadEntity
            var readLocalityDto = localityService.ReadEntity(createdLocalityDto.Id);
            Assert.NotNull(readLocalityDto);
            Assert.Equal(readLocalityDto.Name, createdLocalityDto.Name);
            Assert.Equal(readLocalityDto.Id, createdLocalityDto.Id);
            
            // ReadAllEntity
            var readAllLocalityDto = localityService.ReadAllEntity();
            Assert.NotEmpty(readAllLocalityDto);
            var dto = readAllLocalityDto.First();
            Assert.Equal(dto.Id, readLocalityDto.Id);
            Assert.Equal(dto.Name, readLocalityDto.Name);
            
            // UpdateEntity
            var updateDto = UpdateLocalityDto();
            var updateEntity = localityService.UpdateEntity(updateDto, readLocalityDto.Id);
            Assert.NotEqual(readLocalityDto.Name, updateEntity.Name);
            Assert.NotEqual(readLocalityDto.Id, updateEntity.Id);
            Assert.Equal(updateEntity.Name, updateDto.Name.ToUpper());
            Assert.Throws<KeyNotFoundException>(() => localityService.ReadEntity(readLocalityDto.Id));
            
            // DeleteEntity
            localityService.DeleteEntity(updateEntity.Id);
            Assert.Throws<KeyNotFoundException>(() => localityService.ReadEntity(updateEntity.Id));
            Assert.Empty(localityService.ReadAllEntity());
        }
    }
}