using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Node> connections = new List<Node>();

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
        
    }
}
