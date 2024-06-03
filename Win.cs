using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

public class Win : MonoBehaviour
{
    public TextMeshProUGUI winText; 
    private bool hasWon = false;

    void Start()
    {
        winText.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Finish") && !hasWon)
        {
            hasWon = true;
            StartCoroutine(WinGame());
        }
    }

    IEnumerator WinGame()
    {
        winText.gameObject.SetActive(true); 
        yield return new WaitForSeconds(3); 
        SceneManager.LoadScene(0); 
    }
}
