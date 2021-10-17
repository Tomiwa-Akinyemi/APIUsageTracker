using APIUsage.Core;
using APIUsage.Service;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace APIUsageTracker.Test
{
    [TestClass]
    public class ServiceUnitTest
    {
        private readonly IUsageTracker _usageTracker;
        private IUsageTracker _tracker;
        private IOptions<Band> _options;

        public ServiceUnitTest(IUsageTracker usageTracker)
        {
            _usageTracker = usageTracker;
        }
        public ServiceUnitTest()
        {

        }
        [TestInitialize]
        public void Initalize()
        {
            _tracker = new UsageTracker(_options);
        }
        [TestMethod]
        public void TestIPValidation()
        {
            bool isValid = _tracker.ValidateIPAddress("10.2.4.abc");
            Assert.IsFalse(isValid);
        }

        //[TestMethod]
        //public void TestChargeCalculation()
        //{
        //    double charge = _tracker.FetchCharge(10000001, 1000);
        //    Assert.AreEqual(500,charge);
        //}
    }
}
