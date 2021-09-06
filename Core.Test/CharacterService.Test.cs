using Core.Exceptions;
using Core.Interfaces;
using FluentAssertions;
using Force.DeepCloner;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services.Test
{
    [TestClass]
    public class CharacterServiceTest
    {
        private Character _testCharacter = new();
        private static Mock<IGenericCrudRepository<Character>> _repoMock = new();
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
                .Setup(x => x.GetAll())
                .ReturnsAsync(new List<Character> { _testCharacter });
            _repoMock
                .Setup(x => x.Get(It.IsAny<int>()))
                .ReturnsAsync(_testCharacter);
            _repoMock.Setup(x => x.GetMaxId()).ReturnsAsync(_testCharacter.Id);
            _service = new(_repoMock.Object);
        }

        [TestMethod]
        public void AddLoadoutToCharacter_CharacterDoesNotExist_ShouldThrowException()
        {
            //Assemble
            _repoMock
                .Setup(x => x.Get(It.IsAny<int>()))
                .ReturnsAsync((Character?)null);
            var expected =
                new DataNotFoundException<Character>(_testCharacter.Id)
                .Message;

            //Act
            Func<Task<Loadout>> act = () => _service.AddLoadoutToCharacter(
                _testCharacter.Id,
                _testCharacter.Loadouts[0]
            );

            //Assert
            act
                .Should()
                .ThrowExactlyAsync<DataNotFoundException<Character>>()
                .WithMessage(expected);
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
                x => x.Update(It.Is<Character>(y => y.Loadouts.Count == 2)),
                Times.Once
            );
        }

        [TestMethod]
        public void Update_ShouldThrowException()
        {
            //Act
            Func<Task> act = () => _service.Update(_testCharacter);

            //Assert
            act
                .Should()
                .ThrowExactlyAsync<InvalidOperationException>();
        }

        [TestMethod]
        public void UpdateLoadout_CharacterDoesNotExist_ShouldThrowException()
        {
            //Assemble
            _repoMock
                .Setup(x => x.Get(It.IsAny<int>()))
                .ReturnsAsync((Character?)null);
            var expected =
                new DataNotFoundException<Character>(_testCharacter.Id)
                .Message;

            //Act
            Func<Task<Loadout>> act = () => _service.UpdateLoadout(
                _testCharacter.Id,
                _testCharacter.Loadouts[0]
            );

            //Assert
            act
                .Should()
                .ThrowExactlyAsync<DataNotFoundException<Character>>()
                .WithMessage(expected);
        }

        [TestMethod]
        public void UpdateLoadout_LoadoutDoesNotExist_ShouldThrowException()
        {
            //Assemble
            var loadout = _testCharacter.Loadouts[0].DeepClone();
            _testCharacter.Loadouts = new List<Loadout>();
            var expected =
                new DataNotFoundException<Loadout>(loadout.Id)
                .Message;

            //Act
            Func<Task<Loadout>> act = () => _service.UpdateLoadout(
                _testCharacter.Id,
                loadout
            );

            //Assert
            act
                .Should()
                .ThrowExactlyAsync<DataNotFoundException<Loadout>>()
                .WithMessage(expected);
        }

        [TestMethod]
        public async Task UpdateLoadout_InputIsGood_ShouldReturnLoadout()
        {
            //Assemble
            var expected = _testCharacter.Loadouts[0].DeepClone();
            expected.Name = "asdfasdfas";

            //Act
            var actual = await _service.UpdateLoadout(
                _testCharacter.Id,
                expected
            );

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public async Task UpdateLoadout_InputIsGood_ShouldUpdateCharacter()
        {
            //Assemble
            var expected = _testCharacter.Loadouts[0].DeepClone();
            expected.Name = "asdfasdfas";

            //Act
            await _service.UpdateLoadout(
                _testCharacter.Id,
                expected
            );

            //Assert
            _repoMock.Verify(
                x => x.Update(It.Is<Character>(
                    y => y.Loadouts.Any(z => z.Name == expected.Name)
                )),
                Times.Once
            );
        }
    }
}