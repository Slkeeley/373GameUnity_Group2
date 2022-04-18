using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    //Movement Variables
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 1f; 

    public Transform groundCheck;
    public float groundDistance = 0.4f;

    public LayerMask groundMask;
    Rigidbody tracer; 

    Vector3 velocity;
    bool isGrounded;

    //Ability Variables
    public int blinkCharges = 3;
    private bool blinked = false;
    public float blinkDistance = 7.8f; 
    private void Start()
    {
        tracer = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    
        if(isGrounded && velocity.y <0)
        {
            velocity.y = -2f; 
        }

        float x = Input.GetAxis("Horizontal");    
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;


        controller.Move(move * speed * Time.deltaTime);

     /*   if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); 
        }
       */ 
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity);
         
    }
    private void LateUpdate()
    {
        if (blinkCharges > 0 && blinked==false)
        {
      
            if (Input.GetKeyDown(KeyCode.S) && Input.GetKey(KeyCode.LeftShift))//Blink Forward by Default
            {
                blinkBackwards();
                blinked = true;
            }
            else if (Input.GetKey(KeyCode.LeftShift))//Blink Forward by Default
            {
                blink();
                blinked = true;
            }
        }
    }

    //Basic Function for blinking forward
    void blink()
    {
        Debug.Log("blink");
        Vector3 blink = transform.forward * blinkDistance;
        controller.Move(blink);
        StartCoroutine(waitForBlink());
        blinkCharges--;
        StartCoroutine(blinkCooldown());
    }

    void blinkBackwards()
    {
        Debug.Log("blinkingBackwards");
        Vector3 blink = transform.forward * blinkDistance;
        controller.Move(blink*-1);
        StartCoroutine(waitForBlink());
        blinkCharges--;
        StartCoroutine(blinkCooldown());
    }

    IEnumerator blinkCooldown()//cooldown per blink
    {
        yield return new WaitForSeconds(3.0f);
        blinkCharges++;
    }
    IEnumerator waitForBlink() //additional cooldown to adjust other cooldowns
    {
        yield return new WaitForSeconds(.25f);
        blinked = false;
    }
}
