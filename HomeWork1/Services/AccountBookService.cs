using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using HomeWork1.Models;
using HomeWork1.Repositories;
using HomeWork1.ViewModels;

namespace HomeWork1.Services
{
    public class AccountBookService
    {
        private readonly IRepository<AccountBook> _actBkRepos;
        private readonly IUnitOfWork _unitOfWork;

        public AccountBookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _actBkRepos = new Repository<AccountBook>(_unitOfWork);
        }

        public IEnumerable<BalanceEntry> Lookup()
        {
            var source = _actBkRepos.LookupAll();

            return source.Select(x => new BalanceEntry
            {
                Id = x.Id,
                Category = (EnumCategory) x.Categoryyy,
                Date = x.Dateee,
                Description = x.Remarkkk,
                Money = (decimal) x.Amounttt
            });
        }

        public void Add(BalanceEntry entry)
        {
            _actBkRepos.Create(new AccountBook
            {
                Amounttt = Convert.ToInt32(entry.Money),
                Categoryyy = (int) entry.Category,
                Dateee = entry.Date,
                Id = Guid.NewGuid(),
                Remarkkk = entry.Description
            });
        }

        public void Update(BalanceEntry entry)
        {
            var source = this.GetAccountBook(entry.Id);
            if (source == null)
            {
                throw new ApplicationException("no data exists");
            }
            else
            {
                source.Categoryyy = (int) entry.Category.Value;
                source.Dateee = entry.Date;
                source.Remarkkk = entry.Description;
                source.Amounttt = decimal.ToInt32(entry.Money);
            }
        }

        public BalanceEntry Get(Guid id)
        {
            var source = this.GetAccountBook(id);
            if (source != null)
            {
                return new BalanceEntry
                {
                    Id = source.Id,
                    Category = (EnumCategory)source.Categoryyy,
                    Date = source.Dateee,
                    Description = source.Remarkkk,
                    Money = (decimal)source.Amounttt
                };
            }

            return null;
        }

        protected AccountBook GetAccountBook(Guid id)
        {
            // Expression<Func<BalanceEntry, bool>> 如何轉換為 Expression<Func<AccountBook, bool>>
            Expression<Func<AccountBook, bool>> filter = x => x.Id == id;
            return _actBkRepos.GetSingle(filter);
        }

        public void Save()
        {
            _unitOfWork.Save();
        }
    }
}