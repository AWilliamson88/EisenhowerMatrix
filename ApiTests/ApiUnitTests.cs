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
        public async void GetLists_ListsReturned_ContainToDoItemsList()
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
        public async void DeleteItem_DeleteValidItem_ReturnsNoContent()
        {
            // Arrange
            int count = 4;
            int itemId = 2;
            int listId = 2;
            var controller = GetController(count);

            // Act
            var actionResult = await controller.DeleteItem(listId, itemId);

            // Assert
            Assert.IsType<NoContentResult>(actionResult);
        }

        [Fact]
        public async void DeleteItem_DeleteInvalidItem_ReturnsNotFound()
        {
            // Arrange
            int count = 4;
            int itemId = 5;
            int listId = 5;
            var controller = GetController(count);

            // Act
            var actionResult = await controller.DeleteItem(listId, itemId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(actionResult);
        }

        [Fact]
        public async void DeleteItem_BadItemId_ReturnsBadRequestResult()
        {
            // Arrange
            int count = 4;
            int itemId = 0;
            int listId = 2;
            var controller = GetController(count);

            // Act
            var actionResult = await controller.DeleteItem(listId, itemId);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public async void DeleteItem_BadListId_ReturnsBadRequestResult()
        {
            // Arrange
            int count = 4;
            int itemId = 2;
            int listId = -2;
            var controller = GetController(count);

            // Act
            var actionResult = await controller.DeleteItem(listId, itemId);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }
        
        // AddItems(int listID, IEnumerable<ToDoItem> items)
        // Incorrect itemId results in error.
        // Incorrect ListId results in error.
        // One item is added successfully
        // multiple items are added successfully.
        [Fact]
        public async void AddItems_ListIdInvalid_ReturnsBadRequest()
        {
            // Arrange
            int count = 4;
            int listId = 0;
            var validItem = A.Fake<IEnumerable<ToDoItem>>();
            var controller = GetController(count);

            // Act
            var actionResult = await controller.PutAddItems(listId, validItem);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public async void AddItems_ValidInput_ReturnsOk()
        {
            // Arrange
            int count = 4;
            int listId = 1;
            var validItem = new List<ToDoItem>();
            validItem.Add(A.Fake<ToDoItem>());
            var controller = GetController(count);

            // Act
            var actionResult = await controller.PutAddItems(listId, validItem);

            // Assert
            Assert.IsType<OkResult>(actionResult);
        }

        [Fact]
        public async void AddItems_NonExistantList_ReturnsNotFound()
        {
            // Arrange
            int count = 4;
            int listId = 5;
            var validItem = A.Fake<IEnumerable<ToDoItem>>();
            var controller = GetController(count);

            // Act
            var actionResult = await controller.PutAddItems(listId, validItem);

            // Assert
            Assert.IsType<NotFoundResult>(actionResult);
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
            A.CallTo(() => database.Delete(A<int>.Ignored, A<int>.Ignored))
                .ReturnsLazily((int listId, int itemId) =>
                    listId <= count && itemId <= count ? 1 : 0);
            A.CallTo(() => database.ListAddItems(
                A<int>.Ignored, A<IEnumerable<ToDoItem>>.Ignored))
                .ReturnsLazily((int listId, IEnumerable<ToDoItem> items) => 
                listId <= count && items.Any() ? 1 : 0);

            return new ToDoListController(database);
        }
    }
}