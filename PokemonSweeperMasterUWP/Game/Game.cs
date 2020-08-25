using PokemonSweeper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonSweeperMasterUWP.Game.Field;
using Windows.UI.Xaml.Controls;
using Windows.Media.SpeechRecognition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI;

namespace PokemonSweeperMasterUWP.Game
{
    public class PokeSweepGame
    {
        public PokeSweepGame(int gameDiff)
        {
            Level = gameDiff;
            Pokemon = new List<Pokemon.Pokemon>(); // make empty list of Pokemon captured
            FieldLevels = new List<FieldLevel>(); // Make list of Game Levels
            FieldLevels.Add(new FieldLevel { Rows = 9, Columns = 9, Pokemon = 10 });
            FieldLevels.Add(new FieldLevel { Rows = 16, Columns = 16, Pokemon = 40 });
            FieldLevels.Add(new FieldLevel { Rows = 16, Columns = 30, Pokemon = 99 });


            //Debug & Win Tester
            //FieldLevels.Add(new FieldLevel { Rows = 9, Columns = 9, Pokemon = 1 });
            //FieldLevels.Add(new FieldLevel { Rows = 16, Columns = 16, Pokemon = 1 });
            //FieldLevels.Add(new FieldLevel { Rows = 16, Columns = 30, Pokemon = 1 });

        }

        public List<FieldLevel> FieldLevels { get; set; }
        public List<Pokemon.Pokemon> Pokemon { get; set; }
        public int Level { get; set; }
        public Field.Field Field { get; set; }

        public void NewField(MainPage window)
        {

            window.MineFieldGrid.Children.Clear();
            window.MineFieldGrid.RowDefinitions.Clear();
            window.MineFieldGrid.ColumnDefinitions.Clear();

            RowDefinition rowDef;
            ColumnDefinition colDef;

            for (int i = 0; i < FieldLevels[this.Level].Rows; i++)
            {
                rowDef = new RowDefinition();
                window.MineFieldGrid.RowDefinitions.Add(rowDef);
            }

            for (int i = 0; i < FieldLevels[this.Level].Columns; i++)
            {
                colDef = new ColumnDefinition();
                window.MineFieldGrid.ColumnDefinitions.Add(colDef);
            }
            Field = new Field.Field(FieldLevels[Level].Rows, FieldLevels[Level].Columns,
                FieldLevels[Level].Pokemon, window);

            foreach (var square in Field.Squares)
            {
                square.Tapped += window.MineSquare_Click;
                square.RightTapped += window.MineSquare_MouseRightButtonDown;
                window.MineFieldGrid.Children.Add(square);
                Grid.SetRow(square, square.Row);
                Grid.SetColumn(square, square.Column);
            }
            window.MinesLeftLabel.Text = $"Pokemon: {FieldLevels[Level].Pokemon}";
        }
    }
}
