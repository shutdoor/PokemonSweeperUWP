﻿using PokemonSweeper;
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

namespace PokemonSweeperMasterUWP.Game
{
    public class PokeSweepGame
    {
        public PokeSweepGame(int gameDiff)
        {
            Level = gameDiff;
            Pokemon = new List<Pokemon.Pokemon>(); // make empty list of Pokemon captured
            FieldLevels = new List<FieldLevel>(); // Make list of Game Levels
            FieldLevels.Add(new FieldLevel { Rows = 10, Columns = 10, Pokemon = 10});
            FieldLevels.Add(new FieldLevel { Rows = 16, Columns = 16, Pokemon = 40});
            FieldLevels.Add(new FieldLevel { Rows = 24, Columns = 24, Pokemon = 50});
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

            window.Width = 600 * FieldLevels[Level].Columns / FieldLevels[Level].Rows;
            window.MineFieldGrid.Width = 500 * FieldLevels[Level].Columns / FieldLevels[Level].Rows;
            window.MineFieldGrid.Height = 500 * FieldLevels[Level].Columns / FieldLevels[Level].Rows;
            Field = new Field.Field(FieldLevels[Level].Rows, FieldLevels[Level].Columns,
                FieldLevels[Level].Pokemon,
                FieldLevels[Level].Open, window);

            foreach (var square in Field.Squares)
            {
                square.Tapped += window.MineSquare_Click;
                square.RightTapped += window.MineSquare_MouseRightButtonDown;
                square.Width = window.MineFieldGrid.Width/FieldLevels[Level].Columns;
                square.Height = window.MineFieldGrid.Height/FieldLevels[Level].Rows;
                square.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Black);
                window.MineFieldGrid.Children.Add(square);
                Grid.SetRow(square, square.Row);
                Grid.SetColumn(square, square.Column);
            }
            window.MinesLeftLabel.Text = $"{FieldLevels[Level].Pokemon}";
        }
    }
}
