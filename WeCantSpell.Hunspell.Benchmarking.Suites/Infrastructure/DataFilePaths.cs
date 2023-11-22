using System.IO;
using System.Reflection;

namespace WeCantSpell.Hunspell.Benchmarking.MicroSuites.Infrastructure;

static class DataFilePaths
{
    static DataFilePaths()
    {
        TestAssemblyPath = Path.GetFullPath(typeof(DataFilePaths).GetTypeInfo().Assembly.Location);
        TestFilesFolderPath = Path.Combine(Path.GetDirectoryName(TestAssemblyPath), "files");
    }

    public static string TestAssemblyPath { get; }

    public static string TestFilesFolderPath { get; }

    public static string GetDictionaryFilePath(string fileName) => Path.Combine(TestFilesFolderPath, fileName);
}
