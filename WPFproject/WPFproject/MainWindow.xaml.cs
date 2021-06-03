using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFproject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }

        //Ez tárolja az aktív típusát egy játékban
        private MarkType[] mResults;

        //Ezzel fogom megkülönböztetni 2 játékost ha igaz akkor x ha hamis akkor o
        private bool mPlayer1Turn;

        //Ez az érték akkor lesz igaz ha vége a játéknak
        private bool mGameEnded;


       public void NewGame()
        {

            //Ez fog létrehozni egy új üres "pályát"
            mResults = new MarkType[9];

            //Így fog "végig menni" az összes eredményen
            for (var i = 0; i < mResults.Length; i++)
                mResults[i] = MarkType.Free;

            //Így fog az egyes játékos kezdeni
            mPlayer1Turn = true;

            //A "Container.Children-nek jelölöm ki az összes gombot a gridben
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                //Ezzel "törlöm ki az összes gomb értékét" 
                button.Content = string.Empty;

                //Így állítom be a háttérszínt fehérre
                button.Background = Brushes.White;

                //Így állítottam be a betűk alap színét kékre
                button.Foreground = Brushes.Blue;
            });

            //Így fog folytatódni a játék
            mGameEnded = false;
        }

        //Itt fogom beállítani mi fog történni gomb nyomáskor
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Így indítom újra a játékot ha vége lett
            if (mGameEnded)
            {
                NewGame();
                return;
            }

            var button = (Button)sender;
            //Így kérem be ,hogy a gomb melyik oszlopba van
            var column = Grid.GetColumn(button);
            //Így kérem be , hogy a gomb melyik sorba van
            var row = Grid.GetRow(button);
            //Így tudom meg , hogymelyik gomb lett megnyomva
            var index = column + (row * 3);

            //Ezze ellenőrzm le, hogy üres e a gomb
            if (mResults[index] != MarkType.Free)
                return;
            //Így fogom megadnia gomb értéket a játékos alapján (x vagy o)
            mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;

            //Így fogom megadnia gomb értéket vizuálisan a játékos alapján (x vagy o)
            button.Content = mPlayer1Turn ? "x" : "o";

            //Így fogom a játékosok lépéseit felváltani 
            if (mPlayer1Turn)
                mPlayer1Turn = false;

            else
            {
                mPlayer1Turn = true;
                button.Foreground = Brushes.Red;
            }

            //Itt foglya ellenőrizni hogy lett e már gyöztes
            CheckForWinner();
        }

       
        private void CheckForWinner()
            {

            //Sorok ellenőrzése győzelemért
            //Sor 1.
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
                {
                 mGameEnded = true;

                    Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
                }

            //Sor 2.
            if (mResults[0] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[0])
            {
                mGameEnded = true;

                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }

            //Sor 3.
            if (mResults[0] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[0])
            {
                mGameEnded = true;

                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }

            //Oszlopok ellenőrzése győzelemért
            //Oszlop 1
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                mGameEnded = true;

                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }

            //Oszlop 2.
            if (mResults[0] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[0])
            {
                mGameEnded = true;

                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }

            //Oszlop 3.
            if (mResults[0] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[0])
            {
                mGameEnded = true;

                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
            }

            //Ellenőrzés átlós győzelemre
            //Bal felső-jobb alsó sarok
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                mGameEnded = true;

                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }

            //Jobb felső-bal alsó sarok
            if (mResults[0] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[0])
            {
                mGameEnded = true;

                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;
            }

            //Ellenőrzés döntetlenre
            if (!mResults.Any(f => f == MarkType.Free))
                {
                mGameEnded = true;

                    Container.Children.Cast<Button>().ToList().ForEach(button =>
                    {
                        button.Background = Brushes.Orange;
                    });
                }
            }
}
}
