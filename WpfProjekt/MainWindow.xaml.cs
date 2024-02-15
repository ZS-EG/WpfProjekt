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
        public int Nr { get; set; }
        public int Losowana { get; set; }
        public List<BitmapImage> Images { get; set; }

        DispatcherTimer _timer;
        TimeSpan _time;

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
        }

        private void Button_Wyslij(object sender, RoutedEventArgs e)
        {
            wysylanie();
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
            if(punktyAuto == 3)
            {
                wygrana.Text = "Koniec gry! Wygrywa Auto";
                granie.IsEnabled = false;
            }
            else if(punktyGracz == 3)
            {
                wygrana.Text = "Koniec gry! Wygrywa Gracz";
                granie.IsEnabled = false;
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
            if(Wychodzenie == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        private void MenuItem_ResetWynik(object sender, RoutedEventArgs e)
        {
            punktyAuto = 0;
            punktyGracz = 0;
            wynik.Text = punktyGracz + " : " + punktyAuto;
            granie.IsEnabled = true;
            wygrana.Text = "";
            historia.Text = "";
            tbTime.Visibility = Visibility.Hidden;
            czasID.IsChecked = false;
            dalej.Visibility = Visibility.Hidden;
        }

        private void czasomierz()
        {
            _time = TimeSpan.FromSeconds(5);

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                tbTime.Text = _time.ToString("mm':'ss");
                if (_time == TimeSpan.Zero)
                {
                    _timer.Stop();
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
            }
            else
            {
                _timer.Stop();
                tbTime.Visibility = Visibility.Hidden;
            }
        }

        private void dalej_Click(object sender, RoutedEventArgs e)
        {
            wysylanie();
            _timer.Stop();
            dalej.Visibility = Visibility.Hidden;
            granie.IsEnabled = true;
            czasomierz();
        }
    }
}