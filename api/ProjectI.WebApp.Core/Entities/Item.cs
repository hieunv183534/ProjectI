using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectI.WebApp.Core.Entities
{
    public class Item
    {
        #region Field

        private int itemId;
        private string itemName;
        private int itemPrice;
        private string itemInfo;

        #endregion

        #region Constructor

        public Item()
        {

        }

        #endregion

        #region Property

        public int ItemId
        {
            get { return this.itemId; }
            set { this.itemId = value; }
        }

        public string ItemName
        {
            get { return this.itemName; }
            set { this.itemName = value; }
        }

        public int  ItemPrice
        {
            get { return this.itemPrice; }
            set { this.itemPrice = value; }
        }

        public string ItemInfo
        {
            get { return this.itemInfo; }
            set { this.itemInfo = value; }
        }

        #endregion
    }
}
