using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    Dictionary<Node, Node> predmap;

    // Start is called before the first frame update
    void Start()
    {
        predmap = new Dictionary<Node, Node>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
