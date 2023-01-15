using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class SimpleBFS : MonoBehaviour
{
    public List<Node> nodes;

    List<Node> GetBFSPath(Dictionary<Node, Node> parent, Node from, Node to) {
        List<Node> path = new List<Node>(){to};
        while (path[path.Count - 1] != from)
        {
            path.Append(parent[to]);
        }
        path.Reverse();
        return path;
    }

    List<Node> BFS(Node from, Node to)
    {
        Dictionary<Node, Node> parent = new Dictionary<Node, Node>();
        Queue<Node> q = new Queue<Node>();

        q.Enqueue(from);
        while (q.Count > 0)
        {   
            Node currNode = q.Dequeue();
            foreach (Node node in currNode.connections)
            {
                if(!parent.ContainsKey(node))
                {
                    parent[node] = currNode;
                    q.Enqueue(node);
                    if(node == to)
                    {
                        return GetBFSPath(parent, from, to);
                    }
                }
            }
        }

        return null;
    }

    List<Node> BFS(Node from, System.Func<bool> reachedCondition)
    {
        Dictionary<Node, Node> parent = new Dictionary<Node, Node>();
        Queue<Node> q = new Queue<Node>();

        q.Enqueue(from);
        while (q.Count > 0)
        {
            Node currNode = q.Dequeue();
            foreach (Node node in currNode.connections)
            {
                if (!parent.ContainsKey(node))
                {
                    parent[node] = currNode;
                    q.Enqueue(node);
                    if (reachedCondition())
                    {
                        return GetBFSPath(parent, from, node);
                    }
                }
            }
        }

        return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
