using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace PokemonSweeperMasterUWP.Game.Pokemon
{
    public class Pokemon
    {
        public PokemonEnumList Type { get; set; }

        public int Number
        {
            get { return (int)Type; }
        }

        public string Name
        {
            get { return $"{(PokemonEnumList)Number}"; }
        }

        public BitmapImage Picture
        {
            get
            {
                var number = Number + "";
                if (Number / 100 < 1)
                {
                    if (Number / 10 < 1)
                    {
                        number = "0" + number;
                    }
                    number = "0" + number;
                }

                BitmapImage bitImage = new BitmapImage();
                Uri uri = new Uri($"ms-appx:///Assets/Pokemon/{number}.png");
                bitImage.UriSource = uri;

                return bitImage;
            }
        }
    }
}
