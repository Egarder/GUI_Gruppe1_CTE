using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Windows.Documents;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using TheDebtBook.Models;

namespace TheDebtBook.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private IDialogService _iDialogService;
        private ObservableCollection<DebitorCreditor> _debitorsCreditors;
        private DebitorCreditor _currentDebitorCreditor = null;
        private int _currentIndex = -1;
        
        public MainWindowViewModel(IDialogService dialogService)
        {
            _iDialogService = dialogService;
            
            _debitorsCreditors = new ObservableCollection<DebitorCreditor>()
            {
                #if DEBUG
                new DebitorCreditor("Rasmus Trads", "-120000"),
                new DebitorCreditor("Kurt Thorsen", "-375000"),
                new DebitorCreditor("Stein Bagger", "-575000")
                #endif
            };
        }

        #region Properties

        public ObservableCollection<DebitorCreditor> DebitorsCreditors
        {
            get { return _debitorsCreditors; }
        }

        public DebitorCreditor CurrentDebitorCreditor
        {
            get { return _currentDebitorCreditor; }
            set
            {
                SetProperty(ref _currentDebitorCreditor, value);
            }
        }

        public int CurrentIndex
        {
            get { return _currentIndex; }
            set
            {
                SetProperty(ref _currentIndex, value);
            }
        }

        #endregion


        #region Commands

        private ICommand _addDebtorCommand;

        public ICommand AddDebtorCommand
        {
            get
            {
                return _addDebtorCommand ?? (_addDebtorCommand = new DelegateCommand(AddDebtorExcecute));
            }
        }

        private void AddDebtorExcecute()
        {
            _iDialogService.ShowDialog("DebtorsView");
        }

        #endregion

    }
}