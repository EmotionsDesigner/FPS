using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(WallsDesigner))]
public class ObjectBuilderEditor : Editor
{

    //Custom Editor, własny edytor do generowania korytarzy podczas pracy na scenie



    public override void OnInspectorGUI()
    {
 
        DrawDefaultInspector();

        WallsDesigner myScript = (WallsDesigner)target;
        if(GUILayout.Button("Build Corridor"))
        {
            myScript.BuildCorridor();
        }
    }
}