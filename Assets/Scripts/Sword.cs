using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sword : MonoBehaviour
{
    private PlayerBasicAttack baScript;

    private PlayerMove moveScript;

    public Animator anim;

    public GameObject player;

    public SpriteRenderer sr;

    public CapsuleCollider2D cc;

    public Text OCd;

    int attackCount = 1;

    private float originalAS;

    public float cooldown = 6.0f;
    public float cdtimer = 0.0f;

    Vector2 upColliderOffset = new Vector2(0, 2);
    Vector2 leftColliderOffset =new Vector2(-2, 0);
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
        originalAS = anim.speed;
    }

    // Update is called once per frame
    void Update()
    {
        OCd.text = cdtimer.ToString();

        cdtimer = cdtimer - 1 * Time.deltaTime;


        if (cdtimer < 0.0f)
        {
            cdtimer = 0.0f;
            OCd.text = "";
        }

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
            if (Input.GetKeyDown("o"))
            {
                if (cdtimer <= 0)
                {
                    StartCoroutine(OAbility(3f));
                    cdtimer = cooldown;
                }
            }
        }
    }

    void AttackFinish()
    {
        sr.enabled = false;
        cc.enabled = false;
    }

    IEnumerator OAbility(float duration)
    {
        anim.speed = 2.0f;
        baScript.StartCoroutine(baScript.OAbility(2.0f, duration));
        yield return new WaitForSeconds(duration);
        anim.speed = originalAS;
    }

}
