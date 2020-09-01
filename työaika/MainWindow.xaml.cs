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
using System.Data;
using tyoaika;
using System.Collections.ObjectModel;

namespace työaika
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Tehtavat> tehtavat = new ObservableCollection<Tehtavat>();
        private DataSet1 set = new DataSet1();

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Button_TehtavaLisaa(object sender, RoutedEventArgs e)
        {
            DataSet1 ds = new DataSet1();  
            DataSet1.TehtavatRow rivi = ds.Tehtavat.NewTehtavatRow();
            rivi.Tehtava = this.textBoxTehtava.Text;
            ds.Tehtavat.AddTehtavatRow(rivi);

            tyoaika.DataSet1TableAdapters.TehtavatTableAdapter adap = new tyoaika.DataSet1TableAdapters.TehtavatTableAdapter();
            adap.Update(ds.Tehtavat);
            //Lisätään uusi tehtävä tietokantaan
            HaeData();
        }

        private void btnTehtavaPoista_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnKohdeLisaa_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnKohdePoista_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRiviLisaa_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRiviMuokkaa_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRiviPoista_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRiviLaheta_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRiviTyhjenna_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            HaeData();
        }
        private void HaeData()
        {
            DataSet1 ds = new DataSet1();
            tyoaika.DataSet1TableAdapters.TehtavatTableAdapter adap =
                new tyoaika.DataSet1TableAdapters.TehtavatTableAdapter();
            tehtavat.Clear();
            adap.Fill(ds.Tehtavat);
            foreach (DataRow row in ds.Tables["Tehtavat"].Rows)
            {
                Tehtavat t = new Tehtavat();

                t.Tehtava = row["Tehtava"].ToString();
                tehtavat.Add(t);
            }
            this.listViewTehtavat.ItemsSource = tehtavat;
        }

        private void listViewTehtavat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
