using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agenda
{
    public partial class Form5 : Form
    {
        SqlConnection cn;
        private User u;
        private Aluno a = new Aluno();
        public Form5(string nome, string nasc, User usr)
        {
            InitializeComponent();
            u = usr;
            a.Name = nome;
            a.Nasc = nasc;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 turmas = new Form4(u);
            turmas.Show();
            this.Close();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;

            cn = getSGBDConnection();
            if (!verifySGBDConnection())
                return;
            textBox8.Text = a.Name;
            DateTime nasc = DateTime.Parse(a.Nasc);
            DateTimePicker1.Value = nasc;
            SqlCommand cmd = new SqlCommand("agenda.student_related", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = a.Name;
            cmd.Parameters.Add("@birthday", SqlDbType.Date).Value = a.Nasc;
            cmd.ExecuteNonQuery();
            List<ListViewItem> list = new List<ListViewItem>();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ListViewItem item = new ListViewItem(reader["adulto"].ToString());
                item.SubItems.Add(reader["phone"].ToString());
                SqlCommand cmd2 = new SqlCommand("select agenda.adulto_pl(@nome,@nasc,@phone)", cn);
                cmd2.Parameters.Add("@nome", SqlDbType.VarChar).Value = a.Name;
                cmd2.Parameters.Add("@nasc", SqlDbType.Date).Value = a.Nasc;
                cmd2.Parameters.Add("@phone", SqlDbType.Decimal).Value = reader["phone"].ToString();
                int res = (int) cmd2.ExecuteScalar();
                if(res == 1) { item.SubItems.Add("sim"); } else { item.SubItems.Add("não"); }
                cmd2 = new SqlCommand("select agenda.adulto_p(@nome,@nasc,@phone)", cn);
                cmd2.Parameters.Add("@nome", SqlDbType.VarChar).Value = a.Name;
                cmd2.Parameters.Add("@nasc", SqlDbType.Date).Value = a.Nasc;
                cmd2.Parameters.Add("@phone", SqlDbType.Decimal).Value = reader["phone"].ToString();
                string res2 = (string)cmd2.ExecuteScalar();
                if (res2 != null) { item.SubItems.Add(res2); }
                list.Add(item);
            }
            listView1.Items.AddRange(list.ToArray());
            reader.Close();

            cmd = new SqlCommand("agenda.info_aluno", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = a.Name;
            cmd.Parameters.Add("@nasc", SqlDbType.Date).Value = a.Nasc;
            cmd.ExecuteNonQuery(); 
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                textBox1.Text = reader["problemas"].ToString();
                textBox2.Text = reader["restricao"].ToString();
                textBox3.Text = reader["medicacao"].ToString();
                textBox4.Text = reader["antipiretico"].ToString();
                textBox7.Text = reader["morada"].ToString();
            }
            reader.Close();

            cmd = new SqlCommand("agenda.aluno_encarregado", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = a.Name;
            cmd.Parameters.Add("@nasc", SqlDbType.Date).Value = a.Nasc;
            cmd.ExecuteNonQuery();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                textBox5.Text = reader["nome"].ToString();
                textBox6.Text = reader["phone"].ToString();
            }
            reader.Close();

            cn.Close();
        }

        

        private SqlConnection getSGBDConnection()
        {
            //return new SqlConnection("data source= DESKTOP-J3KGJ50;integrated security=true;initial catalog=Agenda;MultipleActiveResultSets=true;");
            return new SqlConnection("data source=tcp:mednat.ieeta.pt\\SQLSERVER,8101;Initial Catalog=p3g2;User ID=p3g2;Password=mukua-limao2;MultipleActiveResultSets=true;");
        }

        private bool verifySGBDConnection()
        {
            if (cn == null)
                cn = getSGBDConnection();

            if (cn.State != ConnectionState.Open)
                cn.Open();

            return cn.State == ConnectionState.Open;
        }
    }
}
