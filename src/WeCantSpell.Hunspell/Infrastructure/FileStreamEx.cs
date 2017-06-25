using System.IO;

namespace WeCantSpell.Hunspell.Infrastructure
{
    internal static class FileStreamEx
    {
        private const int DefaultBufferSize = 4096;

        public static FileStream OpenReadFileStream(string filePath) =>
            new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, DefaultBufferSize, FileOptions.SequentialScan);

#if !NO_ASYNC
        public static FileStream OpenAsyncReadFileStream(string filePath) =>
            new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, DefaultBufferSize, FileOptions.Asynchronous | FileOptions.SequentialScan);
#endif
    }
}
