using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.VFX;
using static Unity.VisualScripting.Member;

public class Pathfinding
{

    /*
     * https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm#Algorithm
     */

    public static Dictionary<Node,Node> Dikjstras(Node source)
    {

        //Psuedo-minheap
        ISet<Node> unvisited = new HashSet<Node>();
        Dictionary<Node, float> distanceMap = new Dictionary<Node, float>();
        Dictionary<Node, Node> parent = new Dictionary<Node, Node>();


        //Run BFS to get nodes so we don't have to pass in a graph.
        Dictionary<Node, Node> bfsTree = BFS(source);


        //Initialization
        foreach (Node node in bfsTree.Keys)
        {
            distanceMap[node] = float.MaxValue;
            unvisited.Add(node);
            parent[node] = null;
        }
        distanceMap[source] = 0;



        //Primary steps of the algorthm
        while (unvisited.Count > 0)
        {
            Node minNode = null;
            //Ideally P-Queue here
            foreach (Node comparedNode in unvisited)
            {
                if(minNode == null)
                {
                    minNode = comparedNode;
                }
                else if (distanceMap[comparedNode] < distanceMap[minNode])
                {
                    minNode = comparedNode;
                }
            }
            unvisited.Remove(minNode);


            foreach (Node other in minNode.connections)
            {
                float otherDist = minNode.GetEdgeWeight(other) + distanceMap[minNode];
                //Assign the smaller distance
                if (otherDist < distanceMap[other])
                {
                    distanceMap[other] = otherDist;
                    parent[other] = minNode;
                }
            }
        }

        return parent;
    }

    /*
     * To get the path from the tree of BFS.
     */
    public static List<Node> BacktracePredmap(Dictionary<Node, Node> parent, Node from, Node to)
    {
        List<Node> path = new List<Node>() { to };
        while (path[path.Count - 1] != from)
        {
            path.Append(parent[to]);
        }
        path.Reverse();
        return path;
    }

    public static Dictionary<Node, Node> BFS(Node source)
    {
        Dictionary<Node, Node> predmap = new Dictionary<Node, Node>();
        Queue<Node> q = new Queue<Node>();

        q.Enqueue(source);
        while (q.Count > 0)
        {
            Node currNode = q.Dequeue();
            foreach (Node node in currNode.connections)
            {
                if (!predmap.ContainsKey(node))
                {
                    predmap[node] = currNode;
                    q.Enqueue(node);
                }
            }
        }

        return predmap;
    }

    public static Dictionary<Node, Node> BFS(Node from, Node to)
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

        return parent;
    }

    public static Dictionary<Node, Node> BFS(Node from, System.Func<bool> reachedCondition)
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

        return parent;
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
