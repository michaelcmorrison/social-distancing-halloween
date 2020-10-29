using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoLoader : MonoBehaviour
{
    public Rigidbody2D[] munitions;

    public Rigidbody2D GetRandomAmmunition()
    {
        var rand = Random.Range(0, munitions.Length);
        return munitions[rand];
    }
}
