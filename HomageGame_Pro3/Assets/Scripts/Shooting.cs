using UnityEngine;
using UnityEngine.EventSystems;

public class Shooting : MonoBehaviour
{

    public GameObject bulletPrefab;
    public GameObject bulletPoint;
    public float bulletSpeed = 600f;

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletPoint.transform.position, transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
            Destroy(bullet, 1f);
        }
    }
}
