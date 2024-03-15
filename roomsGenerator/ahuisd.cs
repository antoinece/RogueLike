using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(DrunkYardGenerator))]
public class weujhuf :  Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        DrunkYardGenerator gen = (DrunkYardGenerator)target;

        if (GUILayout.Button("Generate"))
        {
            gen.GeneratePath();
        }
    }
       
        
}