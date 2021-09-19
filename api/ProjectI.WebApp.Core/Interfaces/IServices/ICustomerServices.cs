using ProjectI.WebApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectI.WebApp.Core.Interfaces.IServices
{
    public interface ICustomerServices : IBaseServices<Customer>
    {
        /// <summary>
        /// Xử lí nghiệp vụ lọc khách hàng
        /// </summary>
        /// <param name="customerTypeId"></param>
        /// <param name="searchTerms"></param>
        /// <returns></returns>
        ServiceResult GetFilter(int? customerTypeId, string searchTerms);
    }
}
