using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MvcApp.Models
{
    public class Contact : IDataErrorInfo
    {
        public string Error
        {
            get { return "无效联系人！"; }
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "Name": return "姓名是必需的！";
                    case "PhoneNo": return "电话号码格式错误！";
                    case "EmailAdderss": return "无效的电子邮箱地址！";
                    default: return null;
                }
            }
        }

        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string EmailAdderss { get; set; }
    }

}