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

    public float damage = 10.0f;

    private EnemyBase enemyScript;

    Vector2 upColliderOffset = new Vector2(0, 2.5f);
    Vector2 leftColliderOffset =new Vector2(-2, 0);
    Vector2 downColliderOffset = new Vector2(0, -0.5f);
    Vector2 rightColliderOffset = new Vector2(2, 0);

    Vector2 verticalColliderSize = new Vector2(4, 2);
    Vector2 horizontalColliderSize = new Vector2(2, 4);

    private string up1Anim = "SwordAttack1UpAnim";
    private string up2Anim = "SwordAttack2UpAnim";
    private string right1Anim = "SwordAttack1RightAnim";
    private string right2Anim = "SwordAttack2RightAnim";
    private string left1Anim = "SwordAttack1LeftAnim";
    private string left2Anim = "SwordAttack2LeftAnim";
    private string down1Anim = "SwordAttack1Anim";
    private string down2Anim = "SwordAttack2DownAnim";

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
        //AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        //if (stateInfo.IsName("None"))
        //{
            //AttackFinish();
            //sr.enabled = false;
            //cc.enabled = false;
        //}

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
                    RefreshCollider();
                    switch (moveScript.lastwasd)
                    {
                        case 1:
                            anim.Play(up1Anim);
                            cc.offset = upColliderOffset;
                            cc.size = verticalColliderSize;
                            cc.direction = CapsuleDirection2D.Horizontal;
                            break;
                        case 2:
                            anim.Play(left1Anim);
                            cc.offset = leftColliderOffset;
                            cc.size = horizontalColliderSize;
                            cc.direction = CapsuleDirection2D.Vertical;
                            break;
                        case 3:
                            anim.Play(down1Anim);
                            cc.offset = downColliderOffset;
                            cc.size = verticalColliderSize;
                            cc.direction = CapsuleDirection2D.Horizontal;
                            break;
                        case 4:
                            anim.Play(right1Anim);
                            cc.offset = rightColliderOffset;
                            cc.size = horizontalColliderSize;
                            cc.direction = CapsuleDirection2D.Vertical;
                            break;
                    }
                    attackCount++;
                }
                else if (attackCount == 2)
                {
                    
                    switch (moveScript.lastwasd)
                    {
                        case 1:
                            anim.Play(up2Anim);
                            cc.offset = upColliderOffset;
                            cc.size = verticalColliderSize;
                            cc.direction = CapsuleDirection2D.Horizontal;
                            break;
                        case 2:
                            anim.Play(left2Anim);
                            cc.offset = leftColliderOffset;
                            cc.size = horizontalColliderSize;
                            cc.direction = CapsuleDirection2D.Vertical;
                            break;
                        case 3:
                            anim.Play(down2Anim);
                            cc.offset = downColliderOffset;
                            cc.size = verticalColliderSize;
                            cc.direction = CapsuleDirection2D.Horizontal;
                            break;
                        case 4:
                            anim.Play(right2Anim);
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

    public void AttackFinish()
    {
        sr.enabled = false;
        cc.enabled = false;
    }

    IEnumerator OAbility(float duration)
    {
        anim.speed = 2.0f;
        baScript.StartCoroutine(baScript.OAbility(2.0f, duration));
        up1Anim = "FireSwordAttack1UpAnim";
        up2Anim = "FireSwordAttack2UpAnim";
        right1Anim = "FireSwordAttack1RightAnim";
        right2Anim = "FireSwordAttack2RightAnim";
        left1Anim = "FireSwordAttack1LeftAnim";
        left2Anim = "FireSwordAttack2LeftAnim";
        down1Anim = "FireSwordAttack1DownAnim";
        down2Anim = "FireSwordAttack2DownAnim";
        damage = 15.0f;
        yield return new WaitForSeconds(duration);
        anim.speed = originalAS;
        up1Anim = "SwordAttack1UpAnim";
        up2Anim = "SwordAttack2UpAnim";
        right1Anim = "SwordAttack1RightAnim";
        right2Anim = "SwordAttack2RightAnim";
        left1Anim = "SwordAttack1LeftAnim";
        left2Anim = "SwordAttack2LeftAnim";
        down1Anim = "SwordAttack1Anim";
        down2Anim = "SwordAttack2DownAnim";
        damage = 10.0f;
    }

    IEnumerator RefreshCollider()
    {
        sr.enabled = false;
        cc.enabled = false;
        yield return new WaitForSeconds(0.1f);
        sr.enabled = true;
        cc.enabled = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && collision.isTrigger == false)
        {
            GameObject enemyType = collision.gameObject;
            enemyScript.EnemyTakeDamage(enemyType, damage);
            print(damage);
        }
    }

}
