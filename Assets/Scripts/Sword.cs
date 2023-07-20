using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private PlayerBasicAttack baScript;

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
                    anim.Play("SwordAttack1Anim");
                    attackCount++;
                }
                else if (attackCount == 2)
                {
                    sr.enabled = true;
                    cc.enabled = true;
                    anim.Play("SwordAttack2DownAnim");
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
