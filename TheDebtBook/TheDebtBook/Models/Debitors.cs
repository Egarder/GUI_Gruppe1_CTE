using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Documents;
using Prism.Mvvm;

namespace TheDebtBook.Models
{
    public class Debitors: BindableBase
    {
        private string _name;
        private double _balance;
        private string _latestdate;
        private ObservableCollection<Debt> _debts;

        public Debitors() 
        {
            _debts = new ObservableCollection<Debt>();
        }
        public Debitors(string name)
        {
            _name = name;
            _debts = new ObservableCollection<Debt>();
        }

        // +1 overload
        public Debitors(string name, double balance)
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

            _balance = debt.Amount;
        }
        public Debitors(string name, double balance, Debt debt)
        {
            _name = name;
            _balance = balance;
            _debts = new ObservableCollection<Debt>();

            if (debt != null) 
                _debts.Add(debt);
        }

        public ObservableCollection<Debt> Debts
        {
            get { return _debts; }
            set { SetProperty(ref _debts, value); }
        }

        public double SumOfDebt()
        {
            double tempdebt = 0;

            foreach (Debt element in Debts)
            {
                    tempdebt += element.Amount;
            }

            return tempdebt;
        }

        #region Properties

        public string Name
        {
            get { return _name; }
            set => _name = value;
        }

        public double Balance
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

       public void UpdateBalance()
       {
           Balance = SumOfDebt();
       }

        #endregion
    }
}