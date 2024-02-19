using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{

    public GameObject player;

    public SpriteRenderer sr;

    public Animator anim;

    PlayerMove playerScript;

    ElementalShadowBehaviour shadowScript;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerMove>();
        anim = GetComponent<Animator>();
        shadowScript = GetComponent<ElementalShadowBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x < this.transform.position.x)
        {
            sr.flipX = true;
        } else
        {
            sr.flipX = false;
        }

        if (player.transform.position.y < this.transform.position.y)
        {
            sr.sortingOrder = 2;
        } else
        {
            sr.sortingOrder = 4;
        }
    }

    public void EnemyTakeDamage(string enemyType, float damage)
    {
        if (enemyType.Contains("Shadow"))
        {
            shadowScript.ShadowTakeDamage(damage);
        }
    }
}
