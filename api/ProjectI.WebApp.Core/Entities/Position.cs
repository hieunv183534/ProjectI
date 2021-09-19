using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectI.WebApp.Core.Entities
{
    public class Position
    {
        #region Field

        private int positionId;
        private string positionName;

        #endregion

        #region Cosnstructor

        public Position()
        {

        }

        #endregion

        #region Property

        public int PositionId
        {
            get { return this.positionId; }
            set { this.positionId = value; }
        }

        public string PositionName
        {
            get { return this.positionName; }
            set { this.positionName = value; }
        }

        #endregion
    }
}
