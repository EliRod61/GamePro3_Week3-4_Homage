using UnityEngine;

public class GhostEnemy : MonoBehaviour
{
    public int enemyHealth = 25;
    int health;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = enemyHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void TakeDamage()
    {
        health--;
    }
}
