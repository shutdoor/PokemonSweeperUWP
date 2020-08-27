using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace PokemonSweeperMasterUWP.Game.Field
{
    public class Square : Button
    {
        public enum SquareStatus
        {
            Cleared,
            Pokemon,
            Open,
            Flagged,
            Question
        }

        public Square(Field field, int rows, int columns, int row, int column)
        {
            Field = field;
            NrOfRows = rows;
            NrOfColumns = columns;
            Row = row;
            Column = column;
            Pokemon = null;
            Status = SquareStatus.Open;
        }

        public SquareStatus Status { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public int NrOfRows { get; set; }

        public int NrOfColumns
        {
            get { return NrOfRows; }
            set { NrOfRows = value; }
        }

        public Field Field { get; set; }
        public Pokemon.Pokemon Pokemon { get; set; }

        public int Mines
        {
            get
            {
                var mines = 0;
                foreach (var Square in (Field.Squares.Where
                    (s => (s.Row >= Row - 1) && (s.Row <= Row + 1) &&
                          (s.Column >= Column - 1) && (s.Column <= Column + 1))
                    .ToList()))
                {
                    if (Square.Pokemon != null)
                    {
                        mines++;
                    }
                }
                return mines;
            }
        }

        public void RightButton(MainPage sender, Square clickedElement)
        {
            List<Square> FlaggedSquares;
            if (Status == SquareStatus.Open)
            {
                Status = SquareStatus.Flagged;

                Image img = new Image();
                BitmapImage bitImage = new BitmapImage();
                Uri uri = new Uri("ms-appx:///Assets//Game Icons/pokeball.png");
                bitImage.UriSource = uri;
                img.Source = bitImage;

                clickedElement.Content = img;

                FlaggedSquares = Field.Squares.Where(square => square.Status == SquareStatus.Flagged).ToList();
                sender.MinesLeftLabel.Text = $"Poke Balls: {sender.Game.FieldLevels[sender.Game.Level].Pokemon - FlaggedSquares.Count()}";
                if (FlaggedSquares.Count() == sender.Game.FieldLevels[sender.Game.Level].Pokemon && Field.Squares.Where(s => s.Status == SquareStatus.Question).Count() == 0)
                {
                    bool win = true;
                    foreach (var flaggedSquare in FlaggedSquares)
                    {
                        if (flaggedSquare.Pokemon == null)
                        {
                            win = false;
                        }
                    }
                    if (win) sender.showLevelWinFlyOut(); //If the won, we need to send them to a win page.
                }
            }
            else if (Status == SquareStatus.Flagged)
            {
                Status = SquareStatus.Question;
                Image img = new Image();
                BitmapImage bitImage = new BitmapImage();
                Uri uri = new Uri("ms-appx:///Assets//Game Icons/masterball.png");
                bitImage.UriSource = uri;
                img.Source = bitImage;

                clickedElement.Content = img;
                FontWeight = FontWeights.Bold;
                FlaggedSquares = Field.Squares.Where(square => square.Status == SquareStatus.Flagged).ToList();
                sender.MinesLeftLabel.Text = $"Poke Balls: {sender.Game.FieldLevels[sender.Game.Level].Pokemon - FlaggedSquares.Count()}";
            }
            else if (Status == SquareStatus.Question)
            {
                Status = SquareStatus.Open;
                Content = "";
                if (Field.Squares.Where(s => s.Pokemon != null && s.Status == SquareStatus.Flagged).Count() == sender.Game.FieldLevels[sender.Game.Level].Pokemon && Field.Squares.Where(s => s.Status == SquareStatus.Question).Count() == 0)
                {
                    sender.showLevelWinFlyOut();
                }
            }

        }

        public void LeftButton(MainPage window)
        {
            if (Status == SquareStatus.Open)
            {
                SwipeSquare(window);
                if (Field.ClearedSquares + window.Game.FieldLevels[window.Game.Level].Pokemon ==
                    (window.Game.FieldLevels[window.Game.Level].Rows * window.Game.FieldLevels[window.Game.Level].Columns))
                {
                    window.showLevelWinFlyOut();
                }
            }
        }

        public void SwipeSquare(MainPage window)
        {
            if (Pokemon != null)
            {
                Content = new Image { Source = Pokemon.Picture };
                Status = SquareStatus.Pokemon;
                Background = new SolidColorBrush(Color.FromArgb(255, 179,27,0));
                BorderBrush = new SolidColorBrush(Colors.Black);
                BorderThickness = new Thickness(1);
                //IsEnabled = false;
                window.showLossFlyOut(Pokemon.Number);
            }
            else if (Mines > 0)
            {
                Content = Mines;
                Status = SquareStatus.Cleared;
                Background = new SolidColorBrush(Colors.White);
                BorderBrush = new SolidColorBrush(Colors.Black);
                BorderThickness = new Thickness(1);
                //IsEnabled = false;
            }
            else
            {
                Background = new SolidColorBrush(Colors.White);
                BorderBrush = new SolidColorBrush(Colors.Black);
                BorderThickness = new Thickness(1);
                Status = SquareStatus.Cleared;
                foreach (var OtherSquare in (Field.Squares.Where
                    (s => (s.Row >= Row - 1) && (s.Row <= Row + 1) &&
                          (s.Column >= Column - 1) && (s.Column <= Column + 1) && (s.Status == SquareStatus.Open))
                    .ToList()))
                    OtherSquare.SwipeSquare(window);
            }
        }
    }
}
