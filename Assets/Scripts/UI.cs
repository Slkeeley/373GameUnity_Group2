using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    public GameObject player;

    //TEXT OBJECTS
    public TMP_Text hpText;
    public TMP_Text ultChargeText;
    public TMP_Text blinkCDText;
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
    void Start()
    {
        UpdateUI();  
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        hpText.text = player.GetComponent<PlayerMovement>().health.ToString() + "/150";//Player Health
        //Cube Visualization Function
        blinkChargesText.text = player.GetComponent<PlayerMovement>().blinkCharges.ToString();
    }
}
