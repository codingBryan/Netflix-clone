using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NetflixClone.Models;
using NetflixClone.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace NetflixClone.ViewModels
{
    public partial class HomeViewModel:ObservableObject
    {
        private readonly TmdbService service;

        [ObservableProperty]
        private Media trendingMovie;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(ShowMovieInfoBox))]
        private Media? selectedMedia;
        public bool ShowMovieInfoBox => SelectedMedia is not null;
        public HomeViewModel(TmdbService service)
        {
            this.service = service;
        }
        public ObservableCollection<Media> topRated { get; set; } = new ();
        public ObservableCollection<Media> trending { get; set; } = new();
        public ObservableCollection<Media> netflixOriginals { get; set; } = new();
        public ObservableCollection<Media> actionMovies { get; set; } = new();

        public async Task InitializeAsync()
        {
            var trendingListTask = service.GetTrendingAsync();
            var netflixOriginalsListTask = service.GetNetflixOriginalsAsync();
            var topRatedListTask = service.GetTopRatedAsync();
            var actionListTask = service.GetActionAsync();

            var medias = await Task.WhenAll(trendingListTask,
                                    netflixOriginalsListTask,
                                    topRatedListTask,
                                    actionListTask);

            var trendingList = medias[0];
            var netflixOriginalsList = medias[1];
            var topRatedList = medias[2];
            var actionList = medias[3];

            // Seting random trending movie from Trending List to the Trending Movie
            TrendingMovie = trendingList.OrderBy(t => Guid.NewGuid())
                                .FirstOrDefault(t =>
                                    !string.IsNullOrWhiteSpace(t.DisplayTitle)
                                    && !string.IsNullOrWhiteSpace(t.Thumbnail));

            SetMediaCollection(trendingList, trending);
            SetMediaCollection(netflixOriginalsList, netflixOriginals);
            SetMediaCollection(topRatedList, topRated);
            SetMediaCollection(actionList, actionMovies);


        }
        private static void SetMediaCollection(IEnumerable<Media> medias, ObservableCollection<Media> collection)
        {
            collection.Clear();
            foreach (var media in medias)
            {
                collection.Add(media);
            }
        }

        [RelayCommand]
        private void SelectMedia(Media? media = null)
        {
            if (media is not null)
            {
                if (media.Id == SelectedMedia?.Id)
                {
                    Debug.WriteLine("Closing Details page");
                    media = null;
                }
                SelectedMedia = media;
            }
            
        }
    }
}
