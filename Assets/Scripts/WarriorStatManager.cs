using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorStatManager : MonoBehaviour
{
    public GameObject sword;

    public Animator anim;

    Sword swordScript;

    PlayerBasicAttack baScript;

    PlayerMove playerScript;

    public float health = 100.0f;

    public bool canTakeDamage;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sword = GameObject.Find("Sword");
        swordScript = sword.GetComponent<Sword>();
        baScript = GetComponent<PlayerBasicAttack>();
        playerScript = GetComponent<PlayerMove>();

        canTakeDamage = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(playerScript.canMove);
    }

    public void PlayerTakeDamage(int enemyPos, float damage)
    {
        playerScript.canMove = false;
        if (canTakeDamage)
        {
            health -= damage;
            baScript.canAttack = false;

            switch (enemyPos)
            {
                case 1:
                    anim.Play("Hit1UpAnim");
                    break;
                case 2:
                    anim.Play("Hit1LeftAnim");
                    break;
                case 3:
                    anim.Play("Hit1DownAnim");
                    break;
                case 4:
                    anim.Play("Hit1RightAnim");
                    break;
            }
            StartCoroutine(HitAnim());
            swordScript.AttackFinish();
            baScript.AttackFinish();
        }

        if (health <= 0)
        {
            playerScript.canMove = false;
            StartCoroutine(Death(enemyPos));
        }
    }

    public IEnumerator HitAnim()
    {
        //Debug.Log(playerScript.canMove);

        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length * 100);

        //playerScript.canMove = true;
        baScript.canAttack = true;
    }

    public IEnumerator Death(int enemyPos)
    {
        yield return new WaitForSeconds(0.1f);

        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);

        switch (enemyPos)
        {
            case 1:
                anim.Play("WarriorDeadUp");
                break;
            case 2:
                anim.Play("WarriorDeadLeft");
                break;
            case 3:
                anim.Play("WarriorDeadUp");
                break;
            case 4:
                anim.Play("WarriorDeadRight");
                break;
        }

        yield return new WaitForSeconds(0.1f);

        Time.timeScale = 0.0f;
    }
}
