using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulBehaviour : MonoBehaviour
{

    public float moveSpeed;

    public float distance;

    public Rigidbody2D rb;

    private Vector2 moveDirection;

    private Vector2 ghoulPos;

    private Vector2 playerPos;

    public Animator anim;

    public GameObject player;

    PlayerMove playerScript;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(this.transform.position, player.transform.position);

        if (distance < 3.0f)
        {
            moveDirection = new Vector2(0, 0);
            anim.Play("GhoulAttack");
        } else
        {
            ghoulPos = new Vector2(this.transform.position.x, this.transform.position.y);
            playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
            moveDirection = new Vector2(playerPos.x - ghoulPos.x, playerPos.y - ghoulPos.y).normalized;
            anim.Play("GhoulIdle");
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
