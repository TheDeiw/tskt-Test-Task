using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public float bulletSpeed = 5f;
    public GameObject bulletPrefab;
    void Start()
    {
        InvokeRepeating("LaunchBullet", 0.5f, 1.5f);
    }

    void LaunchBullet()
    {
        Transform target = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(90, 0, 0);

        GameObject bullet = Instantiate(bulletPrefab, transform.position, rotation);

        // Pass parameters to the bullet
        bullet.GetComponent<BulletMovement>().SetDirection(direction, bulletSpeed);
    }
}
