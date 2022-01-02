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


        List<string> JuistGeraden = new List<string>();
        List<string> FoutGeraden = new List<string>();


        public MainWindow()
        {
            InitializeComponent();

            resultaat.Text = "Speler 1 klik op nieuw spel";
            raad.Visibility = Visibility.Hidden;

            
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

      
   
        #endregion

        #region Raad



        private void raad_Click(object sender, RoutedEventArgs e)
        {
            #region Woord 
            
            //WOORD 
            if (input.Text.Length > 1)
            {

                if (woord == input.Text) //JUIST GERADEN
                {            
                    raad.Visibility = Visibility.Hidden;

                    input.Text = "";
                    resultaat.Text = $"GEWONNEN! \n\r Goed! Je hebt het woord geraden! \n\r {woord}";
                   
                }
                else//FOUT GERADEN
                {
                    if (levens == 1)//EINDE
                    {

                        aantalLevens.Text = $"0";
                        raad.Visibility = Visibility.Hidden;
                        
                        input.Text = "";
                        resultaat.Text = $"GAME OVER \n\r Helaas, je hebt het woord niet kunnen raden.";

                       
                    }
                    else //MIN LEVEN
                    {

                        levens--;
                        input.Text = "";
                        resultaat.Text = $"Je hebt een fout woord geraden! Je verliest een leven";
                        GebruikerData();
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
                        
                    }
                    else //LETTER GERADEN
                    {
                        string tempInput = input.Text;
                        JuistGeraden.Add(tempInput);

                        input.Text = "";
                        resultaat.Text = $"Goed! Je hebt een letter geraden";

                        GebruikerData();
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

            
            aantalLevens.Text = string.Empty;
            resultaat.Text = "Speler 2 geef een woord en klik op verbergen";
            raad.Visibility = Visibility.Visible;
            verberg.Visibility = Visibility.Visible;

            juisteKarakters.Text = "";
          
            fouteKarakters.Text = "";

            aantalLevens.Text = $"10";
        }


        #endregion

        #region Verberg
        private void verberg_Click(object sender, RoutedEventArgs e)
        {
            verberg.Visibility = Visibility.Hidden;
            raad.Visibility = Visibility.Visible;
            woord = input.Text;

            input.Text = "";
            resultaat.Text = $"Je begint met 10 levens."; 
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
