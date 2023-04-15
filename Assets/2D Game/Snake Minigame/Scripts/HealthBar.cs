using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public Image healthBar;

    public GameObject canvas;
    public GameObject clockUI;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);

        if (health <= 0)
        {
            canvas.SetActive(true);
            Time.timeScale = 0f;
            clockUI.SetActive(false);
        }
    }
}
