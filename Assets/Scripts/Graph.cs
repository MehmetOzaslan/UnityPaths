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
    public GameObject nodeObj;
    public List<Node> nodes;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnDrawGizmos()
    {
        
    }
}


[CustomEditor(typeof(Graph))]
public class GraphEditor : Editor
{
    [SerializeField]
    Bounds bounds = new Bounds();

    private void OnEnable()
    {
        Graph graph = (Graph)target;
        bounds = new Bounds(graph.transform.position, Vector3.one);
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Graph graph = (Graph)target;

        if (GUILayout.Button("New Node"))
        {
            GameObject added = Instantiate(graph.nodeObj, bounds.center + Random.insideUnitSphere, Quaternion.identity, graph.transform);
            graph.nodes.Add(added.GetComponent<Node>());
        }

    }

    private void OnSceneGUI()
    {
        Graph graph = (Graph)target;


        Handles.color = Color.yellow; 
        Handles.DrawWireCube(bounds.center, bounds.size);
        

        foreach (Node node in graph.nodes)
        {
            bounds.Encapsulate(node.transform.position);

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