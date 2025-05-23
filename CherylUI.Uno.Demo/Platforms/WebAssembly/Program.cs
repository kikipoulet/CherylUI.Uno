using Uno.UI.Hosting;
using CherylUI.Uno.Demo;

var host = UnoPlatformHostBuilder.Create()
    .App(() => new App())
    .UseWebAssembly()
    .Build();

await host.RunAsync();