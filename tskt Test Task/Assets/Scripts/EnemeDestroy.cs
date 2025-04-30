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
    void Update()
    {
        GameObject closest = FindTheClosest();
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

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (closest != null)
            {
                Destroy(closest);
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
}
