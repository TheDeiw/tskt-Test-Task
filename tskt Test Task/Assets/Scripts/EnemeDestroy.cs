using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemeDestroy : MonoBehaviour
{
    public GameObject tankGun;
    private GameObject[] enemies;
    public float rotationSpeed = 5f;
    public PlayerMovement playerMovement;
    private Vector3 direction;

    public GameObject laserPrefab;
    public float beamDuration = 0.3f;

    public AudioSource explodeSound;

    public ParticleSystem explosionEffect;

    void Update()
    {
        GameObject closest = FindTheClosest();
        GunRotation(closest);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (closest != null)
            {
                explodeSound.Play();

                // Parameters for the laser
                Vector3 start = gameObject.transform.position;
                Vector3 end = closest.transform.position;
                Vector3 direction = end - start;
                float distance = direction.magnitude;

                GameObject laser = Instantiate(laserPrefab, start + direction / 2f, Quaternion.LookRotation(direction));
                laser.transform.localScale = new Vector3(0.05f, 0.05f, distance);
                Destroy(laser, beamDuration);

                Instantiate(explosionEffect, end, Quaternion.identity);

                Destroy(closest);
                Debug.Log("Enemy destroyed by player");
            }
        }
    }

    private GameObject FindTheClosest()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = enemies
            .OrderBy(e => Vector3.Distance(transform.position, e.transform.position))
            .FirstOrDefault();
        return closest;
    }

    // Rotate the gun towards the closest enemy
    private void GunRotation(GameObject closest)
    {
        if (closest != null)
        {
            direction = closest.transform.position - tankGun.transform.position;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                tankGun.transform.rotation = Quaternion.Lerp(tankGun.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
        }
        else
        {
            direction = playerMovement.moveDirectionForGun;
            Quaternion rotation = Quaternion.LookRotation(direction);
            tankGun.transform.rotation = Quaternion.Lerp(tankGun.transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }
    }

}
