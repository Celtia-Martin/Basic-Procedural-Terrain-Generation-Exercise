using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof(PerlinTexturesGenerator))]
public class PerlinTextureGeneratorEditor : Editor
{
 
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        PerlinTexturesGenerator myGenerator = (PerlinTexturesGenerator)target;
        if (GUILayout.Button("Generate"))
        {
            myGenerator.RemoveWorld();
            myGenerator.GenerateWorld();
        }
       
        GUILayout.Label("\nWater: "+ BlockInfo.waterValue);
        BlockInfo.waterValue = Mathf.Round(GUILayout.HorizontalSlider(BlockInfo.waterValue, 0, 1)*10f) *0.1f;
 

        GUILayout.Label("\nBeach: " + BlockInfo.beachValue);
        BlockInfo.beachValue = Mathf.Round(GUILayout.HorizontalSlider(BlockInfo.beachValue, 0, Mathf.Max(0, 1 -BlockInfo.waterValue)) * 10f) * 0.1f;

        GUILayout.Label("\nField: "+ BlockInfo.fieldValue);
        BlockInfo.fieldValue = Mathf.Round(GUILayout.HorizontalSlider(BlockInfo.fieldValue, 0, Mathf.Max(0, 1 - (BlockInfo.waterValue+BlockInfo.beachValue))) * 10f) * 0.1f;
       
        GUILayout.Label("\nForest: " + BlockInfo.forestValue);
        BlockInfo.forestValue = Mathf.Round(GUILayout.HorizontalSlider(BlockInfo.forestValue, 0, Mathf.Max(0, 1 - (BlockInfo.waterValue + BlockInfo.beachValue + BlockInfo.fieldValue))) * 10f) * 0.1f;

        BlockInfo.mountainValue = Mathf.Round((1f - (BlockInfo.waterValue + BlockInfo.beachValue + BlockInfo.fieldValue + BlockInfo.forestValue)) * 10f) * 0.1f;
        GUILayout.Label("\nMountain: "+ BlockInfo.mountainValue);
        
        GUILayout.Label("\n ");


        if (10 - (int)((BlockInfo.waterValue + BlockInfo.beachValue)*10) < 0) 
        {
            BlockInfo.beachValue = 0;
        }
        if (10 - (int)((BlockInfo.waterValue + BlockInfo.beachValue + BlockInfo.fieldValue)*10) < 0)
        {
            BlockInfo.fieldValue = 0;
        }
        if (10 - (int)((BlockInfo.waterValue + BlockInfo.beachValue + BlockInfo.fieldValue +BlockInfo.forestValue)*10) < 0)
        {
            BlockInfo.forestValue = 0;
        }
        if (10 -(int)(( BlockInfo.waterValue + BlockInfo.beachValue + BlockInfo.fieldValue + BlockInfo.forestValue +BlockInfo.mountainValue)*10) < 0)
        {
            BlockInfo.mountainValue = 0;
        }
    }
}
