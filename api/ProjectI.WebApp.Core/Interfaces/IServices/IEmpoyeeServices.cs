using ProjectI.WebApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectI.WebApp.Core.Interfaces.IServices
{
    public interface IEmployeeServices : IBaseServices<Employee>
    {
        /// <summary>
        /// xử lí nghiệp vụ lọc nhân viên
        /// </summary>
        /// <param name="positionId"></param>
        /// <param name="searchTerms"></param>
        /// <returns></returns>
        ServiceResult GetFilter(int? positionId, string searchTerms);
    }
}
