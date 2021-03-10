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
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TheDebtBook.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private SaveFileDialog saveFile = new SaveFileDialog();
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
            set { SetProperty(ref _debitorsCreditors, value); }
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

        //Open text file command

        private ICommand _openFileCommandtxt;

        public ICommand _OpenFileCommandTxt
        {
            get
            {
                return _openFileCommandtxt ?? (_openFileCommandtxt =
                    new DelegateCommand(OpenFileCommandExecuteTxt, CommandCanExecute).
                        ObservesProperty(() => DebitorsCreditors.Count));
            }
        }

        private void OpenFileCommandExecuteTxt()
        {
            OpenFileDialog fs = new OpenFileDialog();// { Filter = "Json (.json)|.json" };

            fs.ShowDialog();

            fs.ShowDialog();

            string jsonString = File.ReadAllText(fs.FileName);

            DebitorsCreditors = JsonSerializer.Deserialize<ObservableCollection<Debitors>>(jsonString);
        }



        //Save as text command
        private ICommand _saveFileCommandjson;

        public ICommand SaveFileCommandJson
        {
            get
            {
                return _saveFileCommandjson ?? (_saveFileCommandjson =
                    new DelegateCommand(SaveFileCommandExecuteJson, CommandCanExecute).
                        ObservesProperty(() => DebitorsCreditors.Count));
            }
        }

        private void SaveFileCommandExecuteJson()
        {

            SaveFileDialog dialogsave = new SaveFileDialog() {CreatePrompt = true, OverwritePrompt = true, DefaultExt = "txt", Filter = "Text file (.txt)|.txt| JSON file (.json)|.json" };

            dialogsave.ShowDialog();

            string jsonString = JsonSerializer.Serialize(DebitorsCreditors);
            File.WriteAllText(dialogsave.FileName, jsonString);

        }

        private bool CommandCanExecute()
        {
            return true;
        }

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
            Debitors copydebitor = new Debitors(DebitorsCreditors[CurrentIndex]);

            ((App)Application.Current).Debitor = DebitorsCreditors[CurrentIndex];

            _iDialogService.ShowDialog("EditShowDetailView", null, r =>
            {
                if (r.Result == ButtonResult.OK)
                {
                    DebitorsCreditors[CurrentIndex] = ((App)Application.Current).Debitor;
                    DebitorsCreditors[CurrentIndex].UpdateBalance();
                }
                else
                {
                    DebitorsCreditors[CurrentIndex] = copydebitor;
                    DebitorsCreditors[CurrentIndex].UpdateBalance();
                }
            });

        }

        private ICommand _removeDebitor;

        public ICommand RemoveDebitor
        {
            get
            {
                return _removeDebitor ?? (_removeDebitor = new DelegateCommand(() => RemoveDebitorCommand()));
            }
        }

        private void RemoveDebitorCommand()
        {
            DebitorsCreditors.RemoveAt(CurrentIndex);
        }


        #endregion

    }
}