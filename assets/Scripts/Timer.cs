using System;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public float time;
    public int tickInterval;
    public UnityEvent timerTicked;

    private float _lastTick;

    private void Start()
    {
        _lastTick = time;
    }

    private void Update()
    {
        time -= Time.deltaTime;

        if (time <= 0.0f)
        {
            time = 0;
            GameManager.Instance.TriggerEndGame();
        }
        else if (TimePassedSinceLastTick() >= tickInterval)
        {
            Tick();
        }
    }

    private void Tick()
    {
        timerTicked.Invoke();
        _lastTick = time;
    }

    private float TimePassedSinceLastTick()
    {
        return _lastTick - time;
    }
}