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

        if (running)
        {
            if (Input.GetAxisRaw("Vertical") == 1 && Input.GetAxisRaw("Horizontal") == 0)
            {
                anim.Play("SprintUpAnim");
                anim.SetBool("WDown", true);
                anim.SetBool("SDown", false);
                anim.SetBool("ADown", false);
                anim.SetBool("DDown", false);
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

}
