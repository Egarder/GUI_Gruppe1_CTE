using System;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using TheDebtBook.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace TheDebtBook.ViewModels
{
    public class AddViewModel : BindableBase, IDialogAware
    {
        Debitors _currentDebitor;
        private string _title = "Add Debitor";
        

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

        public string Title
        {
            get { return _title; }
            set
            {
                SetProperty(ref _title, value);
            }
        }

        DelegateCommand<string> _closeBtnCommand;
        public DelegateCommand<string> CloseBtnCommand => _closeBtnCommand ?? (_closeBtnCommand = new DelegateCommand<string>(CloseDialog));

        protected virtual void CloseDialog(string parameter)
        {
            ButtonResult result = ButtonResult.None;
            if (parameter.ToLower()=="true")
            {
                result = ButtonResult.OK;
                ((App)Application.Current).Debitor = CurrentDebitor;
            }
            else if(parameter.ToLower() == "false")
            {
                result = ButtonResult.Cancel;
            }
        }
        public bool CanCloseDialog()
        {
            return IsValid;
        }

        public virtual void OnDialogClosed()
        {
           
        }

        public virtual void OnDialogOpened(IDialogParameters parameters)
        {
            
        }

        public event Action<IDialogResult> RequestClose;

        public virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }

    }
}