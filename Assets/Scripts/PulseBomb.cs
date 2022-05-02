using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseBomb : MonoBehaviour
{
    public float damage = 350f;
    public float range = 100;
    public float projectileSpeed = 15;
    private bool moving = true;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(explode());
    }

    private void Update()
    {
        if(moving)
        {
            rb.AddForce(Vector3.forward * projectileSpeed);
        }
    }
    void explosion()
    {
        RaycastHit hit;
        if (Physics.SphereCast(this.transform.position, range/2, transform.forward, out hit, range))
        {
            PropDestruct prop = hit.transform.GetComponent<PropDestruct>();

            if (prop != null)
            {
                Debug.Log("prop not null");
                prop.TakeDamage(damage);
            }
        }
        Destroy(this.gameObject);
    }
    IEnumerator explode()
    {
        yield return new WaitForSeconds(.5f);
        moving = false; 
        yield return new WaitForSeconds(.5f);
        explosion();
       
    }
 
}
