namespace HPPMDotNetCore.ExpenseTracker
{
    public class PageSetting
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int TotalPageNo { get; set; }
        public int TotalRowCount { get; set; }
        public string PageUrl { get; set; }
        public string SearchValue { get; set; }
        public string GetPageUrl(int pageNo)
        {
            return string.Format(PageUrl, pageNo);
        }
    }
}
