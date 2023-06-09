using NetflixClone.Models;
using System.Windows.Input;

namespace NetflixClone.Controls;

public partial class MovieRow : ContentView
{
    
    public MovieRow()
    {
        InitializeComponent();
        MediaDetailsCommand = new Command(ExecuteMediaDetailsCommand);
    }
    public static BindableProperty HeadingProperty = BindableProperty.Create(nameof(Heading), typeof(string), typeof(MovieRow), String.Empty);
    public static BindableProperty MoviesProperty = BindableProperty.Create(nameof(Movies), typeof(IEnumerable<Media>), typeof(MovieRow), Enumerable.Empty<Media>());
    public static BindableProperty IsLargeProperty = BindableProperty.Create(nameof(IsLarge), typeof(bool), typeof(MovieRow), false);

    public string Heading
    {
        get => (string)GetValue(MovieRow.HeadingProperty);
        set => SetValue(MovieRow.HeadingProperty, value);
    }
    public IEnumerable<Media> Movies
    {
        get => (IEnumerable<Media>)GetValue(MovieRow.MoviesProperty);
        set => SetValue(MovieRow.MoviesProperty, value);
    }
    public bool IsLarge
    {
        get => (bool)GetValue(MovieRow.IsLargeProperty);
        set => SetValue(MovieRow.IsLargeProperty, value);
    }

    public bool IsNotLarge => !IsLarge;

    public event EventHandler<MediaSelectEventArgs> onMediaSelected;
    public ICommand MediaDetailsCommand { get; private set; }
    private void ExecuteMediaDetailsCommand(object parameter)
    {
        if (parameter is Media media && media is not null)
        {
            onMediaSelected?.Invoke(this, new MediaSelectEventArgs(media));
        }
    }
}
public class MediaSelectEventArgs : EventArgs
{
    public Media Media { get; set; }
    public MediaSelectEventArgs(Media media) => Media = media;
}