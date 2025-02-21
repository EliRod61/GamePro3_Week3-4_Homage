using UnityEngine;

public class GhostEnemy : MonoBehaviour
{
    public Transform[] points;
    public float speed;
    public int enemyHealth = 25;

    int currentPos;
    int health;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = enemyHealth;
        currentPos = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        if (transform.position != points[currentPos].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, points[currentPos].position, speed * Time.deltaTime);
        }
        else
        {
            currentPos = (currentPos + 1) % points.Length;
        }
    }
    public void TakeDamage()
    {
        health--;
    }
}
