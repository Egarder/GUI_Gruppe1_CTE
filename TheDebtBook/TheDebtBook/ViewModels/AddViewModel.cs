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
        private bool _btnOKPressed;

        public Debitors CurrentDebitor
        {
            get { return _currentDebitor; }
            set => _currentDebitor = value;
        }

        public bool IsValid
        {
            get
            {
                bool isValid = true;
                if (string.IsNullOrWhiteSpace(CurrentDebitor.Name))
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

        public bool BtnOKPressed
        {
            get { return _btnOKPressed; }
            set
            {
                SetProperty(ref _btnOKPressed, value);
            }
        }
        DelegateCommand<string> _closeBtnCommand;
        public DelegateCommand<string> CloseBtnCommand => _closeBtnCommand ?? (_closeBtnCommand = new DelegateCommand<string>(CloseDialog));

        protected virtual void CloseDialog(string parameter)
        {
            ButtonResult result = ButtonResult.None;
            if (parameter?.ToLower()== "true")
            {
                result = ButtonResult.OK;
                BtnOKPressed = true;
            }
            RequestClose(new DialogResult(result));
        }
        public bool CanCloseDialog()
        {
            return IsValid;
        }

        public virtual void OnDialogClosed()
        {
            if (BtnOKPressed)
            {
                ((App)Application.Current).Debitor = CurrentDebitor;
            }
        }

        public virtual void OnDialogOpened(IDialogParameters parameters)
        {
            CurrentDebitor = new Debitors("Default", 0.00, new Debt(0.00));
        }

        public event Action<IDialogResult> RequestClose;

        public virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }

    }
}