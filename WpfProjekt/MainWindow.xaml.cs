using Microsoft.Win32;
using System.IO;
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
using System.Windows.Threading;

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
        public int klik = 0;
        public int Nr { get; set; }
        public int Losowana { get; set; }
        public List<BitmapImage> Images { get; set; }

        DispatcherTimer _timer; //licznik czasu
        TimeSpan _time; //czas/przedzial czasu

        //todo - punkty po czasie
        public MainWindow()
        {
            InitializeComponent();
            przygotujGre();
        }


        private void wysylanie()
        {
            if (!impo.IsChecked)
            {
                losowanie();
                if(granie.IsEnabled == false)
                {
                    //po czasie -> dla auto
                    punktyAuto++;
                    historia.Text = "Wygrywa Auto, koniec czasu!";
                }
                else
                {
                    sprawdzWynik();
                }
            }
            else
            {
                missionImpossible();
                sprawdzWynikImpossible();
            }
            imgAuto.Source = Images[Losowana];
            wynik.Text = punktyGracz + " : " + punktyAuto;
            czyWygrana();
        }

        private void Button_Wyslij(object sender, RoutedEventArgs e)
        {
            wysylanie();
            if (czasID.IsChecked)
            {
                wyslanieCzas();
            }
        }

        private void wyslanieCzas()
        {
            _timer.Stop();
            czasomierz();
        }
        private void losowanie()
        {
            //losuje Auto
            Random random = new Random();
            int losowana = random.Next(0, 3);
            Losowana = losowana;
        }

        private void sprawdzWynik()
        {
            if (Losowana == Nr)
            {
                historia.Text = "Remis!";
            }


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
                historia.Text = "Wygrywa Auto, Kamień wygrywa z nożyczkami.";
            }
            else if (Losowana == 2 && Nr == 0)
            {
                punktyGracz++;
                historia.Text = "Wygrywa Gracz, Kamień wygrywa z nożyczkami.";
            }


            if (Losowana == 1 && Nr == 2)
            {
                punktyGracz++;
                historia.Text = "Wygrywa Gracz, Nożyczki wygrywa z papierem.";
            }
            else if (Losowana == 2 && Nr == 1)
            {
                punktyAuto++;
                historia.Text = "Wygrywa Auto, Nożyczki wygrywa z papierem.";
            }
        }

        private void missionImpossible()
        {
            //wybieranie auto do granie (losowanie^)
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
            //czy wygrana - impossible
            if (Nr == 0)
            {
                historia.Text = "Wygrywa Auto, Papier wygrywa z nożyczkami.";
                punktyAuto++;
            }
            else if (Nr == 1)
            {
                historia.Text = "Wygrywa Auto, Nożyczki wygrywa z papierem.";
                punktyAuto++;
            }
            else
            {
                historia.Text = "Wygrywa Auto, Kmaie wygrywa z nożyczkami.";
                punktyAuto++;
            }
        }

        private void czyWygrana()
        {
            //sprawdza czy wygrana
            if (punktyAuto == 3)
            {
                wygrana.Text = "Koniec gry! Wygrywa Auto";
                granie.IsEnabled = false;
                dalej.IsEnabled = false;
                dalej.Visibility = Visibility.Hidden;
                tbTime.Visibility = Visibility.Hidden;
                if (czasID.IsChecked)
                {
                    _timer.Stop();
                    zakrywa.Visibility = Visibility.Visible;
                }
            }
            else if (punktyGracz == 3)
            {
                wygrana.Text = "Koniec gry! Wygrywa Gracz";
                granie.IsEnabled = false;
                dalej.IsEnabled = false;
                dalej.Visibility = Visibility.Hidden;
                tbTime.Visibility = Visibility.Hidden;
                if (czasID.IsChecked)
                {
                    _timer.Stop();
                    zakrywa.Visibility = Visibility.Visible;
                }
            }
        }

        private void Button_K(object sender, RoutedEventArgs e)
        {
            Nr = 0;
            imgGracz.Source = Images[Nr];
        }

        private void Button_P(object sender, RoutedEventArgs e)
        {
            Nr = 1;
            imgGracz.Source = Images[Nr];
        }

        private void Button_N(object sender, RoutedEventArgs e)
        {
            Nr = 2;
            imgGracz.Source = Images[Nr];
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
            var Wychodzenie = MessageBox.Show("Czy na pewno chcesz wyjść?", "Pytanie", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (Wychodzenie == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        private void MenuItem_ResetWynik(object sender, RoutedEventArgs e)
        {
            //przywraca do oryginalnego wygladu
            punktyAuto = 0;
            punktyGracz = 0;
            wynik.Text = punktyGracz + " : " + punktyAuto;
            wygrana.Text = "";
            historia.Text = "";
            tbTime.Visibility = Visibility.Hidden;
            dalej.Visibility = Visibility.Hidden;
            zakrywa.Visibility = Visibility.Hidden;
            if (czasID.IsChecked)
            {
                _timer.Stop();
            }
            granie.IsEnabled = true;
            dalej.IsEnabled = true;
            czasID.IsChecked = false;
            impo.IsChecked = false;
            calyWidok.Background = Brushes.White;
        }

        private void czasomierz()
        {
            //odlicza 3 sekundy
            _time = TimeSpan.FromSeconds(3);

            //delegate - obiektowy typ, hermetyzuje metode, wywodzone ze wskaznikow
            //dispatcher - z tickami wspolpracuje,
            //timespan musi zawierac 3 argumenty (0, 0, 1) - h:m:s
            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                tbTime.Text = _time.ToString("mm':'ss");
                if (_time == TimeSpan.Zero)
                {
                    _timer.Stop();
                    losowanie();
                    granie.IsEnabled = false;
                    dalej.Visibility = Visibility.Visible;
                }
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);
            _timer.Start();
        }

        private void MenuItem_Czas(object sender, RoutedEventArgs e)
        {
            if (czasID.IsChecked)
            {
                czasomierz();
                tbTime.Visibility = Visibility.Visible;
            }
            else
            {
                _timer.Stop();
                tbTime.Visibility = Visibility.Hidden;
            }
        }

        private void dalej_Click(object sender, RoutedEventArgs e)
        {
            //po ukonczeniu czasu
            wysylanie();
            _timer.Stop();
            dalej.Visibility = Visibility.Hidden;
            granie.IsEnabled = true;
            czasomierz();
        }

        private void uno_Click(object sender, RoutedEventArgs e)
        {
            calyWidok.Background = Brushes.LightSeaGreen;
        }

        private void dos_Click(object sender, RoutedEventArgs e)
        {
            calyWidok.Background = Brushes.Thistle;
        }

        private void tres_Click(object sender, RoutedEventArgs e)
        {
            calyWidok.Background = Brushes.LightSkyBlue;
        }

        private void MenuItem_Stop(object sender, RoutedEventArgs e)
        {
            //przycisk pausy/wznowienia
            if (klik == 1)
            {
                widocznosc.Visibility = Visibility.Hidden;
                stop.Header = "Stop";
                klik--;
                if (czasID.IsChecked)
                {
                    _timer.Start();
                }
            }
            else
            {
                widocznosc.Visibility = Visibility.Visible;
                stop.Header = "Znów";
                if (czasID.IsChecked)
                {
                    _timer.Stop();
                }
                klik++;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //zapis wyniku do pliku txt
            var oknoDialogowe = new SaveFileDialog();
            oknoDialogowe.Filter = "PlainText| *.txt";
            oknoDialogowe.Title = "Zapisane wyniki";
            if (oknoDialogowe.ShowDialog() == true)
            {
                string nazwaPliku = oknoDialogowe.FileName;
                File.WriteAllText(nazwaPliku, wynik.Text);
            }
        }
    }
}