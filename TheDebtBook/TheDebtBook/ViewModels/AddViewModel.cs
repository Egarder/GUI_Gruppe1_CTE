using System;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using TheDebtBook.Models;

namespace TheDebtBook.ViewModels
{
    public class AddViewModel : BindableBase, IDialogAware
    {
        Debitors _currentDebitor;

        public AddViewModel()
        {
        }
        public AddViewModel(Debitors debitor)
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

        ICommand _btnOKCommand;

        public event Action<IDialogResult> RequestClose;

        public ICommand BtnOKCommand
        {
            get
            {
                return _btnOKCommand ?? (_btnOKCommand = new DelegateCommand(
                    BtnOKCommandExecute, BtnOKCommandCanExecute)
                    .ObservesProperty(() => CurrentDebitor.Name)
                    .ObservesProperty(() => CurrentDebitor.Balance));
            }
        }

        public string Title => throw new NotImplementedException();

        private void BtnOKCommandExecute()
        {

        }

        private bool BtnOKCommandCanExecute()
        {
            return IsValid;
        }

        public bool CanCloseDialog()
        {
            throw new NotImplementedException();
        }

        public void OnDialogClosed()
        {
            throw new NotImplementedException();
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            throw new NotImplementedException();
        }
    }
}