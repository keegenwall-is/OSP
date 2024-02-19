using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathFinding : MonoBehaviour
{

    public string nodeTag = "Node";
    private Nodes[] allNodes;
    private float distance;
    private List<Nodes> openSet = new List<Nodes>();
    private Dictionary<Nodes, float> gScores = new Dictionary<Nodes, float>();
    private Dictionary<Nodes, float> hScores = new Dictionary<Nodes, float>();
    private Dictionary<Nodes, Nodes> cameFrom = new Dictionary<Nodes, Nodes>();

    // Start is called before the first frame update
    void Start()
    {
        //allNodes = GameObject.FindGameObjectsWithTag(nodeTag);
        allNodes = GameObject.FindGameObjectsWithTag(nodeTag).Select(go => go.GetComponent<Nodes>()).ToArray();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Nodes FindNearestNode(Vector2 TargetPos)
    {
        float minDistance = float.MaxValue;
        Nodes closestNode = null;
        foreach (Nodes Node in allNodes)
        {
            distance = Vector2.Distance(this.transform.position, Node.transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                closestNode = Node;
            }
        }
        return closestNode;
    }

    public Nodes FindFurthestNode(Vector2 TargetPos)
    {
        float maxDistance = 0.0f;
        Nodes furthestNode = null;
        foreach (Nodes Node in allNodes)
        {
            //distance = Vector2.Distance(this.transform.position, Node.Transform.position);

            if (distance > maxDistance)
            {
                maxDistance = distance;
                furthestNode = Node;
            }
        }
        return furthestNode;
    }

    public List<Vector2> GetPath(Nodes startNode, Nodes endNode)
    {
        //a* algorithm for pathfinding

        //open set is a list of nodes that need to be explored
        openSet.Add(startNode);

        //
        gScores.Add(startNode, 0);
        hScores.Add(startNode, Vector2.Distance(this.transform.position, endNode.transform.position));
        cameFrom.Add(startNode, null);

        while (openSet.Count != 0)
        {
            Nodes currentNode = openSet[0];

            for (int i = 1; i < openSet.Count; i++)
            {
                if (gScores[openSet[i]] + hScores[openSet[i]] < gScores[currentNode] + hScores[currentNode])
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);

            if (currentNode == endNode)
            {
                return ReconstructPath(cameFrom, endNode);
            }

            foreach (Nodes connectedNode in currentNode.connectedNodes)
            {
                if (!connectedNode) continue;
                float tentativegScore = gScores[currentNode] + Vector2.Distance(currentNode.transform.position, connectedNode.transform.position);

                if (!gScores.ContainsKey(connectedNode))
                {
                    gScores.Add(connectedNode, float.MaxValue);
                    hScores.Add(connectedNode, Vector2.Distance(connectedNode.transform.position, endNode.transform.position));
                    cameFrom.Add(connectedNode, null);
                }

                if (tentativegScore < gScores[connectedNode])
                {
                    cameFrom[connectedNode] = currentNode;
                    gScores[connectedNode] = tentativegScore;

                    if (!openSet.Contains(connectedNode))
                    {
                        openSet.Add(connectedNode);
                    }
                }
            }
        }
        return new List<Vector2>();
    }

    public List<Vector2> ReconstructPath(Dictionary<Nodes, Nodes> cameFromMap, Nodes endNode)
    {
        List<Vector2> nodeLocations = new List<Vector2>();

        Nodes nextNode = endNode;
        while (nextNode)
        {
            nodeLocations.Add(nextNode.transform.position);
            nextNode = cameFromMap[nextNode];
        }

        return nodeLocations;
    }
}
