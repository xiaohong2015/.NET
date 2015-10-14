using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp
{
    public interface IEmployeeSearchView
    {
        void BindEmployees(IEnumerable<Employee> employees);
        void BindDepartments(IEnumerable<string> departments);
        event EventHandler<DepartmentSelectedEventArgs> DepartmentSelected;
    }
}