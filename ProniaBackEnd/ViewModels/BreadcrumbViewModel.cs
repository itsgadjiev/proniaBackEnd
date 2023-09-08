namespace ProniaBackEnd.ViewModels
{
    public class BreadcrumbViewModel
    {
        public BreadcrumbViewModel(string title, string pageName)
        {
            Title = title;
            PageName = pageName;
        }

        public string Title { get; set; }
        public string PageName { get; set; }

        
    }
}
