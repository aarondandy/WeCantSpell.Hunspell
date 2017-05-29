#if !NO_IO_FILE

using System.IO;

namespace WeCantSpell.Hunspell.Infrastructure
{
    internal static class FileStreamEx
    {
        private const int DefaultBufferSize = 4096;

        public static FileStream OpenReadFileStream(string filePath) =>
            new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, DefaultBufferSize);

#if !NO_ASYNC
        public static FileStream OpenAsyncReadFileStream(string filePath) =>
            new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, DefaultBufferSize, FileOptions.Asynchronous);
#endif

    }
}

#endif
