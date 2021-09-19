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
    public class CustomerServices: BaseServices<Customer>, ICustomerServices
    {
        ICustomerRepository _customerRepository;
        public CustomerServices(IBaseRepository<Customer> baseRepository, ICustomerRepository customerRepository) : base(baseRepository)
        {
            _customerRepository = customerRepository;
        }

        public ServiceResult GetFilter(int? customerTypeId, string searchTerms)
        {
            try
            {
                //Xử lí nghiệp vụ
                //Lấy dữ liệu từ db
                List<Customer> customers = _customerRepository.GetFilter(customerTypeId, searchTerms);
                if (customers.Count() > 0)
                {
                    _serviceResult.Data = customers;
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
