using System.Collections.Generic;
using UnityEditor;
using System.IO;

public class RuleSetGenerator : EditorWindow {

    public class WordList {
        public string[] words;
    }

    static char[,] Convert(string json) {
        string[] words = json.Replace("\"", "").Replace("[", "").Replace("]","").Trim().Split(',');
        char[,] charTable = new char[GetLongestWordLength(words) + 1, words.Length];
        for (int i = 0; i < charTable.GetLength(0); i++) {
            for (int j = 0; j < charTable.GetLength(1); j++) {
                charTable[i, j] = i < words[j].Length - 1 ? words[j][i] : '\0';
            }
        }
        return charTable;
    }

    static int GetLongestWordLength(string[] words) {
        int maxLength = 0;
        foreach (string word in words) {
            if (word.Length > maxLength) {
                maxLength = word.Length;
            }
        }
        return maxLength;
    }

    [MenuItem("Assets/Generate Rule Set")]
    public static RuleSet Generate() {
        return Generate(File.ReadAllText(AssetDatabase.GetAssetPath(Selection.activeObject)));
    }

    public static RuleSet Generate(string json) {
        RuleSet ruleSet = CreateInstance<RuleSet>();
        char[,] charTable = Convert(json);
        double wordCount = charTable.GetLength(1);
        for (int i = 0; i < charTable.GetLength(0); i++) {
            Dictionary<char, double> column = new Dictionary<char, double>();
            Dictionary<char, int> charCounts = new Dictionary<char, int>();
            for (int j = 0; j < charTable.GetLength(1); j++) {
                if (!charCounts.ContainsKey(charTable[i, j])) {
                    charCounts.Add(charTable[i, j], 1);
                } else {
                    charCounts[charTable[i, j]]++;
                }
            }
            foreach (KeyValuePair<char, int> count in charCounts) {
                column.Add(count.Key, count.Value / wordCount);
            }
            ruleSet.characterProbabilities.Add(column);
        }
        AssetDatabase.CreateAsset(ruleSet, "Assets/NewRuleSet.asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        return ruleSet;
    }

}
