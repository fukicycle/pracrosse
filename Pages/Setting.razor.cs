
using pracrosse.Entities;

namespace pracrosse.Pages;
public partial class Setting
{
    private bool _isLoading = false;
    private SettingItem _currentSetting = null!;
    private string _msg = "";

    protected override void OnInitialized()
    {
        base.OnInitialized();
        _currentSetting = SettingService.GetCurrentSetting();
    }

    private async Task SaveAsync()
    {
        _isLoading = true;
        await SettingService.SaveAsync(_currentSetting.Interval, _currentSetting.IsFixed);
        _isLoading = false;
        _msg = "保存しました。";
        StateHasChanged();
        await Task.Delay(1000);
        _msg = "";
        StateHasChanged();
    }
}
