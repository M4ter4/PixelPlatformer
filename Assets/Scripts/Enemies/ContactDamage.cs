using System;
using System.Collections;
using System.Collections.Generic;
using Basic;
using UnityEngine;

public class ContactDamage : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            other.GetComponent<Health>().TakeDamage(damage);
    }
}
