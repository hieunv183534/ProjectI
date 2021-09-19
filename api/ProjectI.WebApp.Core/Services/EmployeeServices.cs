using ProjectI.WebApp.Core.Entities;
using ProjectI.WebApp.Core.Interfaces.IRepository;
using ProjectI.WebApp.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectI.WebApp.Core.Services
{
    public class EmployeeServices: BaseServices<Employee>, IEmployeeServices
    {
        IEmployeeRepository _employeeRepository;
        public EmployeeServices(IBaseRepository<Employee> baseRepository, IEmployeeRepository employeeRepository) : base(baseRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public ServiceResult GetFilter(int? positionId, string searchTerms)
        {
            try
            {
                //Xử lí nghiệp vụ
                //Lấy dữ liệu từ db
                List<Employee> employees = _employeeRepository.GetFilter(positionId, searchTerms);
                if (employees.Count() > 0)
                {
                    _serviceResult.Data = employees;
                    _serviceResult.StatusCode = 200;
                    return _serviceResult;
                }
                else
                {
                    _serviceResult.StatusCode = 204;
                    return _serviceResult;
                }
            }
            catch (Exception ex)
            {
                var errorObj = new
                {
                    devMsg = ex.Message,
                    userMsg = Resources.ResourceVN.Error_User,
                };
                _serviceResult.Data = errorObj;
                _serviceResult.StatusCode = 500;
                return _serviceResult;
            }
        }
    }
}
