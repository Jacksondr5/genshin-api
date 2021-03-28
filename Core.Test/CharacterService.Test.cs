using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Core.Test
{
    [TestClass]
    public class CharacterServiceTest
    {
        private Character _testCharacter = new();
        private static Mock<ICharacterRepository> _repoMock = new();
        private CharacterService _service = new(_repoMock.Object);

        [TestInitialize()]
        public void InitializeTests()
        {
            _testCharacter = new Character
            {
                Id = 6,
                Name = "Test Character",
                Loadouts = new List<Loadout>
                {
                    new Loadout
                    {
                        CircletId = 3,
                        ClockId = 4,
                        CupId = 5,
                        FeatherId = 6,
                        FlowerId = 7,
                        Id = 8,
                        Name = "Test Loadout Name"
                    }
                }
            };
            _repoMock = new();
            _repoMock
                .Setup(x => x.GetAllCharacters())
                .ReturnsAsync(new List<Character> { _testCharacter });
            _service = new(_repoMock.Object);
        }

        [TestMethod]
        public void AddLoadoutToCharacter_CharacterDoesNotExist_ShouldThrowException()
        {
            //Assemble
            _repoMock
                .Setup(x => x.GetAllCharacters())
                .ReturnsAsync(new List<Character>());

            //Act
            Func<Task<Loadout>> act = () =>
                _service.AddLoadoutToCharacter(5, _testCharacter.Loadouts[0]);

            //Assert
            act
                .Should()
                .ThrowExactly<GenshinException>()
                .WithMessage(GenshinMessages.CharacterNotFound);
        }

        [TestMethod]
        public async Task AddLoadoutToCharacter_InputIsGood_ShouldAssignIdToLoadout()
        {
            //Assemble
            var expectedId = _testCharacter.Loadouts[0].Id + 1;

            //Act
            var actual = await _service.AddLoadoutToCharacter(
                _testCharacter.Id,
                _testCharacter.Loadouts[0]
            );

            //Assert
            actual.Id.Should().Be(expectedId);
        }

        [TestMethod]
        public async Task AddLoadoutToCharacter_InputIsGood_ShouldStoreLoadout()
        {
            //Act
            await _service.AddLoadoutToCharacter(
                _testCharacter.Id,
                _testCharacter.Loadouts[0]
            );

            //Assert
            _repoMock.Verify(
                x => x.UpdateCharacter(
                    It.Is<Character>(y => y.Loadouts.Count == 2)
                ),
                Times.Once
            );
        }

        [TestMethod]
        public async Task CreateCharacter_InputIsGood_ShouldAssignId()
        {
            //Assemble
            var expectedId = _testCharacter.Id + 1;

            //Act
            var actual = await _service.CreateCharacter(_testCharacter);

            //Assert
            actual.Id.Should().Be(expectedId);
        }

        [TestMethod]
        public async Task CreateCharater_InputIsGood_ShouldStoreCharacter()
        {
            //Act
            await _service.CreateCharacter(_testCharacter);

            //Assert
            _repoMock.Verify(
                x => x.CreateCharacter(It.IsAny<Character>()),
                Times.Once
            );
        }

        [TestMethod]
        public async Task GetAllCharacters_ShouldReturnAllCharacters()
        {
            //Act
            var actual = await _service.GetAllCharacters();


            //Assert
            actual.Should().ContainEquivalentOf(_testCharacter);
        }

        [TestMethod]
        public void UpdateLoadout_CharacterDoesNotExist_ShouldThrowException()
        {
            //Assemble
            _repoMock
                .Setup(x => x.GetAllCharacters())
                .ReturnsAsync(new List<Character>());

            //Act
            Func<Task<Loadout>> act = () =>
                _service.UpdateLoadout(5, _testCharacter.Loadouts[0]);

            //Assert
            act
                .Should()
                .ThrowExactly<GenshinException>()
                .WithMessage(GenshinMessages.CharacterNotFound);
        }

        [TestMethod]
        public void UpdateLoadout_LoadoutDoesNotExist_ShouldThrowException()
        {
            //Assemble
            var loadout = _testCharacter.Loadouts[0].DeepClone();
            _testCharacter.Loadouts = new List<Loadout>();

            //Act
            Func<Task<Loadout>> act = () => _service.UpdateLoadout(
                _testCharacter.Id,
                loadout
            );

            //Assert
            act
                .Should()
                .ThrowExactly<GenshinException>()
                .WithMessage(GenshinMessages.LoadoutNotFound);
        }
    }
}