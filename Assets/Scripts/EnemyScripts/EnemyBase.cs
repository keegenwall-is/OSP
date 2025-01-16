using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{

    public GameObject player;

    public SpriteRenderer sr;

    public Animator anim;

    PlayerMove playerScript;

    public ElementalShadowBehaviour shadowScript;
    public RedWizardBehaviour redWizScript;
    public GhoulBehaviour ghoulScript;
    private bool lookRight = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerMove>();
        anim = GetComponent<Animator>();
        //shadowScript = GetComponent<ElementalShadowBehaviour>();
        ghoulScript = GetComponent<GhoulBehaviour>();
        if (this.name.Contains("Ghoul"))
        {
            lookRight = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (lookRight)
        {
            if (player.transform.position.x < this.transform.position.x)
            {
                sr.flipX = true;
            }
            else
            {
                sr.flipX = false;
            }
        } else
        {
            if (player.transform.position.x > this.transform.position.x)
            {
                sr.flipX = true;
            }
            else
            {
                sr.flipX = false;
            }
        }


        if (player.transform.position.y < this.transform.position.y)
        {
            sr.sortingOrder = 2;
        } else
        {
            sr.sortingOrder = 4;
        }
    }

    public void EnemyTakeDamage(GameObject enemyType, float damage)
    {
        if (enemyType.name.Contains("Shadow"))
        {
            shadowScript = enemyType.GetComponent<ElementalShadowBehaviour>();
            shadowScript.ShadowTakeDamage(damage);
        } else if (enemyType.name.Contains("RedWizard"))
        {
            redWizScript = enemyType.GetComponent<RedWizardBehaviour>();
            redWizScript.RedWizardTakeDamage(damage);
        } else if (enemyType.name.Contains("Ghoul"))
        {
            ghoulScript = enemyType.GetComponent<GhoulBehaviour>();
            ghoulScript.GhoulTakeDamage(damage);
        } else
        {
            print("no enemy detected");
        }
    }
}
