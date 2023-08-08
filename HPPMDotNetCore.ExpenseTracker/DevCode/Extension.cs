using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using HPPMDotNetCore.ExpenseTracker;

namespace HPPMDotNetCore
{
    public static class Extension
    {
        public static IQueryable<TSource> Pagination<TSource>(this IQueryable<TSource> source, int pageNo, int pageSize)
        {
            int skip = (pageNo - 1) * pageSize;
            return source
                .Skip(skip)
                .Take(pageSize);
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return str == null || string.IsNullOrEmpty(str);
        }

        public static async Task<PageSetting> ExecutePageSetting<T>
        (
            this IQueryable<T> query,
            int pageNo, int pageSize,
            string searchValue)
        {
            int totalRecords = query.Count();
            int totalPageNo = (totalRecords % pageSize) == 0
                ? (totalRecords / pageSize)
                : (totalRecords / pageSize) + 1;

            PageSetting pageSetting = new PageSetting
            {
                PageNo = pageNo,
                PageSize = pageSize,
                TotalPageNo = totalPageNo,
                TotalRowCount = totalRecords,
                SearchValue = searchValue
            };

            return pageSetting;
        }
    }
}