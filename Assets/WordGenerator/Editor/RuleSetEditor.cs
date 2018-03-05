using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(RuleSet))]
public class RuleSetEditor : Editor {

    RuleSet ruleSet;

    public void OnEnable() {
        ruleSet = (RuleSet)target;
    }

    public override void OnInspectorGUI() {
        foreach (Dictionary<char, double> characterProbabailityColumn in ruleSet.characterProbabilities) {
            foreach (KeyValuePair<char, double> characterProbability in characterProbabailityColumn) {
                if (characterProbability.Key == '\0') {
                    EditorGUILayout.SelectableLabel("[end]:" + characterProbability.Value);
                } else {
                    EditorGUILayout.SelectableLabel(characterProbability.Key + ":" + characterProbability.Value);
                }
            }
        }
    }
}
