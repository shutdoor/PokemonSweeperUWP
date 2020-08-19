using PokemonSweeperMasterUWP.Game;
using PokemonSweeperMasterUWP.Game.Field;
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
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PokemonSweeperMasterUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Game = new PokeSweepGame();
        }

        public PokeSweepGame Game { get; set; }
        public void MineSquare_MouseRightButtonDown(object sender, RightTappedRoutedEventArgs e)
        {
            ((Square)sender).RightButton(this, sender as Square);
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Game.NewField(this);
        }

        public void MineSquare_Click(object sender, RoutedEventArgs e)
        {
            ((Square)sender).LeftButton(this);
        }

        public void MinesLeftLabel(int count)
        {
            MinesLeft.Text = "Pokeballs: " + count;
        }
    }
}
