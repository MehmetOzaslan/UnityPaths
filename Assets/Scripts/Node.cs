using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Node : MonoBehaviour
{
    public HashSet<Node> connections = new HashSet<Node>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


[CustomEditor(typeof(Node))]
public class NodeEditor : Editor
{
    public void OnSceneGUI()
    {

        Node t = (Node)target;
        Node[] otherNodes;

        if (t.transform.GetComponentInParent<Graph>())
        {
            otherNodes = t.transform.transform.parent.GetComponentsInChildren<Node>();
        }
        else
        {
            otherNodes = GameObject.FindObjectsOfType<Node>();
        }

        foreach (Node node in otherNodes) { 
            if(node == this) continue;

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

            Vector3 middleOfLine = (node.transform.position - t.transform.position) /2 + t.transform.position;
            Camera sceneCamera = SceneView.GetAllSceneCameras()[0];



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
