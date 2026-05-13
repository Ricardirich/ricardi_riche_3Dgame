using Unity.VisualScripting;
using UnityEngine;

public class OverheadCam : MonoBehaviour
{
    public Transform player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x,transform.position.y, player.position.z);
    }
}
