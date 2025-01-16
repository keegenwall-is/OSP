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
    private bool isSpawning = true;
    private float health = 20.0f;
    private bool isDead = false;
    private bool isDamaged = false;
    private bool isAttacking = false;
    public CapsuleCollider2D AttackCC;
    private float attackDamage = 5.0f;
    private float enemyPosX;
    private float enemyPosY;
    private float playerPosX;
    private float playerPosY;

    WarriorStatManager warriorStatScript;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        AttackCC = GetComponent<CapsuleCollider2D>();
        player = GameObject.Find("Player");
        warriorStatScript = player.GetComponent<WarriorStatManager>();
        playerScript = player.GetComponent<PlayerMove>();
        StartCoroutine(GhoulSpawning());
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(this.transform.position, player.transform.position);

        if (!isSpawning && !isDead && !isDamaged && !isAttacking)
        {
            if (distance < 2.0f)
            {
                StartCoroutine(Attack());
            }
            else
            {
                ghoulPos = new Vector2(this.transform.position.x, this.transform.position.y);
                playerPos = new Vector2(player.transform.position.x, player.transform.position.y + 1.0f);
                moveDirection = new Vector2(playerPos.x - ghoulPos.x, playerPos.y - ghoulPos.y).normalized;
                anim.Play("GhoulIdle");
            }
        }
    }

    void FixedUpdate()
    {
        if (!isDamaged)
        {
            rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        } else
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    IEnumerator GhoulSpawning()
    {
        anim.Play("GhoulSpawn");

        yield return new WaitForSeconds(0.1f);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length - 0.1f);

        isSpawning = false;
    }

    public void GhoulTakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            isDead = true;
            moveDirection = Vector2.zero;
            anim.Play("GhoulDead");
            StartCoroutine(DestroyAfterAnimation());
        }
        else
        {
            StartCoroutine(KnockBack());
            //print("Ghoul Damaged");
        }
    }

    IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(0.1f);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length - 0.1f);

        Destroy(gameObject);
    }

    IEnumerator KnockBack()
    {
        anim.Play("GhoulDamaged");
        isDamaged = true;

        yield return new WaitForSeconds(1.0f);

        isDamaged = false;
    }

    IEnumerator Attack()
    {
        moveDirection = new Vector2(0, 0);
        anim.Play("GhoulAttack");
        isAttacking = true;

        yield return new WaitForSeconds(0.1f);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length - 0.1f);

        isAttacking = false;
    }

    public void AttackCCOn()
    {
        AttackCC.enabled = true;
    }

    public void AttackCCOff()
    {
        AttackCC.enabled = false;
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
            }
            else if (enemyPosX < playerPosX && enemyPosY < playerPosY + 2.5 && enemyPosY > playerPosY - 2.5)
            {
                enemyRelativePos = 2;
            }
            else if (enemyPosY < playerPosY && enemyPosX < playerPosX + 2.5 && enemyPosX > playerPosX - 2.5)
            {
                enemyRelativePos = 3;
            }
            else if (enemyPosX > playerPosX && enemyPosY < playerPosY + 2.5 && enemyPosY > playerPosY - 2.5)
            {
                enemyRelativePos = 4;
            }
            warriorStatScript.PlayerTakeDamage(enemyRelativePos, attackDamage);
        }
    }
}
