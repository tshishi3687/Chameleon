using System.Runtime;
using Chameleon.Business.Dtos;
using Chameleon.Business.Services;
using Xunit;

namespace Chameleon.Tests.Integrations.Services
{
    public class LocalityServiceBaseTest: BaseTestContext
    {
        [Fact]
        public void CrudServiceTest()
        {
            var localityService = new LocalityServiceBase(CreateDbContext());

            var badDto1 = new LocalityDto();
            Assert.Throws<AmbiguousImplementationException>(() => localityService.CreateEntity(badDto1));

            var badDto2 = new LocalityDto { Name = ""};
            Assert.Throws<AmbiguousImplementationException>(() => localityService.CreateEntity(badDto2));

            var badDto3 = new LocalityDto { Name = " "};
            Assert.Throws<AmbiguousImplementationException>(() => localityService.CreateEntity(badDto3));
            
            var localityDto = new LocalityDto { Name = "test" };

            // CreateEntity
            var createdLocalityDto = localityService.CreateEntity(localityDto);
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
            var updateDto = new LocalityDto { Name = "test 2" };
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