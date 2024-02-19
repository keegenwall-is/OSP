using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodes : MonoBehaviour
{

    public Nodes[] connectedNodes;
    public string nodeTag = "Node";
    public float maxDistance = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] allNodes = GameObject.FindGameObjectsWithTag(nodeTag);

        // List is used as they are dynamic and can be changed if an unknown amount of nodes need to be added
        List<Nodes> nodesWithinDistance = new List<Nodes>();

        foreach (GameObject nodeObj in allNodes)
        {
            Nodes node = nodeObj.GetComponent<Nodes>();
            if (node != this)
            {
                float distance = Vector2.Distance(transform.position, node.transform.position);

                if (distance <= maxDistance)
                {
                    nodesWithinDistance.Add(node);
                }
            }
        }

        connectedNodes = nodesWithinDistance.ToArray();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
