using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectI.WebApp.Core.Entities
{
    // thông tin bắt buộc nhập
    [AttributeUsage(AttributeTargets.Property)]
    public class Requied : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Property)]
    public class NotAllowDuplicate : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKey : Attribute
    {

    }
}
