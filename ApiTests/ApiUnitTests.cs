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
        private readonly EMController controller;

        public ApiUnitTests()
        {
            controller = GetController();
        }

        #region Get Lists
        [Fact]
        public async void GetLists_Returns_OkResult()
        {
            // Arrange

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

            //Act
            var actionResult = await controller.GetLists();

            // Assert
            var result = actionResult.Result as OkObjectResult;
            var returnLists = result.Value as IEnumerable<EMList>;
            Assert.Equal(count, returnLists.Count());
        }

        [Fact]
        public async void GetLists_ListsReturned_ContainToDoItemsList()
        {
            // Arrange

            // Act
            var actionResult = await controller.GetLists();

            // Assert
            var result = actionResult.Result as OkObjectResult;
            var returnLists = result.Value as IEnumerable<EMList>;
            var items = returnLists.Select(x => x.EMListItems);
            Assert.All(items, i => Assert.IsType<List<EMListItem>>(i));
        }

        [Fact]
        public async void GetLists_Returns_NotFoundResult()
        {
            // Arrange
            int count = 0;
            var testController = GetController(count);

            // Act
            var actionResult = await testController.GetLists();

            // Assert
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }
        #endregion

        #region Delete items
        [Fact]
        public async void DeleteItem_DeleteValidItem_ReturnsNoContent()
        {
            // Arrange
            int itemId = 2;
            int listId = 2;

            // Act
            var actionResult = await controller.DeleteItem(listId, itemId);

            // Assert
            Assert.IsType<NoContentResult>(actionResult);
        }

        [Fact]
        public async void DeleteItem_DeleteInvalidItem_ReturnsNotFound()
        {
            // Arrange
            int itemId = 5;
            int listId = 5;

            // Act
            var actionResult = await controller.DeleteItem(listId, itemId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(actionResult);
        }

        [Fact]
        public async void DeleteItem_BadItemId_ReturnsBadRequestResult()
        {
            // Arrange
            int itemId = 0;
            int listId = 2;

            // Act
            var actionResult = await controller.DeleteItem(listId, itemId);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public async void DeleteItem_BadListId_ReturnsBadRequestResult()
        {
            // Arrange
            int itemId = 2;
            int listId = -2;

            // Act
            var actionResult = await controller.DeleteItem(listId, itemId);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }
        #endregion

        #region Add items
        [Fact]
        public async void AddItems_ListIdInvalid_ReturnsBadRequest()
        {
            // Arrange
            int listId = 0;
            var validItem = A.Fake<IEnumerable<EMListItem>>();

            // Act
            var actionResult = await controller.AddItems(listId, validItem);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public async void AddItems_ValidInput_ReturnsOk()
        {
            // Arrange
            int listId = 1;
            var validItem = new List<EMListItem>();
            validItem.Add(A.Fake<EMListItem>());

            // Act
            var actionResult = await controller.AddItems(listId, validItem);

            // Assert
            Assert.IsType<OkResult>(actionResult);
        }

        [Fact]
        public async void AddItems_NonExistantList_ReturnsNotFound()
        {
            // Arrange
            int listId = 5;
            var validItem = A.Fake<IEnumerable<EMListItem>>();

            // Act
            var actionResult = await controller.AddItems(listId, validItem);

            // Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }
        #endregion

        #region Update Item
        [Fact]
        public async void UpdateItem_NullItem_ReturnsBadRequest()
        {
            // Arrange
            EMListItem item = null;

            // Act
            var actionResult = await controller.UpdateItem(item);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public async void UpdateItem_ValidInput_ReturnsOk()
        {
            // Arrange
            var validItem = A.Fake<EMListItem>();
            validItem.EMListItemId = 2;

            // Act
            var actionResult = await controller.UpdateItem(validItem);

            // Assert
            Assert.IsType<OkResult>(actionResult);
        }

        [Fact]
        public async void UpdateItem_BadItemId_ReturnsNotFound()
        {
            // Arrange
            var invalidItem = A.Fake<EMListItem>();
            invalidItem.EMListItemId = 0;

            // Act
            var actionResult = await controller.UpdateItem(invalidItem);

            // Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }
        #endregion

        private static EMController GetController(int count = 4)
        {
            var fakeLists = A.CollectionOfDummy<EMList>(count).AsEnumerable();
            var database = A.Fake<IEMDataService>();

            A.CallTo(() => database.GetTasks()).Returns(Task.FromResult(fakeLists));

            A.CallTo(() => database.Delete(A<int>.Ignored, A<int>.Ignored))
                .ReturnsLazily((int listId, int itemId) =>
                    listId <= count && itemId <= count ? 1 : 0);

            A.CallTo(() => database.ListAddItems(
                A<int>.Ignored, A<IEnumerable<EMListItem>>.Ignored))
                .ReturnsLazily((int listId, IEnumerable<EMListItem> items) =>
                listId <= count && items.Any() ? 1 : 0);

            A.CallTo(() => database.UpdateItem(A<EMListItem>.Ignored))
                .ReturnsLazily((EMListItem item) => item.EMListItemId > 0 ? 1 : 0);

            return new EMController(database);
        }
    }
}