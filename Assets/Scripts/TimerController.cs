using UnityEngine;

public class TimerController : MonoBehaviour
{
    public static TimerController Instance;

    private float elapsedTime = 0f;
    private bool isTimerRunning = false;

    private void Awake()
    {
        // Singleton (optional)
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        StartTimer(); // Auto start on game start
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            elapsedTime += Time.deltaTime;
        }
    }

    /// <summary>
    /// Starts the timer manually (if needed)
    /// </summary>
    public void StartTimer()
    {
        elapsedTime = 0f;
        isTimerRunning = true;
    }

    /// <summary>
    /// Stops the timer and returns final time (in seconds)
    /// </summary>
    public float StopTimer()
    {
        isTimerRunning = false;
        return elapsedTime;
    }

    /// <summary>
    /// Returns current time without stopping
    /// </summary>
    public float GetCurrentTime()
    {
        return elapsedTime;
    }
}
