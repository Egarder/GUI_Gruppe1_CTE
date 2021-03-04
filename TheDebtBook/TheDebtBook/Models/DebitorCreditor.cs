using System;
using System.Collections.Specialized;

namespace TheDebtBook.Models
{
    public class DebitorCreditor
    {
        private string _name;
        private string _balance;
        private string _lastupdated;
        public DebitorCreditor(string name)
        {
            _name = name;
        }

        // +1 overload
        public DebitorCreditor(string name, string balance)
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

        public string LastUpdated
        {
            get
            {
                return DateTime.Now.ToLongDateString();
            }
            set => _lastupdated = value;
        }

        #endregion
    }
}