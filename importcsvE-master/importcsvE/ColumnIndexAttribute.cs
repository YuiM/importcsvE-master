using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace importcsvE
{
    class ColumnIndexAttribute :Attribute
    {
        public ColumnIndexAttribute(int columnIndex)
        {
            this.ColumnIndex = columnIndex;
        }
        public int ColumnIndex { get; set; }
    }


    class UserInfomation
    {
        [ColumnIndex(0)]
        public string CusNam { get; set; } // プロパティでもOK

        [ColumnIndex(1)]
        public string UserName { get; set; } // プロパティでもOK

        [ColumnIndex(2)]
        public string UserTel { get; set; } // プロパティでもOK






    }
}
