using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed;
    public float moveX = 0;
    public float moveY = 0;
    private bool running = false;
    public bool canMove = true;
    public float sprintSpeed;
    public Rigidbody2D rb;

    public int lastwasd = 3;

    private Vector2 moveDirection;

    public Animator anim;

    public GameObject sword;

    Sword swordScript;

    PlayerBasicAttack baScript;

    //public AudioSource audioP;

    void Start()
    {
        anim = GetComponent<Animator>();
        sword = GameObject.Find("Sword");
        swordScript = sword.GetComponent<Sword>();
        baScript = GetComponent<PlayerBasicAttack>();
        lastwasd = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove == true)
        {
            MoveAnimations();
        }

        ProcessInputs();

        //Debug.Log(Input.GetAxisRaw("Horizontal"));
        if (baScript.attacking == false){
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                if (canMove == true)
                {
                    swordScript.sr.enabled = false;
                    swordScript.cc.enabled = false;
                }
            }
        } else
        {
            swordScript.sr.enabled = true;
            swordScript.cc.enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed = moveSpeed + sprintSpeed;
            anim.SetBool("ShiftDown", true);
            running = true;
        }

        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = moveSpeed - sprintSpeed;
            anim.SetBool("ShiftDown", false);
            running = false;
        }

    }

    void FixedUpdate()
    {
        //Physics calculations
        Move();
        
    }

    void ProcessInputs()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        
        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void Move()
    {
        if (canMove == true)
        {
            rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        } else
        {
            rb.velocity = new Vector2(0, 0);
        }
        
    }

    void MoveAnimations()
    {

        if (running)
        {
            if (Input.GetAxisRaw("Vertical") == 1 && Input.GetAxisRaw("Horizontal") == 0)
            {
                anim.Play("SprintUpAnim");
                anim.SetBool("WDown", true);
                anim.SetBool("SDown", false);
                anim.SetBool("ADown", false);
                anim.SetBool("DDown", false);
                lastwasd = 1;
            } else if (Input.GetAxisRaw("Vertical") == 1 && Input.GetAxisRaw("Horizontal") == 1)
            {
                anim.Play("SprintUpRightAnim");
                anim.SetBool("WDown", true);
                anim.SetBool("SDown", false);
                anim.SetBool("ADown", false);
                anim.SetBool("DDown", true);
            }
            else if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 1)
            {
                anim.Play("SprintRightAnim");
                anim.SetBool("WDown", false);
                anim.SetBool("SDown", false);
                anim.SetBool("ADown", false);
                anim.SetBool("DDown", true);
                lastwasd = 4;
            }
            else if (Input.GetAxisRaw("Vertical") == -1 && Input.GetAxisRaw("Horizontal") == 1)
            {
                anim.Play("SprintDownRightAnim");
                anim.SetBool("WDown", false);
                anim.SetBool("SDown", true);
                anim.SetBool("ADown", false);
                anim.SetBool("DDown", true);
            }
            else if (Input.GetAxisRaw("Vertical") == -1 && Input.GetAxisRaw("Horizontal") == 0)
            {
                anim.Play("SprintDownAnim");
                anim.SetBool("WDown", false);
                anim.SetBool("SDown", true);
                anim.SetBool("ADown", false);
                anim.SetBool("DDown", false);
                lastwasd = 3;
            }
            else if (Input.GetAxisRaw("Vertical") == -1 && Input.GetAxisRaw("Horizontal") == -1)
            {
                anim.Play("SprintDownLeftAnim");
                anim.SetBool("WDown", false);
                anim.SetBool("SDown", true);
                anim.SetBool("ADown", true);
                anim.SetBool("DDown", false);
            }
            else if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == -1)
            {
                anim.Play("SprintLeftAnim");
                anim.SetBool("WDown", false);
                anim.SetBool("SDown", false);
                anim.SetBool("ADown", true);
                anim.SetBool("DDown", false);
                lastwasd = 2;
            }
            else if (Input.GetAxisRaw("Vertical") == 1 && Input.GetAxisRaw("Horizontal") == -1)
            {
                anim.Play("SprintUpLeftAnim");
                anim.SetBool("WDown", true);
                anim.SetBool("SDown", false);
                anim.SetBool("ADown", true);
                anim.SetBool("DDown", false); ;
            }
        } else
        {
            if (Input.GetAxisRaw("Vertical") == 1 && Input.GetAxisRaw("Horizontal") == 0)
            {
                anim.Play("WalkUpAnim");
                anim.SetBool("WDown", true);
                anim.SetBool("SDown", false);
                anim.SetBool("ADown", false);
                anim.SetBool("DDown", false);
                lastwasd = 1;
            }
            else if (Input.GetAxisRaw("Vertical") == 1 && Input.GetAxisRaw("Horizontal") == 1)
            {
                anim.Play("WalkUpRightAnim");
                anim.SetBool("WDown", true);
                anim.SetBool("SDown", false);
                anim.SetBool("ADown", false);
                anim.SetBool("DDown", true);
            }
            else if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 1)
            {
                anim.Play("WalkRightAnim");
                anim.SetBool("WDown", false);
                anim.SetBool("SDown", false);
                anim.SetBool("ADown", false);
                anim.SetBool("DDown", true);
                lastwasd = 4;
            }
            else if (Input.GetAxisRaw("Vertical") == -1 && Input.GetAxisRaw("Horizontal") == 1)
            {
                anim.Play("WalkDownRightAnim");
                anim.SetBool("WDown", false);
                anim.SetBool("SDown", true);
                anim.SetBool("ADown", false);
                anim.SetBool("DDown", true);
            }
            else if (Input.GetAxisRaw("Vertical") == -1 && Input.GetAxisRaw("Horizontal") == 0)
            {
                anim.Play("WalkDownAnim");
                anim.SetBool("WDown", false);
                anim.SetBool("SDown", true);
                anim.SetBool("ADown", false);
                anim.SetBool("DDown", false);
                lastwasd = 3;
            }
            else if (Input.GetAxisRaw("Vertical") == -1 && Input.GetAxisRaw("Horizontal") == -1)
            {
                anim.Play("WalkDownLeftAnim");
                anim.SetBool("WDown", false);
                anim.SetBool("SDown", true);
                anim.SetBool("ADown", true);
                anim.SetBool("DDown", false);
            }
            else if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == -1)
            {
                anim.Play("WalkLeftAnim");
                anim.SetBool("WDown", false);
                anim.SetBool("SDown", false);
                anim.SetBool("ADown", true);
                anim.SetBool("DDown", false);
                lastwasd = 2;
            }
            else if (Input.GetAxisRaw("Vertical") == 1 && Input.GetAxisRaw("Horizontal") == -1)
            {
                anim.Play("WalkUpLeftAnim");
                anim.SetBool("WDown", true);
                anim.SetBool("SDown", false);
                anim.SetBool("ADown", true);
                anim.SetBool("DDown", false); ;
            }
        }

        if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0)
        {
            anim.SetBool("WDown", false);
            anim.SetBool("SDown", false);
            anim.SetBool("ADown", false);
            anim.SetBool("DDown", false);
        }

    }

}
