using System.Collections.Generic;
using Lounge.Core.Domain;
using Lounge.Tests.TestData;
using NUnit.Framework;

namespace Lounge.Tests.Domain
{
    [TestFixture]
    public class ViewResultTests
    {
        [Test]
        public void ReturnsTrueWhenViewResultHasRows()
        {
            var viewResult = new ViewResult<TestDocument>();
            var viewRow = new ViewResultRow<TestDocument>();
            var viewResultRows = new List<ViewResultRow<TestDocument>> {viewRow};
            viewResult.Rows = viewResultRows.ToArray();
            
            Assert.That(viewResult.HasRows(), Is.True);
        }
    }
}