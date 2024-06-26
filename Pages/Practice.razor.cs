using Timer = System.Timers.Timer;
namespace pracrosse.Pages;
public partial class Practice
{
    private Random _randomInstance = Random.Shared;
    private Timer _timer = new Timer(1000);
    private int _intervalInSec = 5;
    private int _countInSec = 0;
    private string[] _panels = new string[9];
    private const string _activeClassName = "active";
    private int _displayCountDownValue = 5;

    protected override void OnInitialized()
    {
        base.OnInitialized();
        _timer.Elapsed += TimerTick;
        _timer.Start();
        SetRandomPanel();
    }

    private void SetRandomPanel()
    {
        int currentIdx = _panels.ToList().IndexOf(_activeClassName);
        for (int i = 0; i < _panels.Length; i++)
        {
            _panels[i] = "";
        }
        _panels[GetRandomValue(currentIdx)] = _activeClassName;
        StateHasChanged();
    }

    private void TimerTick(object? sender, EventArgs e)
    {
        _countInSec++;
        if (_countInSec == _intervalInSec)
        {
            SetRandomPanel();
            _countInSec = 0;
        }
        _displayCountDownValue = CountDown();
        StateHasChanged();
    }

    private int GetRandomValue(int currentIdx)
    {
        int random = _randomInstance.Next(0, _panels.Length - 1);
        if (random == currentIdx)
        {
            return GetRandomValue(currentIdx);
        }
        return random;
    }

    private int CountDown()
    {
        return _intervalInSec - _countInSec;
    }

    public void Dispose()
    {
        _timer.Dispose();
    }
}