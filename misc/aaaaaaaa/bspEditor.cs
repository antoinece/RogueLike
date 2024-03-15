using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;



[CustomEditor(typeof(bspGenrator))]

public class BSPLifeEditor: Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        bspGenrator gen = (bspGenrator)target;

        if (GUILayout.Button("Generate"))
        {
            gen.Generated();
        }
    }
       
        
}

