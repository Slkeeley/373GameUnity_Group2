using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracerAbilities : MonoBehaviour
{

    public Vector3 currpos; 
        // Start is called before the first frame update
    void Start()
    {
        currpos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift))
           {
            Debug.Log("blink");
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z+7.8f);
            }

        

    }
}
