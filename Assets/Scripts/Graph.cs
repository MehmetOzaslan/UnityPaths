using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;

public class Graph : MonoBehaviour
{
    [SerializeField]
    public GameObject nodeObj;
    [SerializeField]
    public List<Node> nodes;
}


[CustomEditor(typeof(Graph))]
public class GraphEditor : Editor
{

    //Hacky fix because of the desire to render both the graph editor, and any child editors.
    Graph graph {  get { return GetGraph(); } }
    
    Graph GetGraph()
    {
        if (target.GetType() == typeof(Graph))
        {
            return (Graph)target;
        }
        else
        {
            return target.GetComponentInParent<Graph>();
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("New Node"))
        {
            GameObject added = Instantiate(graph.nodeObj, target.GameObject().transform.position + Random.insideUnitSphere, Quaternion.identity, graph.transform);
            graph.nodes.Add(added.GetComponent<Node>());
            Selection.activeGameObject = added;
        }

    }

    protected void OnSceneGUI()
    {
        Handles.color = Color.yellow; 
        
        foreach (Node node in graph.nodes)
        {

            Handles.color = Color.red;
            Handles.DrawWireDisc(node.transform.position, node.transform.position - SceneView.GetAllSceneCameras()[0].transform.position, 0.2f);

            Handles.color = Color.white;
            foreach (Node connected in node.connections)
            {
                Handles.DrawLine(node.transform.position, connected.transform.position, 3);
            }
        }
    }
}