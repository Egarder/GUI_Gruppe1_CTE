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
        private Debt _currentPost = new Debt();
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


        ///Commands
        ///
        private ICommand _newPost;

        public ICommand NewPost
        {
            get
            {
                return _newPost ?? (_newPost = new DelegateCommand(() =>
                {
                    if (CurrentDebitor.Debts != null)
                    {
                        CurrentPost = new Debt();
                        CurrentDebitor.Debts.Add(CurrentPost);
                        ++CurrentIndex;
                        CurrentDebitor.UpdateBalance();
                    }
                }));
            }
        }

        ICommand _deletePostCommand;
        public ICommand DeletePostCommand => _deletePostCommand ?? (_deletePostCommand = new DelegateCommand(DeletePost, DeletePost_CanExecute).ObservesProperty(() => CurrentIndex));

        private void DeletePost()
        {
            MessageBoxResult res = MessageBox.Show("Are you sure you want to delete post " + CurrentPost.PostName + "?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            
            if (res == MessageBoxResult.Yes)
            {
                CurrentDebitor.Debts.Remove(CurrentPost);
            }

            CurrentDebitor.UpdateBalance();
        }

        private bool DeletePost_CanExecute()
        {
            return true;
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
            ((App)Application.Current).Debitor = CurrentDebitor;
        }

        public virtual void OnDialogOpened(IDialogParameters parameters)
        {
            CurrentDebitor = ((App)Application.Current).Debitor;
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