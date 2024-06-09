using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleManagement.Service.Models.Response
{
    public class PaginatedResponse<T>
    {
        public bool Success
        {
            get
            {
                return Errors == null || Errors.Count == 0;
            }
        }
        public List<string> Errors { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public List<T> Results { get; set; }
    }
}
