using UnityEngine;
using UnityEngine.SceneManagement;

public class Endgame : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private bool hasEnded = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasEnded) { return; }
        if (other.CompareTag("Player"))
        {
            hasEnded = true;
            FPC playerScript = other.GetComponent<FPC>();
            if (playerScript != null)
            {
                playerScript.DisableMovement();
            }
        }
        Invoke("Restartgame", 5f);
    }

    
}
