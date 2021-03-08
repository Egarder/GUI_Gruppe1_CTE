using System;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using TheDebtBook.Models;

namespace TheDebtBook.ViewModels
{
    public class DebtsViewModel : BindableBase
    {
        Debitors _currentDebitor;
        public DebtsViewModel(Debitors debitor)
        {
            CurrentDebitor = debitor;
        }

        public Debitors CurrentDebitor
        {
            get { return _currentDebitor; }
            set
            {
                SetProperty(ref _currentDebitor, value);
            }
        }

        public bool IsValid
        {
            get
            {
                bool isValid = true;
                if (string.IsNullOrWhiteSpace(CurrentDebitor.Name))
                    isValid = false;
                if (string.IsNullOrWhiteSpace(CurrentDebitor.Balance))
                    isValid = false;
                return isValid;
            }
        }
    }
}