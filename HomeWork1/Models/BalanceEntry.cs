using System;
using System.ComponentModel.DataAnnotations;

namespace HomeWork1.Models
{
    public class BalanceEntry
    {
        public EnumCategory? Category { get; set; }

        public decimal Money { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public string CategoryText
        {
            get
            {
                if (Category.HasValue)
                {
                    switch (Category.Value)
                    {
                        case EnumCategory.Expense:
                            return "支出";
                        case EnumCategory.Revenue:
                            return "收入";
                    }
                }

                return null;
            }
        }

    }

    public enum EnumCategory
    {
        [Display(Name = "支出")]
        Expense,
        [Display(Name = "收入")]
        Revenue
    }
}