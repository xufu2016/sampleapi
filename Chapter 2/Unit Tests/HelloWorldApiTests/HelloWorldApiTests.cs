using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using HelloWorldApi.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HelloWorldApiTests
{
    [TestClass]
    public class HelloWorldContollerTests
    {
        private HelloWorldController _helloWorldController;

        [TestInitialize]
        public void TestInitialize()
        {
            _helloWorldController = new HelloWorldController
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration(),
                //ControllerContext = new System.Web.Http.Controllers.HttpControllerContext(),
                //ActionContext = new System.Web.Http.Controllers.HttpActionContext(),
                //RequestContext = new System.Web.Http.Controllers.HttpRequestContext()
            };
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void Should_fail_void()
        {
            _helloWorldController.Void(0);
        }

        [TestMethod]
        public void Should_succeed_void()
        {
            _helloWorldController.Void(1);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void Should_fail_httpresponsemessage()
        {
            _helloWorldController.HttpResponseMessage(0);
        }

        [TestMethod]
        public void Should_succeed_httpresponsemessage()
        {
            var response = _helloWorldController.HttpResponseMessage(1);

            Resource resource;
            Assert.IsTrue(response.TryGetContentValue(out resource));
        }

        [TestMethod]
        public void Should_fail_ihttpactionresult()
        {
            var response = _helloWorldController.IHttpActionResult(0);
            
            Assert.IsNotNull(response as BadRequestResult);
        }

        [TestMethod]
        public void Should_succeed_ihttpactionresult()
        {
            var response = _helloWorldController.IHttpActionResult(1);

            Assert.IsNotNull(response as OkNegotiatedContentResult<Resource>);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void Should_fail_resource()
        {
            _helloWorldController.Resource(0);
        }

        [TestMethod]
        public void Should_succeed_resource()
        {
            var resource = _helloWorldController.Resource(1);

            Assert.IsNotNull(resource);
        }
    }
}
