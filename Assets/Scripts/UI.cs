using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    public GameObject player;
    private int playerHealth;

    //TEXT OBJECTS
    public TMP_Text hpText;
    public TMP_Text ultChargeText;
    public TMP_Text blinkChargesText;
    public TMP_Text recallCDText;

    //SPRITE OBJECTS
    public GameObject healthCube1; 
    public GameObject healthCube2; 
    public GameObject healthCube3; 
    public GameObject healthCube4; 
    public GameObject healthCube5; 
    public GameObject healthCube6; 
    public GameObject ultCharged;
    public GameObject blinksUP;
    public GameObject blinksDown;
    public GameObject recallUP;
    public GameObject recallDown;
    // Start is called before the first frame update

    private bool rcCD = false;
    void Start()
    {
        playerHealth = player.GetComponent<PlayerMovement>().health;
        UpdateUI();
        blinksDown.SetActive(false);
        recallDown.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        hpText.text = playerHealth.ToString() + "/150";//Player Health
        cubeCheck();
        blinkChargesText.text = player.GetComponent<PlayerMovement>().blinkCharges.ToString();
        ultTimer();
        abilityCheck();
    }

    void cubeCheck()
    {
      
    }

    void ultTimer()
    {
        if(player.GetComponent<PlayerMovement>().bombCharged)
        {
            ultCharged.SetActive(true);
        }
        else
        {
            ultCharged.SetActive(false);
        }//Bring up the ultimate charged icon if bomb is fully charged
    }

    void abilityCheck()
    {
        if(player.GetComponent<PlayerMovement>().blinkCharges>0)
        {
            blinksUP.SetActive(true);
            blinksDown.SetActive(false);
        }
        else
        {
            blinksUP.SetActive(false);
            blinksDown.SetActive(true);
        }//check if player has any blink charges left


        if (player.GetComponent<PlayerMovement>().canRecall == false)//check if player has used the recall ability
        {
            recallUP.SetActive(false);
            recallDown.SetActive(true);
            if (rcCD == false)
            {
                StartCoroutine(recallCooldown());
            }
        }
        else
        {
            recallUP.SetActive(true);
            recallDown.SetActive(false);
        }

    }

    IEnumerator recallCooldown()//Cooldown Text for Recall Ability
    {
        rcCD = true;
        recallCDText.text = "12";
        for (int i=11; i >=0; i--)
        {
            yield return new WaitForSeconds(1.0f);
            recallCDText.text = i.ToString();
        }
        recallCDText.text = "";
    }
}
