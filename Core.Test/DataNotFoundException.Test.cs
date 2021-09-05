using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Reflection;
using static Core.Services.Test.GenericCrudServiceTest;

namespace Core.Exceptions.Test
{
    [TestClass]
    public class DataNotFoundExceptionTest
    {
        [DataTestMethod]
        [StorableDataTypes]
        public void DataNotFoundException_ShouldHaveAMessageForEachStorableData(
            Type storableDataType
        )
        {
            //Assemble
            var constructor = typeof(DataNotFoundException<>)
                .MakeGenericType(new Type[] { storableDataType })
                .GetConstructor(new Type[] { typeof(int) });
            if (constructor == null)
                throw new Exception(
                    "Could not find constructor for DataNotFoundException<T>"
                );

            //Act
            Func<object> exception =
                () => constructor.Invoke(new object[] { 1 });

            //Assert
            //If this fails, the constructor does not know about the type
            exception.Should().NotThrow();
        }

        [TestMethod]
        public void DataNotFoundException_TypeNotKnown_ShouldThrowException()
        {
            //Act
            Func<DataNotFoundException<UnknownStorableData>> act =
                () => new DataNotFoundException<UnknownStorableData>(1);

            //Assert
            act
                .Should()
                .ThrowExactly<GenshinApplicationException>()
                .WithMessage(
                    DataNotFoundException<UnknownStorableData>.UnknownType
                );
        }

        [TestMethod]
        public void DataNotFoundException_InputIsGood_ShouldUseIdInMessage()
        {
            //Assemble
            var expected = 7;

            //Act
            var exception = new DataNotFoundException<Artifact>(expected);

            //Assert
            exception.Message.Should().Contain($"with id {expected}");
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class StorableDataTypesAttribute : Attribute, ITestDataSource
    {
        public IEnumerable<object[]> GetData(MethodInfo methodInfo) =>
            new List<object[]>
            {
                new object[] { typeof(Artifact) },
                new object[] { typeof(Character) },
                new object[] { typeof(Loadout) },
                new object[] { typeof(Team) },
                new object[] { typeof(TestStorableData) },
            };

        public string GetDisplayName(MethodInfo methodInfo, object[] data) =>
            $"{((Type)data[0]).Name}";
    }

    /// <summary>
    /// The DataNotFoundException doesn't know about this type
    /// </summary>
    public class UnknownStorableData : StorableData { }
}