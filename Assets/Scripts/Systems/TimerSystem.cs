using System;
using System.Collections;
using UnityEngine;

public class TimerSystem
{
    private float remainingTime;
    private bool running;

    public event Action<float> OnTimeChanged;
    public event Action OnTimeUp;

    public TimerSystem(float duration)
    {
        remainingTime = duration;
    }

    public void Start(MonoBehaviour runner)
    {
        running = true;
        runner.StartCoroutine(TimerRoutine());
    }

    public void Stop() => running = false;

    private IEnumerator TimerRoutine()
    {
        while (running && remainingTime > 0)
        {
            yield return new WaitForSeconds(1f);
            remainingTime--;
            OnTimeChanged?.Invoke(remainingTime);
        }

        if (running) OnTimeUp?.Invoke();
    }
}