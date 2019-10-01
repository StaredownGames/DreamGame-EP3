using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Camera cam;
    Animator anim;
    public float speed = 1.25f;
    bool sneakin = false;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindObjectOfType<Camera>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //MOVEMENT
        float axisH = Input.GetAxis("Horizontal");
        float axisV = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(axisV,-axisH,0) * speed * Time.deltaTime);

        //FACE THE MOUSE POINTER

        //convert the mouse position to world coordinates.
        Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

        //what direction we want to look at.
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;

        //get the distance from player to mousePosition.
        float playerToMouseDistance = Vector2.Distance(mousePosition,(Vector2)transform.position);

        if (playerToMouseDistance >= .25f)
        {
            transform.right = direction;

        }

        //ANIMATION
        if (Input.GetMouseButton(1))
        {
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger("punch");
            }
            if (anim.GetBool("isSneaking") == false)
            {
                anim.SetBool("isSneaking", true);
            }
        }
        else
        {
            anim.SetBool("isSneaking", false);

        } 

        switch (axisV)
        {
            case 1:
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    anim.SetBool("isWalking", true);
                    speed = 1.5f;
                    break;
                }
                anim.SetBool("isRunning", true);
                speed = 2.5f;
                break;
            case -1:
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    anim.SetBool("isWalkingBackward", true);
                    speed = 1.5f;
                    break;
                }
                anim.SetBool("isRunningBackward", true);
                speed = 2.5f;
                break;
            default:
                anim.SetBool("isRunning", false);
                anim.SetBool("isRunningBackward", false);
                anim.SetBool("isWalking", false);
                anim.SetBool("isWalkingBackward", false);
                break;
        }

        switch (axisH)
        {
            case 1:
                anim.SetBool("strafeRight", true);
                break;
            case -1:
                anim.SetBool("strafeLeft", true);
                break;
            default:
                anim.SetBool("strafeRight", false);
                anim.SetBool("strafeLeft", false);
                break;
        }
    }
}