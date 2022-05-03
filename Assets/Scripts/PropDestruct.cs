
using UnityEngine;

public class PropDestruct : MonoBehaviour
{
    public float health = 10f;
    public GameObject combined;
    public GameObject[] pieces;
    private bool destroyed = false;
   
    public void TakeDamage(float amount)//Props have "health" and will be split/destroyed with enough damage.
    {
        
        health -= amount;
        if (health <= 0f)
        {
            if (!destroyed)
            {
                Split();
            }
        }
    }

    void Die()
    {
        Destroy(this.gameObject);
    }

    void Split()
    {
        Debug.Log("Split");
        destroyed = true; 
        combined.SetActive(false);
        foreach (var pieces in pieces)
        {
            Instantiate(pieces, transform.position, Quaternion.identity);
        }
    }
}
