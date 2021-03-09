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
                new Debitors("Rasmus Trads",(new Debt("dildos",-100))),
                new Debitors("Kurt Thorsen",(new Debt("cocaine",-1000))),
                new Debitors("Stein Bagger",(new Debt("teddybears",-1000)))
                #endif
            };

            DebitorsCreditors[0].addDebt("awsomeness",10000);
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

        private string _title = "TheDebtBook";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        #endregion


        #region Commands

        //Go to Add view
        private ICommand _addDebtorCommand;

        public ICommand AddDebtorCommand
        {
            get
            {
                return _addDebtorCommand ?? (_addDebtorCommand = new DelegateCommand(AddDebtorCommandExecute));
            }
        }

        public void AddDebtorCommandExecute()
        {
            //var newDebitor = new Debitors("Insert name");
            //Is AddView, naming error
            _iDialogService.ShowDialog("AddView", null, r =>
            {
                if (r.Result == ButtonResult.OK)
                {
                    DebitorsCreditors.Add(((App)Application.Current).Debitor);
                }
            });
        }

        //Go to EditShowDetailView
        private DelegateCommand _editDebtorCommand;

        public DelegateCommand EditDebtorCommand =>
            _editDebtorCommand ?? (_editDebtorCommand = new DelegateCommand(ShowEditDebtorCommand));

        public void ShowEditDebtorCommand()
        {
            var tempdebitor = new Debitors()
            {
                Debts = CurrentDebitorCreditor.Debts,
                Name = CurrentDebitorCreditor.Name,
                Balance = CurrentDebitorCreditor.Balance,
                LatestDebt = CurrentDebitorCreditor.LatestDebt
            };

            ((App) Application.Current).Debitor = tempdebitor;

            _iDialogService.ShowDialog("DebtorsView", null, r =>
            {
                if (r.Result == ButtonResult.None)
                {
                    Title = "Result is None";
                }
                else if (r.Result == ButtonResult.OK)
                {
                    Title = "Result is OK";
                    CurrentDebitorCreditor = ((App)Application.Current).Debitor;
                }
                else if (r.Result == ButtonResult.Cancel)
                {
                    Title = "Result is Cancel";
                }
                else
                {
                    Title = "I Don't know what you did!?";
                }


            });

        }


        #endregion

    }
}