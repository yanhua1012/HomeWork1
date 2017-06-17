using HomeWork1.Models;

namespace HomeWork1.Helper
{
    public static class BalanceEntryHelper
    {
        public static string GetCategoryText(this BalanceEntry entry)
        {
            if (entry.Category.HasValue)
            {
                switch (entry.Category.Value)
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