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

        public Debitors() { }
        public Debitors(string name)
        {
            _name = name;
            _debts = new ObservableCollection<Debt>();
        }

        // +1 overload
        public Debitors(string name, string balance)
        {
            _name = name;
            _balance = balance;
            _debts = new ObservableCollection<Debt>();
        }
        public Debitors(string name, Debt debt)
        {
            _name = name;
            _debts = new ObservableCollection<Debt>();

            if (debt != null)
                _debts.Add(debt);

            _balance = debt.Amount.ToString();
        }
        public Debitors(string name, string balance, Debt debt)
        {
            _name = name;
            _balance = balance;
            _debts = new ObservableCollection<Debt>();

            if (debt != null) 
                _debts.Add(debt);
        }


        public string SumOfDebt()
        {
            double tempdebt = 0;

            foreach (Debt element in _debts)
            {
                tempdebt += element.Amount;
            }

            return (tempdebt.ToString());
        }

        #region Properties

        public string Name
        {
            get { return _name; }
            set => _name = value;
        }

        public string Balance
        {
            get { return SumOfDebt(); }
            set => _balance = value;
        }

       public string LatestDebt 
        {
            get => DateTime.Now.ToLongDateString();
            set => _latestdate = DateTime.Now.ToLongDateString(); 
        }

       public void addDebt(string postname, double postvalue)
       {
           _debts.Add(new Debt(postname, postvalue));
       }

        #endregion
    }
}