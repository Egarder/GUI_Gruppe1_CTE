using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using TheDebtBook.Models;

namespace TheDebtBook.ViewModels
{
    public class EditShowDetailViewModel : BindableBase, IDialogAware
    {
        private Debitors _currentDebitor;
        private ObservableCollection<Debt> _debitorCreditorDetails;
        private Debt _currentPost;
        private int currentIndex = -1;


        public int CurrentIndex
        {
            get { return currentIndex; }
            set
            {
                SetProperty(ref currentIndex, value);
            }
        }

        public Debt CurrentPost
        {
            get { return _currentPost;}
            set { SetProperty(ref _currentPost, value); }
        }

        public Debitors CurrentDebitor
        {
            get { return _currentDebitor; }
            set { SetProperty(ref _currentDebitor, value); }
        }


        public ObservableCollection<Debt> DebitorCreditorDetails
        {
            get { return _debitorCreditorDetails; }
            set { SetProperty(ref _debitorCreditorDetails, value); }
        }


        ///Commands
        ///
        private ICommand _newPost;

        public ICommand NewPost
        {
            get
            {
                return _newPost ?? (_newPost = new DelegateCommand(() =>
                {
                    var newpost = new Debt();
                    if (DebitorCreditorDetails != null) DebitorCreditorDetails.Add(newpost);
                    CurrentPost = newpost;
                   CurrentIndex = 0;
                }));
            }
        }



        //Dialogaware
        public bool CanCloseDialog()
        {
            return true;
        }

        private DelegateCommand<string> _closeDialogCommand;
        public DelegateCommand<string> CloseDialogCommand =>
            _closeDialogCommand ?? (_closeDialogCommand = new DelegateCommand<string>(CloseDialog));

        protected virtual void CloseDialog(string parameter)
        {
            ButtonResult result = ButtonResult.None;

            if (parameter?.ToLower() == "true")
            {
                result = ButtonResult.OK;
                // Use the Application object to transfer data to the MainWindow
                ((App)Application.Current).Debitor = CurrentDebitor;
            }
            else if (parameter?.ToLower() == "false")
                result = ButtonResult.Cancel;

            RaiseRequestClose(new DialogResult(result));
        }

        public virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }

        public void OnDialogClosed()
        {
            
        }

        public virtual void OnDialogOpened(IDialogParameters parameters)
        {
            CurrentDebitor = ((App)Application.Current).Debitor;

            DebitorCreditorDetails = new ObservableCollection<Debt>();

            DebitorCreditorDetails = CurrentDebitor.Debts;
        }

        private string _title = "EditShowDetail dialog";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public event Action<IDialogResult> RequestClose;
    }
}