
using UnityEngine;

public class PropDestruct : MonoBehaviour
{
    public float health = 10f;

    public void TakeDamage(float amount)
    {
        Debug.Log("DamageTaken");
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(this.gameObject);
    }
}
