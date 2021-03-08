using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using TheDebtBook.Models;

namespace TheDebtBook.ViewModels
{
    public class EditShowDetailViewModel : BindableBase
    {
        private Debitors _currentDebitor;
        private ObservableCollection<Debt> _debitorCreditorDetails;
        private Debt _currentPost;
       private int currentIndex = -1;

        public EditShowDetailViewModel(Debitors debitor)
        {

            CurrentDebitor = debitor;

            DebitorCreditorDetails = new ObservableCollection<Debt>();

            DebitorCreditorDetails = _currentDebitor._debts;

            CurrentPost = DebitorCreditorDetails[0];
        }

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
            set => _debitorCreditorDetails = value;
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
                   DebitorCreditorDetails.Add(newpost);
                   CurrentPost = newpost;
                   CurrentIndex = 0;
                }));
            }
        }



    }
}