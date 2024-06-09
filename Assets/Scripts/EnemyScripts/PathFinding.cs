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

    public Nodes FindNearestNode(Vector2 targetPos)
    {
        float minDistance = float.MaxValue;
        Nodes closestNode = null;
        foreach (Nodes Node in allNodes)
        {
            distance = Vector2.Distance(targetPos, Node.transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                closestNode = Node;
            }
        }
        return closestNode;
    }

    public Nodes FindSecondNearestNode(Vector2 targetPos)
    {
        float minDistance = float.MaxValue;
        float secondMinDistance = float.MaxValue;
        Nodes nearestNode = null;
        Nodes secondNearestNode = null;

        foreach (Nodes node in allNodes)
        {
            float distance = Vector2.Distance(targetPos, node.transform.position);

            if (distance < minDistance)
            {
                secondMinDistance = minDistance;
                secondNearestNode = nearestNode;

                minDistance = distance;
                nearestNode = node;
            }
            else if (distance < secondMinDistance)
            {
                secondMinDistance = distance;
                secondNearestNode = node;
            }
        }

        return secondNearestNode;
    }

    public Nodes FindFurthestNode(Vector2 targetPos)
    {
        float maxDistance = 0.0f;
        Nodes furthestNode = null;
        foreach (Nodes Node in allNodes)
        {
            distance = Vector2.Distance(targetPos, Node.transform.position);

            if (distance > maxDistance)
            {
                maxDistance = distance;
                furthestNode = Node;
            }
        }
        //print(furthestNode);
        return furthestNode;
    }

    public List<Vector2> GetPath(Nodes startNode, Nodes endNode, Nodes excludedNode = null)
    {
        openSet.Clear();
        gScores.Clear();
        hScores.Clear();
        cameFrom.Clear();
        //a* algorithm for pathfinding

        //open set is a list of nodes that need to be explored
        openSet.Add(startNode);

        gScores[startNode] = 0;
        hScores[startNode] = Vector2.Distance(startNode.transform.position, endNode.transform.position);
        cameFrom[startNode] = null;

        while (openSet.Count > 0)
        {
            Nodes currentNode = openSet.OrderBy(node => gScores[node] + hScores[node]).First();

            if (currentNode == endNode)
            {
                return ReconstructPath(cameFrom, endNode);
            }

            openSet.Remove(currentNode);

            foreach (Nodes connectedNode in currentNode.connectedNodes)
            {
                if (connectedNode == null || connectedNode == excludedNode) continue;
                float tentativegScore = gScores[currentNode] + Vector2.Distance(currentNode.transform.position, connectedNode.transform.position);

                if (!gScores.ContainsKey(connectedNode))
                {
                    gScores[connectedNode] = float.MaxValue;
                    hScores[connectedNode] = Vector2.Distance(connectedNode.transform.position, endNode.transform.position);
                    cameFrom[connectedNode] = null;
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

            /*for (int i = 1; i < openSet.Count; i++)
            {
                if (gScores[openSet[i]] + hScores[openSet[i]] < gScores[currentNode] + hScores[currentNode])
                {
                    currentNode = openSet[i];
                }
            }*/

        }
        return new List<Vector2>();
    }

    public List<Vector2> ReconstructPath(Dictionary<Nodes, Nodes> cameFromMap, Nodes endNode)
    {
        List<Vector2> nodeLocations = new List<Vector2>();
        Nodes currentNode = endNode;

        while (currentNode != null)
        {
            nodeLocations.Add(currentNode.transform.position);
            cameFromMap.TryGetValue(currentNode, out currentNode);
        }

        nodeLocations.Reverse();
        return nodeLocations;
    }
}
