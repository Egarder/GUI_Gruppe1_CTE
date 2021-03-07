using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace TheDebtBook.Models
{
    class Debt
    {
        private double _amount;

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
    }
}
