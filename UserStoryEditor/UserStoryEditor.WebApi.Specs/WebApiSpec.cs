﻿using System.Collections.Generic;

namespace UserStoryEditor.WebApi.Specs
{
    using System;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.DependencyInjection;
    using UserStoryEditor.Core;
    using Xbehave;

    public abstract class WebsiteApiSpec
    {
        private TestServer server;

        protected WebsiteHttpClient Client { get; private set; }

        [Background]
        public void Background()
        {
            "Server is running".x(() =>
                {
                    var serverHostBuilder = new WebHostBuilder();
                    serverHostBuilder
                        .ConfigureServices(s
                            => s
                                .AddSingleton(new ProductBacklog(
                                    new List<string>(),
                                    new FakePersistor(),
                                    new RealFakeEstimatesPersistor())))
                        .UseStartup<Startup>();

                    this.server = new TestServer(serverHostBuilder);
                })
            .Teardown(() =>
                {
                    this.server.Dispose();
                });

            "client is connected to server".x(() =>
                {
                    var httpClient = this.server.CreateClient();
                    httpClient.BaseAddress = new Uri("http://localhost/api/");

                    this.Client = new WebsiteHttpClient(
                        httpClient);
                })
                .Teardown(() =>
                {
                    this.Client.Dispose();
                });
        }
    }
}