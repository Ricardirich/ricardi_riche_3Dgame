using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject obejcttospawn;
    public Transform  spawnPoint;
    void Start()
    {
        spawn();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawn()
    {
        Instantiate(obejcttospawn, spawnPoint.position, spawnPoint.rotation);

    }
}
