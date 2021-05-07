using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Courses.Models.Services.Application
{
    public class Money
    {
        private decimal amount = 0;
        
        public Money(Currency currency, decimal amount)
        {
            this.Amount = amount;
            this.Currency = currency;
        }

        public Currency Currency
        {
            get; set;
        }

        public decimal Amount
        {
            get
            {
                return amount;
            }
            set
            {
                if (value <0)
                {
                    throw new InvalidOperationException("The amount cannot be negative");
                }
                amount = value;
            }
        }
        public Money(): this(Currency.EUR, 0.00m)
        {
        }

        public override bool Equals(object obj)
        {
            var money = obj as Money;

            return money != null && this.Amount == money.Amount && this.Currency == money.Currency;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Amount, Currency);
        }

        public override string ToString()
        {
            return $"{Currency} {Amount:#.00}";
        }
    }
}
