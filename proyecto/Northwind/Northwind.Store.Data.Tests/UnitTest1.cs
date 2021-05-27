using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Northwind.Store.Data.Tests
{
    /// <summary>
    /// MSTest
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            int a = 1;
            int b = 1;

            // Act
            int c = a + b;

            // Assert
            Assert.IsTrue(c == 2, "El resultado de la suma es inválido");
        }

        [Ignore]
        [TestMethod]
        public void TestMethod2()
        {
            // Arrange
            int a = 1;
            int b = 1;

            // Act
            int c = a - b;

            // Assert
            Assert.IsTrue(c == 0, "El resultado de la suma es inválido");
        }
    }
}
