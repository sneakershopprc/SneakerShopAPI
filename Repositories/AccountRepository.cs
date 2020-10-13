using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SneakerShopAPI.Models;
using SneakerShopAPI.ViewModels;

namespace SneakerShopAPI.Repositories
{
    public class AccountRepository : BaseRepository
    {
        public AccountRepository(SneakerShopContext _context) : base(_context)
        {
        }

        public IEnumerable<AccountVModel> GetList(string role)
        {
            var listAcc = new List<AccountVModel>();
            List<Account> result;

            if (role == null) result = context.Account.ToList();
            else
            {
                result = context.Account.Where(e => e.Role.Equals(role)).ToList();
            }

            if (result != null)
            {
                foreach (Account acc in result)
                {
                    listAcc.Add(AccountVModel.ToVModel(acc));
                }
            }

            return listAcc;
        }

        public AccountVModel GetById(string username)
        {
            return AccountVModel.ToVModel(context.Account.Find(username));
        }

        public AccountVModel UpdateAccount(AccountVModel vmodel)
        {
            var acc = context.Account.Find(vmodel.Username);

            if (acc != null)
            {
                acc.Email = vmodel.Email;
                acc.Fullname = vmodel.Fullname;
                acc.Photo = vmodel.Photo;

                context.Account.Update(acc);
                context.SaveChanges();

                return AccountVModel.ToVModel(acc);
            }

            return null;
        }

        public bool SwitchAccountMode(string username)
        {
            var acc = context.Account.Find(username);

            if (acc == null) return false;

            acc.DelFlg = !acc.DelFlg;
            context.Account.Update(acc);
            context.SaveChanges();
            return true;
        }
    }
}
