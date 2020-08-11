using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using ClockUniverse.Controllers;
using ClockUniverse;
using System.Text;
using System;
using System.Web.Routing;
using System.Web;
using Moq;

using ClockUniverse.Controllers;


namespace ClockUniverse.Tests.Controllers
{
    [TestClass]
    public class ProductManagerControllerTest
    {
        [TestMethod]
        public void TestIndex()
        {
            var db = new CsK23T3bEntities();
            var controller = new ProductManagerController();
            var result = controller.Index();

            var view = result as ViewResult;
            Assert.IsNotNull(view);

            var model = view.Model as List<ProductTable>;
            Assert.AreEqual(db.ProductTables.Count(), model.Count);
            
        }

        [TestMethod]
        public void TestCreateG() {
            var controller = new ProductManagerController();
            var result = controller.Create() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestCreateP() {
        }

        [TestMethod]
        public void TestDetail()
        {
            {
                var db = new CsK23T3bEntities();
                var item = db.ProductTables.First();
                var controller = new ProductManagerController();

                var result = controller.Details(item.Watch_ID);
                var view = result as ViewResult;
                Assert.IsNotNull(view);

                var model = view.Model as ProductTable;
                Assert.IsNotNull(model);
                Assert.AreEqual(item.Watch_ID, model.Watch_ID);

                result = controller.Details(0);
                Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
            }
        }

        [TestMethod]
        public void TestEditG()
        {
            var controller = new ProductManagerController();
            var result0 = controller.Edit(0);
            Assert.IsInstanceOfType(result0, typeof(HttpNotFoundResult));

            var db = new CsK23T3bEntities();
            var item = db.ProductTables.First();
            var result1 = controller.Edit(item.Watch_ID) as ViewResult;
            Assert.IsNotNull(result1);
            var model = result1.Model as ProductTable;
            Assert.AreEqual(item.Watch_ID, model.Watch_ID);
        }
        // new test edit 27/05
        [TestMethod]
        public void TestEdit_Product_fail()
        {

            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            // var controller = new Controllers.ProductManagerController();
            var controller = new ProductManagerController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            var serverMock = new Mock<HttpServerUtilityBase>();
            serverMock.Setup(x => x.MapPath("~/App_Data")).Returns(@"D:\Anh_Quoc\K\Năm 2\SOFTWARE TESTING\Team Project\Final project team\Code\FinalProj\ClockUniverse\ClockUniverse\App_Data");
            context.Setup(x => x.Server).Returns(serverMock.Object);
            // var file1Mock = new Mock<HttpPostedFileBase>();
            // file1Mock.Setup(x => x.FileName).Returns("30_0");
            var Producttable = new ProductTable
            {
                Watch_ID = 1089,
                Watch_Name = "KhacHuy",
                Watch_Description = "dsadasdal",
                WatchType_ID = 1,
                //Original_Price = 1000,
                //Selling_Price = 100,
                InStock = 1,
                Watch_Static ="cc",
            };
            var actual = controller.Edit(Producttable) as RedirectToRouteResult;
            Assert.IsNull(actual);  
        }
        [TestMethod]
        public void TestEdit_Product_success()
        {

            var helper = new MockHelper();
            var context = helper.MakeFakeContext();
            // var controller = new Controllers.ProductManagerController();
            var controller = new ProductManagerController();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            var serverMock = new Mock<HttpServerUtilityBase>();
            serverMock.Setup(x => x.MapPath("~/App_Data")).Returns(@"D:\Anh_Quoc\K\Năm 2\SOFTWARE TESTING\Team Project\Final project team\Code\FinalProj\ClockUniverse\ClockUniverse\App_Data");


            var request = new Mock<HttpRequestBase>(MockBehavior.Strict);
          //request.Setup(r => r.Files).Returns(new System.Web.HttpRequestBase.Files());

            context.Setup(x => x.Server).Returns(serverMock.Object);

            

            var file1Mock = new Mock<HttpPostedFileBase>();
            file1Mock.Setup(x => x.FileName).Returns("1087_0");
            var image = file1Mock.Object;
            var Producttable = new ProductTable
            {
                Watch_ID = 1087,
                Watch_Name = "KhacHuy",
                Watch_Description = "dsadasdal",
                WatchType_ID = 1,
                Original_Price = 1000,
                Selling_Price = 100,
                InStock = 1,
                Watch_Static = "cc",
                Image = "image",
            };
            var actual = controller.Edit(Producttable) as RedirectToRouteResult;
            Assert.AreEqual("Index", actual.RouteValues["Action"]);
        }
        [TestMethod]
        public void Delete_Success()
        {
            // Arrange
            ProductManagerController controller = new ProductManagerController();
            // Act
           // var redirectToRouteResult = controller.Delete(1083) as RedirectToRouteResult;
            ViewResult result = controller.Delete(0) as ViewResult;
            // Assert
            Assert.IsNull(result);
        }
        [TestMethod]
        public void Delete_DeleteConfirmed_success()
        {
            // Arrange
            ProductManagerController controller = new ProductManagerController();
            // Act
            // var redirectToRouteResult = controller.Delete(1083) as RedirectToRouteResult;
            ViewResult result = controller.DeleteConfirmed(34) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void Delete_DeleteConfirmed_success2()
        {
            // Arrange
            ProductManagerController controller = new ProductManagerController();
            // Act
            // var redirectToRouteResult = controller.Delete(1083) as RedirectToRouteResult;
            var redirectToRouteResult = controller.DeleteConfirmed(89) as RedirectToRouteResult;
            // Assert
            Assert.AreEqual("Index", redirectToRouteResult.RouteValues["Action"]);
           // Assert.AreEqual("Account", redirectToRouteResult.RouteValues["controller"]);
        }
    }
}
