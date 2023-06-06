using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed;
    private bool running = false;
    public float sprintSpeed;
    public Rigidbody2D rb;

    private Vector2 moveDirection;

    public Animator anim;

    //public AudioSource audioP;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();

        MoveAnimations();

    }

    void FixedUpdate()
    {
        //Physics calculations
        Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    void MoveAnimations()
    {
        /*if (Input.GetAxisRaw("Horizontal") == 1)
        {
            anim.SetBool("DDown", true);
            anim.SetBool("ADown", false);
        }
        else if (Input.GetAxisRaw("Horizontal") == -1)
        {
            anim.SetBool("ADown", true);
            anim.SetBool("DDown", false);
        }
        else if (Input.GetAxisRaw("Horizontal") == 0)
        {
            anim.SetBool("DDown", false);
            anim.SetBool("ADown", false);
        }

        if (Input.GetAxisRaw("Vertical") == 1)
        {
            anim.SetBool("WDown", true);
            anim.SetBool("SDown", false);
        }
        else if (Input.GetAxisRaw("Vertical") == -1)
        {
            anim.SetBool("SDown", true);
            anim.SetBool("WDown", false);
        }
        else if (Input.GetAxisRaw("Vertical") == 0)
        {
            anim.SetBool("SDown", false);
            anim.SetBool("WDown", false);
        }*/
        if (running)
        {
            if (Input.GetAxisRaw("Vertical") == 1 && Input.GetAxisRaw("Horizontal") == 0)
            {
                anim.Play("SprintUpAnim");
                anim.SetBool("WDown", true);
                anim.SetBool("SDown", false);
                anim.SetBool("ADown", false);
                anim.SetBool("DDown", false);
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
            }
        }

        if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0)
        {
            anim.SetBool("WDown", false);
            anim.SetBool("SDown", false);
            anim.SetBool("ADown", false);
            anim.SetBool("DDown", false);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Sprint();
            running = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            StopSprint();
            running = false;
        }

    }


    void Sprint()
    {
        moveSpeed = moveSpeed + sprintSpeed;
        anim.SetBool("ShiftDown", true);
    }

    void StopSprint()
    {
        moveSpeed = moveSpeed - sprintSpeed;
        anim.SetBool("ShiftDown", false);
    }

}
