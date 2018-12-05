using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Stads_App.Annotations;
using Stads_App.Models;
using Stads_App.Utils;

namespace Stads_App.ViewModels
{
    public sealed class CategoriesViewModel : INotifyPropertyChanged
    {
        private List<Category> _categories;

        public List<Category> Categories
        {
            get => _categories;
            private set
            {
                _categories = value;
                OnPropertyChanged(nameof(Categories));
            }
        }

        private bool _isLoaded;

        public bool IsLoaded
        {
            get => _isLoaded;
            private set
            {
                _isLoaded = value;
                OnPropertyChanged(nameof(IsLoaded));
            }
        }

        #region Data loaders

        private async Task<List<Category>> GetCategoriesAsync()
        {
            return await StadsAppRestApiClient.Instance.GetListAsync<Category>("Categories");
        }

        #endregion

        public async Task LoadData()
        {
            Categories = await GetCategoriesAsync();
            IsLoaded = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}