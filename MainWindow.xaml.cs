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

namespace hangman_project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string woord;
        private int levens = 10;

        List<string> MaskedWoord = new List<string>();

        List<string> JuistGeraden = new List<string>();
        List<string> FoutGeraden = new List<string>();

        char[] maskedArray;


        public MainWindow()
        {
            InitializeComponent();



            resultaat.Text = "Speler 1 klik op nieuw spel";
            raad.Visibility = Visibility.Hidden;

            #region comment
            //char.Parse(woord);
            //char[] maskedWoord = woord.ToCharArray();
            //for (int i = 0; i < maskedWoord.Length; i++)
            //{
            //maskedWoord[i] = masked.Text;
            //}
            #endregion
            MaskedWoord.Add(woord);


            char[] maskedArray = woord.ToCharArray();
            
        }

        #region Methods
        //alle textblokken leegmaken en opnieuw data invullen
        private void GebruikerData()
        {
            
            aantalLevens.Text = string.Empty;
            juisteKarakters.Text = string.Empty;
            fouteKarakters.Text = string.Empty;


            foreach (var item in JuistGeraden)
            {
                //correcte letter textbox invullen 
                juisteKarakters.Text += (item.ToString().ToLower() + " ");
                
            }
            foreach (var item in FoutGeraden)
            {
                //correcte letter textbox invullen 
                fouteKarakters.Text += (item.ToString().ToLower() + " ");               
            }

            aantalLevens.Text = $"{levens}";
        }


        private void EmptyList()
        {

            fouteKarakters.Text = string.Empty;
            juisteKarakters.Text = string.Empty;
            JuistGeraden.Clear();
            FoutGeraden.Clear();
            
        }        
        
        private void Masking(char oldValue, char? newValue)
        {
            #region comment
            //woord = "";
            //foreach (char c in maskedWoord)
            //{
            //   woord += "_";
            //   woord += " ";
            //}
            #endregion
            oldValue = $"{woord}";
            newValue = '_';
            woord.Replace('char', '_');
        }
        public void Images()
        {
            //img.Source = new BitmapImage(new Uri($@"C:\Users\tessa\source\repos\hangman-project\hangman-project\img"));
        }

        #endregion
        #region Raad

        private void raad_Click(object sender, RoutedEventArgs e)
        {
            Masking();
               
            #region Woord 

            //WOORD 
            if (input.Text.Length > 1)
            {

                if (woord == input.Text) //JUIST GERADEN
                {
                    
                    raad.Visibility = Visibility.Hidden;

                    input.Text = "";
                    resultaat.Text = $"GEWONNEN! \n\r Goed! Je hebt het woord geraden!";
                    masked.Text = woord;



                }
                else//FOUT GERADEN
                {
                    if (levens == 1)//EINDE
                    {
                        
                        aantalLevens.Text = $"0";
                        raad.Visibility = Visibility.Hidden;
                        
                        input.Text = "";
                        resultaat.Text = $"GAME OVER \n\r Helaas, je hebt het woord niet kunnen raden.";
                        masked.Text = woord;
                        
                    }
                    else //MIN LEVEN
                    {
                        
                        levens--;
                        input.Text = "";
                        resultaat.Text = $"Je hebt een fout woord geraden! Je verliest een leven";
                        GebruikerData();
                        masked.Text = woord;

                    }
                }
                
            }
            #endregion
            #region Letter
            else //LETTER
            {
                
                if (woord.Contains(input.Text)) //JUIST GERADEN
                {
                    if (input.Text.Length == woord.Length) //WOORD GERADEN
                    {

                        input.Text = "";
                        resultaat.Text = $"GEWONNEN! \n\r Goed! Je hebt het woord geraden! \n\r {woord}";
                        masked.Text = woord;
                        
                    }
                    else //LETTER GERADEN
                    {
                        string tempInput = input.Text;
                        JuistGeraden.Add(tempInput);

                        

                        input.Text = "";
                        resultaat.Text = $"Goed! Je hebt een letter geraden";

                        GebruikerData();
                        masked.Text = woord;
                    }
                }
                else //FOUT GERADEN
                {
                    if (levens == 1) //EINDE
                    {
                       
                        aantalLevens.Text = $"0";
                        raad.Visibility = Visibility.Hidden;
                        
                        input.Text = "";
                        resultaat.Text = $"GAME OVER \n\r Helaas, je hebt het woord niet kunnen raden.";

                    }
                    else //MIN LEVEN
                    {
                     
                        string tempInput = input.Text;
                        FoutGeraden.Add(tempInput);


                        levens--;
                        input.Text = "";
                        resultaat.Text = $"Fout! Je hebt een leven verloren";

                        GebruikerData();
                    }
                }
                
            }       
            
            #endregion
        }

        #endregion

        #region Nieuw spel
        private void nieuw_Click(object sender, RoutedEventArgs e)
        {
            Array.Clear(maskedWoord, 0, maskedWoord.Length);

            aantalLevens.Text = string.Empty;
            resultaat.Text = "Speler 2 geef een woord en klik op verbergen";
            raad.Visibility = Visibility.Visible;
            verberg.Visibility = Visibility.Visible;

            EmptyList();
            aantalLevens.Text = $"10";
            levens = 10;
        }
        #endregion

        #region Verberg
        private void verberg_Click(object sender, RoutedEventArgs e)
        {
            verberg.Visibility = Visibility.Hidden;
            raad.Visibility = Visibility.Visible;
            woord = input.Text;

            //string tempInput = input.Text;
            //MaskedWoord.Add(tempInput);


            input.Text = "";
            resultaat.Text = $"Je begint met 10 levens.";
            EmptyList();
            masked.Text = woord;
            Masking();
             
        }

        #endregion


    }

    #region Kort Schema
    /*
    Woord  OF  letter

    letter  -   juist   -   toon tekst juist/fout/levens
                OF      
                geraden -   einde                                                                            
                OF
                fout    -   -1  +   toon tekst juist/fout/levens
                            OF
                            0 levens einde


    woord   -   geraden -   einde
                OF 
                fout    -   -1  +   toon tekst juist/fout/levens
                            OF
                            0 levens einde

    */


    #endregion


}
