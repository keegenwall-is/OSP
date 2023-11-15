using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameCircle : MonoBehaviour
{

    public Animator anim;
    public float cooldown = 3.0f;
    public float cdtimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
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
            }
        }
    }
}
