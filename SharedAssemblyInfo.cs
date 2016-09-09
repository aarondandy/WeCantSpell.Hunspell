using System.Reflection;

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#elif RELEASE
[assembly: AssemblyConfiguration("Release")]
#else
[assembly: AssemblyConfiguration("Unknown")]
#endif

[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Hunspell.NetCore")]
[assembly: AssemblyCopyright("")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]