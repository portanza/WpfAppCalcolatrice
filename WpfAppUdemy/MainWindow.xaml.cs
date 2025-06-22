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
using System.Globalization;


namespace WpfAppCalcolatrice;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{

    double primoNumero, secondoNumero;
    OperazioneSelezionata operazioneSelezionata;



    public MainWindow()
    {
        InitializeComponent();

        // Imposta la cultura italiana per usare la virgola come separatore decimale
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("it-IT");
        System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("it-IT");


        acButton.Click += AcButton_Click;
        negativoButton.Click += NegativoButton_Click;
        percentualeButton.Click += PercentualeButton_Click;
        ugualeButton.Click += UgualeButton_Click;

        dividiButton.Click += OperazioneButton_Click;
        moltiplicaButton.Click += OperazioneButton_Click;
        menoButton.Click += OperazioneButton_Click;
        piuButton.Click += OperazioneButton_Click;

        virgolaButton.Click += VirgolaButton_Click;  
        zeroButton.Click += NumeroButton_Click;
        unoButton.Click += NumeroButton_Click;
        dueButton.Click += NumeroButton_Click;
        treButton.Click += NumeroButton_Click;
        quattroButton.Click += NumeroButton_Click;
        cinqueButton.Click += NumeroButton_Click;
        seiButton.Click += NumeroButton_Click;
        setteButton.Click += NumeroButton_Click;
        ottoButton.Click += NumeroButton_Click;
        noveButton.Click += NumeroButton_Click;



    }



    private void AcButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender == acButton)
        {
            risultatoLabel.Content = "0";

         }
    }

    private void NegativoButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender == negativoButton)
        {
            if (double.TryParse(risultatoLabel.Content.ToString(), out double primoNumero))
            {
                primoNumero *= -1;
                risultatoLabel.Content = primoNumero.ToString("G", CultureInfo.CurrentCulture);

            }
        }
    }

    private void PercentualeButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender == percentualeButton)
        {
            if (double.TryParse(risultatoLabel.Content.ToString(), out double primoNumero))
            {
                primoNumero /= 100;
                risultatoLabel.Content = primoNumero.ToString();

            }
        }
    }

    private void VirgolaButton_Click(object sender, RoutedEventArgs e)
    {

        if (risultatoLabel.Content.ToString().Contains(","))
        {
            // Non fare niente
        }
        else
        {
            // Aggiungi la virgola al contenuto esistente
            risultatoLabel.Content = $"{risultatoLabel.Content},";
        }
    }

    private void NumeroButton_Click(object sender, RoutedEventArgs e)
    {
        int valoreCliccato = 0;

        if (sender == zeroButton) valoreCliccato = 0;
        if (sender == unoButton) valoreCliccato = 1;
        if (sender == dueButton) valoreCliccato = 2;
        if (sender == treButton) valoreCliccato = 3;
        if (sender == quattroButton) valoreCliccato = 4;
        if (sender == cinqueButton) valoreCliccato = 5;
        if (sender == seiButton) valoreCliccato = 6;
        if (sender == setteButton) valoreCliccato = 7;
        if (sender == ottoButton) valoreCliccato = 8;
        if (sender == noveButton) valoreCliccato = 9;



        if (risultatoLabel.Content.ToString() == "0")
        {
            // Se il contenuto è 0, sostituiscilo con il numero selezionato
            risultatoLabel.Content = valoreCliccato.ToString();

        }
        else
        {
            // Altrimenti, aggiungi il numero selezionato al contenuto esistente
            risultatoLabel.Content = $"{risultatoLabel.Content}{valoreCliccato}";

        }
    }

    private void OperazioneButton_Click(object sender, RoutedEventArgs e)
    {
        if (double.TryParse(risultatoLabel.Content.ToString(), out primoNumero))
        {
            risultatoLabel.Content = "0";
        }



        // Memorizza l'operazione richiesta dall'utente in operazioneSelezionata

        if (sender == piuButton)
        {
            operazioneSelezionata = OperazioneSelezionata.Addizione;
        }
        if (sender == menoButton)
        {
            operazioneSelezionata = OperazioneSelezionata.Sottrazione;
        }
        if (sender == moltiplicaButton)
        {
            operazioneSelezionata = OperazioneSelezionata.Moltiplicazione;
        }
        if (sender == dividiButton)
        {
            operazioneSelezionata = OperazioneSelezionata.Divisione;
        }
    }

    private void UgualeButton_Click(object sender, RoutedEventArgs e)
    {

        if (sender == ugualeButton)
        {
            // Il secondoNumero è quello inserito dopo l'operazione selezionata

            if (double.TryParse(risultatoLabel.Content.ToString(), out secondoNumero))
            {
                switch (operazioneSelezionata)
                {
                    case OperazioneSelezionata.Addizione:
                        this.primoNumero = Matematica.Add(primoNumero, secondoNumero);
                        break;
                    case OperazioneSelezionata.Sottrazione:
                        this.primoNumero = Matematica.Subtract(primoNumero, secondoNumero);
                        break;
                    case OperazioneSelezionata.Moltiplicazione:
                        this.primoNumero = Matematica.Multiply(primoNumero, secondoNumero);
                        break;
                    case OperazioneSelezionata.Divisione:
                        this.primoNumero = Matematica.Divide(primoNumero, secondoNumero);
                        break;
                }

                risultatoLabel.Content = this.primoNumero.ToString("0.################", CultureInfo.CurrentCulture);

                secondoNumero = 0; // Resetta il secondo numero dopo l'operazione
            }
        }

    }


 


    public enum OperazioneSelezionata
    {
        Addizione,
        Sottrazione,
        Divisione,
        Moltiplicazione
    }

    public class Matematica
    {
        public static double Add(double a, double b) => a + b;
        public static double Subtract(double a, double b) => a - b;
        public static double Multiply(double a, double b) => a * b;
        public static double Divide(double a, double b) => b != 0 ? a / b : throw new DivideByZeroException("Divisione per zero non permessa.");
    }

}