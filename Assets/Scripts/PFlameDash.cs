using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PFlameDash : MonoBehaviour
{

    public Animator ballAnim;
    public Renderer sRenderer;
    public GameObject player;
    public Rigidbody2D rbody;
    public GameObject expl;
    public Text PCd;
    private Vector3 startPos;
    private Vector3 endPos;
    private PlayerMove moveScript;
    private FireExpl explScript;
    public int noOfExpl = 5;
    public int maxCharges = 3;
    private int chargesLeft = 3;
    private float cooldown = 1.0f;
    public float cdtimer = 0.0f;
    public float abilityDuration = 2.0f;
    public float adtimer = 0.0f;
    private float dashDuration = 0.1f;
    private float dashSpeed = 125.0f;
    private bool abilityUsed = false;
    public CircleCollider2D cc;
    public float damage = 40.0f;
    private EnemyBase enemyScript;
    private WarriorStatManager warriorStatScript;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rbody = player.GetComponent<Rigidbody2D>();
        sRenderer = GetComponent<Renderer>();
        ballAnim = GetComponent<Animator>();
        moveScript = player.GetComponent<PlayerMove>();
        explScript = player.GetComponent<FireExpl>();
        cc = GetComponent<CircleCollider2D>();
        warriorStatScript = player.GetComponent<WarriorStatManager>();
        //adtimer = abilityDuration + 1.0f;

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
        PCd.text = cdtimer.ToString();
        
        cdtimer = cdtimer - 1 * Time.deltaTime;

        if (cdtimer < 0.0f)
        {
            cdtimer = 0.0f;
            PCd.text = "";
        }

        if (adtimer >= 0.01)
        {
            PCd.text = adtimer.ToString();
        }

        if (abilityUsed == true)
        {
            adtimer = adtimer + 1 * Time.deltaTime;
        }

        if (adtimer > abilityDuration)
        {
            adtimer = 0.0f;
            cdtimer = cooldown;
            abilityUsed = false;
        }

        if (chargesLeft == 0)
        {
            cdtimer = cooldown;
            chargesLeft = maxCharges;
            abilityUsed = false;
        }

        if (cdtimer > 0)
        {
            chargesLeft = maxCharges;
        }

        if (chargesLeft > 0)
        {
            if (Input.GetKeyDown("p"))
            {
                if (cdtimer <= 0.0f)
                {
                    abilityUsed = true;
                    if (chargesLeft == maxCharges)
                    {
                        adtimer = 0.0f;
                    }
                    ballAnim.Play("FlameBallAnim");
                    cc.enabled = true;
                    StartCoroutine(CCOff());
                    chargesLeft--;
                    switch (moveScript.lastwasd)
                    {
                        case 1:
                            sRenderer.sortingOrder = 5;
                            break;
                        case 2:
                            sRenderer.sortingOrder = 6;
                            break;
                        case 3:
                            sRenderer.sortingOrder = 7;
                            break;
                        case 4:
                            sRenderer.sortingOrder = 6;
                            break;
                    }
                    moveScript.isDashing = true;
                    startPos = player.transform.position;
                    StartCoroutine(Dash());
                }
            }
        } 
    }

    IEnumerator CCOff()
    {
        warriorStatScript.canTakeDamage = false;

        yield return new WaitForSeconds(ballAnim.GetCurrentAnimatorStateInfo(0).length);

        cc.enabled = false;
        warriorStatScript.canTakeDamage = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && collision.isTrigger == false)
        {
            GameObject enemyType = collision.gameObject;
            enemyScript.EnemyTakeDamage(enemyType, damage);
        }
    }

    IEnumerator Dash()
    {
        moveScript.moveSpeed += dashSpeed;
        yield return new WaitForSeconds(dashDuration);
        moveScript.moveSpeed -= dashSpeed;
        moveScript.isDashing = false;

        //Controls the spawn location of the fire balls
        endPos = player.transform.position;
        Vector3 direction = (endPos - startPos).normalized;
        float dashLength = Vector3.Distance(startPos, endPos);
        float spacing = dashLength / noOfExpl;

        // Spawn explosions in a line from startPos to endPos
        for (int i = 0; i < noOfExpl; i++)
        {
            Vector3 explPos = startPos + direction * spacing * i;
            GameObject fireExplCopy = Instantiate(expl, explPos, Quaternion.identity);
        }

        adtimer = 0.0f;
    }
}
