using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRuleSet.asset", menuName = "Word Generator/Rule Set", order = 0)]
public class RuleSet : ScriptableObject {

    public List<Dictionary<char,double>> characterProbabilities;

    private void OnEnable() {
        if (characterProbabilities == null) characterProbabilities = new List<Dictionary<char, double>>();
    }

    void OnGUI() {
        foreach(Dictionary<char,double> characterProbabailityColumn in characterProbabilities) {
            foreach (KeyValuePair<char,double> characterProbability in characterProbabailityColumn) {
                EditorGUILayout.SelectableLabel(characterProbability.Key+":"+characterProbability.Value);            }
        }
    }

}
