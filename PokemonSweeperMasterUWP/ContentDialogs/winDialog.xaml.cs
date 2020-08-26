using PokemonSweeperMasterUWP.Strings.de;
using PokemonSweeperMasterUWP.Strings.en_US;
﻿using PokemonSweeperMasterUWP.Game.Field;
using PokemonSweeperMasterUWP.Game.Pokemon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PokemonSweeperMasterUWP
{

    public sealed partial class winDialog : ContentDialog
    {
    public string Result { get; set; }
        public winDialog()
        {
            this.InitializeComponent();

            string language = Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride;
            System.Resources.ResourceManager resource = null;

            switch (language)
            {
                case "de":
                    resource = new System.Resources.ResourceManager(typeof(DeResources));
                    break;
                default:
                    resource = new System.Resources.ResourceManager(typeof(EnResources));
                    break;
            }
            winContentDialog.Title = resource.GetString("YouWon");
            WellDone.Text = resource.GetString("WellDone");
            Caught.Text = resource.GetString("CaughtPokemon");
            nextLevelPanelButton.Content = resource.GetString("NextLevel");
            mainMenuPanelButton.Content = resource.GetString("MainMenu");
        }

        public static async Task<String> showWin(MainPage sender, Field Field)
        {
            var PokeList = new List<Pokemon>();
            var Winner = new winDialog();

            foreach (var square in Field.Squares.Where(s => s.Pokemon != null))
            {
                Winner.ListBoxPokemon.Items.Add(square.Pokemon);
                PokeList.Add(square.Pokemon);
            }

            if (sender.Game.Level < 2)
            {
                Winner.nextLevelPanelButton.Visibility = Visibility.Visible;
                Winner.mainMenuPanelButton.Visibility = Visibility.Visible;

            }
            else
            {
                Winner.nextLevelPanelButton.Visibility = Visibility.Collapsed;
            }

            await Winner.ShowAsync();


            return Winner.Result;
        }

        private void nextLevelPanelButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Result = "next";
            winContentDialog.Hide();
        }

        private void mainMenuPanelButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Result = "main";
            winContentDialog.Hide();
        }
    }
}
