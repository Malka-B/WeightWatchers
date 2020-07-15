using System;
using Xunit;
using Subscriber.Services;
using Subscriber.Services.Models;
using Subscriber.Data;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Subscriber.Data.Profiles;
using Microsoft.EntityFrameworkCore;

namespace XUnitTestWeightWatchers
{
    public class SubscriberUnitTest
    {
        static MapperConfiguration mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new SubscriberProfile());
        });

        IMapper mapper = mappingConfig.CreateMapper();


        [Fact]
        public async void GetCardAsync_CardIdNotExist_GotNull()
        {
            var optionsBuilder = new DbContextOptionsBuilder<WeightWatchersContext>()
        .UseSqlServer("Server = C1; Database = WeightWatchersDB ;Trusted_Connection=True; ").Options;
            WeightWatchersContext weightWatchersContext = new WeightWatchersContext(optionsBuilder);


            var subscriberService = new SubscriberService(new SubscriberRepository(weightWatchersContext, mapper), mapper);

            //Act - Call the method being tested
            var cardExist = await subscriberService.GetCardAsync(12);

            //Assert            
            Assert.Null(cardExist);


        }

        [Fact]
        public async void GetCardAsync_CardExist_GotCardModel()
        {
            var optionsBuilder = new DbContextOptionsBuilder<WeightWatchersContext>()
        .UseSqlServer("Server = C1; Database = WeightWatchersDB ;Trusted_Connection=True; ").Options;
            WeightWatchersContext weightWatchersContext = new WeightWatchersContext(optionsBuilder);


            var subscriberService = new SubscriberService(new SubscriberRepository(weightWatchersContext, mapper), mapper);

            //Act - Call the method being tested
            var card = await subscriberService.GetCardAsync(1);

            //Assert            
            Assert.IsType<CardModel>(card);


        }

        [Fact]
        public async void LoginAsync_EmailExist_GotMinus1()
        {
            var optionsBuilder = new DbContextOptionsBuilder<WeightWatchersContext>()
         .UseSqlServer("Server = C1; Database = WeightWatchersDB ;Trusted_Connection=True; ").Options;
            WeightWatchersContext weightWatchersContext = new WeightWatchersContext(optionsBuilder);


            var subscriberService = new SubscriberService(new SubscriberRepository(weightWatchersContext, mapper), mapper);
            //Act - Call the method being tested
            var isEmailExist = await subscriberService.LoginAsync("dk@gmail.com", "4545");

            //Assert            
            Assert.Equal(isEmailExist,-1);


        }
    }
}
