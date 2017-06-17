using System;
using System.Collections.Generic;
using System.Linq;
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

        public void Save()
        {
            _unitOfWork.Save();
        }
    }
}