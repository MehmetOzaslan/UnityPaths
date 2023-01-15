using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;

public class Graph : MonoBehaviour
{
    public List<Node> nodes;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


[CustomEditor(typeof(Graph))]
public class GraphEditor : Editor
{
    private void OnSceneGUI()
    {
        Graph graph = (Graph)target; 

        foreach (Node node in graph.nodes)
        {
            Handles.DrawWireDisc(node.transform.position, node.transform.position - SceneView.GetAllSceneCameras()[0].transform.position, 0.2f);
            foreach (Node connected in node.connections)
            {
                Handles.DrawLine(node.transform.position, connected.transform.position);
            }
        }
    }
}