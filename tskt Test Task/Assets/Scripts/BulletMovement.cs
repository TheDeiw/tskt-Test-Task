using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private Vector3 direction;
    private float speed;

    // Take parameters from BulletSpawner
    public void SetDirection(Vector3 dir, float spd)
    {
        direction = dir;
        speed = spd;
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    // Destroy the bullet if it goes out of bounds
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
            Debug.Log("Bullet destroyed by wall");
        }
    }
}
