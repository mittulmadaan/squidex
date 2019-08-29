﻿using System;
using System.Reflection;
using DeploymentApp.Entities;
using DeploymentApp.Extensions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.Options;
using Squidex.ClientLibrary;

namespace DeploymentApp
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            var consoleApp = new CommandLineApplication
            {
                Name = "DeploymentApp"
            };

            consoleApp.HelpOption("-?|-h|--help");

            consoleApp.VersionOption("-v|--version", () =>
            {
                return string.Format("Version {0}", Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion);
            });

            var appOption =
                consoleApp.Option("-a|--app <optionvalue>",
                    "App name",
                    CommandOptionType.SingleValue);

            var urlOption =
                consoleApp.Option("-u|--url <optionvalue>",
                    "Cosmos URL",
                    CommandOptionType.SingleValue);

            var identityServerOption =
                consoleApp.Option("-i|--identity <optionvalue>",
                    "Identity server URL",
                    CommandOptionType.SingleValue);

            var clientOption =
                consoleApp.Option("-c|--client <optionvalue>",
                    "Client Name",
                    CommandOptionType.SingleValue);

            var clientSecretOption =
                consoleApp.Option("-s|--secret <optionvalue>",
                    "Client secret",
                    CommandOptionType.SingleValue);

            var skipRuleOption =
                consoleApp.Option("--skip-rules",
                    "Set, to skip generating rules",
                    CommandOptionType.NoValue);

            var createTestDataOption =
                consoleApp.Option("--create-test-data",
                    "Set, to create test data.",
                    CommandOptionType.NoValue);

            consoleApp.OnExecute(async () =>
            {
                var app = 
                    appOption.HasValue() ?
                    appOption.Value() :
                    "commentary";

                var url =
                    urlOption.HasValue() ?
                    urlOption.Value() :
                    "http://localhost:5000";

                var identityServer =
                    identityServerOption.HasValue() ?
                    identityServerOption.Value() :
                    "http://identityservice.systest.tesla.cha.rbxd.ds/connect/token";

                var client =
                    clientOption.HasValue() ?
                    clientOption.Value() :
                    "CMSDeployer";

                var clientSecret =
                    clientSecretOption.HasValue() ?
                    clientSecretOption.Value() :
                    "p@55w0rd";

                var authenticator =
                    new CachingAuthenticator("TOKEN", new MemoryCache(Options.Create(new MemoryCacheOptions())),
                        new IcisAuthenticatorExtensions(
                            identityServer,
                            client,
                            clientSecret));

                var clientManager = new SquidexClientManager(url, app, authenticator, true);

                await clientManager.CreateApp();

                await clientManager.UpsertSchema(Schemas.Commodity);
                await clientManager.UpsertSchema(Schemas.Region);
                await clientManager.UpsertSchema(Schemas.CommentaryType);
                await clientManager.UpsertSchema(Schemas.Commentary(url));

                foreach (var role in Roles.All)
                {
                    await clientManager.UpsertRole(role);
                }

                foreach (var language in Languages.All)
                {
                    await clientManager.UpsertLanguage(language);
                }

                foreach (var contributor in Contributors.All)
                {
                    await clientManager.UpsertContributor(contributor);
                }

                foreach (var workflow in Workflows.All)
                {
                    await clientManager.UpsertWorkflow(workflow);
                }

                if (!skipRuleOption.HasValue())
                {
                    foreach (var rule in Rules.AllKafkaRules)
                    {
                        await clientManager.UpsertKafkaRule(rule);
                    }
                }

                if (createTestDataOption.HasValue())
                {
                    foreach (var (id, name) in TestData.CommentaryTypes)
                    {
                        await clientManager.CreateContentAsync("commentary-type", id, name);
                    }

                    foreach (var (id, name) in TestData.Regions)
                    {
                        await clientManager.CreateContentAsync("region", id, name);
                    }

                    foreach (var (id, name) in TestData.Commodities)
                    {
                        await clientManager.CreateContentAsync("commodity", id, name);
                    }
                }

                return 0;
            });

            try
            {

                return consoleApp.Execute(args);
            }
            catch (CommandParsingException ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to execute application: {0}", ex.Message);
                return -1;
            }
        }
    }
}
