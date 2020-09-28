using SneakerShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SneakerShopAPI.ViewModels
{
    public class AccountVModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
        public string Role { get; set; }
        public bool? DelFlg { get; set; }

        public const string ROLE_ADMIN = "admin";
        public const string ROLE_CUSTOMER = "customer";

        public static AccountVModel ToVModel(Account model)
        {
            return model == null ? null :
                new AccountVModel()
                {
                    Username = model.Username,
                    DelFlg = model.DelFlg,
                    Email = model.Email,
                    Fullname = model.Fullname,
                    Photo  = model.Photo,
                    Role = model.Role
                };
        }
    }
}
