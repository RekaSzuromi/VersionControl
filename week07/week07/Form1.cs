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
using week07.Entities;

namespace week07
{
    //..

    public partial class Form1 : Form
    {
        List<Person> Population = new List<Person>();
        List<BirthProbability> BirthProbabilities = new List<BirthProbability>();
        List<DeathProbability> DeathProbabilities = new List<DeathProbability>();
        Random rng = new Random(1234);
        List<int> NbrOfFemales = new List<int>();
        List<int> NbrOfMales = new List<int>();


        public Form1()
        {
            InitializeComponent();
            Population = GetPopulation(@"C:\Users\pc\AppData\Local\Temp\nép-teszt.csv");
            BirthProbabilities = GetBirthProbabilities(@"C:\Users\pc\AppData\Local\Temp\születés.csv");
            DeathProbabilities = GetDeathProbabilities(@"C:\Users\pc\AppData\Local\Temp\halál.csv");
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public List<Person> GetPopulation(string csvpath)
        {
            List<Person> population = new List<Person>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    population.Add(new Person()
                    {
                        BirthYear = int.Parse(line[0]),
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[1]),
                        NbrOfChildren = int.Parse(line[2])
                    });
                }
            }

            return population;
        }
        
        public List<BirthProbability> GetBirthProbabilities(string csvpath)
        {
            List<BirthProbability> birthProbabilities = new List<BirthProbability>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    birthProbabilities.Add(new BirthProbability()
                    {   
                        Age = int.Parse(line[0]),
                        NbrOfChildren = int.Parse(line[1]),
                        bProbability = double.Parse(line[2])
                    });
                }
            }

            return birthProbabilities;
        }
        public List<DeathProbability> GetDeathProbabilities(string csvpath)
        {
            List<DeathProbability> deathProbabilities = new List<DeathProbability>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    deathProbabilities.Add(new DeathProbability()
                    {
                        Age = int.Parse(line[0]),
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[1]),
                        dProbability = double.Parse(line[2])
                    });
                }
            }

            return deathProbabilities;
        }
        private void SimStep(int year, Person person)
        {
            //Ha halott akkor kihagyjuk, ugrunk a ciklus következő lépésére
            if (!person.IsAlive) return;

            // Letároljuk az életkort, hogy ne kelljen mindenhol újraszámolni
            byte age = (byte)(year - person.BirthYear);

            // Halál kezelése
            // Halálozási valószínűség kikeresése
            double pDeath = (from x in DeathProbabilities
                             where x.Gender == person.Gender && x.Age == age
                             select x.dProbability).FirstOrDefault();
            // Meghal a személy?
            if (rng.NextDouble() <= pDeath)
                person.IsAlive = false;

            //Születés kezelése - csak az élő nők szülnek
            if (person.IsAlive && person.Gender == Gender.Female)
            {
                //Szülési valószínűség kikeresése
                double pBirth = (from x in BirthProbabilities
                                 where x.Age == age
                                 select x.bProbability).FirstOrDefault();
                //Születik gyermek?
                if (rng.NextDouble() <= pBirth)
                {
                    Person újszülött = new Person();
                    újszülött.BirthYear = year;
                    újszülött.NbrOfChildren = 0;
                    újszülött.Gender = (Gender)(rng.Next(1, 3));
                    Population.Add(újszülött);
                }
            }
        }

        public void Szimulacio() 
        {
            // Végigmegyünk a vizsgált éveken
            for (int year = 2005; year <= numericUpDown1.Value; year++)
            {
                // Végigmegyünk az összes személyen
                for (int i = 0; i < Population.Count; i++)
                {
                    SimStep(year, Population[i] );
                }

                int nbrOfMales = (from x in Population
                                  where x.Gender == Gender.Male && x.IsAlive
                                  select x).Count();
                NbrOfMales.Add(nbrOfMales);
                int nbrOfFemales = (from x in Population
                                    where x.Gender == Gender.Female && x.IsAlive
                                    select x).Count();
                NbrOfFemales.Add(nbrOfFemales);
                Console.WriteLine(
                    string.Format("Év:{0} Fiúk:{1} Lányok:{2}", year, nbrOfMales, nbrOfFemales));
            }


        }

        private void Startbtn_Click(object sender, EventArgs e)
        {
            Population = GetPopulation(string.Format(@"{0}", textBox1.Text));
            NbrOfFemales.Clear();
            NbrOfMales.Clear();
            richTextBox1.Clear();
            Szimulacio();
            DisplayResults();
        }

        private void DisplayResults() 
        {
            for (int year = 2005; year <= numericUpDown1.Value; year++) 
            {
                richTextBox1.Text += "Szimulációs év: " + year +
                                 "\n\t Fiúk: " + NbrOfMales[year - 2005] +
                                 "\n\t Lányok: " + NbrOfFemales[year - 2005] + "\n\n";
            
            }
        
        
        
        }

        private void Browsebtn_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = @"C:\Users\pc\AppData\Local\Temp\nép-teszt.csv";
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Title = "Browse Files";
            //textBox1.Text = openFileDialog1.FileName;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                textBox1.Text = openFileDialog1.FileName;
            }

        }
    }
}
