using Blazored.LocalStorage;
using pracrosse.Entities;

namespace pracrosse
{
    public sealed class SettingService
    {
        private const string LocalStorageKey = "pracrosse-storage-key";
        private readonly ILocalStorageService _localStorageService;
        private SettingItem _settingItem = new SettingItem();
        public SettingService(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public async Task LoadAsync()
        {
            SettingItem? tmpSettingItem = await _localStorageService.GetItemAsync<SettingItem>(LocalStorageKey);
            if (tmpSettingItem == null)
            {
                tmpSettingItem = new SettingItem();
                tmpSettingItem.Interval = 5;
                tmpSettingItem.IsFixed = true;
            }
            _settingItem = tmpSettingItem;
        }

        public async Task SaveAsync(int interval, bool isFixed)
        {
            _settingItem.Interval = interval;
            _settingItem.IsFixed = isFixed;
            await _localStorageService.SetItemAsync(LocalStorageKey, _settingItem);
        }

        public SettingItem GetCurrentSetting()
        {
            return _settingItem;
        }
    }
}
