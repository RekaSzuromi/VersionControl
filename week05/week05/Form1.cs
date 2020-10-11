using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using week05.Entities;

namespace week05
{
    public partial class Form1 : Form
    {
        PortfolioEntities context = new PortfolioEntities();
        List<Tick> Ticks;
        List<PortfolioItem> Portfolio = new List<PortfolioItem>();
        List<decimal> Nyereségek = new List<decimal>();


        public Form1()
        {
            InitializeComponent();
            Ticks = context.Ticks.ToList();
            dataGridView1.DataSource = Ticks;
            CreatePortfolio();
            VARszamol();
            

        }

        private void CreatePortfolio()
        {
            Portfolio.Add(new PortfolioItem() { Index = "OTP", Volume = 10 });
            Portfolio.Add(new PortfolioItem() { Index = "ZWACK", Volume = 10 });
            Portfolio.Add(new PortfolioItem() { Index = "ELMU", Volume = 10 });

            dataGridView2.DataSource = Portfolio;

        }
        private decimal GetPortfolioValue(DateTime date)
        {
            decimal value = 0;
            foreach (var item in Portfolio)
            {
                var last = (from x in Ticks
                            where item.Index == x.Index.Trim()
                               && date <= x.TradingDay
                            select x)
                            .First();
                value += (decimal)last.Price * item.Volume;
            }
            return value;

            
            

        }
        private void VARszamol() 
        {
            int intervalum = 30;
            DateTime kezdőDátum = (from x in Ticks select x.TradingDay).Min();
            DateTime záróDátum = new DateTime(2016, 12, 30);
            TimeSpan z = záróDátum - kezdőDátum;
            for (int i = 0; i < z.Days - intervalum; i++)
            {
                decimal ny = GetPortfolioValue(kezdőDátum.AddDays(i + intervalum))
                           - GetPortfolioValue(kezdőDátum.AddDays(i));
                Nyereségek.Add(ny);
                Console.WriteLine(i + " " + ny);

            }
            var nyereségekRendezve = (from x in Nyereségek
                                      orderby x
                                      select x)
                            .ToList();
            MessageBox.Show(nyereségekRendezve[nyereségekRendezve.Count() / 5].ToString());



        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            SaveFileDialog sfd = new SaveFileDialog();

            // Opcionális rész
            sfd.InitialDirectory = Application.StartupPath; // Alapértelmezetten az exe fájlunk mappája fog megnyílni a dialógus ablakban
            sfd.Filter = "Comma Seperated Values (*.txt)|*.txt"; 
            sfd.DefaultExt = "txt"; // A csv lesz az alapértelmezetten kiválasztott kiterjesztés
            sfd.AddExtension = true; // Ha ez igaz, akkor hozzáírja a megadott fájlnévhez a kiválasztott kiterjesztést, de érzékeli, ha a felhasználó azt is beírta és nem fogja duplán hozzáírni

            // Ez a sor megnyitja a dialógus ablakot és csak akkor engedi tovább futni a kódot, ha az ablakot az OK gombbal zárták be
            if (sfd.ShowDialog() != DialogResult.OK) return;

            // Az előző kódsor az alábbi két sor rövidített írásmódja
            // DialogResult eredmény = sfd.ShowDialog(); // A dialógusablak bezárása után visszakapunk egy DialogResult típusú értéket, mely az ablak bezárásnak körülményeit tárolja
            // if (eredmény != DialogResult.OK) return; // Ha a bezárás nem az OK gomb lenyomására következett be, akkor kilépünk a metódusból és nem hajtjuk végre a mentést

            // Ha a using blokk használatával példányosítunk egy osztályt akkor a példány csak a using blokk végéig létezik, utána törlésre kerül
            // StreamWriter és StreamReader használata esetén ezzel a módszerrel megspórolhatjuk a Close() metódus használatát és az írás / olvasási hibák egy részét is lekezeljük
            // A StreamWriter paraméterei:
            //    1) Fájlnév: mi itt azt a fájlnevet adjuk át, amit a felhasználó az sfd dialógusban megadott
            //    2) Append: ha igaz és már létezik ilyen fájl, akkor a végéhez fűzi a sorokat, ha hamis, akkor felülírja a létező fájlt
            //    3) Karakterkódolás: a magyar nyelvnek is megfelelő legáltalánosabb karakterkódolás az UTF8
            using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
            {
                int szamlalo = 0;
                // Végigmegyünk a nyereség lista elemein
                sw.Write("Időszak");
                sw.Write(";");
                sw.Write("Nyereség");
                sw.WriteLine();


                foreach (var s in Nyereségek)
                {
                    
                    sw.Write(szamlalo.ToString());
                    sw.Write(";");
                    sw.Write(s.ToString());
                    sw.WriteLine(); // Ez a sor az alábbi módon is írható: sr.Write("\n");
                    szamlalo++;
                }
            }
        }
    }
}
