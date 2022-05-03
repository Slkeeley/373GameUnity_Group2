using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseBomb : MonoBehaviour
{
    public float damage = 350f;
    public float range;
    public float projectileSpeed = 15;
    private bool moving = true;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()//instantiation
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
    void explosion(Vector3 center, float radius)//Explode and damage any props within a short range of the explosion. 
    {

        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.tag == "Prop")
            {

                Debug.Log("prop not null");
                PropDestruct prop = hitCollider.transform.GetComponent<PropDestruct>();
                prop.TakeDamage(damage);
            }
        }

        Destroy(this.gameObject);
    }
    IEnumerator explode()//behavior before explosion
    {
        yield return new WaitForSeconds(.5f);
        moving = false; 
        yield return new WaitForSeconds(.5f);
        explosion(this.transform.position, range);
       
    }
 
}
