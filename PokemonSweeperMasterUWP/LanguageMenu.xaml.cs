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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LanguageMenu : Page
    {
        public LanguageMenu()
        {
            this.InitializeComponent();
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO add language selection and translation functionality here
           
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainMenu));
        }

        private void LanguageSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string language = e.AddedItems[0].ToString();
            var resourceContext = new Windows.ApplicationModel.Resources.Core.ResourceContext();
            switch(language)
            {
                case "English":
                    resourceContext.QualifierValues["Language"] = "en-US";
                    Windows.ApplicationModel.Resources.Core.ResourceContext.SetGlobalQualifierValue("Language", "en-US");
                    Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = "en-US";
                    break;
                case "German":
                    resourceContext.QualifierValues["Language"] = "de";
                    Windows.ApplicationModel.Resources.Core.ResourceContext.SetGlobalQualifierValue("Language", "de");
                    Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = "de";
                    break;
            }
            
        }
    }
}
