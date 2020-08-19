using PokemonSweeper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokemonSweeperMasterUWP.Game.Field;
using Windows.UI.Xaml.Controls;

namespace PokemonSweeperMasterUWP.Game
{
    public class PokeSweepGame
    {
        public PokeSweepGame()
        {
            Level = 0;
            Pokemon = new List<Pokemon.Pokemon>(); // make empty list of Pokemon captured

            FieldLevels = new List<FieldLevel>(); // Make list of Game Levels
            // ------------------------
            FieldLevels.Add(new FieldLevel { Rows = 10, Columns = 10, Pokemon = 10, NextLevel = 1000 });
            // Beginner standard minesweeper
            FieldLevels.Add(new FieldLevel { Rows = 40, Columns = 40, Pokemon = 40, NextLevel = 10000 });
            // Intermediate standard minesweeper
            FieldLevels.Add(new FieldLevel { Rows = 99, Columns = 99, Pokemon = 99, NextLevel = 20000 });
        }

        public List<FieldLevel> FieldLevels { get; set; }
        public List<Pokemon.Pokemon> Pokemon { get; set; }
        public int Level { get; set; }
        public Field.Field Field { get; set; }

        public void NewField(MainPage window)
        {
            window.MineFieldGrid.Children.Clear();

            RowDefinition rowDef;
            ColumnDefinition colDef;

            for(int i = 0; i < FieldLevels[this.Level].Rows; i++)
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
                FieldLevels[Level].Pokemon,
                FieldLevels[Level].Open, window);

            foreach (var square in Field.Squares)
            {
                square.Tapped += window.MineSquare_Click;
                square.RightTapped += window.MineSquare_MouseRightButtonDown;
                square.Width = 60;
                square.Height = 60;
                window.MineFieldGrid.Children.Add(square);
                Grid.SetRow(square, square.Row);
                Grid.SetColumn(square, square.Column);
                //square.Content = $"{square.Row}, {square.Column}";
            }
            window.MinesLeftLabel.Text = $"{FieldLevels[Level].Pokemon}";
        }
    }
}
