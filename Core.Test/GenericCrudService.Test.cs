using Core.Exceptions;
using Core.Interfaces;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services.Test
{
    [TestClass]
    public class GenericCrudServiceTest
    {
        private TestStorableData _testData = new();
        private Mock<IGenericCrudRepository<TestStorableData>> _repoMock =
            new();
        private GenericCrudService<TestStorableData> _service;

        public GenericCrudServiceTest()
        {
            _service =
                new GenericCrudService<TestStorableData>(_repoMock.Object);
        }

        [TestInitialize()]
        public void InitializeTests()
        {
            _testData = new TestStorableData
            {
                Id = 1,
                Name = "Test",
            };
            _repoMock = new();
            _repoMock
                .Setup(x => x.GetAll())
                .ReturnsAsync(new List<TestStorableData> { _testData });
            _repoMock
                .Setup(x => x.Get(It.IsAny<int>()))
                .ReturnsAsync(_testData);
            _repoMock.Setup(x => x.GetMaxId()).ReturnsAsync(_testData.Id);
            _service =
                new GenericCrudService<TestStorableData>(_repoMock.Object);
        }


        [TestMethod]
        public async Task Create_MaxIdIsNull_ShouldAssignId()
        {
            //Assemble
            var expectedId = 1;
            _repoMock.Setup(x => x.GetMaxId()).ReturnsAsync((int?)null);

            //Act
            var actual = await _service.Create(_testData);

            //Assert
            actual.Id.Should().Be(expectedId);
        }

        [TestMethod]
        public async Task Create_MaxIdIsNotNull_ShouldAssignId()
        {
            //Assemble
            var expectedId = 6;
            _repoMock.Setup(x => x.GetMaxId()).ReturnsAsync(expectedId - 1);

            //Act
            var actual = await _service.Create(_testData);

            //Assert
            actual.Id.Should().Be(expectedId);
        }

        [TestMethod]
        public async Task Create_InputIsGood_ShouldStoreData()
        {
            //Assemble
            var expectedId = _testData.Id + 1;

            //Act
            await _service.Create(_testData);

            //Assert
            _repoMock.Verify(
                x => x.Create(
                    It.Is<TestStorableData>(y => y.Id == expectedId)
                ),
                Times.Once
            );
        }

        [TestMethod]
        public void Delete_EntityDoesNotExist_ShouldThrowException()
        {
            //Assemble
            _repoMock
                .Setup(x => x.Get(It.IsAny<int>()))
                .ReturnsAsync((TestStorableData?)null);
            var expected =
                new DataNotFoundException<TestStorableData>(_testData.Id)
                .Message;

            //Act
            Func<Task> act = () => _service.Delete(_testData.Id);

            //Assert
            act
                .Should()
                .ThrowExactly<DataNotFoundException<TestStorableData>>()
                .WithMessage(expected);
        }

        [TestMethod]
        public async Task Delete_EntityExists_ShouldDeleteEntity()
        {
            //Act
            await _service.Delete(_testData.Id);

            //Assert
            _repoMock.Verify(
                x => x.Delete(
                    It.Is<TestStorableData>(y => y.Id == _testData.Id)
                ),
                Times.Once
            );
        }

        [TestMethod]
        public async Task GetAll_ShouldReturnAllCharacters()
        {
            //Act
            var actual = await _service.GetAll();

            //Assert
            actual.Should().ContainEquivalentOf(_testData);
        }

        [TestMethod]
        public void Get_EntityDoesNotExist_ShouldThrowException()
        {
            //Assemble
            _repoMock
                .Setup(x => x.Get(It.IsAny<int>()))
                .ReturnsAsync((TestStorableData?)null);
            var expected =
                new DataNotFoundException<TestStorableData>(_testData.Id)
                .Message;

            //Act
            Func<Task> act = () => _service.Get(_testData.Id);

            //Assert
            act
                .Should()
                .ThrowExactly<DataNotFoundException<TestStorableData>>()
                .WithMessage(expected);
        }

        [TestMethod]
        public async Task Get_EntityExists_ShouldReturnEntity()
        {
            //Assemble
            var id = 6;
            var expected = new TestStorableData
            {
                Id = id,
                Name = "different",
            };
            _repoMock.Setup(x => x.Get(id)).ReturnsAsync(expected);

            //Act
            var actual = await _service.Get(id);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void Update_EntityDoesNotExist_ShouldThrowException()
        {
            //Assemble
            _repoMock
                .Setup(x => x.Get(It.IsAny<int>()))
                .ReturnsAsync((TestStorableData?)null);
            var expected =
                new DataNotFoundException<TestStorableData>(_testData.Id)
                .Message;

            //Act
            Func<Task> act = () => _service.Update(_testData);

            //Assert
            act
                .Should()
                .ThrowExactly<DataNotFoundException<TestStorableData>>()
                .WithMessage(expected);
        }

        [TestMethod]
        public async Task Update_EntityExists_ShouldUpdateEntity()
        {
            //Assemble
            var input = new TestStorableData
            {
                Id = _testData.Id,
                Name = "different",
            };

            //Act
            await _service.Update(input);

            //Assert
            _repoMock.Verify(
                x => x.Update(It.Is<TestStorableData>(y =>
                    y.Id == _testData.Id &&
                    y.Name == input.Name
                )),
                Times.Once
            );
        }

        public class TestStorableData : StorableData
        {
            public string Name { get; set; } = "";
        }
    }
}