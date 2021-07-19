using FluentAssertions;
using Force.DeepCloner;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Test
{
    [TestClass]
    public class ArtifactServiceTest
    {
        private Artifact _testArtifact = new();
        private static Mock<IArtifactRepository> _repoMock = new();
        private ArtifactService _service = new(_repoMock.Object);

        [TestInitialize()]
        public void InitializeTests()
        {
            _testArtifact = new Artifact
            {
                Id = 5,
                MainStat = new ArtifactStat
                {
                    StatName = 7,
                    StatType = 4,
                    Value = 7
                }
            };
            _repoMock = new();
            _repoMock
                .Setup(x => x.GetAllArtifacts())
                .ReturnsAsync(new List<Artifact> { _testArtifact });
            _repoMock.Setup(x => x.GetMaxId()).ReturnsAsync(_testArtifact.Id);
            _service = new(_repoMock.Object);
        }

        [TestMethod]
        public async Task CreateArtifact_MaxIdIsNull_ShouldAssignId()
        {
            //Assemble
            var expectedId = 1;
            _repoMock.Setup(x => x.GetMaxId()).ReturnsAsync((int?)null);

            //Act
            var actual = await _service.CreateArtifact(_testArtifact);

            //Assert
            actual.Id.Should().Be(expectedId);
        }

        [TestMethod]
        public async Task CreateArtifact_MaxIdIsNotNull_ShouldAssignId()
        {
            //Assemble
            var expectedId = _testArtifact.Id + 1;

            //Act
            var actual = await _service.CreateArtifact(_testArtifact);

            //Assert
            actual.Id.Should().Be(expectedId);
        }

        [TestMethod]
        public async Task CreateArtifact_InputIsGood_ShouldStoreArtifact()
        {
            //Act
            await _service.CreateArtifact(_testArtifact);

            //Assert
            _repoMock.Verify(
                x => x.CreateArtifact(It.IsAny<Artifact>()),
                Times.Once
            );
        }

        [TestMethod]
        public void DeleteArtifact_ArtifactDoesNotExist_ShouldThrowException()
        {
            //Assemble
            _repoMock
                .Setup(x => x.GetAllArtifacts())
                .ReturnsAsync(new List<Artifact>());

            //Act
            Func<Task<Artifact>> act = () =>
                _service.DeleteArtifact(_testArtifact.Id);

            //Assert
            act
                .Should()
                .ThrowExactly<GenshinException>()
                .WithMessage(GenshinMessages.ArtifactNotFound);
        }

        [TestMethod]
        public async Task DeleteArtifact_InputIsGood_ShouldDeleteArtifact()
        {
            //Assemble
            var expected = _testArtifact;

            //Act
            var actual = await _service.DeleteArtifact(_testArtifact.Id);

            //Assert
            actual.Should().BeEquivalentTo(expected);
            _repoMock.Verify(
                x => x.DeleteArtifact(_testArtifact.Id),
                Times.Once
            );
        }

        [TestMethod]
        public async Task GetAllArtifacts_ShouldReturnAllArtifacts()
        {
            //Act
            var actual = await _service.GetAllArtifacts();


            //Assert
            actual.Should().ContainEquivalentOf(_testArtifact);
        }

        [TestMethod]
        public void UpdateArtifact_ArtifactDoesNotExist_ShouldThrowException()
        {
            //Assemble
            _repoMock
                .Setup(x => x.GetAllArtifacts())
                .ReturnsAsync(new List<Artifact>());

            //Act
            Func<Task<Artifact>> act = () =>
                _service.UpdateArtifact(_testArtifact);

            //Assert
            act
                .Should()
                .ThrowExactly<GenshinException>()
                .WithMessage(GenshinMessages.ArtifactNotFound);
        }

        [TestMethod]
        public async Task UpdateArtifact_InputIsGood_ShouldUpdateArtifact()
        {
            //Assemble
            var input = _testArtifact.DeepClone();
            input.Type = 9;

            //Act
            var actual = await _service.UpdateArtifact(input);

            //Assert
            actual.Type.Should().Be(input.Type);
            _repoMock.Verify(
                x => x.UpdateArtifact(
                    It.Is<Artifact>(y => y.Type == input.Type)
                ),
                Times.Once
            );
        }
    }
}