using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float pointsGainedPerKid;
    public float score;

    public void AddScore(int numOfKids)
    {
        Debug.Log(numOfKids * pointsGainedPerKid + " Points!");
        score += numOfKids * pointsGainedPerKid;
    }
}
