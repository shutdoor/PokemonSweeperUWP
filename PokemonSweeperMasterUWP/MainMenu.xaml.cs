using PokemonSweeperMasterUWP.Strings.de;
using PokemonSweeperMasterUWP.Strings.en_US;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.Resources.Core;
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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainMenu : Page
    {
        public MainMenu()
        {
            this.InitializeComponent();
            string language = Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride;
            System.Resources.ResourceManager resource = null;

            switch(language)
            {
                case "de":
                    resource = new System.Resources.ResourceManager(typeof(DeResources));
                    break;
                default:
                    resource = new System.Resources.ResourceManager(typeof(EnResources));
                    break;
            }
            StartButton.Content = resource.GetString("StartButton.Content");
            LanguageButton.Content = resource.GetString("LanguagesButton.Content");
            QuitButton.Content = resource.GetString("QuitButton.Content");
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LevelMenu));
        }

        private void LanguageButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LanguageMenu));
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            CoreApplication.Exit();
        }
    }
}
