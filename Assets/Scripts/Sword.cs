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

    Vector2 upColliderOffset = new Vector2(0, 2);
    Vector2 leftColliderOffset = new Vector2(-2, 0);
    Vector2 downColliderOffset = new Vector2(0, -2);
    Vector2 rightColliderOffset = new Vector2(2, 0);

    Vector2 verticalColliderSize = new Vector2(6, 3);
    Vector2 horizontalColliderSize = new Vector2(3, 6);

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
                            cc.offset = upColliderOffset;
                            cc.size = verticalColliderSize;
                            cc.direction = CapsuleDirection2D.Horizontal;
                            break;
                        case 2:
                            anim.Play("SwordAttack1LeftAnim");
                            cc.offset = leftColliderOffset;
                            cc.size = horizontalColliderSize;
                            cc.direction = CapsuleDirection2D.Vertical;
                            break;
                        case 3:
                            anim.Play("SwordAttack1Anim");
                            cc.offset = downColliderOffset;
                            cc.size = verticalColliderSize;
                            cc.direction = CapsuleDirection2D.Horizontal;
                            break;
                        case 4:
                            anim.Play("SwordAttack1RightAnim");
                            cc.offset = rightColliderOffset;
                            cc.size = horizontalColliderSize;
                            cc.direction = CapsuleDirection2D.Vertical;
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
                            cc.offset = upColliderOffset;
                            cc.size = verticalColliderSize;
                            cc.direction = CapsuleDirection2D.Horizontal;
                            break;
                        case 2:
                            anim.Play("SwordAttack2LeftAnim");
                            cc.offset = leftColliderOffset;
                            cc.size = horizontalColliderSize;
                            cc.direction = CapsuleDirection2D.Vertical;
                            break;
                        case 3:
                            anim.Play("SwordAttack2DownAnim");
                            cc.offset = downColliderOffset;
                            cc.size = verticalColliderSize;
                            cc.direction = CapsuleDirection2D.Horizontal;
                            break;
                        case 4:
                            anim.Play("SwordAttack2RightAnim");
                            cc.offset = rightColliderOffset;
                            cc.size = horizontalColliderSize;
                            cc.direction = CapsuleDirection2D.Vertical;
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
