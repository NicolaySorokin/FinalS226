using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectibleItem : Sounds
{
    public string bonusName;
    public TextMeshProUGUI coinCount;
    private static int coins = 0;


    void Awake()
    {
        coinCount.text = coins.ToString();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Player")
        {
            switch(bonusName)
            {
                case "coin":
                    PlaySound(sounds[0], destroyed: true);
                    coins++; 
                    coinCount.text = coins.ToString();
                    Destroy(gameObject);
                    break;


            }
        }
    }
}
