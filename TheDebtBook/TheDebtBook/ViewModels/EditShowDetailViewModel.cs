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