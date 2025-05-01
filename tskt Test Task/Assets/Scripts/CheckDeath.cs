using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class CheckDeath : MonoBehaviour
{
    public HealthBar healthBar;
    public GameObject losepanel;

    void Start()
    {
        Time.timeScale = 1f;
        losepanel.SetActive(false);
    }

    void Update()
    {
        if (healthBar.slider.value <= 0)
        {
            losepanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
