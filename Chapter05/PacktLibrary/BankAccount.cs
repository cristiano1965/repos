using System;
using System.Collections.Generic;
using System.Text;

namespace Packt.Shared
{
    public  class BankAccount
    {
        public string AccountName;
        public decimal Balance;
        public static decimal InterestRate; //static: sarà condiviso con tutti i membri di istanza
    }
}
