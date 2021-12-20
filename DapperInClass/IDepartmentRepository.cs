using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperInClass
{
    public interface IDepartmentRepository
    {
        public IEnumerable<Department> GetAllDepartments();
    }
}
