﻿using NUnit.Framework;

namespace FubuMVC.IntegrationTesting.Diagnostics
{
    [TestFixture]
    public class can_bring_up_the_diagnostics_home_page
    {
        [Test]
        public void get_the_200()
        {
            TestHost.Scenario(_ =>
            {
                _.Get.Url("_fubu");
                _.StatusCodeShouldBeOk();
            });
        }
    }
}