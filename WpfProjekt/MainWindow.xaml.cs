using System.Media;
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
        public int Losowana { get; set; }
        public List<BitmapImage> Images { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            przygotujGre();
        }

         private void Button_Wyslij(object sender, RoutedEventArgs e)
         {
                Random random = new Random();
                int losowana = random.Next(0, 3);
                Losowana = losowana;
                imgAuto.Source = Images[Losowana];
                //MessageBox.Show(losowana.ToString());
                sprawdzWynik();
                wynik.Text = punktyGracz + " : " + punktyAuto;
                //SystemSounds.Hand.Play();
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
                SystemSounds.Question.Play();
            }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //nigdy nie wygra auto
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            //normalne
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            /*nie mozliwe
             losowana ma zawsze wygrac
             */
            
        }
    }
    }

/*
0 - kamien
1 - papier
2 - nozyczki
 */