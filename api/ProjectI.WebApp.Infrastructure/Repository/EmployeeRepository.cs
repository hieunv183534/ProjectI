using Dapper;
using ProjectI.WebApp.Core.Entities;
using ProjectI.WebApp.Core.Interfaces.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectI.WebApp.Infrastructure.Repository
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public List<Employee> GetFilter(int? positionId, string searchTerms)
        {
            DynamicParameters parameters = new DynamicParameters();
            var sql = $"select * from Employee ";
            var sqlCondition = $"where 1=1 ";

            // nếu có search thì thêm điều kiệu sql là hoặc fullname, hoặc mã khách hàng, hoặc số điện thoại
            if (searchTerms != null && searchTerms != "")
            {
                //parameters.Add($"@FullName", searchTerms);
                parameters.Add($"@PhoneNumber", searchTerms);
                sqlCondition += $" and ( FullName like N'%{searchTerms}%' or PhoneNumber=@PhoneNumber ) ";
            }

            // nếu CustomerGroupId được nhận và là 1 guid thì thêm điều kiện and CustomerGroupId = @CustomerGroupId
            if (positionId != null)
            {
                parameters.Add($"@PositionId", positionId);
                sqlCondition += $" and PositionId = @PositionId ";
            }
            sql += sqlCondition;

            // thực hiện truy vấn
            var employees = dbConnection.Query<Employee>(sql, param: parameters);
            return (List<Employee>)employees;
        }
    }
}
