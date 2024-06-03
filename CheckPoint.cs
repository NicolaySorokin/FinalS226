using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Transform player;
    public int index;
    private SpriteRenderer spriteRend;
    private Material matBlink;
    private Material matDefault;

    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        matBlink = Resources.Load("EnemyBlink", typeof(Material)) as Material;
        matDefault = spriteRend.material;
    }
    void Awake()
    {
        player = GameObject.Find("Player").transform;
        if (DataScr.checkpointIndex == index)
        {
            player.position = transform.position;
            spriteRend.material = matBlink;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            DataScr.checkpointIndex = index;
            spriteRend.material = matBlink;
        }
    }
    void ResetMaterial()
    {
        spriteRend.material = matDefault;
    }
}