using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTestControl : MonoBehaviour
{
    public AudioSource audio_;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("volume")) audio_.volume = 1;
    }
    void Update()
    {
        audio_.volume = PlayerPrefs.GetFloat("volume");
    }
}
