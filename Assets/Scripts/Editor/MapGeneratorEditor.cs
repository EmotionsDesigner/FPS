using UnityEngine;
using System.Collections;
using UnityEditor;

//wybieramy, który skrypt dostosowywujemy
[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor {



	//Custom Editor, własny edytor do generowania ścian (ulepszony)





    //nadpisujemy funkcję wywoływaną za każdym razem gdy Unity rysuje Gui
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        //target automatycznie zawiera referencję do obiektu typu MapGenerator
        MapGenerator myScript = (MapGenerator)target;
        GUILayoutOption width = GUILayout.Width(50);
        GUILayoutOption height = GUILayout.Height(20);

        GUILayout.BeginHorizontal();
            if(GUILayout.Button("UP", GUILayout.Width(100), height))
                myScript.GenerateUp();
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();       
            if (GUILayout.Button("LEFT", width, height))
                 myScript.GenerateLeft();
            if (GUILayout.Button("RIGHT", width, height))
                 myScript.GenerateRight();
         GUILayout.EndHorizontal();

         GUILayout.BeginHorizontal();
            if (GUILayout.Button("DOWN", GUILayout.Width(100), height))
                 myScript.GenerateDown();
         GUILayout.EndHorizontal();
       
    }
}
