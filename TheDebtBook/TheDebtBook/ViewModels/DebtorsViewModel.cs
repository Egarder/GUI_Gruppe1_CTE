using System;
using Prism.Services.Dialogs;

namespace TheDebtBook.ViewModels
{
    public class DebtorsViewModel : IDialogAware
    {
        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            
        }

        public string Title { get; }
        public event Action<IDialogResult> RequestClose;
    }
}