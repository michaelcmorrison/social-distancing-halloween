using UnityEngine;

public class Ammunition : MonoBehaviour
{
    public bool isAlcohol;
    public float selfDestructTime;

    private void Start()
    {
        Destroy(gameObject, selfDestructTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
