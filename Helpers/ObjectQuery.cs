using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_web_api.Helpers
{
    public class ObjectQuery
    {
        public string CompanyName{set;get;}=string.Empty;
        public string SortBy{set;get;}=string.Empty;
        public bool IsDescending{set;get;}=false;
        public int PageNumber{set;get;}=1;
        public int PageSize{set;get;}=20;
    }
}