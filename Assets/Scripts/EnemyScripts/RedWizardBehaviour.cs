using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedWizardBehaviour : MonoBehaviour
{

    List<Vector2> currentPath;

    public Rigidbody2D rb;

    public bool canMove = true;

    public float moveSpeed = 6.0f;

    private Vector2 moveDirection;

    PathFinding pathFindingScript;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        currentPath = new List<Vector2>();

        pathFindingScript = GetComponent<PathFinding>();

        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //currentPath = pathfinding function output

        if (canMove == true)
        {
            //getting path away
            //currentPath = pathFindingScript.GetPath(pathFindingScript.FindNearestNode(this.transform.position), pathFindingScript.FindFurthestNode(player.transform.position));
            MoveAlongPath();
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    private void MoveAlongPath()
    {
        if (currentPath.Count == 0)
        {
            return;
        }

        moveDirection = new Vector2(currentPath[currentPath.Count - 1].x - this.transform.position.x, currentPath[currentPath.Count - 1].y - this.transform.position.y).normalized;

        if (Vector2.Distance(this.transform.position, currentPath[currentPath.Count - 1]) == 0)
        {
            currentPath.RemoveAt(currentPath.Count - 1);
        }
    }
}
