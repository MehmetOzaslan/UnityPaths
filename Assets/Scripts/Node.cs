using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Node : MonoBehaviour, IComparable
{

    [SerializeField]
    public List<Node> connections = new List<Node>();

    public int CompareTo(object obj)
    {
        if (obj.GetType() == typeof(Node))
        {
            return (int)GetEdgeWeight((Node)obj);
        }
        else
        {
            throw new NotImplementedException();
        }
    }

    public float GetEdgeWeight(Node node)
    {
        return (transform.position - node.transform.position).magnitude;
    }
}


[CanEditMultipleObjects]
[CustomEditor(typeof(Node))]
public class NodeEditor : GraphEditor
{

    SerializedProperty connections;

    public void OnEnable()
    {
        connections = serializedObject.FindProperty("connections");
    }


    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(connections);
        serializedObject.ApplyModifiedProperties();

    }

    public new void OnSceneGUI()
    {
        base.OnSceneGUI();


        Camera sceneCamera = SceneView.GetAllSceneCameras()[0];

        Node t = (Node)target;
        Node[] otherNodes;

        if (t.transform.GetComponentInParent<Graph>())
        {
            Graph graph = t.transform.GetComponentInParent<Graph>();

            otherNodes = t.transform.transform.parent.GetComponentsInChildren<Node>(); 
        }
        else
        {
            otherNodes = GameObject.FindObjectsOfType<Node>();
        }

        foreach (Node node in otherNodes) { 
            if(node == this) continue;

            //Line Colors for the node's connections
            if (t.connections.Contains(node))
            {
                Handles.color = Color.green;
                Handles.DrawLine(t.transform.position, node.transform.position, 3);
            }

            else
            {
                Handles.color = Color.white;
                Handles.DrawLine(t.transform.position, node.transform.position);
            }

            //Location between two nodes.
            Vector3 middleOfLine = (node.transform.position - t.transform.position) /2 + t.transform.position;

            // Buttons for toggling if a node is connected or not.
            if (Handles.Button(middleOfLine, sceneCamera.transform.rotation, 0.1f, 0.1f, Handles.RectangleHandleCap))
            {
                if (t.connections.Contains(node))
                {
                    t.connections.Remove(node);
                }
                else
                {
                    t.connections.Add(node);
                }
            }
        }
    }
}

