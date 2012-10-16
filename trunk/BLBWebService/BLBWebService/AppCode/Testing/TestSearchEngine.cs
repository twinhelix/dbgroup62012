using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUnit.Framework;
using System.Data;

namespace BLBWebService.AppCode.Testing
{
    [TestFixture]
    public class TestSearchEngine
    {
        SearchEngine se;

        [SetUp]
        public void SetUp()
        {
            se = new SearchEngine();
        }

        [TearDown]
        public void TearDown()
        {
            se = null;
        }

        [Test]
        public void ConstructorTest()
        {
            Assert.IsNotNull(se);
        }

        [Test]
        public void LegalSearchTest()
        {
            DataSet ds = se.SearchBond(SearchType.CUSIP, "134098US30");
            Assert.AreEqual(ds.Tables[0].Rows.Count, 1);
        }

        [Test]
        public void IllegalSearchTest()
        {
            DataSet ds = se.SearchBond(SearchType.CUSIP, "134098US30-bla");
            Assert.AreEqual(ds.Tables[0].Rows.Count, 0);
        }


    }
}