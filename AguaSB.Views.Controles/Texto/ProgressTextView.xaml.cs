using System.Windows;
using System.Windows.Controls;

using ReactiveUI;

using AguaSB.ViewModels.Controles;
using System.Reactive.Disposables;

namespace AguaSB.Views.Controles.Texto
{
    public partial class ProgressTextView : UserControl, IViewFor<ProgressText>
    {
        public ProgressTextView()
        {
            InitializeComponent();

            this.WhenActivated(d =>
            {
                this.OneWayBind(ViewModel, vm => vm.Title, v => v.Title.Text).DisposeWith(d);
                this.OneWayBind(ViewModel, vm => vm.Subtitle, v => v.Subtitle.Text).DisposeWith(d);
            });
        }

        public Style TitleStyle
        {
            get { return (Style)GetValue(TitleStyleProperty); }
            set { SetValue(TitleStyleProperty, value); }
        }

        public static readonly DependencyProperty TitleStyleProperty =
            DependencyProperty.Register(nameof(TitleStyle), typeof(Style), typeof(ProgressTextView), new PropertyMetadata(null));

        public Style SubtitleStyle
        {
            get { return (Style)GetValue(SubtitleStyleProperty); }
            set { SetValue(SubtitleStyleProperty, value); }
        }

        public static readonly DependencyProperty SubtitleStyleProperty =
            DependencyProperty.Register(nameof(SubtitleStyle), typeof(Style), typeof(ProgressTextView), new PropertyMetadata(null));

        #region IViewFor
        public ProgressText ViewModel
        {
            get { return (ProgressText)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel), typeof(ProgressText), typeof(ProgressTextView), new PropertyMetadata(null));

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (ProgressText)value;
        }
        #endregion
    }
}
