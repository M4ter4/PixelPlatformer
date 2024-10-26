using System;
using System.Collections;
using System.Collections.Generic;
using Basic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUpdate : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image actualHealthBar;

    private void Update()
    {
        actualHealthBar.fillAmount = playerHealth.GetHealthPercentage();
    }
}
