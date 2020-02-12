// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;

namespace Microsoft.Extensions.Hosting
{
    /// <summary>
    /// Provides convenience methods for creating instances of <see cref="IHostBuilder"/> with pre-configured defaults.
    /// </summary>
    public static class Host
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HostBuilder"/> class with pre-configured defaults.
        /// </summary>
        /// <remarks>
        ///   The following defaults are applied to the returned <see cref="HostBuilder"/>:
        ///   <list type="bullet">
        ///     <item><description>set the <see cref="IHostEnvironment.ContentRootPath"/> to the result of <see cref="Directory.GetCurrentDirectory()"/></description></item>
        ///     <item><description>load host <see cref="IConfiguration"/> from "DOTNET_" prefixed environment variables</description></item>
        ///     <item><description>load app <see cref="IConfiguration"/> from 'appsettings.json' and 'appsettings.[<see cref="IHostEnvironment.EnvironmentName"/>].json'</description></item>
        ///     <item><description>load app <see cref="IConfiguration"/> from User Secrets when <see cref="IHostEnvironment.EnvironmentName"/> is 'Development' using the entry assembly</description></item>
        ///     <item><description>load app <see cref="IConfiguration"/> from environment variables</description></item>
        ///     <item><description>configure the <see cref="ILoggerFactory"/> to log to the console, debug, and event source output</description></item>
        ///     <item><description>enables scope validation on the dependency injection container when <see cref="IHostEnvironment.EnvironmentName"/> is 'Development'</description></item>
        ///   </list>
        /// </remarks>
        /// <returns>The initialized <see cref="IHostBuilder"/>.</returns>
        public static IHostBuilder CreateDefaultBuilder() =>
            CreateDefaultBuilder(args: null);

        /// <summary>
        /// Initializes a new instance of the <see cref="HostBuilder"/> class with pre-configured defaults.
        /// </summary>
        /// <remarks>
        ///   The following defaults are applied to the returned <see cref="HostBuilder"/>:
        ///   <list type="bullet">
        ///     <item><description>set the <see cref="IHostEnvironment.ContentRootPath"/> to the result of <see cref="Directory.GetCurrentDirectory()"/></description></item>
        ///     <item><description>load host <see cref="IConfiguration"/> from "DOTNET_" prefixed environment variables</description></item>
        ///     <item><description>load host <see cref="IConfiguration"/> from supplied command line args</description></item>
        ///     <item><description>load app <see cref="IConfiguration"/> from 'appsettings.json' and 'appsettings.[<see cref="IHostEnvironment.EnvironmentName"/>].json'</description></item>
        ///     <item><description>load app <see cref="IConfiguration"/> from User Secrets when <see cref="IHostEnvironment.EnvironmentName"/> is 'Development' using the entry assembly</description></item>
        ///     <item><description>load app <see cref="IConfiguration"/> from environment variables</description></item>
        ///     <item><description>load app <see cref="IConfiguration"/> from supplied command line args</description></item>
        ///     <item><description>configure the <see cref="ILoggerFactory"/> to log to the console, debug, and event source output</description></item>
        ///     <item><description>enables scope validation on the dependency injection container when <see cref="IHostEnvironment.EnvironmentName"/> is 'Development'</description></item>
        ///   </list>
        /// </remarks>
        /// <param name="args">The command line args.</param>
        /// <returns>The initialized <see cref="IHostBuilder"/>.</returns>
        public static IHostBuilder CreateDefaultBuilder(string[] args)
        {
            var builder = new HostBuilder();

            builder.UseContentRoot(Directory.GetCurrentDirectory());
            // 加载主机配置，只是委托，不会执行
            builder.ConfigureHostConfiguration(config =>
            {
                // 加载环境变量
                config.AddEnvironmentVariables(prefix: "DOTNET_");
                if (args != null)
                {
                    // 加载命令行参数
                    config.AddCommandLine(args);
                }
            });

            // 应用配置，ASP.NET Core 一个叫主机（对象，当前应用的所有资源），一个叫应用
            builder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                var env = hostingContext.HostingEnvironment;

                // 加载应用配置文件
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                      .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

                
                if (env.IsDevelopment() && !string.IsNullOrEmpty(env.ApplicationName))
                {
                    var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
                    if (appAssembly != null)
                    {
                        config.AddUserSecrets(appAssembly, optional: true);
                    }
                }

                config.AddEnvironmentVariables();

                if (args != null)
                {
                    config.AddCommandLine(args);
                }
            })
                // 配置默认的日志组件
            .ConfigureLogging((hostingContext, logging) =>
            {
                var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

                // IMPORTANT: This needs to be added *before* configuration is loaded, this lets
                // the defaults be overridden by the configuration.
                if (isWindows)
                {
                    // Default the EventLogLoggerProvider to warning or above
                    logging.AddFilter<EventLogLoggerProvider>(level => level >= LogLevel.Warning);
                }

                // 启用配置
                logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                // 日志提供程序
                logging.AddConsole();
                logging.AddDebug();
                logging.AddEventSourceLogger();

                if (isWindows)
                {
                    // Add the EventLogLoggerProvider on windows machines
                    logging.AddEventLog();
                }
            })

                // 默认的服务容器
            .UseDefaultServiceProvider((context, options) =>
            {
                var isDevelopment = context.HostingEnvironment.IsDevelopment();
                options.ValidateScopes = isDevelopment;
                options.ValidateOnBuild = isDevelopment;
            });

            return builder;
        }
    }
}
