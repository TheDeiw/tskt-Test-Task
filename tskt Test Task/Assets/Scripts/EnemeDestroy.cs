using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemeDestroy : MonoBehaviour
{
    private GameObject[] enemies;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject closest = FindTheClosest();
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
