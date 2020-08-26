using PokemonSweeperMasterUWP.Strings.de;
using PokemonSweeperMasterUWP.Strings.en_US;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PokemonSweeperMasterUWP
{

    public sealed partial class lossDialog : ContentDialog
    {
        public string Result { get; set; }
        public lossDialog()
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
            retryButton.Content = resource.GetString("Retry");
            returnButton.Content = resource.GetString("ReturnLevels");
            lossContentDialog.Title = resource.GetString("YouLost");
        }

        private  void innerStackPanelButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Result = "retry";
            lossContentDialog.Hide();
        }

        private void returnButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Result = "return";
            lossContentDialog.Hide();
        }
    }
}
