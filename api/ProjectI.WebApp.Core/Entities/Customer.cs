using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectI.WebApp.Core.Entities
{
    public class Customer: Person
    {
        #region Field

        private int customerId;
        private int customerTypeId;
        private int? debit;

        #endregion

        #region Constructor

        public Customer()
        {

        }

        #endregion

        #region Property

        [Requied]
        [NotAllowDuplicate]
        public int CustomerId
        {
            get { return this.customerId; }
            set { this.customerId = value; }
        }  

        public int CustomerTypeId
        {
            get { return this.customerTypeId; }
            set { this.customerTypeId = value; }
        }

        public int? Debit
        {
            get { return this.debit; }
            set { this.debit = value; }
        }

        #endregion

        #region Method

        #endregion
    }
}
