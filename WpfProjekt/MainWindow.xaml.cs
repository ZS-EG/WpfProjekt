using System.Media;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfProjekt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int punktyGracz = 0;
        public int punktyAuto = 0;
        public int odliczanie = 5;
        public int Nr { get; set; }
        public int Losowana { get; set; }
        public List<BitmapImage> Images { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            przygotujGre();
        }

        private void Button_Wyslij(object sender, RoutedEventArgs e)
        {
            if (!impo.IsChecked)
            {
                losowanie();
                sprawdzWynik();
            }
            else
            {
                missionImpossible();
                sprawdzWynikImpossible();
            }
            imgAuto.Source = Images[Losowana];
            wynik.Text = punktyGracz + " : " + punktyAuto;
            czyWygrana();
            //SystemSounds.Hand.Play();
        }

        private void losowanie()
        {
            Random random = new Random();
            int losowana = random.Next(0, 3);
            Losowana = losowana;
        }
        private void sprawdzWynik()
        {
            if (Losowana == Nr)
            {
                historia.Text = "Remis.";
            }

            //kamien < papier < nozyczki < kamien
            if (Losowana == 0 && Nr == 1)
            {
                punktyGracz++;
                historia.Text = "Wygrywa Gracz, Papier wygrywa z kamieniem.";
            }
            else if (Losowana == 1 && Nr == 0)
            {
                punktyAuto++;
                historia.Text = "Wygrywa Komputer, Papier wygrywa z kamieniem.";
            }


            if (Losowana == 0 && Nr == 2)
            {
                punktyAuto++;
                historia.Text = "Wygrywa Auto, Kamien wygrywa z nozyczkami.";
            }
            else if (Losowana == 2 && Nr == 0)
            {
                punktyGracz++;
                historia.Text = "Wygrywa Gracz, Kamien wygrywa z nozyczkami.";
            }


            if (Losowana == 1 && Nr == 2)
            {
                punktyGracz++;
                historia.Text = "Wygrywa Gracz, Nozyczki wygrywa z papierem.";
            }
            else if (Losowana == 2 && Nr == 1)
            {
                punktyAuto++;
                historia.Text = "Wygrywa Auto, Nozyczki wygrywa z papierem.";
            }
        }

        private void missionImpossible()
        {
            if (Nr == 0)
            {
                Losowana = 1;
            }
            else if (Nr == 1)
            {
                Losowana = 2;
            }
            else
            {
                Losowana = 0;
            }
        }
        private void sprawdzWynikImpossible()
        {
            //0-kam 1-pap 2-noz
            if (Nr == 0)
            {
                historia.Text = "Wygrywa Auto, Papier wygrywa z nozyczkami.";
                punktyAuto++;
            }
            else if (Nr == 1)
            {
                historia.Text = "Wygrywa Auto, NOzyczki wygrywa z papierem.";
                punktyAuto++;
            }
            else
            {
                historia.Text = "Wygrywa Auto, Kmaien wygrywa z nozyczkami.";
                punktyAuto++;
            }
        }
        private void czyWygrana()
        {
            //jesli punkty == 3 to koniec, wylacz przycisz
            if(punktyAuto == 3)
            {
                granie.IsEnabled = false;
                wygrana.Text = "Koniec gry! Wygrywa Auto";
            }
            else if(punktyGracz == 3)
            {
                granie.IsEnabled = false;
                wygrana.Text = "Koniec gry! Wygrywa Gracz";
            }
        }

        private void Button_K(object sender, RoutedEventArgs e)
        {
            //Kamien
            Nr = 0;
            imgGracz.Source = Images[Nr];
            SystemSounds.Asterisk.Play();
        }

        private void Button_P(object sender, RoutedEventArgs e)
        {
            //Papier
            Nr = 1;
            imgGracz.Source = Images[Nr];
            SystemSounds.Beep.Play();
        }

        private void Button_N(object sender, RoutedEventArgs e)
        {
            //Nozyce
            Nr = 2;
            imgGracz.Source = Images[Nr];
            SystemSounds.Exclamation.Play();
        }
        private void przygotujGre()
        {
            Images = new List<BitmapImage>();
            Images.Add(new BitmapImage(new Uri("img/kamien.jpg", UriKind.Relative)));
            Images.Add(new BitmapImage(new Uri("img/papier.jpg", UriKind.Relative)));
            Images.Add(new BitmapImage(new Uri("img/nozyczki.jpg", UriKind.Relative)));
        }


        private void MenuItem_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuItem_ResetWynik(object sender, RoutedEventArgs e)
        {
            punktyAuto = 0;
            punktyGracz = 0;
            wynik.Text = punktyGracz + " : " + punktyAuto;
            granie.IsEnabled = true;
            wygrana.Text = "";
            historia.Text = "";
            //czas_txt.Text = "0 : 5";
        }

        
        private void MenuItem_Czas(object sender, RoutedEventArgs e)
        {

            if (czas1.IsChecked)
            {
                for (int i=1; i<=5; i++)
                {
                    odliczanie--;
                    czas_txt.Text = "0 : " + odliczanie;
                    //nie pokazuje do konca, jak chce ^ do naprawy
                }
            }
        }

        private void MenuItem_Impo(object sender, RoutedEventArgs e)
        {
            //nie da sie wygrac -> 0>1, 1>2, 2<0
            if (impo.IsChecked)
            {
                if(Nr == 0)
                {
                    Losowana = 1;
                    imgAuto.Source = Images[Losowana];
                }
                else if(Nr == 1)
                {
                    Losowana = 2;
                    imgAuto.Source = Images[Losowana];
                }
                else
                {
                    Losowana = 0;
                    imgAuto.Source = Images[Losowana];
                }
            }
        }

    }
}
