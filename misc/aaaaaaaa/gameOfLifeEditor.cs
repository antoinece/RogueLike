

using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;



[CustomEditor(typeof(gameOfLife))]

    public class gameOfLifeEditor: Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            gameOfLife gen = (gameOfLife)target;

            if (GUILayout.Button("Generate"))
            {
                gen.Genetate();
            }
            if (GUILayout.Button("life"))
            {
                gen.life();
            }
        }
       
        
    }
