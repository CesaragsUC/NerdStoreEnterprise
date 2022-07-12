namespace NSE.WebApp.MVC.Models
{
    public interface IPagedList
    {
         string ReferenceAction { get; set; }
         int PageIndex { get; set; }
         int PageSize { get; set; }
         string Query { get; set; }
         int TotalResults { get; set; }
         double TotalPages { get; }
    }
}