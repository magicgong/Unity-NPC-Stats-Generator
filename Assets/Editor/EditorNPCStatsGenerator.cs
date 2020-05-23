using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

[CustomEditor(typeof(NPCStatsGenerator))]
public class EditorNPCStatsGenerator : Editor
{
    NPCStatsGenerator npcStatsGenerator;
    string filePath = "Assets/Data/";
    string fileName = "StatsEnum";

    private void OnEnable()
    {
        npcStatsGenerator = (NPCStatsGenerator)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Save"))
        {
            EditorMethods.WriteToEnum(filePath, fileName, npcStatsGenerator.stats);
        }

        int max = npcStatsGenerator.GetStatPoints();

        int i = 0;
        foreach (string stat in npcStatsGenerator.stats)
        {

            if (npcStatsGenerator.statMinConstraints.ElementAtOrDefault(i).Name != null)
            {
                StatConstraint scMin = npcStatsGenerator.statMinConstraints[i];
                scMin.Value = EditorGUILayout.IntField(stat + " Min", scMin.Value);
                npcStatsGenerator.statMinConstraints[i] = scMin;
            }
            else
            {
                StatConstraint statConstraint = new StatConstraint(stat + "Min", 0);
                npcStatsGenerator.statMinConstraints.Add(statConstraint);
            }

            if (npcStatsGenerator.statMaxConstraints.ElementAtOrDefault(i).Name != null)
            {
                StatConstraint scMax = npcStatsGenerator.statMaxConstraints[i];
                scMax.Value = EditorGUILayout.IntField(stat + " Max", scMax.Value);
                npcStatsGenerator.statMaxConstraints[i] = scMax;
            }
            else
            {
                StatConstraint statConstraint = new StatConstraint(stat + "Max", max);
                npcStatsGenerator.statMaxConstraints.Add(statConstraint);
            }
            ++i;
        }

    }

}
