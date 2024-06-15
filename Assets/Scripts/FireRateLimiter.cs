using System;
using UnityEngine;

public class FireRateLimiter
{
    private readonly float _fireRate;
    private readonly Action _action;
    private float _nextFrameTime;

    public FireRateLimiter(float fireRate, Action action)
    {
        _fireRate = fireRate;
        _action = action;
    }

    public void ExecuteLimitedAction()
    {
        if (!(Time.time >= _nextFrameTime))
            return;

        _nextFrameTime = Time.time + 1f / _fireRate;
        _action();
    }
}