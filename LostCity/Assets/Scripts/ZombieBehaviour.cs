﻿using UnityEngine;
using System.Collections;
public class ZombieBehaviour : MonoBehaviour
{
    public int health = 30;
    public GameObject explosionPrefab;
    public int damage = 2;
    public float adjustExplosionAngle = 0.0f;

    private Transform player;
    void Start()
    {
        if (GameObject.FindWithTag("Player"))
        {
            player = GameObject.FindWithTag("Player").transform;
            GetComponent<MoveTowardsObject>().target = player;
            GetComponent<SmoothLookAtTarget2D>().target = player;
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.SendMessage("TakeDamage", damage);
        }
    }


    public bool TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Quaternion newRot = Quaternion.Euler(transform.eulerAngles.x,
                                                 transform.eulerAngles.y,
                                                 transform.eulerAngles.z + adjustExplosionAngle);
            Instantiate(explosionPrefab, transform.position, newRot);
            GetComponent<AddScore>().DoSendScore();
            RoundControl.ZombiesKilled += 1;
            Destroy(gameObject);
            return true;
        }
        else
        {
            return false;
        }
    }
    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0.0f;
    }

}
