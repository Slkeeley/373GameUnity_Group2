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

    //ABILITY VARIABLES
    //blink vars
    public int blinkCharges = 3;
    private bool blinked = false;
    private int blinkType = 0;
    public float blinkDistance = 7.8f;
    //Recall Vars
    public bool canRecall = true;
    private bool readyToUpdate = true;
    public float rcCooldown = 12.0f;
    public Vector3 currPos;
    public Vector3 pos1;
    public Vector3 pos2;
    public Vector3 pos3;
    public Vector3 pos4;
    public Vector3 pos5;

    //Pulse Bomb; 
    public GameObject pulseBomb;
    public Transform pulseStartPos;
    public bool bombCharged = true;
    private bool ultCharging = false;
    private float pulseCooldown = 100f;

    public int health = 50;
    public int healthPool = 150; 
    private void Awake()
    {

        tracer = GetComponent<Rigidbody>();

        
                currPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                pos1 = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                pos2 = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                pos3 = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                pos4 = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                pos4 = new Vector3(transform.position.x, transform.position.y, transform.position.z);
          
    }


    // Update is called once per frame
    void Update()
    {

        //BASIC MOVEMENT FUNCS
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
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
        //END BASIC MOVEMENT
   
        if (canRecall == true)//Recall Ability
        {
            if (Input.GetKey(KeyCode.E))
            {
                Recall();
            }
        }
    }

    private void LateUpdate()
    {
        
        if (blinkCharges > 0 && blinked==false)//Blink Requirements
        {
            if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftShift))//Blink Backwards
            {
                blinkType = 1;
                blink();
                blinked = true;
            }
            else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift))//Blink Right
            {
                blinkType = 2;
                blink();
                blinked = true;
            }
            else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift))//Blink Left
            {
                blinkType = 3;
                blink();
                blinked = true;
            }
            else if (Input.GetKey(KeyCode.LeftShift))//Blink Forward by Default
            {
                blink();
                blinked = true;
            }
        }//Blink Ability
     
        if (Input.GetKey(KeyCode.Q) && bombCharged==true)//Pulse Bomb Throw
        {
            PulseBomb();
        }
        if (health>healthPool)//Check for health overflow
        {
            health = healthPool; 
        }
     
        if(ultCharging)
        {
            StartCoroutine(ultCooldown());
        }

        if (readyToUpdate)//update Positions for recall
        {
            StartCoroutine(updatePos());
        }
    }


    //ABILITY FUNCS
     void blink()//Function To blink in each direction
    {
        Vector3 blinkVertical = transform.forward * blinkDistance;
        Vector3 blinkHorizontal = transform.right * blinkDistance;
        switch (blinkType)
        {
            case 1:
                controller.Move(blinkVertical * -1);//Blink Backwards
                break;
            case 2:
                controller.Move(blinkHorizontal);//Right
                break;
            case 3:
                controller.Move(blinkHorizontal*-1);//Left 
                break;
            default:
                controller.Move(blinkVertical);//Forward by default
                break;
        }
        blinkType = 0;
        StartCoroutine(waitForBlink());
        blinkCharges--;
        StartCoroutine(blinkCooldown());
    }
   
    void Recall()
    {
        readyToUpdate = false;
        gameObject.transform.position = new Vector3(pos5.x, pos5.y, pos5.z);
        StartCoroutine(recallCooldown());
    }

    void PulseBomb()
    {
        Instantiate(pulseBomb, new Vector3(pulseStartPos.transform.position.x, pulseStartPos.transform.position.y, pulseStartPos.transform.position.z), Quaternion.identity);
        bombCharged = false;
        ultCharging = true; 
    }
    //COOLDOWNS 
    IEnumerator blinkCooldown()//cooldown per blink
    {
        yield return new WaitForSeconds(3.0f);
        blinkCharges++;
    }
    IEnumerator recallCooldown()//cooldown per blink
    {
        yield return new WaitForSeconds(.25f);
        canRecall = false;
        yield return new WaitForSeconds(rcCooldown);
        canRecall = true;
    }
    IEnumerator waitForBlink() //additional cooldown to adjust other cooldowns
    {
        yield return new WaitForSeconds(.25f);
        blinked = false;
    }

    IEnumerator updatePos()
    {
        readyToUpdate = false;
        pos5 = pos4;
        pos4 = pos3;
        pos3 = pos2;
        pos2 = pos1;
        pos1 = currPos;
        currPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        yield return new WaitForSecondsRealtime(.5f);
        readyToUpdate = true;
    }

       IEnumerator ultCooldown()//Cooldown for PusleBomb
    {
        ultCharging = false;
        yield return new WaitForSeconds(pulseCooldown);
        bombCharged = true; 
    }
  
}
