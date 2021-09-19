using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjectI.WebApp.Core.Services
{
    class Common
    {
        /// <summary>
        /// Kiểm tra một email có hợp lệ hay không
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        /// Author hieunv 16/08/2021
        public static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
    }
}
