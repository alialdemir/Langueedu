namespace Langueedu.Web.Components.Services;

public class Timer : IDisposable
{
    private static System.Timers.Timer _timer;

    public static void Start(TimeSpan timeSpan, Func<Task<bool>> action)
    {
        double value = (timeSpan.Hours + timeSpan.Minutes / 100.0 + timeSpan.Seconds / 10000.0) * (timeSpan > TimeSpan.Zero ? 1 : -1);

        _timer = new System.Timers.Timer(value);
        _timer.Elapsed += async (source, e) =>
        {
            bool isActive = await action?.Invoke();
            if (!isActive)
                StopTimer();
        };

        _timer.Enabled = true;
    }

    private static void StopTimer()
    {
        if (_timer == null)
            return;

        _timer.Enabled = false;
        _timer.Dispose();
    }

    ~Timer()
    {
        _timer?.Dispose();

    }

    public void Dispose()
    {
        _timer?.Dispose();
        GC.SuppressFinalize(this);
    }
}
