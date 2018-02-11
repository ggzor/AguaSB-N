using ReactiveUI;

namespace AguaSB.ViewModels.Controles
{
    public class ProgressText : ReactiveObject
    {
        public ProgressText(string initialTitle = "", string initialSubtitle = "")
        {
            title = initialTitle ?? string.Empty;
            subtitle = initialSubtitle ?? string.Empty;
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { this.RaiseAndSetIfChanged(ref title, value); }
        }

        private string subtitle;

        public string Subtitle
        {
            get { return subtitle; }
            set { this.RaiseAndSetIfChanged(ref subtitle, value); }
        }

        public void Set(string title, string subtitle = "")
        {
            Title = title ?? string.Empty;
            Subtitle = subtitle ?? string.Empty;
        }

        public void Clear() => Title = Subtitle = string.Empty;
    }
}
