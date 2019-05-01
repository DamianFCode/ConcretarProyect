namespace Concretar.Backend.Models
{
    public class AppSettings
    {
        public PagingClass Paging { get; set; }

        public class PagingClass {
            public int PagesToDisplay { get; set; }
            public int RowsPerPage { get; set; }
        }
        public string UrlApi { get; set; }
        public string Path888Entes { get; set; }
        public int MaxNotificaciones { get; set; }
    }
}
