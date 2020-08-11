using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using ClockUniverse.Models;
using ClockUniverse.Controllers;

namespace ClockUniverse.Tests.Controllers
{
    class MockODM
    {
        public static Mock<OrderManagerController> MockOrderManager()
        {
            var alli = new List<Order_Detail>();
            var mock = new Mock<OrderManagerController>();
            mock.Setup(x => x.Delete(It.IsAny<int>())).Callback((int id)=> {
                var remitem = alli.FirstOrDefault(i => i.Order_ID == id);
                alli.Remove(remitem);
                
            });
            return mock;
        }
    }
}
