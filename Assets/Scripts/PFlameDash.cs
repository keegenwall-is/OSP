using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PFlameDash : MonoBehaviour
{

    public Animator ballAnim;

    public Renderer sRenderer;

    public GameObject player;

    public GameObject expl;

    private Vector3 startPos;
    private Vector3 endPos;

    private PlayerMove moveScript;

    private FireExpl explScript;

    public int noOfExpl = 5;

    public int maxCharges = 3;
    private int chargesLeft = 3;

    public float cooldown = 19.0f;
    public float cdtimer = 0.0f;

    public float abilityDuration = 2.0f;
    public float adtimer = 0.0f;

    public float dashDistance = 10.0f;

    private bool abilityUsed = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        sRenderer = GetComponent<Renderer>();
        ballAnim = GetComponent<Animator>();
        moveScript = player.GetComponent<PlayerMove>();
        explScript = player.GetComponent<FireExpl>();
        //adtimer = abilityDuration + 1.0f;
    }

    // Update is called once per frame
    void Update()
    {

        cdtimer = cdtimer - 1 * Time.deltaTime;

        if (cdtimer < 0.0f)
        {
            cdtimer = 0.0f;
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
                    chargesLeft--;
                    switch (moveScript.lastwasd)
                    {
                        case 1:
                            sRenderer.sortingOrder = 5;
                            startPos = player.transform.position;
                            player.transform.position += Vector3.up * dashDistance;
                            endPos = player.transform.position;
                            for (int i = 0; i < noOfExpl; i++)
                            {
                                GameObject fireExplCopy = Instantiate(expl);
                                Vector3 explPos = startPos;
                                explPos.y = endPos.y - i * 2;
                                explPos.x = endPos.x;
                                fireExplCopy.transform.position = explPos;
                            }
                            adtimer = 0.0f;
                            break;
                        case 2:
                            sRenderer.sortingOrder = 6;
                            startPos = player.transform.position;
                            player.transform.position += Vector3.left * dashDistance;
                            endPos = player.transform.position;
                            for (int i = 0; i < noOfExpl; i++)
                            {
                                GameObject fireExplCopy = Instantiate(expl);
                                Vector3 explPos = startPos;
                                explPos.x = endPos.x + i * 2;
                                explPos.y = endPos.y + 1;
                                fireExplCopy.transform.position = explPos;
                            }
                            adtimer = 0.0f;
                            break;
                        case 3:
                            sRenderer.sortingOrder = 7;
                            startPos = player.transform.position;
                            player.transform.position += Vector3.down * dashDistance;
                            endPos = player.transform.position;
                            for (int i = 0; i < noOfExpl; i++)
                            {
                                GameObject fireExplCopy = Instantiate(expl);
                                Vector3 explPos = startPos;
                                explPos.y = endPos.y + i * 2;
                                explPos.x = endPos.x;
                                fireExplCopy.transform.position = explPos;
                            }
                            adtimer = 0.0f;
                            break;
                        case 4:
                            sRenderer.sortingOrder = 6;
                            startPos = player.transform.position;
                            player.transform.position += Vector3.right * dashDistance;
                            endPos = player.transform.position;
                            for (int i = 0; i < noOfExpl; i++)
                            {
                                GameObject fireExplCopy = Instantiate(expl);
                                Vector3 explPos = startPos;
                                explPos.x = endPos.x - i * 2;
                                explPos.y = endPos.y + 1;
                                fireExplCopy.transform.position = explPos;
                            }
                            adtimer = 0.0f;
                            break;
                    }
                }
            }
        } 
    }
}
