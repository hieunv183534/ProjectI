using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectI.WebApp.Core.Entities
{
    public class Employee: Person
    {
        #region Field

        private int employeeId;
        private string identityNumber;
        private DateTime? joinDate;
        private int positionId;
        private int? workStatus;
        private int? salary;



        #endregion

        #region Constructor

        public Employee()
        {

        }

        #endregion

        #region Property

        /// <summary>
        /// Id nhân viên
        /// </summary>
        [Requied]
        [NotAllowDuplicate]
        public int EmployeeId
        {
            get { return this.employeeId; }
            set { this.employeeId = value; }
        }

   
        /// <summary>
        /// Số cmnd/cccd
        /// </summary>
        public string IdentityNumber
        {
            get { return this.identityNumber; }
            set { this.identityNumber = value; }
        }

        /// <summary>
        /// Ngày vào công ty
        /// </summary>
        public DateTime? JoinDate
        {
            get { return this.joinDate; }
            set { this.joinDate = value; }
        }

        /// <summary>
        /// id Vị trí
        /// </summary>
        public int PositionId
        {
            get { return this.positionId; }
            set { this.positionId = value; }
        }

        /// <summary>
        /// Tình trang làm việc: 0-đã nghỉ việc; 1-đang làm việc; 2-đan thử việc
        /// </summary>
        public int? WorkStatus
        {
            get { return this.workStatus; }
            set { this.workStatus = value; }
        }

        /// <summary>
        /// Lương cơ bản
        /// </summary>
        public int? Salary
        {
            get { return this.salary; }
            set { this.salary = value; }
        }

        #endregion

        #region Method

        #endregion
    }
}
