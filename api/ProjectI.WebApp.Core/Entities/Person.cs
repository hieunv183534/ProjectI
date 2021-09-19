using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectI.WebApp.Core.Entities
{
    public class Person
    {
        #region Field
        private string fullName;
        private int? gender;
        private DateTime? dateOfBirth;
        private string address;
        private string email;
        private string phoneNumber;
        #endregion

        #region Constructor

        public Person()
        {
        }

        #endregion

        #region Property

        /// <summary>
        /// Họ và tên
        /// </summary>
        [Requied]
        public string FullName
        {
            get { return this.fullName; }
            set { this.fullName = value; }
        }

        /// <summary>
        /// Giới tính: 0-nữ; 1-nam; 2-không xác định
        /// </summary>
        public int? Gender
        {
            get { return this.gender; }
            set { this.gender = value; }
        }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth
        {
            get { return this.dateOfBirth; }
            set { this.dateOfBirth = value; }
        }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address
        {
            get { return this.address; }
            set { this.address = value; }
        }

        /// <summary>
        /// Email
        /// </summary>
        public string Email
        {
            get { return this.email; }
            set { this.email = value; }
        }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string PhoneNumber
        {
            get { return this.phoneNumber; }
            set { this.phoneNumber = value; }
        }

        #endregion

        #region Method

        #endregion
    }
}
