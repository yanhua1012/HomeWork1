using System;
using System.ComponentModel.DataAnnotations;
using HomeWork1.Attributes;

namespace HomeWork1.ViewModels
{
    public class BalanceEntry
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "類別為必填欄位")]
        public EnumCategory? Category { get; set; }

        [Required(ErrorMessage = "金額為必填欄位")]
        [Range(0, int.MaxValue, ErrorMessage= "金額需大於0")]
        public decimal Money { get; set; }

        [Required(ErrorMessage = "日期為必填欄位")]
        [DataType(DataType.Date, ErrorMessage ="請輸入日期格式")]
        [LargeOrEqualDate()]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "備註為必填欄位")]
        [StringLength(100, ErrorMessage = "備註最多輸入100個字元")]
        public string Description { get; set; }
    }

    public enum EnumCategory
    {
        [Display(Name = "支出")]
        Expense,
        [Display(Name = "收入")]
        Revenue
    }
}