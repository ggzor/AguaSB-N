using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using AguaSB.Views.Controles.Animaciones;

namespace AguaSB.Views.Controles.Texto
{
    public partial class AnimatedText : UserControl
    {
        public AnimatedText()
        {
            InitializeComponent();
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(AnimatedText),
                new PropertyMetadata(string.Empty, (o, a) => (o as AnimatedText)?.HandleTextChange((string)a.NewValue)));

        public Style TextStyle
        {
            get { return (Style)GetValue(TextStyleProperty); }
            set { SetValue(TextStyleProperty, value); }
        }

        public static readonly DependencyProperty TextStyleProperty =
            DependencyProperty.Register("TextStyle", typeof(Style), typeof(AnimatedText), new PropertyMetadata(null));

        private TextBlock Previous;

        private void HandleTextChange(string newValue)
        {
            if (Previous != null && Content.Children.Contains(Previous))
            {
                var toRemove = Previous;
                toRemove.Apply(onComplete: (s, a) => Content.Children.Remove(toRemove));
            }

            Previous = new TextBlock
            {
                Text = newValue,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            if (TextStyle != null)
                Previous.Style = TextStyle;


            Content.Children.Add(Previous);
        }
    }
}
