using System.IO;

namespace WeCantSpell.Hunspell;

internal static class StreamEx
{
    private const int DefaultBufferSize = 4096;

    public static FileStream OpenReadFileStream(string filePath) =>
        new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, DefaultBufferSize, FileOptions.SequentialScan);

    public static FileStream OpenAsyncReadFileStream(string filePath) =>
        new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, DefaultBufferSize, FileOptions.Asynchronous | FileOptions.SequentialScan);
}
