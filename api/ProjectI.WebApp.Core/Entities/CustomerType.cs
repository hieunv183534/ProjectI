using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectI.WebApp.Core.Entities
{
    public class CustomerType
    {
        #region Field 

        private int customerTypeId;
        private string customerTypeName;
        private float? offerCoefficient;

        #endregion

        #region Constructor

        public CustomerType()
        {

        }

        #endregion

        #region Property

        public int CustomerTypeId
        {
            get { return this.customerTypeId; }
            set { this.customerTypeId = value; }
        }

        public string CustomerTypeName
        {
            get { return this.customerTypeName; }
            set { this.customerTypeName = value; }
        }

        public float? OfferCoefficient
        {
            get { return this.offerCoefficient; }
            set { this.offerCoefficient = value; }
        }

        #endregion
    }
}
