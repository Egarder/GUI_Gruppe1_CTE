using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Windows.Documents;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using TheDebtBook.Models;
using TheDebtBook.Views;
using Microsoft.Win32;
using System.Threading;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Threading;


namespace TheDebtBook.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private IDialogService _iDialogService;
        private ObservableCollection<Debitors> _debitorsCreditors;
        private Debitors _currentDebitorCreditor = null;
        private int _currentIndex = -1;

        public MainWindowViewModel(IDialogService dialogService)
        {
            _iDialogService = dialogService;
            
            _debitorsCreditors = new ObservableCollection<Debitors>()
            {
                #if DEBUG
                new Debitors("Rasmus Trads", "-120000"),
                new Debitors("Kurt Thorsen", "-375000"),
                new Debitors("Stein Bagger", "-575000")
                #endif
            };
        }

        #region Properties

        public ObservableCollection<Debitors> DebitorsCreditors
        {
            get { return _debitorsCreditors; }
        }

        public Debitors CurrentDebitorCreditor
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
                return _addDebtorCommand ?? (_addDebtorCommand = new DelegateCommand(()=>
                {
                    var newDebitor = new Debitors("Insert name");
                    //Is AddView, naming error
                    _iDialogService.ShowDialog("DebtsView", null, (r) =>
                       {
                           _debitorsCreditors.Add(newDebitor);
                           CurrentDebitorCreditor = newDebitor;
                       });
                }));
            }
        }


        #endregion

    }
}