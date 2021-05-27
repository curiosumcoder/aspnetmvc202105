using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Northwind.Store.Data.Tests
{
    [TestClass]
    public class CatalogoD_Tests
    {
        /// <summary>
        /// DRY (Don't Repeat Yourself)
        /// </summary>
        [TestMethod]
        public void CategoryD_LeerPorLlave()
        {
            // Arrange
            var cD = new Northwind.Store.Data.CategoryD();
            var key = 4;

            // Act
            var c = cD.Read(key);

            // Assert
            Assert.IsNotNull(c, "No se encontró la categoría");
            Assert.IsTrue(c.Description.Length > 6, "La descripción es incompleta");
        }
    }
}
