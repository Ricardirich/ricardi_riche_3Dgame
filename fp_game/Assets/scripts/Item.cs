using UnityEngine;

public class Item : MonoBehaviour
{
    public Enemy enemy;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        enemy = FindFirstObjectByType<Enemy>();

    }
  public void PickUp(Transform parent, Vector3 pos)
    {

        rb.isKinematic = true;
        transform.SetParent(parent);
        transform.position = pos;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        if (enemy != null)
        {
            enemy.AlertEnemy(parent);
        }

    }

    public void Throw (float force,Vector3 direction)
    {
        rb.isKinematic = false;
        transform.SetParent(null);
        rb.AddForce(direction * force, ForceMode.Impulse);
    }
}
