using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace TheDebtBook.Models
{
    public class Debt
    {
        private double _amount;

        private string _lastupdated;

        // Default constructor
        public Debt()
        { }

        // Explicit constructor
        public Debt(double amount)
        {
            _amount = amount;
        }

        public double Amount
        {
            get => _amount;
            set => _amount = value;
        }

        public string LastUpdated
        {
            get
            {
                return DateTime.Now.ToLongDateString();
            }
            set => _lastupdated = value;
        }
    }
}
