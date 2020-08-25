using PokemonSweeperMasterUWP.Game;
using PokemonSweeperMasterUWP.Game.Field;
using PokemonSweeperMasterUWP.Game.Pokemon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PokemonSweeperMasterUWP
{


    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public PokeSweepGame Game { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Game = new PokeSweepGame((int)e.Parameter);
            Game.NewField(this);
        }

        public void MineSquare_MouseRightButtonDown(object sender, RightTappedRoutedEventArgs e)
        {
            ((Square)sender).RightButton(this, (Square)sender);
        }

        public void MineSquare_Click(object sender, TappedRoutedEventArgs e)
        {
            ((Square)sender).LeftButton(this);
        }

        #region Flyouts

        //On Lose Show This FlyOut
        public async void showLossFlyOut(int pokemonNumber)
        {
            lossDialog dialog = new lossDialog();
            dialog.EscapedPokemon.Source = getPokemonImageForFlyOut(pokemonNumber);
            dialog.textBoxContentFlyOut.Text = $"Sadly, {(PokemonEnumList)pokemonNumber} - {pokemonNumber} Escaped.";
            await dialog.lossConentDialog.ShowAsync();

            if (dialog.Result == "retry")
            {
                Game.NewField(this);
            }
            else if (dialog.Result == "return")
            {
                this.Frame.Navigate(typeof(LevelMenu));
            }
        }
        public async void showLevelWinFlyOut()
        {
            winDialog dialog = new winDialog();

            if (Game.Level < 2)
            {
                dialog.nextLevelPanelButton.Visibility = Visibility.Visible;
                dialog.mainMenuPanelButton.Visibility = Visibility.Visible;

            }
            else
            {
                dialog.nextLevelPanelButton.Visibility = Visibility.Collapsed;
            }

            await dialog.winConentDialog.ShowAsync();

            if (dialog.Result == "next")
            {
                Game.Level++;
                Game.NewField(this);
            }
            else if (dialog.Result == "main")
            {
                this.Frame.Navigate(typeof(MainMenu));
            }
        }

        #endregion

        #region Logic

        public BitmapImage getPokemonImageForFlyOut(int pokemonNumber)
        {
            string fileNumber;
            if (pokemonNumber < 10)
            {
                fileNumber = $"00{pokemonNumber}";
            }
            else if (pokemonNumber < 100)
            {
                fileNumber = $"0{pokemonNumber}";
            }
            else
            {
                fileNumber = $"{pokemonNumber}";
            }

            string filePath = $"Assets/Pokemon/{fileNumber}.png";

            BitmapImage bitImage = new BitmapImage();
            Uri uri = new Uri(this.BaseUri, filePath);
            bitImage.UriSource = uri;

            return bitImage;
        }

        public BitmapImage getPokeballImage()
        {
            BitmapImage bitImage = new BitmapImage();
            Uri uri = new Uri(this.BaseUri, $"Assets/Game Icons/pokeball.png");
            bitImage.UriSource = uri;

            return bitImage;
        }
        #endregion

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LevelMenu));
        }
    }
}
