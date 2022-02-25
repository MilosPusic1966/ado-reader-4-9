using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ado_reader_4_9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public string sredi(string tekst, int duzina)
        {
            string pom;
            pom = "             " + tekst;
            return pom.Substring(pom.Length - duzina, duzina);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection veza = new SqlConnection("Data source=INF_4_PROFESOR\\SQLPBG; Initial catalog=MilosP2021; Integrated security=true");
            SqlCommand komanda = new SqlCommand("SELECT * FROM promet", veza);
            veza.Open();
            SqlDataReader citac = komanda.ExecuteReader();
            double zbir = 25.50;
            int zbir2 = 345;
            string test2 = zbir2.ToString("D8");
            listBox1.Items.Add(test2);
            string test = String.Format("ovo je primer {1:F3} teksta", zbir, zbir+5);
            listBox1.Items.Add(test);
            listBox1.Items.Add("      Ulaz     Izlaz    Stanje          Povod");
            listBox1.Items.Add("----------------------------------------------");
            while (citac.Read())
            {
                double ulaz, izlaz;
                
                if (citac.IsDBNull(1)) ulaz = 0;
                else ulaz = (double)citac["ulaz"];
                if (citac.IsDBNull(2)) izlaz = 0;
                else izlaz = (double)citac["izlaz"];

                zbir = zbir + ulaz - izlaz;
                string red = sredi(citac["ulaz"].ToString(), 10) + sredi(citac["izlaz"].ToString(), 10);
                red = red + sredi(zbir.ToString(), 10) + sredi(citac["povod"].ToString(), 15);
                listBox1.Items.Add(red);
            }
            veza.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection veza = new SqlConnection("Data source=INF_4_PROFESOR\\SQLPBG; Initial catalog=MilosP2021; Integrated security=true");
            SqlCommand komanda = new SqlCommand("SELECT * FROM promet", veza);
            veza.Open();
            SqlDataReader citac = komanda.ExecuteReader();
            DataTable tabela = new DataTable();
            tabela.Load(citac);
            dataGridView1.DataSource = tabela;
        }
    }
}
