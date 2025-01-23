using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

public class FlameCircle : MonoBehaviour
{

    public Animator anim;
    public Text ICd;
    public float cooldown = 3.0f;
    public float cdtimer = 0.0f;

    public CapsuleCollider2D cc;

    public float damage = 20;

    private EnemyBase enemyScript;

    private PlayerMove playerScript;

    public GameObject player;

    public GameObject expl;

    public EventReference flameCircleSound;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
        cc = GetComponent<CapsuleCollider2D>();
        playerScript = player.GetComponent<PlayerMove>();

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

        ICd.text = cdtimer.ToString();

        cdtimer = cdtimer - 1 * Time.deltaTime;
        if (cdtimer < 0.0f)
        {
            cdtimer = 0.0f;
            ICd.text = "";
        }

        if (Input.GetKeyDown("i"))
        {
            if (cdtimer <= 0.0f)
            {
                AudioManager.instance.PlayOneShot(flameCircleSound, this.transform.position);
                anim.Play("FlameCircleAnim");
                cdtimer = cooldown;
                Instantiate(expl, player.transform.position, Quaternion.identity);
                if (cc != null)
                {
                    cc.enabled = true;
                    StartCoroutine(CCOff());
                }
            }
        }
    }

    IEnumerator CCOff()
    {
        playerScript.canMove = false;
        yield return new WaitForSeconds(0.1f);

        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length - 0.15f);

        playerScript.canMove = true;
        cc.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && collision.isTrigger == false)
        {
            GameObject enemyType = collision.gameObject;
            enemyScript.EnemyTakeDamage(enemyType, damage);
        }
    }
}
