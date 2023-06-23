using Alojat.interfa;
using Moq;
using Alojat.Models;
using Alojat.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TestNunit
{
    public class CategoriaControllerTest
    {
        [Test]
        public void DebeDevolverUnaLista()
        {
            var mockCategoria = new Mock<ICategoria>();
            var mockValidate = new Mock<IVcategoria>();

            List<Categoria> list = new();

            mockCategoria.Setup(v => v.LisCategoria()).Returns(list);
            var controller = new CategoriaController(mockCategoria.Object, mockValidate.Object);

            var events = controller.Index();
            Assert.IsNotNull(events);
            Assert.IsInstanceOf<IActionResult>(events);
        }

        [Test]
        public void DebeDevolverUnViewResult()
        {

            var controller = new CategoriaController(null, null);

            var events = controller.Create();
            Assert.IsNotNull(events);
            Assert.IsInstanceOf<ViewResult>(events);
        }

        [Test]
        public void DebePoderRegistrarUnCategoria()
        {
            var mockCategoria = new Mock<ICategoria>();
            var mockValidate = new Mock<IVcategoria>();

            Categoria categoria = new()
            {
                NombreCategoria = "Otro"
            };

            ModelStateDictionary modelState = new();

            mockCategoria.Setup(v => v.SaveCategoria(categoria));
            mockValidate.Setup(c => c.Validate(categoria, modelState)).Returns(true);

            var controller = new CategoriaController(mockCategoria.Object, mockValidate.Object);
            controller.ViewData.ModelState.Clear();

            var events = controller.Create(categoria) as RedirectToActionResult;
            Assert.IsNotNull(events);
            Assert.AreEqual("Index", events.ActionName);
        }

        [Test]
        public void NoDebePoderRegistrarUnCategoria()
        {
            var mockCategoria = new Mock<ICategoria>();
            var mockValidate = new Mock<IVcategoria>();

            Categoria categoria = new();
            ModelStateDictionary modelState = new();

            mockCategoria.Setup(v => v.SaveCategoria(categoria));
            mockValidate.Setup(c => c.Validate(categoria, modelState)).Returns(false);
            var controller = new CategoriaController(mockCategoria.Object, mockValidate.Object);
            controller.ModelState.AddModelError("", "");

            var events = controller.Create(categoria);
            Assert.IsNotNull(events);
            Assert.IsInstanceOf<ViewResult>(events);
        }

        [Test]
        public void NoDebePoderEditUnCategoriaIdCero()
        {
            var mockCategoria = new Mock<ICategoria>();

            Categoria categoria = new();

            mockCategoria.Setup(v => v.ValidateCategoria(0)).Returns(false);

            var controller = new CategoriaController(mockCategoria.Object, null);

            var events = controller.Edit(0) as NotFoundResult;

            Assert.IsNotNull(events);
            Assert.AreEqual(404, events.StatusCode);
        }

        [Test]
        public void NoDebePoderEditUnCategoriaExistente()
        {
            var mockCategoria = new Mock<ICategoria>();

            Categoria categoria = new();

            mockCategoria.Setup(v => v.ValidateCategoria(1)).Returns(true);

            var controller = new CategoriaController(mockCategoria.Object, null);

            var events = controller.Edit(1) as NotFoundResult;

            Assert.IsNotNull(events);
            Assert.AreEqual(404, events.StatusCode);
        }

        [Test]
        public void DebePoderEditUnCategoriaExistente()
        {
            var mockCategoria = new Mock<ICategoria>();

            Categoria categoria = new()
            {
                CategoriaID = 1,
                NombreCategoria = "Cate",
            };

            mockCategoria.Setup(v => v.ValidateCategoria(1)).Returns(true);
            mockCategoria.Setup(c => c.FindCategoria(1)).Returns(categoria);

            var controller = new CategoriaController(mockCategoria.Object, null);

            var events = controller.Edit(1) as ViewResult;

            Assert.IsNotNull(events);
            Assert.AreEqual(categoria, events.Model);
            Assert.IsInstanceOf<ViewResult>(events);
        }

        [Test]
        public void NoDebePoderActualizarUnCategoria()
        {
            var mockCategoria = new Mock<ICategoria>();

            Categoria categoria = new()
            {
                CategoriaID = 1,
                NombreCategoria = "Cate",
            };

            mockCategoria.Setup(v => v.ValidateCategoria(1)).Returns(true);
            mockCategoria.Setup(c => c.FindCategoria(1)).Returns(categoria);

            var controller = new CategoriaController(mockCategoria.Object, null);
            controller.ModelState.AddModelError("", "");

            var events = controller.Edit(2, categoria) as NotFoundResult;

            Assert.IsNotNull(events);
            Assert.AreEqual(404, events.StatusCode);
        }

        [Test]
        public void DebePoderActualizarUnCategoria()
        {
            var mockCategoria = new Mock<ICategoria>();
            var mockValidate = new Mock<IVcategoria>();

            Categoria categoria = new()
            {
                CategoriaID = 1,
                NombreCategoria = "otro"
            };

            ModelStateDictionary modelState = new();

            mockCategoria.Setup(v => v.ValidateCategoria(1)).Returns(true);
            mockCategoria.Setup(c => c.UpdateCategoria(categoria));
            mockValidate.Setup(c => c.UpdateCate(categoria,modelState)).Returns(true);

            var controller = new CategoriaController(mockCategoria.Object, mockValidate.Object);
            controller.ModelState.Clear();

            var events = controller.Edit(1, categoria) as RedirectToActionResult;

            Assert.IsNotNull(events);
            Assert.AreEqual("Index", events.ActionName);
        }

        [Test]
        public void NoDebePoderEliminarUnCategoria()
        {
            var mockCategoria = new Mock<ICategoria>();

            mockCategoria.Setup(v => v.ValidateCategoria(1)).Returns(false);

            var controller = new CategoriaController(mockCategoria.Object,null);
            controller.ModelState.AddModelError("", "");

            var events = controller.Delete(1) as NotFoundResult;

            Assert.IsNotNull(events);
            Assert.AreEqual(404, events.StatusCode);
        }

        [Test]
        public void DebePoderEliminarUnCategoria()
        {
            Categoria categoria = new()
            {
                CategoriaID = 1,
                NombreCategoria = "otro"
            };


            var mockCategoria = new Mock<ICategoria>();

            mockCategoria.Setup(v => v.ValidateCategoria(1)).Returns(true);
            mockCategoria.Setup(c => c.FirstCategoria(1)).Returns(categoria);
            mockCategoria.Setup(c => c.RemoveCategoria(categoria));

            var controller = new CategoriaController(mockCategoria.Object, null);
            controller.ModelState.Clear();

            var events = controller.Delete(1) as RedirectToActionResult;

            Assert.IsNotNull(events);
            Assert.AreEqual("Index", events.ActionName);
        }
    }
}
