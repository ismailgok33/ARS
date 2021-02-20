using AdessoRideShare.Controllers;
using AdessoRideShare.Data.Access.Repositories;
using AdessoRideShare.Exceptions;
using AdessoRideShare.UniTest.TestParameters;
using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AdessoRideShare.UniTest.Tests
{
    public class TravelApiTest
    {
        private readonly IMapper _mapper;

        public TravelApiTest(IMapper mapper)
        {
            _mapper = mapper;
        }

        [Theory(DisplayName = "Join to Travel")]
        [ClassData(typeof(JoinTravelTestDataWithRandomId))]
        public void JoinTravel_ShouldTravelNotFound_WhenIdIsNotInDb(JoinTravelParameters parameters)
        {
            // Arrange
            var mockRepo = new Mock<TravelRepository>();
            var controller = new TravelController(mockRepo.Object, _mapper);

            var result = Record.Exception(() => controller.JoinTravel(parameters.TestJoinTravelDto));
            //var result = await Record.ExceptionAsync(() => service.AddItemToCart(parameters.TestItemDto, parameters.TestShoppingCart));
            var exception = Assert.IsType<TravelNotFoundException>(result);
        }

        [Theory(DisplayName = "Change Publish Status")]
        [ClassData(typeof(ChangePublishStatusTestDataWithRandomId))]
        public void ChangePublishStatus_ShouldTravelNotFound_WhenIdIsNotInDb(ChangePublishStatusParameters parameters)
        {
            // Arrange
            var mockRepo = new Mock<TravelRepository>();
            var controller = new TravelController(mockRepo.Object, _mapper);

            var result = Record.Exception(() => controller.ChangePublishStatus(parameters.TestChangeStatusDto));
            //var result = await Record.ExceptionAsync(() => service.AddItemToCart(parameters.TestItemDto, parameters.TestShoppingCart));
            var exception = Assert.IsType<TravelNotFoundException>(result);
        }

        [Theory(DisplayName = "Filter Travel By City Info")]
        [ClassData(typeof(FilterTravelByTestDataWithNullValues))]
        public void ChangePublishStatus_ShouCityNotValid_WhenCityFromOrToIsNull(FilterTravelByParameters parameters)
        {
            // Arrange
            var mockRepo = new Mock<TravelRepository>();
            var controller = new TravelController(mockRepo.Object, _mapper);

            var result = Record.Exception(() => controller.FilterTravelBy(parameters.TestFilterTravelDto));
            //var result = await Record.ExceptionAsync(() => service.AddItemToCart(parameters.TestItemDto, parameters.TestShoppingCart));
            var exception = Assert.IsType<TravelNotFoundException>(result);
        }
    }
}
