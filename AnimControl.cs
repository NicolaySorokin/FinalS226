using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimControl : MonoBehaviour
{
    private Animator animator; 

    void Start()
    {
        animator = GetComponent<Animator>(); 
    }

    void Update()
    {
        float moveSpeed = GetComponent<Rigidbody2D>().velocity.magnitude;

        animator.SetFloat("Speed", moveSpeed);
    }
}
