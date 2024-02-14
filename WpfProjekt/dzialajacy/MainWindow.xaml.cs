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
        public int Nr { get; set; }
        public List<BitmapImage> Images {  get; set; }
        public MainWindow()
        {
            InitializeComponent();
            przygotujGre();
        }

        private void Button_Wyslij(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            int losowana = random.Next(0, 3);
            imgAuto.Source = Images[losowana];
            //MessageBox.Show(losowana.ToString());

            
            if(losowana == Nr)
            {
                historia.Text = "Remis.";
            }


            if (losowana == 0 && Nr == 1)
            {
                punktyGracz++;
                historia.Text = "Wygrywa Gracz, Papier wygrywa z kamieniem.";
            }
            else if(losowana == 1 && Nr == 0)
            {
                punktyAuto++;
                historia.Text = "Wygrywa Komputer, Papier wygrywa z kamieniem.";
            }
            
            
            if (losowana == 0 && Nr == 2)
            {
                punktyAuto++;
                historia.Text = "Wygrywa Auto, Kamien wygrywa z nozyczkami.";
            }
            else if (losowana == 2 && Nr == 0)
            {
                punktyGracz++;
                historia.Text = "Wygrywa Gracz, Kamien wygrywa z nozyczkami.";
            }


            if (losowana == 1 && Nr == 2)
            {
                punktyGracz++;
                historia.Text = "Wygrywa Gracz, Nozyczki wygrywa z papierem.";
            }
            else if (losowana == 2 && Nr == 1)
            {
                punktyAuto++;
                historia.Text = "Wygrywa Auto, Nozyczki wygrywa z papierem.";
            }

            wynik.Text = punktyGracz + " : " + punktyAuto;
        }

        private void Button_K(object sender, RoutedEventArgs e)
        {
            //Kamien
            Nr = 0;
            imgGracz.Source = Images[Nr];
        }

        private void Button_P(object sender, RoutedEventArgs e)
        {
            //Papier
            Nr = 1;
            imgGracz.Source = Images[Nr];
        }

        private void Button_N(object sender, RoutedEventArgs e)
        {
            //Nozyce
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
    }
}

/*
0 - kamien
1 - papier
2 - nozyczki

kamien < papier < nozyczki < kamien
 */