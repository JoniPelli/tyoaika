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
using System.Data.SqlClient;
using Microsoft.Win32;
using System.IO;
using System.Configuration;


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

         //Lisätään työntekijänID numero
        int globaltyöntekijäID = -1;


        public MainWindow()
        {
            InitializeComponent();
            
            for (int i = 1; i < 25; i++)
            {
                this.comboBoxTunnit.Items.Add(i);
            }
            this.comboBoxTunnit.SelectedIndex = 0;

            //Lisätään Windows käyttäjätunnus oikeaan yläkulmaan
            String kayttajatunnus = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            this.textblockKayttaja.Text = kayttajatunnus;


        }

        private void Button_TehtavaLisaa(object sender, RoutedEventArgs e)
        {
            //Lisätään uusi tehtävä tietokantaan
            DataSet1 ds = new DataSet1();
            DataSet1.TehtavatRow rivi = ds.Tehtavat.NewTehtavatRow();
            rivi.Tehtava = this.textBoxTehtava.Text;
            String sana = this.textBoxTehtava.Text;


            ds.Tehtavat.AddTehtavatRow(rivi);

            if (this.textBoxTehtava.Text.Length == 0)
            {
                MessageBox.Show("Et voi lisätä tyhjää riviä!", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            //Etsitään SQL komentoja tietokantaan lisättävästä rivistä
            else if (kiellettySana(sana) == true)
            {
                MessageBox.Show("Sisältää SQL komentoja, korjaa tehtävän teksti");
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
            if (listViewTehtavat.SelectedItem != null)
            {
                Tehtavat item = (Tehtavat)listViewTehtavat.SelectedItem;

                DataSet1 ds = new DataSet1();
                KirjausTableAdapter adapKirjaus = new KirjausTableAdapter();
                adapKirjaus.FillBy(ds.Kirjaus, item.TehtavatId);
                if (ds.Tables["Kirjaus"].Rows.Count > 0)
                {
                    MessageBox.Show("Tehtävällä on kirjauksia, ei voida poistaa!");
                }

                // Poistetaan tietoa Tehtävä-taulusta
                else
                {
                    TehtavatTableAdapter adap = new TehtavatTableAdapter();
                    adap.Fill(ds.Tehtavat);
                    adap.DeleteQuery(item.TehtavatId, item.Tehtava);
                    adap.Update(ds.Tehtavat);
                    HaeDataTehtavat();
                }
            }
        }

        private void btnKohdeLisaa_Click(object sender, RoutedEventArgs e)
        {
            DataSet1 ds = new DataSet1();
            DataSet1.KohteetRow rivi = ds.Kohteet.NewKohteetRow();
            rivi.Kohde = this.textBoxKohde.Text;
            ds.Kohteet.AddKohteetRow(rivi);
            string kohde = this.textBoxKohde.Text;

            if (this.textBoxKohde.Text.Length == 0)
            {
                MessageBox.Show("Et voi lisätä tyhjää riviä!", "Asetukset", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            //Etsitään SQL komentoja tietokantaan lisättävästä rivistä
            else if (kiellettySana(kohde) == true)
            {
                MessageBox.Show("Sisältää SQL komentoja, korjaa kohteen teksti");
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
            // Poistetaan tietoa Kohde-taulusta
            if (listViewKohde.SelectedItem != null)
            {
                Kohteet item = (Kohteet)listViewKohde.SelectedItem;

                DataSet1 ds = new DataSet1();
                KirjausTableAdapter adapKirjaus = new KirjausTableAdapter();
                adapKirjaus.FillBy(ds.Kirjaus, item.KohdeID);
                if (ds.Tables["Kirjaus"].Rows.Count > 0)
                {
                    MessageBox.Show("Kohteelle on kirjauksia, ei voida poistaa!");
                }

                // Poistetaan tietoa Tehtävä-taulusta
                else
                {
                    KohteetTableAdapter adap = new KohteetTableAdapter();
                    adap.Fill(ds.Kohteet);
                    adap.DeleteQuery(item.KohdeID, item.Kohde);
                    adap.Update(ds.Kohteet);
                    HaeDataKohde();
                }
            }
        }

        private void btnRiviLisaa_Click(object sender, RoutedEventArgs e)
        {
            string vteksti = this.textBoxVapaateksti.Text;
            //Viesti, jos päivämäärä on tyhjä
            if (this.datePickerPaivamaara.SelectedDate == null)
            {
                MessageBox.Show("Päivämäärä on tyhjä.", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            //Etsitään SQL komentoja tietokantaan lisättävästä rivistä
            else if (kiellettySana(vteksti) == true)
            {
                MessageBox.Show("Sisältää SQL komentoja, korjaa vapaateksti");
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

            // Lisätään työaika -välilehden listanäkymässä olevat rivit  sekä työntekijän ID numero Kirjaus -tauluun tietokantaan 
            foreach (Tyoaika t in tyoaika)
            {
                DataSet1.KirjausRow rivi = ds.Kirjaus.NewKirjausRow();

                rivi.TyontekijaID = globaltyöntekijäID;
                rivi.TehtavaID = t.TehtavaID;
                rivi.KohdeID = t.KohdeID;
                rivi.Pvm = t.Pvm;
                rivi.Tunnit = int.Parse(t.Tunnit);
                rivi.Vapaateksti = t.Vapaateksti;
                ds.Kirjaus.AddKirjausRow(rivi);
            }

            KirjausTableAdapter adap = new KirjausTableAdapter();
            adap.Update(ds.Kirjaus);

            //Lisätään Windows käyttäjätunnus tietokantaan käyttäjän tunnistamiseksi
            DataSet1.TyontekijaRow rivi2 = ds.Tyontekija.NewTyontekijaRow();
            rivi2.Kayttajatunnus = textblockKayttaja.Text;
            TyontekijaTableAdapter adap1 = new TyontekijaTableAdapter();
            adap1.Update(ds.Tyontekija);

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

            //Katsotaan käyttäjätunnuksen ja käyttö-oikeuksien perusteella näkyykö asetukset välilehti
            DataSet1 ds = new DataSet1();
            TyontekijaTableAdapter adap = new TyontekijaTableAdapter();

            adap.Fill(ds.Tyontekija);

            string kayttajatunnus = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            bool loyty = false;


            foreach (DataRow row in ds.Tables["Tyontekija"].Rows)
            {
                string kTunnus = row["Kayttajatunnus"].ToString();
                var kOikeus = row["Kayttooikeus"];
                globaltyöntekijäID = (int)row["TyontekijaID"];

                //Käyttäjäoikeuksilla = kOikeus numero 1 on oikeudet asetukset välilehteen muilla ei
                if (kayttajatunnus.CompareTo(kTunnus) == 0 && kOikeus.Equals(1))
                {
                    loyty = true;
                }

            }
            if (loyty)
            {
                this.textblockKayttaja.Text = kayttajatunnus;
            }
            else
            {
                this.tabAsetukset.Visibility = Visibility.Hidden;
            }



        }

        // Haetaan Tehtävä-taulusta data comboboxeihin

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

        // Haetaan Kohde-taulusta data comboboxeihin

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

        private bool kiellettySana(String sana)
        {
            //Tarkastetaan, ettei tekstikenttiin voi kirjoittaa SQL komentoja
            string[] sanat = new string[]{"select","drop","update","delete","insert","create","alter",
            "from","select*"};
            string[] lista = new string[] { };
            string pienella = sana.ToLower();
            lista = pienella.Split(' ');

            for (int i = 0; i < sanat.Length; i++)
            {
                for (int j = 0; j < lista.Length; j++)
                {
                    if (sanat[i].Equals(lista[j]))
                    { return true; }

                }

            }
            return false;

        }

        /* Haetaan tietokannasta käyttäjän aiemmin syöttämät työajat ja listataan ne näkymään
            Mikäli päivämäärävalitsimet jää tyhjäksi, annetaan tästä ilmoitus*/

        private void bntHae_Click(object sender, RoutedEventArgs e)
        {
            DataSet1 ds = new DataSet1();
            KirjausTableAdapter adap = new KirjausTableAdapter();
            if (this.datePickerHaeAlku.SelectedDate == null || this.datePickerHaeLoppu.SelectedDate == null)
            {
                MessageBox.Show("Valitse päivämäärä!", "", MessageBoxButton.OK, MessageBoxImage.Information); 
            }
            else
            {
                String alku = this.datePickerHaeAlku.SelectedDate.Value.ToString();
                String loppu = this.datePickerHaeLoppu.SelectedDate.Value.ToString();
                adap.FillByDate(ds.Kirjaus, alku, loppu);
                this.listViewRaportti.Items.Clear();

                foreach (DataRow row in ds.Tables["Kirjaus"].Rows)
                {
                    Tyoaika t = new Tyoaika();
                    //t.Pvm = DateTime.Parse(row["Pvm"].ToString());
                    t.Pvm = DateTime.Parse(row["Pvm"].ToString());
                    t.KohdeID = int.Parse(row["KohdeID"].ToString());
                    t.Kohde = row["Kohde"].ToString();
                    t.TehtavaID = int.Parse(row["TehtavaID"].ToString());
                    t.Tehtava = row["Tehtava"].ToString();
                    t.Tunnit = row["Tunnit"].ToString();
                    t.Vapaateksti = row["Vapaateksti"].ToString();
                    t.paivaysMerkkijoniksi();
                    this.listViewRaportti.Items.Add(t);
                }
            }
        }

        // Tallennetaan näkymän tiedot haluttuun tiedostomuotoon

        private void btnRaportti_Click(object sender, RoutedEventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "txt files (*.txt)|*.txt|CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            sfd.FilterIndex = 2;

            Nullable<bool> dr = sfd.ShowDialog();
            if (dr == true)
            {
                File.WriteAllText(sfd.FileName, ListViewToCSV(listViewRaportti).ToString(), Encoding.UTF8);
            }
        }

        // Muunnetaan erotinmerkit Excel-yhteensopivaksi

        public static StringBuilder ListViewToCSV(ListView listView)
        {
            StringBuilder results = new StringBuilder();
            foreach(Tyoaika item in listView.Items)
            {
                results.AppendLine(item.CSVRiviksi());
            }

            return results;
        }

        private void listViewRaportti_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void listViewRaportti_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }
    }
}