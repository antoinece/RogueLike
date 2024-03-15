using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(RoomGenerator))]
public class RoomGeneratorEditor :  Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        RoomGenerator gen = (RoomGenerator)target;

        if (GUILayout.Button("GenerateRandom"))
        {
            gen.GenerateRa();
        }
        if (GUILayout.Button("GenerateDrunk"))
        {
            gen.GenerateD();
        }
        if (GUILayout.Button("GenerateRule"))
        {
            gen.GenerateRu();
        }
    }
}