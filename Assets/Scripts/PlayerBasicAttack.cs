using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicAttack : MonoBehaviour
{

    public Animator anim;

    public GameObject sword;

    private PlayerMove moveScript;

    private EnemyBase enemyScript;

    private Sword swordScript;

    int attackCount = 1;
    public bool canAttack = true;
    public bool attacking = false;

    private float originalAS;

    // Start is called before the first frame update
    void Start()
    {
        sword = GameObject.Find("Sword");
        anim = GetComponent<Animator>();
        moveScript = GetComponent<PlayerMove>();
        originalAS = anim.speed;
        swordScript = sword.GetComponent<Sword>();

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            enemyScript = enemy.GetComponent<EnemyBase>();
            if (enemyScript != null)
            {
                break;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (canAttack == true)
        {
            if (Input.GetKeyDown("u"))
            {
                if (attackCount == 1)
                {
                    switch (moveScript.lastwasd)
                    {
                        case 1:
                            anim.Play("Attack1UpAnim");
                            break;
                        case 2:
                            anim.Play("Attack1LeftAnim");
                            break;
                        case 3:
                            anim.Play("Attack1DownAnim");
                            break;
                        case 4:
                            anim.Play("Attack1RightAnim");
                            break;
                    }
                    attackCount++;
                }
                else if (attackCount == 2)
                {
                    switch (moveScript.lastwasd)
                    {
                        case 1:
                            anim.Play("Attack2UpAnim");
                            break;
                        case 2:
                            anim.Play("Attack2LeftAnim");
                            break;
                        case 3:
                            anim.Play("Attack2DownAnim");
                            break;
                        case 4:
                            anim.Play("Attack2RightAnim");
                            break;
                    }
                    attackCount--;
                }

            }
        }
    }

    void AttackStart()
    {
        moveScript.canMove = false;
        canAttack = false;
        attacking = true;
    }

    public void AttackFinish()
    {
        moveScript.canMove = true;
        canAttack = true;
        attacking = false;
        swordScript.AttackFinish();
    }

    public IEnumerator OAbility(float newSpeed, float duration)
    {
        anim.speed = newSpeed;

        yield return new WaitForSeconds(duration);
        
        anim.speed = originalAS;
    }
}
