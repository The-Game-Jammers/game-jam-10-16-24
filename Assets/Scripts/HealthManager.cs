using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 100f;
    public bool isDead;
    [SerializeField] GameObject healthBarPrefab;
    [SerializeField] GameObject gameManager;
    Buttons gameManagerLink;

    private void Start()
    {
        gameManagerLink = gameManager.GetComponent<Buttons>();
    }

    public float getHealth()
    {
        return healthAmount;
    }

    public void setHealth(float heathSet)
    {
        healthAmount = heathSet;
        healthAmount = Mathf.Clamp(healthAmount , 0 , 100f);

        healthBar.fillAmount = healthAmount/ 100f;
    }

    private void Update()
    {
        if (gameManagerLink.GameIsPaused)
        {
            healthBarPrefab.SetActive(false);
        }
        else
        {
            healthBarPrefab.SetActive(true);
        }

        if(healthAmount == 0)
        {
            isDead = true;
        }
        else
        {
            isDead= false;
        }
    }


}
