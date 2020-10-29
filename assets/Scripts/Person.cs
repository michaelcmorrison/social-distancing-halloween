using UnityEngine;

public class Person : MonoBehaviour
{
    protected Vector2 ChooseRandomDirection()
    {
        var rand = Random.Range(1, 3);
        return rand == 1 ? Vector2.left : Vector2.right;
    }

}