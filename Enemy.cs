using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;
    public int positionOfPatrol;
    public Transform point;
    private UnityEngine.Object explosion;
    private Material matBlink;
    private Material matDefault;
    private SpriteRenderer spriteRend;
    bool moveingRight;

    Transform player;
    public float stoppingDistance;
    bool chill = false;
    bool angry = false;
    bool goBack = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        explosion = Resources.Load("Explosion");
        spriteRend = GetComponent<SpriteRenderer>();
        matBlink = Resources.Load("EnemyBlink", typeof(Material)) as Material;
        matDefault = spriteRend.material;
    }
    void Update()
    {
        if (health <= 0)
        {
            GameObject explosionRef = (GameObject)Instantiate(explosion);
            explosionRef.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Destroy(gameObject);
        }
        
        if (Vector2.Distance(transform.position, point.position) < positionOfPatrol && angry == false)
        {
            chill = true;
        }

        if (Vector2.Distance(transform.position, player.position) < stoppingDistance)
        {
            angry = true;
            chill = false;
            goBack = false;
        }

        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            goBack = true;
            angry = false;
        }

        if (chill == true)
        {
            Chill();
        }
        else if (angry == true)
        {
            Angry();
        }
        else if (goBack == true)
        {
            GoBack();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        spriteRend.material = matBlink;
        Invoke("ResetMaterial", .2f);
    }
    void ResetMaterial()
    {
        spriteRend.material = matDefault;
    }

    void Chill()
    {
        if (transform.position.x > point.position.x + positionOfPatrol)
        {
            moveingRight = false;
        }
        else if (transform.position.x < point.position.x - positionOfPatrol)
        {
            moveingRight = true;
        }

        if (moveingRight)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);  
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void Angry()
    {
        Vector2 direction = player.position - transform.position;
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        if (direction.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void GoBack()
    {
        Vector2 direction = point.position - transform.position;
        transform.position = Vector2.MoveTowards(transform.position, point.position, speed * Time.deltaTime);

        if (direction.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
