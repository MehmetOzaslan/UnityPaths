using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Agent : Node
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

}




[CustomEditor(typeof(Agent))]
class AgentEditor : NodeEditor
{



    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Agent t = (Agent)target;
        if (GUILayout.Button("Source To All BFS"))
        {
            predMap = Pathfinding.BFS;
        }
        if (GUILayout.Button("Source To All Dikjstra"))
        {
            predMap = Pathfinding.Dikjstras;
        }


    }

    Func<Node , Dictionary<Node, Node>> predMap = Pathfinding.Dikjstras;
    protected new void OnSceneGUI()
    {
        base.OnSceneGUI();

        Agent t = (Agent)target;


        if(predMap(t) != null)
        {
            foreach (var pair in predMap(t))
            {
                Handles.color = Color.cyan;
                if(pair.Value != null && pair.Key != null)
                    Handles.DrawLine(pair.Value.transform.position, pair.Key.transform.position, 4);
            }
        }

 

    }
}

