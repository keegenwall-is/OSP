using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicAttack : MonoBehaviour
{

    public Animator anim;

    public GameObject sword;

    private PlayerMove moveScript;

    int attackCount = 1;
    public bool canAttack = true;
    public bool attacking = false;

    // Start is called before the first frame update
    void Start()
    {
        sword = GameObject.Find("Sword");
        anim = GetComponent<Animator>();
        moveScript = GetComponent<PlayerMove>();
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
                    anim.Play("Attack1DownAnim");
                    attackCount++;
                }
                else if (attackCount == 2)
                {
                    anim.Play("Attack2DownAnim");
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

    void AttackFinsih()
    {
        moveScript.canMove = true;
        canAttack = true;
        attacking = false;
    }
}
