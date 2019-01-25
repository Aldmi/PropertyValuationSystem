using System.Linq;
using System.Threading.Tasks;
using Digests.Core.Model._4House;
using FluentAssertions;
using Xunit;
using Xunit.Priority;


namespace Digests.IntegrationTests
{
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class SharedItemsStorageTest 
    {
    
        [Fact, Priority(3)]
        public void Test1()
        {
    
        }

        [Fact, Priority(20)]
        public void Test2()
        {

        }

        [Fact, Priority(1)]
        public void Test3()
        {

        }
    }
}