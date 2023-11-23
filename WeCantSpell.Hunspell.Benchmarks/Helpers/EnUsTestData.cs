using System.IO;

namespace WeCantSpell.Hunspell.Benchmarks.Helpers;

public static class EnUsTestData
{
    public static WordList WordList { get; private set; }
    public static CategorizedWordData Data { get; private set; }
    public static string FilePathDic => DataFilePaths.GetDictionaryFilePath("English (American).dic");
    public static string FilePathAff => Path.ChangeExtension(FilePathDic, "aff");

    static EnUsTestData()
    {
        WordList = CreateDictionary();
        Data = CategorizedWordData.Create(CategorizedWordData.GetAssortedEnUsWords(), WordList);
    }

    public static WordList CreateDictionary() => WordList.CreateFromFiles(FilePathDic);
}
