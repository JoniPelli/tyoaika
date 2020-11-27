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
using System.Globalization;
using System.Threading;
using tyoaika.DataSet1TableAdapters;
using System.Windows.Controls.Primitives;

namespace työaika
{
    /// <summary>
    /// </summary>
    /// kommenttia
    public partial class MainWindow : Window
    {
        private ObservableCollection<Kohteet> kohteet = new ObservableCollection<Kohteet>();
        private DataSet1 set1 = new DataSet1();

        private ObservableCollection<Tehtavat> tehtavat = new ObservableCollection<Tehtavat>();
        private DataSet1 set = new DataSet1();

        private ObservableCollection<Tyoaika> tyoaika = new ObservableCollection<Tyoaika>();
        
        public MainWindow()
        {

            InitializeComponent();

            //Lisätään ComboBoxTunnit numerot 1-24
            for (int i = 1; i < 25; i++)
            {
                this.comboBoxTunnit.Items.Add(i);
            }
            this.comboBoxTunnit.SelectedIndex = 0;

        }

        private void Button_TehtavaLisaa(object sender, RoutedEventArgs e)
        {
            //Lisätään uusi tehtävä tietokantaan
            DataSet1 ds = new DataSet1();
            DataSet1.TehtavatRow rivi = ds.Tehtavat.NewTehtavatRow();
            rivi.Tehtava = this.textBoxTehtava.Text;
            ds.Tehtavat.AddTehtavatRow(rivi);

            if (this.textBoxTehtava.Text.Length == 0)
            {
                MessageBox.Show("Et voi lisätä tyhjää riviä!", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                tyoaika.DataSet1TableAdapters.TehtavatTableAdapter adap = new tyoaika.DataSet1TableAdapters.TehtavatTableAdapter();
                adap.Update(ds.Tehtavat);
            }
            HaeDataTehtavat();
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
            //Marjolle hommia
        }

        private void btnRiviLisaa_Click(object sender, RoutedEventArgs e)
        {
            //Viesti, jos päivämäärä on tyhjä
            if (this.datePickerPaivamaara.SelectedDate == null)
            {
                MessageBox.Show("Päivämäärä on tyhjä.", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                //Lisätään työaika listanäkymään valitut päivä, tehtävä, kohde, tunnit ja vapaateksti
                Tyoaika t = new Tyoaika();
                t.Pvm = this.datePickerPaivamaara.SelectedDate.Value;
                t.KohdeID = int.Parse(this.comboBoxKohde.SelectedItem.ToString().
                    Substring(0, this.comboBoxKohde.SelectedItem.ToString().IndexOf(' ')));
                t.Kohde = this.comboBoxKohde.SelectedItem.ToString().Substring(2);
                t.TehtavaID = int.Parse(this.comboBoxTehtava.SelectedItem.ToString().
                    Substring(0, this.comboBoxTehtava.SelectedItem.ToString().IndexOf(' ')));
                t.Tehtava = this.comboBoxTehtava.SelectedItem.ToString().Substring(2);
                t.Tunnit = this.comboBoxTunnit.SelectedItem.ToString();
                t.Vapaateksti = this.textBoxVapaateksti.Text;
                t.paivaysMerkkijoniksi();
                this.listViewRivi.Items.Add(t);

                //lisätään Tyoaika-olio tyoaika kokoelmaan
                tyoaika.Add(t);
            }
        }

        private void btnRiviPoista_Click(object sender, RoutedEventArgs e)
        {
            if (listViewRivi.SelectedItems.Count == 0)
            {
                MessageBox.Show("Valitse poistettava rivi", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                var valitut = listViewRivi.SelectedItems.Cast<object>().ToList();
                
                //Poistetaan valitut rivit listalta ja Tyoaika oliosta
                foreach (object item in valitut)
                {
                    listViewRivi.Items.Remove(item);
                    tyoaika.Remove((Tyoaika)item);
                }
            }
        }

        private void btnRiviLaheta_Click(object sender, RoutedEventArgs e)
        {
            DataSet1 ds = new DataSet1();

            // Lisätään työaika -välilehden listanäkymässä olevat rivit tietokantaan
            foreach (Tyoaika t in tyoaika)
            {
                DataSet1.KirjausRow rivi = ds.Kirjaus.NewKirjausRow();

                rivi.TehtavaID = t.TehtavaID;
                rivi.KohdeID = t.KohdeID;
                rivi.Pvm = t.Pvm;
                rivi.Tunnit = int.Parse(t.Tunnit);
                ds.Kirjaus.AddKirjausRow(rivi);
            }

            KirjausTableAdapter adap = new KirjausTableAdapter();
            adap.Update(ds.Kirjaus);

            //Tyhjentää lopuksi listanäkymän ja työaika -olio
            this.listViewRivi.Items.Clear();
            tyoaika.Clear();
        }

        private void btnRiviTyhjenna_Click(object sender, RoutedEventArgs e)
        {
            //tyhjennetään työaika välilehden listanäkymä ja tyoaika -olio
            this.listViewRivi.Items.Clear();
            tyoaika.Clear();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            HaeDataTehtavat();
            HaeDataKohde();
        }


        private void HaeDataTehtavat()
        {
            DataSet1 ds = new DataSet1();
            tyoaika.DataSet1TableAdapters.TehtavatTableAdapter adap =
                new tyoaika.DataSet1TableAdapters.TehtavatTableAdapter();
            tehtavat.Clear();
            adap.Fill(ds.Tehtavat);
            this.comboBoxTehtava.Items.Clear();
            this.comboBoxTehtava.SelectedIndex = 0;

            //Lisätään Tehtävät tietokantaan
            foreach (DataRow row in ds.Tables["Tehtavat"].Rows)
            {
                Tehtavat t = new Tehtavat();
                t.TehtavatId = int.Parse(row["TehtavaId"].ToString());
                t.Tehtava = row["Tehtava"].ToString();
                tehtavat.Add(t);

                //Lisätään tehtävät työaikavälilehden ComboBoksiin
                this.comboBoxTehtava.Items.Add(t.TehtavatId + " " + t.Tehtava);
            }
            //Lisätään tehtävät työaikavälilehden listanäkymään
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

        private void bntHae_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRaportti_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}