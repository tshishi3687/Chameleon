using Chameleon.Business.Dtos;
using Chameleon.Business.Services;
using Xunit;

namespace Chameleon.Tests.Integrations.Services
{
    public class LocalityServiceTest: BaseTestContext
    {
        [Fact]
        public void CrudServiceTest()
        {
            using var context = CreateDbContext();
            var localityService = new LocalityService(context);

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
            var updateEntity = localityService.updateEntity(updateDto, readLocalityDto.Id);
            Assert.NotEqual(readLocalityDto.Name, updateEntity.Name);
            Assert.NotEqual(readLocalityDto.Id, updateEntity.Id);
            Assert.Equal(updateEntity.Name, updateDto.Name.ToUpper());
            Assert.Throws<KeyNotFoundException>(() => localityService.ReadEntity(readLocalityDto.Id));
            
            // DeleteEntity
            localityService.delete(updateEntity.Id);
            Assert.Throws<KeyNotFoundException>(() => localityService.ReadEntity(updateEntity.Id));
            Assert.Empty(localityService.ReadAllEntity());
        }
    }
}