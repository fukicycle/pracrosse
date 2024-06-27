
namespace pracrosse.Pages;
public partial class Index
{
    private bool _isLoading = true;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        await SettingService.LoadAsync();
        _isLoading = false;
    }
}
