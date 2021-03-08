using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Documents;

namespace TheDebtBook.Models
{
    public class Debitors
    {
        private string _name;
        private string _balance;
        private string _latestdate;
        public ObservableCollection<Debt> _debts;

        public Debitors(string name)
        {
            _name = name;
        }

        // +1 overload
        public Debitors(string name, string balance)
        {
            _name = name;
            _balance = balance;
        }


        #region Properties

        public string Name
        {
            get { return _name; }
            set => _name = value;
        }

        public string Balance
        {
            get { return _balance; }
            set => _balance = value;
        }

       public string LatestDebt 
        {
            get => DateTime.Now.ToLongDateString();
            set => _latestdate = DateTime.Now.ToLongDateString(); 
        }

        #endregion
    }
}