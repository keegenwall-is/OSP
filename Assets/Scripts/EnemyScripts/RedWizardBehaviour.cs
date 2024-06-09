using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedWizardBehaviour : MonoBehaviour
{

    List<Vector2> currentPath;
    List<Vector2> potentialPath;
    public Rigidbody2D rb;
    public bool canMove = true;
    public float moveSpeed = 6.0f;
    private Vector2 moveDirection;
    PathFinding pathFindingScript;
    public GameObject player;
    public float health = 30f;
    public Animator anim;
    private bool firstMove = true;
    private float attackCD = 3.0f;
    private float attackCurrent = 0.0f;
    private bool isAttacking = false;
    public GameObject ghoul;
    private Nodes previousPlayerNode = null;
    private CapsuleCollider2D redWizCapCol;
    private bool newPath = false;

    // Start is called before the first frame update
    void Start()
    {
        currentPath = new List<Vector2>();
        redWizCapCol = GetComponent <CapsuleCollider2D>();
        pathFindingScript = GetComponent<PathFinding>();
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //currentPath = pathfinding function output
        UpdatePath();
        MoveAlongPath();

        attackCurrent += Time.deltaTime;

        if (attackCurrent >= attackCD)
        {
            StartCoroutine(RedWizAttack());
        }
    }

    void FixedUpdate()
    {
        rb.velocity = moveDirection * moveSpeed;
    }

    public void RedWizardTakeDamage(float damage)
    {
        health -= damage;
        //print(health);
        if (health <= 0)
        {
            canMove = false;
            anim.Play("RedWizDeath");
            StartCoroutine(DestroyAfterAnimation());
        }
        else
        {
            //anim.Play("BlueShadowHitAnim");
            //StartCoroutine(KnockBack());
        }
    }

    IEnumerator RedWizAttack()
    {
        attackCurrent = 0f;
        anim.Play("RedWizAttack");
        isAttacking = true;

        yield return new WaitForSeconds(0.1f);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length - 0.1f);

        isAttacking = false;
    }

    IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(0.1f);

        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length - 0.1f);

        Destroy(gameObject);
    }

    public void SpawnGhoul()
    {
        Instantiate(ghoul, this.transform);
    }

    private void UpdatePath()
    {
        Nodes startNode = pathFindingScript.FindNearestNode(this.transform.position);
        Nodes endNode = pathFindingScript.FindFurthestNode(player.transform.position);
        Nodes playerNode = pathFindingScript.FindNearestNode(player.transform.position);
        potentialPath = pathFindingScript.GetPath(startNode, endNode, playerNode);
      
        if (firstMove)
        {
            currentPath = potentialPath;
            newPath = true;
            firstMove = false;
        }

        if (currentPath != null)
        {
            if (currentPath.Count == 0 || currentPath[currentPath.Count - 1] != potentialPath[potentialPath.Count - 1] || previousPlayerNode != playerNode)
            {
                previousPlayerNode = playerNode;
                currentPath = potentialPath;
                newPath = true;
            }
        } else
        {
            previousPlayerNode = playerNode;
            currentPath = potentialPath;
            newPath = true;
        }
        /*foreach (Vector2 node in currentPath)
        {
            Debug.Log("Node position: " + node);
        }*/

        if (currentPath.Count >= 2 && newPath)
        {
            if ((Vector2.Distance(currentPath[0], endNode.transform.position)) > (Vector3.Distance(currentPath[1], endNode.transform.position)))
            {
                currentPath.RemoveAt(0);
                newPath = false;
            }
        }

    }

    private void MoveAlongPath()
    {
        Vector2 targetPos = currentPath[0];
        Vector2 finalTargetPos = currentPath[currentPath.Count - 1];

        if (targetPos == finalTargetPos)
        {
            if (Vector2.Distance(this.transform.position, targetPos) < 0.1f)
            {
                moveDirection = Vector2.zero;
                rb.velocity = Vector2.zero;
                if (!isAttacking)
                {
                    anim.Play("RedWizIdle");
                }
            } else
            {
                moveDirection = (targetPos - (Vector2)this.transform.position).normalized;
                if (!isAttacking)
                {
                    anim.Play("RedWizRun");
                }
            }
            return;
        } else
        {
            moveDirection = (targetPos - (Vector2)this.transform.position).normalized;
            if (!isAttacking)
            {
                anim.Play("RedWizRun");
            }
        }

        if (Vector2.Distance(this.transform.position, targetPos) < 0.1f)
        {
            currentPath.RemoveAt(0);
        }
    }

    /*void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            redWizCapCol.enabled = false;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            redWizCapCol.enabled = true;
        }
    }*/
}
