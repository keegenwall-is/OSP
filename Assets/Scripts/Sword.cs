using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private PlayerBasicAttack baScript;

    private PlayerMove moveScript;

    public Animator anim;

    public GameObject player;

    public SpriteRenderer sr;

    public CapsuleCollider2D cc;

    int attackCount = 1;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
        baScript = player.GetComponent<PlayerBasicAttack>();
        moveScript = player.GetComponent<PlayerMove>();
        sr = GetComponent<SpriteRenderer>();
        cc = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (baScript.canAttack == true)
        {
            if (Input.GetKeyDown("u"))
            {
                if (attackCount == 1)
                {
                    sr.enabled = true;
                    cc.enabled = true;
                    switch (moveScript.lastwasd)
                    {
                        case 1:
                            anim.Play("SwordAttack1UpAnim");
                            break;
                        case 2:
                            anim.Play("SwordAttack1LeftAnim");
                            break;
                        case 3:
                            anim.Play("SwordAttack1Anim");
                            break;
                        case 4:
                            anim.Play("SwordAttack1RightAnim");
                            break;
                    }
                    attackCount++;
                }
                else if (attackCount == 2)
                {
                    sr.enabled = true;
                    cc.enabled = true;
                    switch (moveScript.lastwasd)
                    {
                        case 1:
                            anim.Play("SwordAttack2UpAnim");
                            break;
                        case 2:
                            anim.Play("SwordAttack2LeftAnim");
                            break;
                        case 3:
                            anim.Play("SwordAttack2DownAnim");
                            break;
                        case 4:
                            anim.Play("SwordAttack2RightAnim");
                            break;
                    }
                    attackCount--;
                }
            }
        }
    }

    void AttackFinish()
    {
        sr.enabled = false;
        cc.enabled = false;
    }

}
