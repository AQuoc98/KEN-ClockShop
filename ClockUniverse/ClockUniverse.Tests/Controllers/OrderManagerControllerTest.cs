using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using Moq;
using System.Transactions;
using System.Collections.Generic;
using System.Linq;
using DotNetNuke;
using DotNetNuke.Modules;
using ClockUniverse.Controllers;
using ClockUniverse.Models;
using System.Web.SessionState;
using System.Web.Routing;
namespace ClockUniverse.Tests.Controllers
{
    [TestClass]
    public class OrderManagerControllerTest
    {
        [TestMethod]
        public void TestIndex()
        {
            var db = new CsK23T3bEntities();
            var controller = new OrderManagerController();
            var result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
            var model = result.Model as List<Order>;
            Assert.AreEqual(db.Orders.Count(), model.Count);
        }
        [TestMethod]
        public void TestEditG()
        {
            var db = new CsK23T3bEntities();
            var controller = new OrderManagerController();
            var result0 = controller.Edit(0);
            Assert.IsInstanceOfType(result0, typeof(HttpNotFoundResult));
            var item = db.Orders.First();
            var result1 = controller.Edit(item.Order_ID) as ViewResult;
            Assert.IsNotNull(result1);
            var model = result1.Model as Order;
            Assert.AreEqual(item.Order_ID, model.Order_ID);
        }
        [TestMethod]
        public void TestEditP()
        {
            var db = new CsK23T3bEntities();
            var model = new Order
            {
                Order_ID = db.Orders.AsNoTracking().First().Order_ID,
                Deliver_Status = 1
            };
            var controller = new OrderManagerController();

            using (var scope = new TransactionScope())
            {
                var result = controller.Edit(model, model.Deliver_Status);
                var view = result as ViewResult;

                var redirect = result as RedirectToRouteResult;
                Assert.IsNotNull(redirect);
                Assert.AreEqual("Index", redirect.RouteValues["action"]);
                var item = db.Orders.Find(model.Order_ID);
                Assert.IsNotNull(item);
                Assert.AreEqual(item.Deliver_Status, model.Deliver_Status);
            }

        }
        [TestMethod]
        public void TestCreateG()
        {
            var db = new CsK23T3bEntities();
            
            var Controller = new OrderManagerController();
            var rs = Controller.Create() as ViewResult;
            Assert.IsNotNull(rs);
        }
        
       
        [TestMethod]
        public void TestDetails()
        {
            var ls = new Mock<Order_Detail>();
            var db = new CsK23T3bEntities();
            var controller = new OrderManagerController();
            var result0 = controller.Details(0);
            
            Assert.IsInstanceOfType(result0, typeof(HttpStatusCodeResult));
            var item = db.Orders.First();
            var result1 = controller.Details(item.Order_ID) as ViewResult;
            Assert.IsNotNull(result1);
            var model = result1.Model as Order;
            Assert.AreEqual(item.Order_ID, model.Order_ID);
           
        }
        
    }
   
}
