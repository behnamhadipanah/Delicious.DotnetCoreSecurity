// See https://aka.ms/new-console-template for more information
using Microsoft.AspNetCore.DataProtection;

Console.WriteLine("Hello, World!");
//save in RAM

string purpose = "SecurityApp.Ephemeral";

var provider = new EphemeralDataProtectionProvider();
var protector = provider.CreateProtector(purpose);
Console.WriteLine("Enter your value : ");
string input = Console.ReadLine();



string protectedPyload = protector.Protect(input);
Console.WriteLine($"Protect returned: {protectedPyload}");

string unProtectedPyload = protector.Unprotect(protectedPyload);
Console.WriteLine($"UnProtect returned: {unProtectedPyload}");




provider = new EphemeralDataProtectionProvider();
protector = provider.CreateProtector(purpose);

 unProtectedPyload = protector.Unprotect(protectedPyload); // throw
