using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectable : MonoBehaviour
{
    [SerializeField] private float healAmount = 100f;
    [SerializeField] private AudioClip healSound;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Health health = other.GetComponent<Health>();
        if (health != null && health.NeedsHealing() && other.tag == "Player")
        {
            health.Heal(healAmount);
            gameObject.SetActive(false);
            SoundManager.Instance.PlaySound(healSound);
        }
    }
}
