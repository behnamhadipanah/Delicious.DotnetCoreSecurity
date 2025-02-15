
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;


ServiceCollection serviceCollection = new ServiceCollection();


var registryKey= Registry.CurrentUser.CreateSubKey(@"SOFTWATE\KEYS\NETSECURITY");
serviceCollection.AddDataProtection()
    .PersistKeysToRegistry(registryKey);
ServiceProvider service = serviceCollection.BuildServiceProvider();


IDataProtectionProvider dataProtectionProvider = service.GetService<IDataProtectionProvider>();

IDataProtector dataProtector = dataProtectionProvider.CreateProtector("HPIsMyKey");

string title = "My name is behnam hadipanah.i am a CSharp Developer";


string protectedTitle = dataProtector.Protect(title);



Console.ReadKey();

