using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalShadowBehaviour : MonoBehaviour
{

    public float moveSpeed = 6.0f;

    public float AttackMSDebuff = 3.0f;

    private bool isAttacking = false;

    public float circleRadius = 20.0f;

    public bool canMove = true;

    public float distance;

    public Rigidbody2D rb;

    public CircleCollider2D cc;

    private Vector2 moveDirection;

    private Vector2 shadowPos;

    private Vector2 playerPos;

    public float angleOffset = 0.0f;

    public Animator anim;

    public GameObject player;

    PlayerMove playerScript;

    WarriorStatManager warriorStatScript;
    
    private float enemyPosX;
    private float enemyPosY;
    private float playerPosX;
    private float playerPosY;

    public float attackDamage = 40;

    public float health = 60.0f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerMove>();
        warriorStatScript = player.GetComponent<WarriorStatManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove == true)
        {
            distance = Vector2.Distance(this.transform.position, player.transform.position);

            shadowPos = new Vector2(this.transform.position.x, this.transform.position.y);
            playerPos = new Vector2(player.transform.position.x, player.transform.position.y);

            if (distance < 5.0f)
            {
                if (!isAttacking)
                {
                    StartCoroutine(MSDebuff());
                    moveDirection = new Vector2(playerPos.x - shadowPos.x, playerPos.y - shadowPos.y).normalized;
                    anim.Play("BlueShadowAttack");
                }
            }
            else if (distance > 20.0f)
            {
                anim.Play("BlueShadowIdle");
                moveDirection = new Vector2(0, 0);
            }
            else
            {
                if (!isAttacking)
                {
                    moveDirection = new Vector2(playerPos.x - shadowPos.x, playerPos.y - shadowPos.y).normalized;
                    //Vector2 circleOffset = new Vector2(Mathf.Cos(Time.time) * circleRadius, Mathf.Sin(Time.time) * circleRadius);
                    moveDirection = (playerPos - shadowPos).normalized;
                    anim.Play("BlueShadowMove");
                }
            }
        } else
        {
            moveDirection = new Vector2(0, 0);
        }
        
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    IEnumerator MSDebuff()
    {
        isAttacking = true;
        moveSpeed = moveSpeed - AttackMSDebuff;

        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);

        moveSpeed = moveSpeed + AttackMSDebuff;
        isAttacking = false;
    }

    IEnumerator AttackCC()
    {
        cc.enabled = true;

        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length/3);

        cc.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player") && collision.isTrigger == false)
        {
            //Debug.Log("Player entered the attack trigger!");
            int enemyRelativePos = 0;
            enemyPosX = this.transform.position.x;
            enemyPosY = this.transform.position.y;
            playerPosX = collision.transform.position.x;
            playerPosY = collision.transform.position.y;
            if (enemyPosY > playerPosY && enemyPosX < playerPosX + 2 && enemyPosX > playerPosX - 2)
            {
                enemyRelativePos = 1;
            } else if (enemyPosX < playerPosX && enemyPosY < playerPosY + 2.5 && enemyPosY > playerPosY - 2.5)
            {
                enemyRelativePos = 2;
            } else if (enemyPosY < playerPosY && enemyPosX < playerPosX + 2.5 && enemyPosX > playerPosX - 2.5)
            {
                enemyRelativePos = 3;
            } else if (enemyPosX > playerPosX && enemyPosY < playerPosY + 2.5 && enemyPosY > playerPosY - 2.5)
            {
                enemyRelativePos = 4;
            }
            warriorStatScript.PlayerTakeDamage(enemyRelativePos, attackDamage);
        }
    }

    public void ShadowTakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            canMove = false;
            anim.Play("BlueShadowDeath");
            StartCoroutine(DestroyAfterAnimation());
        } else
        {
            anim.Play("BlueShadowHitAnim");
            StartCoroutine(KnockBack());
        }
    }

    IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length * 4 / 5);

        Destroy(gameObject);
    }

    IEnumerator KnockBack()
    {
        canMove = false;

        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length / 2);

        canMove = true;
    }
}
