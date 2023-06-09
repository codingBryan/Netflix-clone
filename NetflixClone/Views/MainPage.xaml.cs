﻿
using NetflixClone.ViewModels;

namespace NetflixClone.Views;

public partial class MainPage : ContentPage
{
	private readonly HomeViewModel _homeViewModel;


	public MainPage(HomeViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		_homeViewModel = viewModel;
	}

	protected async override void OnAppearing()
	{
		base.OnAppearing();
		await _homeViewModel.InitializeAsync();
	}

    private void MovieRow_MediaSelected(object sender, Controls.MediaSelectEventArgs e)
    {
        _homeViewModel.SelectMediaCommand.Execute(e.Media);
    }

    private void MovieInfoBox_Closed(object sender, EventArgs e)
    {
        _homeViewModel.SelectMediaCommand.Execute(null);
    }


}

