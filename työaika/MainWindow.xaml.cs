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
        private ObservableCollection<Kohteet> kohteet = new ObservableCollection<Kohteet>();
        private DataSet1 set1 = new DataSet1();

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

            if (this.textBoxTehtava.Text.Length == 0)
            {
                MessageBox.Show("Et voi lisätä tyhjää riviä!", "Asetukset", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                tyoaika.DataSet1TableAdapters.TehtavatTableAdapter adap = new tyoaika.DataSet1TableAdapters.TehtavatTableAdapter();
                adap.Update(ds.Tehtavat);
            }
            //Lisätään uusi tehtävä tietokantaan
            HaeData();
        }

        private void btnTehtavaPoista_Click(object sender, RoutedEventArgs e)
        {
            //Marjolle hommia
        }

        private void btnKohdeLisaa_Click(object sender, RoutedEventArgs e)
        {
            DataSet1 ds = new DataSet1();
            DataSet1.KohteetRow rivi = ds.Kohteet.NewKohteetRow();
            rivi.Kohde = this.textBoxKohde.Text;
            ds.Kohteet.AddKohteetRow(rivi);

            if (this.textBoxKohde.Text.Length == 0)
            {
                MessageBox.Show("Et voi lisätä tyhjää riviä!", "Asetukset", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                tyoaika.DataSet1TableAdapters.KohteetTableAdapter adap = new tyoaika.DataSet1TableAdapters.KohteetTableAdapter();
                adap.Update(ds.Kohteet);
            }

            HaeDataKohde();

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

            HaeDataKohde();
        }

       
        private void HaeData()
        {
            DataSet1 ds = new DataSet1();
            tyoaika.DataSet1TableAdapters.TehtavatTableAdapter adap =
                new tyoaika.DataSet1TableAdapters.TehtavatTableAdapter();
            tehtavat.Clear();
            adap.Fill(ds.Tehtavat);
            this.comboBoxTehtava.Items.Clear(); 
            this.comboBoxTehtava.SelectedIndex = 0;
            foreach (DataRow row in ds.Tables["Tehtavat"].Rows)
            {
                Tehtavat t = new Tehtavat();
                t.TehtavatId = int.Parse(row["TehtavaId"].ToString());
                t.Tehtava = row["Tehtava"].ToString();
                tehtavat.Add(t);
                this.comboBoxTehtava.Items.Add(t.TehtavatId + " " + t.Tehtava);
                //Lisätään tehtävät työaikavälilehden ComboBoksiin
            }
            this.listViewTehtavat.ItemsSource = tehtavat;
            this.textBoxTehtava.Clear();
            

           
        }

        private void HaeDataKohde()
        {
            DataSet1 ds = new DataSet1();
            tyoaika.DataSet1TableAdapters.KohteetTableAdapter adap =
                new tyoaika.DataSet1TableAdapters.KohteetTableAdapter();
            kohteet.Clear();
            adap.Fill(ds.Kohteet);
            this.comboBoxKohde.Items.Clear();
            this.comboBoxKohde.SelectedIndex = 0;
            foreach (DataRow row in ds.Tables["Kohteet"].Rows)
            {
                Kohteet k = new Kohteet();
                k.KohdeID = int.Parse(row["KohdeID"].ToString());
                k.Kohde = row["Kohde"].ToString();
                kohteet.Add(k);
                this.comboBoxKohde.Items.Add(k.KohdeID + " " + k.Kohde);
            }
            this.listViewKohde.ItemsSource = kohteet;
            this.textBoxKohde.Clear();
        }

        private void listViewTehtavat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void listViewKohde_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnHae_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnHakuTyhjenna_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
