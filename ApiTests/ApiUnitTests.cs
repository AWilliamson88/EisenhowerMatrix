using Api.Controllers;
using BusinessLogic.Services;
using DataModels.Models;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ApiTests
{
    public class ApiUnitTests
    {
        #region GetLists
        [Fact]
        public async void GetLists_Returns_OkResult()
        {
            // Arrange
            int count = 4;
            var controller = GetController(count);

            //Act
            var actionResult = await controller.GetLists();

            // Assert
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async void GetLists_Returns_CorrectNumberOfLists()
        {
            // Arrange
            int count = 4;
            var controller = GetController(count);
            
            //Act
            var actionResult = await controller.GetLists();

            // Assert
            var result = actionResult.Result as OkObjectResult;
            var returnLists = result.Value as IEnumerable<ToDoList>;
            Assert.Equal(count, returnLists.Count());
        }

        [Fact]
        public async void GetLists_ListsReturned_ContainToDoItems()
        {
            // Arrange
            int count = 4;
            var controller = GetController(count);

            // Act
            var actionResult = await controller.GetLists();

            // Assert
            var result = actionResult.Result as OkObjectResult;
            var returnLists = result.Value as IEnumerable<ToDoList>;
            var items = returnLists.Select(x => x.ToDoItems);
            Assert.All(items, i => Assert.IsType<List<ToDoItem>>(i));
        }

        [Fact]
        public async void GetLists_Returns_NotFoundResult()
        {
            // Arrange
            int count = 0;
            var controller = GetController(count);

            // Act
            var actionResult = await controller.GetLists();

            // Assert
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }
        #endregion

        // DeleteItem(int itemID)
        // ToDoItem is deleted.
        // All other items remain.
        // itemID incorrect returns error.
    // MethodName_StateUnderTest_ExpectedBehavior:
        [Fact]
        public async void DeleteItem_Returns_NoContent()
        {
            // Arrange
            int count = 4;
            int itemId = 0;
            int listId = 0;
            var controller = GetController(count);

            // Act
            var actionResult = await controller.DeleteItem(listId, itemId);

            // Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }
        
        [Fact]
        public void DeleteItem_Deletes_Item()
        {

        }

        [Fact]
        public void DeleteItem_Deletes_OnlyOneItem()
        {

        }

        [Fact]
        public void DeleteItem_ItemIdOutOfRange_ReturnsBadRequestResult()
        {

        }

        [Fact]
        public void DeleteItem_ListIdOutOfRange_ReturnsBadRequestResult()
        {

        }
        // AddItems(int listID, IEnumerable<ToDoItem> items)
        // Incorrect itemId results in error.
        // Incorrect ListId results in error.
        // One item is added successfully
        // multiple items are added successfully.

        [Fact]
        public void AddItems_Requires_CorrectItemId()
        {

        }

        [Fact]
        public void AddItems_Requires_CorrectListId()
        {

        }

        [Fact]
        public void AddItems_SuccessfullyAdds_OneItem()
        {

        }

        [Fact]
        public void AddItems_SuccessfullyAdds_MultipleItems()
        {

        }

        [Fact]
        public void AddItems_ReturnsCorrect_Responsetype()
        {

        }
        // UpdateItem(ToDoItem item)
        // The Item is updated.
        // Number of items doesn't change.
        // All other items remain the same.



        private static ToDoListController GetController(int count)
        {
            var fakeLists = A.CollectionOfDummy<ToDoList>(count).AsEnumerable();
            var database = A.Fake<IToDoDataService>();
            A.CallTo(() => database.GetTasks()).Returns(Task.FromResult(fakeLists));
            return new ToDoListController(database);
        }
    }
}