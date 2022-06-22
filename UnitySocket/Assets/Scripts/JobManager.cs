using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is uses for do action that want to run on main thread.
/// </summary>
public class JobManager : MonoBehaviour
{
    public static JobManager Instance;

    private ConcurrentQueue<Action> jobs = new ConcurrentQueue<Action>();

    public void AddJob(Action newJob)
    {
        if (newJob != null)
        {
            jobs.Enqueue(newJob);
        }
    }

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        while (jobs.Count > 0)
        {
            if (jobs.TryDequeue(out Action job))
            {
                job.Invoke();
            }
        }
    }
}
