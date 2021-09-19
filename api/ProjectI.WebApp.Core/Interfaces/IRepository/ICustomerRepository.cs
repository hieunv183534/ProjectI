using ProjectI.WebApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectI.WebApp.Core.Interfaces.IRepository
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        /// <summary>
        /// lấy ds khách hàng theo 2 tiêu chí
        /// </summary>
        /// <param name="customerTypeId"></param>
        /// <param name="searchTerms"></param>
        /// <returns></returns>
        List<Customer> GetFilter(int? customerTypeId, string searchTerms);
    }
}
