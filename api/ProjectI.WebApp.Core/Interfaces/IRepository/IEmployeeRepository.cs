using ProjectI.WebApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectI.WebApp.Core.Interfaces.IRepository
{
    public interface IEmployeeRepository: IBaseRepository<Employee>
    {
        /// <summary>
        /// lấy ds nhân viên theo hai tiêu chí
        /// </summary>
        /// <param name="positionId"></param>
        /// <param name="searchTerms"></param>
        /// <returns></returns>
        List<Employee> GetFilter(int? positionId, string searchTerms);
    }
}
