using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectI.WebApp.Core.Entities
{
    public class Profit
    {
        #region Field

        private int revenueId;
        private int totalRevenue;
        private int totalExpenditure;

        #endregion

        #region Constructor

        public Profit()
        {

        }

        #endregion

        #region Property

        public int RevenueId
        {
            get { return this.revenueId; }
            set { this.revenueId = value; }
        }

        public int TotalRevenue
        {
            get { return this.totalRevenue; }
            set { this.totalRevenue = value; }
        }

        public int TotalExpenditure
        {
            get { return this.totalExpenditure; }
            set { this.totalExpenditure = value; }
        }

        #endregion
    }
}
