using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleManagement.Service.Models.Response
{
    public class ResponseModel<T>
    {
        public bool Success
        {
            get
            {
                return Errors == null || Errors.Count == 0;
            }
        }
        public List<string> Errors { get; set; }
        public T Result { get; set; } = default;
    }
}
