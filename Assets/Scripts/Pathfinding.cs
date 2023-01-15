using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;
using static Unity.VisualScripting.Member;

public class Pathfinding : MonoBehaviour
{

    /*
     * https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm#Algorithm
     */

    Dictionary<Node,Node> Dikjstras(Graph graph, Node source)
    {
        HashSet<Node> unvisited = new HashSet<Node>();
        Dictionary<Node, float> distanceMap = new Dictionary<Node, float>();
        Dictionary<Node, Node> parent = new Dictionary<Node, Node>();
        
        //Initialization
        distanceMap[source] = 0;
        Node current = source;
        foreach (Node node in graph.nodes)
        {
            distanceMap[node] = float.MaxValue;
            unvisited.Add(node);
            parent[node] = null;
        }

        //Primary steps of the algorthm
        while (unvisited.Count > 0)
        {
            //Ideally P-Queue here
            foreach (Node minNode in unvisited)
            {
                if (distanceMap[minNode] < distanceMap[current])
                {
                    current = minNode;
                }
            }
            unvisited.Remove(current);

            foreach (Node other in current.connections)
            {
                float otherDist = current.GetEdgeWeight(other) + distanceMap[current];
                //Assign the smaller distance
                if (otherDist < distanceMap[other])
                {
                    distanceMap[other] = otherDist;
                    parent[other] = current;
                }
            }
        }

        return parent;
    }

    /*
     * To get the path from the tree of BFS.
     */
    List<Node> BacktracePredmap(Dictionary<Node, Node> parent, Node from, Node to)
    {
        List<Node> path = new List<Node>() { to };
        while (path[path.Count - 1] != from)
        {
            path.Append(parent[to]);
        }
        path.Reverse();
        return path;
    }

    Dictionary<Node, Node> BFS(Node from, Node to)
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
                    if (node == to)
                    {
                        return parent;
                    }
                }
            }
        }

        return null;
    }

    Dictionary<Node, Node> BFS(Node from, System.Func<bool> reachedCondition)
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
                        return parent;
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
