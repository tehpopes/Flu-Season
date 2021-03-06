﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Boss : D_EnemyAbstract
{
    public int Speed;
    private GameObject player;
    private float wait_seconds = 0.9f;
    private float start_time = 0.0f;
    public GameObject the_purse;
    private float offset;
    private float bullet_speed;
    private int b_lives;
    bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        Speed = 2;
        start_time = Time.time + wait_seconds;
        bullet_speed = 10f;
        offset = 0.5f;
        b_lives = 35;
        player = GameObject.FindWithTag("Player");
    }

    private void OnEnable()
    {
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (player != null)
            transform.position = Vector2.MoveTowards(transform.position, player.GetComponent<Transform>().position, Speed * Time.deltaTime);

        if (start_time <= Time.time)
        {
            Shoot();

            start_time = Time.time + wait_seconds;
        }
    }

    void Shoot()
    {
        Vector3 player_pos = player.GetComponent<Transform>().position;
        Vector2 direction = player_pos - this.transform.position;
        direction.Normalize();

        GameObject clone = Instantiate(the_purse, this.transform.position + (Vector3)(direction * 0.5f), Quaternion.identity);
        float rotation_z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        clone.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotation_z + offset);
        clone.GetComponent<Rigidbody2D>().velocity = direction * bullet_speed;
    }
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            if (b_lives == 1)
            {
                GameObject.FindGameObjectWithTag("LevelManager").GetComponent<D_SimpleLevelManager>().EnemyKill(3);
                Destroy(this.gameObject);
            }
            else
            {
                b_lives--;
            }

        }
    }
    */
    public override void TakeDamage()
    {
        if (b_lives == 1)
        {
            if (dead)
                return;
            GameObject.FindGameObjectWithTag("LevelManager").GetComponent<D_SimpleLevelManager>().EnemyKill(20);
            dead = true;
            Destroy(this.gameObject);
        }
        else
        {
            b_lives--;
        }
    }
}
