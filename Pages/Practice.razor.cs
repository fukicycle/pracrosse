using pracrosse.Entities;
using Timer = System.Timers.Timer;
namespace pracrosse.Pages;
public partial class Practice
{
    private Random _randomInstance = Random.Shared;
    private Timer _timer = new Timer(1000);
    private int _intervalInSec;
    private int _countInSec = 0;
    private string[] _panels = new string[9];
    private const string _activeClassName = "active";
    private int _displayCountDownValue;
    private SettingItem _currentSetting = null!;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        _currentSetting = SettingService.GetCurrentSetting();
        if (_currentSetting.IsFixed)
        {
            _intervalInSec = _currentSetting.Interval;
            _displayCountDownValue = _currentSetting.Interval;
        }
        else
        {
            SetRandomInterval();
        }
        _timer.Elapsed += TimerTick;
        _timer.Start();
        await SetRandomPanelAsync();
    }

    private void SetRandomInterval()
    {
        if (!_currentSetting.IsFixed)
        {
            _intervalInSec = _randomInstance.Next(1, 10);
        }
    }

    private void InitailizeArray()
    {
        for (int i = 0; i < _panels.Length; i++)
        {
            _panels[i] = "";
        }
    }

    private async Task SetRandomPanelAsync()
    {
        InitailizeArray();
        _panels[_randomInstance.Next(0, _panels.Length - 1)] = _activeClassName;
        StateHasChanged();
        await Task.Delay(200);
        InitailizeArray();
        StateHasChanged();
    }

    private async void TimerTick(object? sender, EventArgs e)
    {
        _countInSec++;
        if (_countInSec == _intervalInSec)
        {
            await SetRandomPanelAsync();
            SetRandomInterval();
            _countInSec = 0;
        }
        _displayCountDownValue = CountDown();
        StateHasChanged();
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