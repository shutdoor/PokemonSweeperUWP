using PokemonSweeperMasterUWP.Game;
using PokemonSweeperMasterUWP.Game.Field;
using PokemonSweeperMasterUWP.Game.Pokemon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            Game = new PokeSweepGame();
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


        //TappedRoutedEventArgs
        #region Flyouts

        //On Lose Show This FlyOut
        public void showLossFlyOut(int pokemonNumber)
        {
            EscapedPokemon.Source = getPokemonImageForFlyOut(pokemonNumber);
            textBoxContentFlyOut.Text = $"Sadly {(PokemonEnumList)pokemonNumber} - {pokemonNumber} Escaped.";
            innerStackPanelButton.Content = "Retry";
            innerStackPanelButton.Tapped += InnerStackPanelLossButton_Tapped;
            FlyoutBase.ShowAttachedFlyout(MineFieldGrid);
        }
        public void showLevelWinFlyOut()
        {
            EscapedPokemon.Source = getPokeballImage();
            textBoxContentFlyOut.Text = $"Well done!\nYou have caught all the Pokemon";
            innerStackPanelButton.Content = "Continue";
            innerStackPanelButton.Tapped += InnerStackPanelLevelWinButton_Tapped;
            FlyoutBase.ShowAttachedFlyout(MineFieldGrid);
        }

        private void InnerStackPanelLossButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Game = new PokeSweepGame();
            Game.NewField(this);
        }

        private void InnerStackPanelLevelWinButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            throw new NotImplementedException();
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
    }
}
