using ADM.Tests.Common;
using Memory.Domain;
using Plugins.ADM.MSSQL;

namespace ADM.Tests.StorageProcedures
{
    [TestClass]
    public class UserListCategoryGetAsyncTests
    {
        private readonly ADMContext Context;
        public UserListCategoryGetAsyncTests()
        {
            Context = ADMFactory.Create();
        }
        [TestMethod]
        public void UserListCategoryGetAsync_Success()
        {
            //List<TmpUserTaskListCategory> with_pTenantID_0_ShouldBe = new List<TmpUserTaskListCategory>()
            List<TmpUserTaskListCategory> expected = new List<TmpUserTaskListCategory>()
            {
                new TmpUserTaskListCategory{UserID = 1, TaskListCategoryID = 1},
                new TmpUserTaskListCategory{UserID = 1, TaskListCategoryID = 2},
                new TmpUserTaskListCategory{UserID = 2, TaskListCategoryID = 1},
                new TmpUserTaskListCategory{UserID = 2, TaskListCategoryID = 2}
            };

            // Arrange
            var hndlr = new ADMSQLRepository(Context);
            List<TmpUser> tmpUsers = new List<TmpUser>()
            {
                new TmpUser { ID = 1},
                new TmpUser { ID = 2},
                new TmpUser { ID = 3}
            };
            List<TmpListCategory> tmpListCategories = new List<TmpListCategory>()
            {
                new TmpListCategory { ID = 1, PermissionExtID = 0 },
                new TmpListCategory { ID = 2, PermissionExtID = 0 },
                new TmpListCategory { ID = 3, PermissionExtID = 1 }
            };

            // Act
            var actual = hndlr.UserListCategoryGetAsync(0, tmpUsers, tmpListCategories);
            bool test = expected.SequenceEqual(actual);

            // Assert
            /*
                Assert.IsTrue(expected.SequenceEqual(actual));
                List<TmpListCategory> x = new List<TmpListCategory> { new TmpListCategory { ID = 1, PermissionExtID = 1} };
                List<TmpListCategory> z = new List<TmpListCategory> { new TmpListCategory { ID = 1, PermissionExtID = 1 } };
                bool listsAreEqual = expected.Count == actual.Count && expected.All(actual.Contains);
                Assert.IsTrue(listsAreEqual);
            */
            //Долбаный nullable short? -10 минут жизни...
            bool listsAreEqual =
                expected.Count == actual.Count &&
                actual.Zip(expected, (a, b) =>
                    a.UserID == b.UserID &&
                    a.TaskListCategoryID == b.TaskListCategoryID)
                .All(match => match);
            Assert.IsTrue(listsAreEqual);
        }
    }
}
