﻿using Microsoft.Extensions.Configuration;
using NServiceBus;
using Tracking.Services;
using Tracking.Services.Interfaces;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Tracking.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Tracking.Api.Profiles;

namespace Tracking.NSB
{
    class Program
    {
        static async Task Main()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();



            Console.Title = "Tracking";

            var endpointConfiguration = new EndpointConfiguration("Tracking");

            var containerSettings = endpointConfiguration.UseContainer(new DefaultServiceProviderFactory());
            containerSettings.ServiceCollection.AddScoped<ITrackingService, TrackingService>();
            containerSettings.ServiceCollection.AddScoped<ITrackingRepository, TrackingRepository>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new TrackingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper(); 
            containerSettings.ServiceCollection.AddSingleton(mapper);

            containerSettings.ServiceCollection.AddDbContext<TrackingContext>(
                  options => options.UseSqlServer("Server = C1; Database = Tracking ;Trusted_Connection=True; "));
            //endpointConfiguration.EnableOutbox();
            endpointConfiguration.EnableInstallers();

            //var connection = configuration.GetConnectionString("MeasurePersistanceDB");

            //var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();

            //persistence.SqlDialect<SqlDialect.MsSqlServer>();
            //persistence.ConnectionBuilder(
            //    connectionBuilder: () =>
            //    {
            //        return new SqlConnection(connection);
            //    });

            //var subscription = persistence.SubscriptionSettings();
            //subscription.CacheFor(TimeSpan.FromMinutes(1));

            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            transport.UseConventionalRoutingTopology();
            transport.ConnectionString(configuration.GetConnectionString("RabbitMQConnectionString"));

            var recoverability = endpointConfiguration.Recoverability();
            //recoverability.AddUnrecoverableException<MyBusinessException>();
            //recoverability.CustomPolicy(MyCustomRetryPolicy);            
            recoverability.Immediate(
                immediate =>
                {
                    immediate.NumberOfRetries(3);
                });
            recoverability.Delayed(
                delayed =>
                {
                    var retries = delayed.NumberOfRetries(3);
                    retries.TimeIncrease(TimeSpan.FromSeconds(2));
                });

            endpointConfiguration.SendFailedMessagesTo(GetQueue("errorQueue"));
            endpointConfiguration.AuditProcessedMessagesTo(GetQueue("auditQueue"));
            //endpointConfiguration.AuditSagaStateChanges(
            //        serviceControlQueue: GetQueue("serviceControlQueue"));

            var conventions = endpointConfiguration.Conventions();
            conventions.DefiningCommandsAs(type => type.Namespace == "Messages.Commands");
            conventions.DefiningEventsAs(type => type.Namespace == "Messages.Events");


            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            await endpointInstance.Stop()
                .ConfigureAwait(false);

            string GetQueue(string queueName)
            {
                return configuration.GetSection("Queues").GetSection(queueName).Value;
            }
        }
        //static async Task Main()
        //{
        //    var builder = new ConfigurationBuilder()
        //        .SetBasePath(Directory.GetCurrentDirectory())
        //        .AddJsonFile("appsettings.json");

        //    var configuration = builder.Build();



        //    Console.Title = GetQueue("queueName");

        //    var endpointConfiguration = new EndpointConfiguration(GetQueue("queueName"));

        //    var containerSettings = endpointConfiguration.UseContainer(new DefaultServiceProviderFactory());
        //    containerSettings.ServiceCollection.AddScoped<ITrackingService, TrackingService>();
        //    containerSettings.ServiceCollection.AddScoped<ITrackingRepository, TrackingRepository>();

        //    var mappingconfig = new MapperConfiguration(mc =>
        //    {
        //        mc.AddProfile(new TrackingProfile());
        //    });

        //    IMapper mapper = mappingconfig.CreateMapper();
        //    containerSettings.ServiceCollection.AddSingleton(mapper);

        //    //containerSettings.ServiceCollection.AddDbContext<TrackingContext>(
        //    //      options => options.UseSqlServer("Server = C1; Database = Tracking ;Trusted_Connection=True; "));
        //    //containerSettings.ServiceCollection.AddDbContext<TrackingContext>(
        //    //      options => options.UseSqlServer("Server = C1; Database = Tracking; Trusted_Connection = True; "));
        //    //endpointConfiguration.EnableOutbox();
        //     endpointConfiguration.EnableInstallers();

        //    //var connection = "Server = C1; Database = TrackingPersistance ;Trusted_Connection=True; ";
        //    //var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();

        //    //persistence.SqlDialect<SqlDialect.MsSqlServer>();
        //    //persistence.ConnectionBuilder(
        //    //    connectionBuilder: () =>
        //    //    {
        //    //        return new SqlConnection(connection);
        //    //    });

        //    //var subscription = persistence.SubscriptionSettings();
        //    //subscription.CacheFor(TimeSpan.FromMinutes(1));

        //    var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
        //    transport.UseConventionalRoutingTopology();
        //    transport.ConnectionString("host= localhost:5672;username=guest;password=guest");

        //    var recoverability = endpointConfiguration.Recoverability();
        //    //recoverability.AddUnrecoverableException<MyBusinessException>();
        //    //recoverability.CustomPolicy(MyCustomRetryPolicy);            
        //    recoverability.Immediate(
        //        immediate =>
        //        {
        //            immediate.NumberOfRetries(3);
        //        });
        //    recoverability.Delayed(
        //        delayed =>
        //        {
        //            var retries = delayed.NumberOfRetries(3);
        //            retries.TimeIncrease(TimeSpan.FromSeconds(2));
        //        });

        //    endpointConfiguration.SendFailedMessagesTo(GetQueue("errorQueue"));
        //    endpointConfiguration.AuditProcessedMessagesTo(GetQueue("auditQueue"));
        //    //endpointConfiguration.AuditSagaStateChanges(
        //    //        serviceControlQueue: GetQueue("serviceControlQueue"));

        //    var conventions = endpointConfiguration.Conventions();
        //    conventions.DefiningCommandsAs(type => type.Namespace == "Messages.Commands");
        //    conventions.DefiningEventsAs(type => type.Namespace == "Messages.Events");


        //    var endpointInstance = await Endpoint.Start(endpointConfiguration)
        //        .ConfigureAwait(false);

        //    Console.WriteLine("Press Enter to exit.");
        //    Console.ReadLine();

        //    await endpointInstance.Stop()
        //        .ConfigureAwait(false);

        //    string GetQueue(string queueName)
        //    {
        //        return configuration.GetSection("Queues").GetSection(queueName).Value;
        //    }
        //}
    }
}
