// See https://aka.ms/new-console-template for more information
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography;
using System.Text;

Console.WriteLine("Hello, World!");


// add data protection service
ServiceCollection serviceCollection = new ServiceCollection();
serviceCollection.AddDataProtection();

ServiceProvider service = serviceCollection.BuildServiceProvider();

// create an instance of IDataProtection
IDataProtectionProvider dataProtectionProvider = service.GetService<IDataProtectionProvider>();

IDataProtector dataProtector = dataProtectionProvider.CreateProtector("HPIsMyKey");


// Protect and UnProtect a payload


string title = "My name is behnam hadipanah.i am a CSharp Developer";

Console.WriteLine($"Original value= {title}");

string protectedTitle = dataProtector.Protect(title);
Console.WriteLine($"Protected value= {protectedTitle}");


string unProtectedTitle = dataProtector.Unprotect(protectedTitle);
Console.WriteLine($"UnProtected value= {unProtectedTitle}");
Console.ReadKey();

Console.WriteLine($"*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
string password = "12345@behnam";
byte[] salt = new byte[128 / 8];

using (var random = RandomNumberGenerator.Create())
{
    random.GetBytes(salt);
}
string hashPaswword = Convert.ToBase64String(KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA256, 10000, 256 / 8));
Console.WriteLine($"password value= {password}");
Console.WriteLine($"Hash password value= {hashPaswword}");


Console.ReadKey();

Console.WriteLine($"*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
dataProtector = dataProtectionProvider.CreateProtector("HPIsMyKeyLifTime");
ITimeLimitedDataProtector timeLimitDataProtector = dataProtector.ToTimeLimitedDataProtector();

Console.WriteLine($"Original value= {title}");


protectedTitle = timeLimitDataProtector.Protect(title,
    lifetime: TimeSpan.FromSeconds(10));
Console.WriteLine($"Protected Title value= {protectedTitle}");




unProtectedTitle = timeLimitDataProtector.Unprotect(protectedTitle);

Console.WriteLine($"UnProtected value= {unProtectedTitle}");
Console.WriteLine($"Waiting 12 second ...");

Thread.Sleep(11000);

try
{
    unProtectedTitle = timeLimitDataProtector.Unprotect(protectedTitle);
    Console.WriteLine($"UnProtected value= {unProtectedTitle}");
}
catch (Exception ex)
{

    Console.WriteLine(ex.Message);
}


Console.ReadKey();



Console.WriteLine($"*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");

serviceCollection.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(@"C:\encryptKyes"))
    .ProtectKeysWithDpapi();
service = serviceCollection.BuildServiceProvider();




// create an instance of IDataProtection
dataProtectionProvider = service.GetService<IDataProtectionProvider>();

dataProtector = dataProtectionProvider.CreateProtector("HPIsMyKey");

Console.WriteLine($"Original value= {title}");

protectedTitle = dataProtector.Protect(title);
Console.WriteLine($"Protected value= {protectedTitle}");


unProtectedTitle = dataProtector.Unprotect(protectedTitle);
Console.WriteLine($"UnProtected value= {unProtectedTitle}");

var keyManager = service.GetService<IKeyManager>();
keyManager.RevokeAllKeys(DateTimeOffset.Now);
try
{
    protectedTitle = dataProtector.Protect(title);
    Console.WriteLine($"Protected value= {protectedTitle}");
}
catch (Exception ex)
{

    Console.WriteLine(ex.Message);
}
Console.ReadKey();




Console.WriteLine($"*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
// return data without key
try
{
    IPersistedDataProtector persistedDataProtector = dataProtector as IPersistedDataProtector;
    if (persistedDataProtector is null)
        Console.WriteLine("Could not Convert PersistedDataProtector");

    byte[] titleByte = Encoding.UTF8.GetBytes(title);

    var protectedTitleByte = dataProtector.Protect(titleByte);

      Console.WriteLine($"Protected value= {Convert.ToBase64String(protectedTitleByte)}");



    var unProtectedTitleByte = dataProtector.Unprotect(protectedTitleByte);

    Console.WriteLine($"unProtected value= {Encoding.UTF8.GetString(unProtectedTitleByte)}");


    bool requiresMigration, wasRevoed;
   var unProtectedPayload = persistedDataProtector.DangerousUnprotect(
       protectedTitleByte,
       ignoreRevocationErrors: true,
       requiresMigration:out requiresMigration,
       wasRevoked:out wasRevoed
       );



    Console.WriteLine($"requiresMigration value= {requiresMigration}");
    Console.WriteLine($"wasRevoed value= {wasRevoed}");

    Console.WriteLine($"UnProtectedPayload value= {Encoding.UTF8.GetString(unProtectedPayload)}");
}
catch (Exception ex)
{

    Console.WriteLine(ex.Message);

}
Console.ReadKey();


Console.WriteLine($"*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
//Get All information encrypt key

var keys=keyManager.GetAllKeys();

foreach (var key in keys)
{
    Console.WriteLine($"key id : {key.KeyId} , created : {key.CreationDate}," +
        $"Expiration : {key.ExpirationDate}, is revoked:{key.IsRevoked}");
}
Console.WriteLine($"*****************************************************");

keyManager.RevokeAllKeys(DateTimeOffset.Now);

keys = keyManager.GetAllKeys();

foreach (var key in keys)
{
    Console.WriteLine($"key id : {key.KeyId} , created : {key.CreationDate}," +
        $"Expiration : {key.ExpirationDate}, is revoked:{key.IsRevoked}");
}


Console.ReadKey();
