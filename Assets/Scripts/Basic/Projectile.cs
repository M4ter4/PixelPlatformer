using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float projectileSpeed;
    protected Rigidbody2D _rigidbody;
    protected BoxCollider2D _collider;
    
    
    protected float _lifetime;
    protected void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        _lifetime += Time.deltaTime;
        if(_lifetime >= 10f)
            Deactivate();
    }

    // Update is called once per frame

    public void Shoot(Transform shootPoint, float angle)
    {
        gameObject.SetActive(true);
        _collider.enabled = true;
        gameObject.transform.position = shootPoint.position;
        gameObject.transform.rotation =  Quaternion.Euler(0, 0, angle);
        _rigidbody.velocity = new Vector2
            (projectileSpeed*Mathf.Cos(Mathf.Deg2Rad*angle), projectileSpeed*Mathf.Sin(Mathf.Deg2Rad*angle));
        
        _lifetime = 0f;
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "Enemy" || other.tag == "Ground" || other.tag == "Door")
        {
            Deactivate();
        }
    }

    protected void Deactivate()
    {
        gameObject.SetActive(false);
    }
}