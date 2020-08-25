using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonSweeperMasterUWP.Game.Pokemon;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace PokemonSweeperMasterUWP.Game.Field
{
    public class Field
    {
        private readonly Random Random = new Random();
        public Stopwatch Timer;

        public Field(int rows, int columns, int nrOfPokemon, MainPage window)
        {
            Rows = rows;
            Columns = columns;
            PopulateField(nrOfPokemon, window);
            NrOfClicks = 0;
        }

        public int Columns { get; set; }
        public int Rows { get; set; }
        public List<Square> Squares { get; set; }

        public int ClearedSquares
        {
            get { return Squares.Where(Square => Square.Status == Square.SquareStatus.Cleared).Count(); }
        }

        public int NrOfClicks { get; set; }

        private void PopulateField(int nrOfPokemon, MainPage window)
        {
            var pokemonPlacers = new List<int>();


            int pokemonLocation;
            for (var i = 0; i < nrOfPokemon; i++)
            {
                do
                {
                    pokemonLocation = Random.Next(Rows * Columns);
                }
                while (pokemonPlacers.Contains(pokemonLocation));
                pokemonPlacers.Add(pokemonLocation);
            }
            Squares = new List<Square>();
            for (var row = 0; row < Rows; row++)
            {
                for (var column = 0; column < Columns; column++)
                {
                    //Creating a Square/Button & adding Styling
                    Square s;
                    s = new Square(this, Rows, Columns, row, column);
                    s.Background = new SolidColorBrush(Color.FromArgb(255, 221, 221, 221));
                    s.BorderBrush = new SolidColorBrush(Colors.Black);
                    s.BorderThickness = new Thickness(1);
                    s.Width = 50;
                    s.Height = 50;

                    if (pokemonPlacers.Contains(Squares.Count - 1))
                    {

                        //Cheat Mode - Changes Color of Squares Containting Mines
                        //Squares[Squares.Count - 1].Background = new SolidColorBrush(Colors.Red);

                        Squares[Squares.Count - 1].Pokemon = new Pokemon.Pokemon
                        {
                            Type = (PokemonEnumList)Random.Next(1, 386)
                        };
                    }

                    Squares.Add(s);
                }
            }
        }
    }
}
