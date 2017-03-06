
#if TEST
using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Topevery.FMP.ObjectModel.Configuration;

namespace Topevery.FMP.ObjectModel.Test
{

    [TestFixture]
    public class FMPConfigurationTest
    {
        [Test]
        public void ReadConfiguration()
        {
            FMPConfigurationSection result = FMPConfiguration.Section;
            Assert.IsNotNull(result);
        }
    }
}
#endif