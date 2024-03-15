using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(WFCAnalysyer))]

public class WFCAnalysyerEditor: Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        WFCAnalysyer gen = (WFCAnalysyer)target;

        if (GUILayout.Button("Generate"))
        {
            gen.analyze();
        }
    }

}
