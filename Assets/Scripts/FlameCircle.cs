using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameCircle : MonoBehaviour
{

    public Animator anim;
    public float cooldown = 3.0f;
    public float cdtimer = 0.0f;

    public CapsuleCollider2D cc;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        cc = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        cdtimer = cdtimer - 1 * Time.deltaTime;
        if (cdtimer < 0.0f)
        {
            cdtimer = 0.0f;
        }

        if (Input.GetKeyDown("i"))
        {
            if (cdtimer <= 0.0f)
            {
                anim.Play("FlameCircleAnim");
                cdtimer = cooldown;
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
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);

        cc.enabled = false;
    }
}