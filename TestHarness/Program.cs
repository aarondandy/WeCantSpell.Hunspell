﻿using Cocona;

using WeCantSpell.Hunspell.TestHarness;

var app = CoconaApp.Create();

app.AddCommand("load", (string file) => LoadTest.LoadDictionary(file));
app.AddCommand("load-all", (string path) => LoadTest.LoadAllDictionaries(path));
app.AddCommand("check", (string dicFile, string wordFile) => CheckTest.Run(dicFile, wordFile));
app.AddCommand("suggest", (string dicFile, string wordFile) => SuggestTest.Run(dicFile, wordFile));
app.AddCommand("suggest-word", (string dicFile, string word) => SuggestWordTest.Run(dicFile, word));
app.AddCommand("issue88", SimpleIssueScenarios.Issue88);

app.Run();
