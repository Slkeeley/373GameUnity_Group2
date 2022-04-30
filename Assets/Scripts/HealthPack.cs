using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public int healthGiven;
    public GameObject healthPack;
    public float respawnTime; 
    private bool healthUP = true; 
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag =="Player")
        {
            Debug.Log("trigger");
            if (healthUP)
            {
                healthUP = false;
                healthPack.SetActive(false);
                other.GetComponent<PlayerMovement>().health = other.GetComponent<PlayerMovement>().health + healthGiven;
                StartCoroutine(respawnPack());
            }
            
        }
    }

    IEnumerator respawnPack()
    {
        yield return new WaitForSeconds(respawnTime);
        healthPack.SetActive(true);
        healthUP = true; 
    }
}
