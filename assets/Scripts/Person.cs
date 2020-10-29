using UnityEngine;

public class Person : MonoBehaviour
{
    public float minMoveSpeed;
    public float maxMoveSpeed;
    [HideInInspector] public float moveSpeed;
    
    protected Vector2 ChooseRandomDirection()
    {
        var rand = Random.Range(1, 3);
        return rand == 1 ? Vector2.left : Vector2.right;
    }

    protected float GetRandomSpeed()
    {
        var rand = Random.Range(minMoveSpeed, maxMoveSpeed);
        return rand;
    }

}