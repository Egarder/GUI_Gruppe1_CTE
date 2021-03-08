using System;
using System.Collections.ObjectModel;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using TheDebtBook.Models;

namespace TheDebtBook.ViewModels
{
    public class EditShowDetailViewModel : BindableBase
    {

        private Debitors _currentDebitors;

        public Debitors CurrentAgent
        {
            get { return _currentDebitors; }
            set { SetProperty(ref _currentDebitors, value); }
        }

        private ObservableCollection<Debt> _debitorCreditorDetails;

        public ObservableCollection<Debt> DebitorCreditorDetails
        {
            get { return _debitorCreditorDetails; }
        }

        public EditShowDetailViewModel()
        {
            _debitorCreditorDetails = new ObservableCollection<Debt>();

            _debitorCreditorDetails = _currentDebitors._debts;
        }
    }
}