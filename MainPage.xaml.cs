using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//Szablon elementu Pusta strona jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x415

namespace Kalkulator
{
    /// <summary>
    /// Pusta strona, która może być używana samodzielnie lub do której można nawigować wewnątrz ramki.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        double wynik = 0; //wyniki obliczeń
        string dzialanie = ""; //wybrane dzialanie
        bool wprowadzona_wartosc = false;
        bool zero = false;
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void cyfry(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            if (Convert.ToDouble(b.Content) == 0)
                zero = true;
            else
                zero = false;
            double Num;
            bool isNum = double.TryParse(Oper.Text, out Num);
            if (!isNum)
                Oper.Text = "";
            if ((Convert.ToString(Display.Text) == "0") || (wprowadzona_wartosc)) //kasowanie cyfr
                Display.Text = "";
            wprowadzona_wartosc = false;
            if (Convert.ToString(b.Content) == ",")
            {
                if (!Display.Text.Contains(",")) //zapobiega kilkukrotnemu wstawieniu przecinka
                {
                    Display.Text += Convert.ToString(b.Content);
                    Oper.Text += Convert.ToString(b.Content); //wyświetla operacje na gornym wyswietlaczu
                }
            }
            else
            {
                Display.Text = Display.Text + Convert.ToString(b.Content);
                Oper.Text += Convert.ToString(b.Content);
            }
        }

        private void operacje(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            if (wynik != 0)
            {
                obliczenia(); //Zamiast tej metody powinienem użyć coś podobnego do PerformClick z WindowsForms
                wprowadzona_wartosc = true;
                dzialanie = Convert.ToString(b.Content);
                Oper.Text += dzialanie;
            }
            else
            {
                dzialanie = Convert.ToString(b.Content);
                wynik = Convert.ToDouble(Display.Text);
                wprowadzona_wartosc = true;
                Oper.Text += dzialanie;
            }
        }

        private void rownosc(object sender, RoutedEventArgs e)
        {
            Oper.Text = "";
            switch (dzialanie)
            {
                case "+":
                    Display.Text = (wynik += Convert.ToDouble(Display.Text)).ToString();
                    break;
                case "-":
                    Display.Text = (wynik -= Convert.ToDouble(Display.Text)).ToString();
                    break;
                case "X":
                    Display.Text = (wynik *= Convert.ToDouble(Display.Text)).ToString();
                    break;
                case "/":
                    if (zero)
                    {
                        Oper.Text = "Nieskonczonosc";
                        wynik = 0;
                        dzialanie = "";
                        Display.Text = "";
                    }
                    else
                        Display.Text = (wynik /= Convert.ToDouble(Display.Text)).ToString();
                    break;
                default:
                    break;
            }
            Display.Text = Convert.ToString(wynik);
            dzialanie = "";
        }

        void obliczenia() //Zamiast tej metody powinienem użyć coś podobnego do PerformClick z WindowsForms
        {
            Oper.Text = "";
            switch (dzialanie)
            {
                case "+":
                    Display.Text = (wynik += Convert.ToDouble(Display.Text)).ToString();
                    break;
                case "-":
                    Display.Text = (wynik -= Convert.ToDouble(Display.Text)).ToString();
                    break;
                case "X":
                    Display.Text = (wynik *= Convert.ToDouble(Display.Text)).ToString();
                    break;
                case "/":
                    if (zero)
                    {
                        Oper.Text = "Nieskonczonosc";
                        wynik = 0;
                        dzialanie = "";
                        Display.Text = "";
                    }
                    else
                        Display.Text = (wynik /= Convert.ToDouble(Display.Text)).ToString();
                    break;
                default:
                    break;
            }
            Display.Text = Convert.ToString(wynik);
            dzialanie = "";
        }

        private void C(object sender, RoutedEventArgs e)
        {
            Display.Text = "0";
            wynik = 0;
            Oper.Text = "";
        }

        private void CE(object sender, RoutedEventArgs e)
        {
            Display.Text = "0";
        }

        private void kwadrat(object sender, RoutedEventArgs e)
        {
            dzialanie = "";
            Oper.Text = "";
            Display.Text = (wynik = Math.Pow(Convert.ToDouble(Display.Text),2)).ToString();
        }

        private void pierwiastek(object sender, RoutedEventArgs e)
        {
            dzialanie = "";
            if (Convert.ToDouble(Display.Text)<0)
            {
                Display.Text = "";
                Oper.Text = "Nie ma pierw. z liczby ujemnej!";
                wynik = 0;
            }
            else
                Display.Text =( wynik = Math.Sqrt(Convert.ToDouble(Display.Text)) ).ToString();
        }

        private void ulamek(object sender, RoutedEventArgs e)
        {
            dzialanie = "";
            if (Convert.ToDouble(Display.Text) == 0)
            {
                Display.Text = "";
                Oper.Text = "Nieskonczoność";
                wynik = 0;
            }
            else
                Display.Text = ( wynik = 1 / (Convert.ToDouble(Display.Text)) ).ToString();
        }

        private void zmiana_znaku(object sender, RoutedEventArgs e)
        {
            if (Convert.ToDouble(Display.Text) != 0)
            {
                    wynik = Convert.ToDouble(Display.Text);
                    wynik *= (-1);
                    Display.Text = Convert.ToString(wynik);
            }
        }

        private void kasowanie(object sender, RoutedEventArgs e)
        {
            if (Display.Text!="")
                Display.Text = Display.Text.Remove( (Display.Text.Length) - 1 );
            else
                Oper.Text = "";
            if (Oper.Text != "")
                Oper.Text = Oper.Text.Remove( (Oper.Text.Length) - 1 );
        }

        private void Oper_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Display_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        
    }
}
