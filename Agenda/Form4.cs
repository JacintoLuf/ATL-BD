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

namespace Agenda
{
    public partial class Form4 : Form
    {
        SqlConnection cn;
        private User u;
        public object ListView1 { get; private set; }

        public Form4(User usr)
        {
            InitializeComponent();
            u = usr;
            if (u.UsrType == "normal") {
                button3.Visible = false;
                button5.Visible = false;
                button10.Visible = false;
                button11.Visible = false;
                button12.Visible = false;
                button14.Visible = false;
                button15.Visible = false;
                button16.Visible = false;
                button18.Visible = false;
                button19.Visible = false;
                button20.Visible = false;
                button21.Visible = false;
                button22.Visible = false;
                label1.Visible = false;
                label2.Visible = false;
                label6.Visible = false;
                label7.Visible = false;
                label8.Visible = false;
                label9.Visible = false;
                label11.Visible = false;
                label19.Visible = false;
                label20.Visible = false;
                label21.Visible = false;
                label22.Visible = false;
                textBox1.Visible = false;
                textBox2.Visible = false;
                textBox3.Visible = false;
                textBox4.Visible = false;
                textBox5.Visible = false;
                textBox6.Visible = false;
                textBox7.Visible = false;
                textBox8.Visible = false;
                textBox9.Visible = false;
                textBox10.Visible = false;
                dateTimePicker1.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cn = getSGBDConnection();
            if (!verifySGBDConnection())
                return;
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Selecione um ano");
                return;
            }

            listView1.Items.Clear();
            SqlCommand cmd = new SqlCommand("agenda.specific_class", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@degree", SqlDbType.Int).Value = 1;
            cmd.Parameters.Add("@year", SqlDbType.VarChar).Value = comboBox1.SelectedItem;
            cmd.ExecuteNonQuery();
            List<ListViewItem> list = new List<ListViewItem>();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ListViewItem item = new ListViewItem("1");
                item.SubItems.Add(reader["nome"].ToString());
                item.SubItems.Add(reader["nasc"].ToString().Substring(0, 10));
                list.Add(item);
            }
            reader.Close();
            cmd = new SqlCommand("agenda.specific_class", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@degree", SqlDbType.Int).Value = 2;
            cmd.Parameters.Add("@year", SqlDbType.VarChar).Value = comboBox1.SelectedItem;
            cmd.ExecuteNonQuery();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ListViewItem item = new ListViewItem("2");
                item.SubItems.Add(reader["nome"].ToString());
                item.SubItems.Add(reader["nasc"].ToString().Substring(0, 10));
                list.Add(item);
            }
            reader.Close();
            cmd = new SqlCommand("agenda.specific_class", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@degree", SqlDbType.Int).Value = 3;
            cmd.Parameters.Add("@year", SqlDbType.VarChar).Value = comboBox1.SelectedItem;
            cmd.ExecuteNonQuery();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ListViewItem item = new ListViewItem("3");
                item.SubItems.Add(reader["nome"].ToString());
                item.SubItems.Add(reader["nasc"].ToString().Substring(0, 10));
                list.Add(item);
            }
            reader.Close();
            cmd = new SqlCommand("agenda.specific_class", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@degree", SqlDbType.Int).Value = 4;
            cmd.Parameters.Add("@year", SqlDbType.VarChar).Value = comboBox1.SelectedItem;
            cmd.ExecuteNonQuery();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ListViewItem item = new ListViewItem("4");
                item.SubItems.Add(reader["nome"].ToString());
                item.SubItems.Add(reader["nasc"].ToString().Substring(0, 10));
                list.Add(item);
            }
            reader.Close();
            listView1.Items.AddRange(list.ToArray());

            cn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ListViewItem item;
            try
            {
                item = listView1.SelectedItems[0];
            }
            catch
            {
                MessageBox.Show("Escolha um aluno");
                return;
            }
            string nome = item.SubItems[1].Text;
            string nasc = item.SubItems[2].Text;
            Form5 detalhes = new Form5(nome, nasc, u);
            detalhes.ShowDialog();
            this.Show();
            
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

        private void Form4_Load(object sender, EventArgs e)
        {
            cn = getSGBDConnection();
            if (!verifySGBDConnection())
                return;

            comboBox1.Items.Clear();
            SqlCommand cmd = new SqlCommand("agenda.anos_letivos", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader["ano"].ToString());
            }
            reader.Close();

            var count = comboBox1.Items.Count;
            if (count > 0) { comboBox1.SelectedIndex = 0; }

            listView1.Items.Clear();
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;

            cmd = new SqlCommand("agenda.specific_class", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@degree", SqlDbType.Int).Value = 1;
            cmd.Parameters.Add("@year", SqlDbType.VarChar).Value = comboBox1.SelectedItem;
            cmd.ExecuteNonQuery();
            List<ListViewItem> list = new List<ListViewItem>();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ListViewItem item = new ListViewItem("1");
                item.SubItems.Add(reader["nome"].ToString());
                item.SubItems.Add(reader["nasc"].ToString().Substring(0, 10));
                list.Add(item);
            }
            reader.Close();
            cmd = new SqlCommand("agenda.specific_class", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@degree", SqlDbType.Int).Value = 2;
            cmd.Parameters.Add("@year", SqlDbType.VarChar).Value = comboBox1.SelectedItem;
            cmd.ExecuteNonQuery();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ListViewItem item = new ListViewItem("2");
                item.SubItems.Add(reader["nome"].ToString());
                item.SubItems.Add(reader["nasc"].ToString().Substring(0, 10));
                list.Add(item);
            }
            reader.Close();
            cmd = new SqlCommand("agenda.specific_class", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@degree", SqlDbType.Int).Value = 3;
            cmd.Parameters.Add("@year", SqlDbType.VarChar).Value = comboBox1.SelectedItem;
            cmd.ExecuteNonQuery();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ListViewItem item = new ListViewItem("3");
                item.SubItems.Add(reader["nome"].ToString());
                item.SubItems.Add(reader["nasc"].ToString().Substring(0, 10));
                list.Add(item);
            }
            reader.Close();
            cmd = new SqlCommand("agenda.specific_class", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@degree", SqlDbType.Int).Value = 4;
            cmd.Parameters.Add("@year", SqlDbType.VarChar).Value = comboBox1.SelectedItem;
            cmd.ExecuteNonQuery();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ListViewItem item = new ListViewItem("4");
                item.SubItems.Add(reader["nome"].ToString());
                item.SubItems.Add(reader["nasc"].ToString().Substring(0, 10));
                list.Add(item);
            }
            reader.Close();
            listView1.Items.AddRange(list.ToArray());
            
            //------------------------------tab visitas---------------------------------------

            listView2.Items.Clear();
            listView2.View = View.Details;
            listView2.FullRowSelect = true;
            listView2.GridLines = true;

            cmd = new SqlCommand("agenda.todos_alunos", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            List<ListViewItem> list2 = new List<ListViewItem>();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ListViewItem item2 = new ListViewItem(reader["id"].ToString());
                item2.SubItems.Add(reader["nome"].ToString());
                item2.SubItems.Add(reader["nasc"].ToString().Substring(0, 10));
                list2.Add(item2);
            }
            reader.Close();
            listView2.Items.AddRange(list2.ToArray());

            listView3.Items.Clear();
            listView3.View = View.Details;
            listView3.FullRowSelect = true;
            listView3.GridLines = true;

            cmd = new SqlCommand("agenda.visitas", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            List<ListViewItem> list3 = new List<ListViewItem>();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ListViewItem item3 = new ListViewItem(reader["nome"].ToString());
                item3.SubItems.Add(reader["data"].ToString().Substring(0, 10));
                item3.SubItems.Add(reader["partida"].ToString());
                item3.SubItems.Add(reader["chegada"].ToString());
                list3.Add(item3);
            }
            reader.Close();
            listView3.Items.AddRange(list3.ToArray());

            //------------------------------tab atividades------------------------------------

            listView4.Items.Clear();
            listView4.View = View.Details;
            listView4.FullRowSelect = true;
            listView4.GridLines = true;

            cmd = new SqlCommand("agenda.todos_alunos", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            List<ListViewItem> list4 = new List<ListViewItem>();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ListViewItem item4 = new ListViewItem(reader["id"].ToString());
                item4.SubItems.Add(reader["nome"].ToString());
                item4.SubItems.Add(reader["nasc"].ToString().Substring(0, 10));
                list4.Add(item4);
            }
            reader.Close();
            listView4.Items.AddRange(list4.ToArray());

            listView5.Items.Clear();
            listView5.View = View.Details;
            listView5.FullRowSelect = true;
            listView5.GridLines = true;

            cmd = new SqlCommand("agenda.profs_atividade", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            List<ListViewItem> list5 = new List<ListViewItem>();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ListViewItem item5 = new ListViewItem(reader["atividade"].ToString());
                item5.SubItems.Add(reader["prof"].ToString());
                item5.SubItems.Add(reader["email"].ToString());
                list5.Add(item5);
            }
            reader.Close();
            listView5.Items.AddRange(list5.ToArray());

            //------------------------------tab profs/turmas----------------------------------

            listView6.Items.Clear();
            listView6.View = View.Details;
            listView6.FullRowSelect = true;
            listView6.GridLines = true;

            cmd = new SqlCommand("agenda.todos_profs", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            List<ListViewItem> list6 = new List<ListViewItem>();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ListViewItem item6 = new ListViewItem(reader["id"].ToString());
                item6.SubItems.Add(reader["nome"].ToString());
                item6.SubItems.Add(reader["phone"].ToString());
                item6.SubItems.Add(reader["Email"].ToString());
                list6.Add(item6);
            }
            reader.Close();
            listView6.Items.AddRange(list6.ToArray());

            listView7.Items.Clear();
            listView7.View = View.Details;
            listView7.FullRowSelect = true;
            listView7.GridLines = true;

            cmd = new SqlCommand("agenda.prof_turmas", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            List<ListViewItem> list7 = new List<ListViewItem>();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ListViewItem item7 = new ListViewItem(reader["class_id"].ToString());
                item7.SubItems.Add(reader["grau"].ToString());
                item7.SubItems.Add(reader["ano"].ToString());
                item7.SubItems.Add(reader["prof_id"].ToString());
                item7.SubItems.Add(reader["nome"].ToString());
                list7.Add(item7);
            }
            reader.Close();
            listView7.Items.AddRange(list7.ToArray());

            listView8.Items.Clear();
            listView8.View = View.Details;
            listView8.FullRowSelect = true;
            listView8.GridLines = true;

            cmd = new SqlCommand("agenda.profs_atividade", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            List<ListViewItem> list8 = new List<ListViewItem>();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ListViewItem item8 = new ListViewItem(reader["atividade"].ToString());
                item8.SubItems.Add(reader["prof"].ToString());
                item8.SubItems.Add(reader["email"].ToString());
                list8.Add(item8);
            }
            reader.Close();
            listView8.Items.AddRange(list8.ToArray());

            //------------------------------tab alunos/adultos--------------------------------

            listView9.Items.Clear();
            listView9.View = View.Details;
            listView9.FullRowSelect = true;
            listView9.GridLines = true;

            cmd = new SqlCommand("agenda.todos_alunos", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            List<ListViewItem> list9 = new List<ListViewItem>();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ListViewItem item9 = new ListViewItem(reader["id"].ToString());
                item9.SubItems.Add(reader["nome"].ToString());
                item9.SubItems.Add(reader["nasc"].ToString().Substring(0, 10));
                list9.Add(item9);
            }
            reader.Close();
            listView9.Items.AddRange(list9.ToArray());

            listView10.Items.Clear();
            listView10.View = View.Details;
            listView10.FullRowSelect = true;
            listView10.GridLines = true;

            cmd = new SqlCommand("agenda.prof_turmas", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            List<ListViewItem> list10 = new List<ListViewItem>();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ListViewItem item10 = new ListViewItem(reader["class_id"].ToString());
                item10.SubItems.Add(reader["grau"].ToString());
                item10.SubItems.Add(reader["ano"].ToString());
                item10.SubItems.Add(reader["prof_id"].ToString());
                item10.SubItems.Add(reader["nome"].ToString());
                list10.Add(item10);
            }
            reader.Close();
            listView10.Items.AddRange(list10.ToArray());

            cn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ListViewItem item;
            try
            {
                item = listView1.SelectedItems[0];
            }
            catch
            {
                MessageBox.Show("Escolha um aluno");
                return;
            }
            string nome = item.SubItems[1].Text;
            string nasc = item.SubItems[2].Text;
            Form2 editar = new Form2(nome, nasc);
            editar.Closed += (s, args) => this.Show();
            editar.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 login = new Form1();
            login.Show();
            this.Hide();
        }
      

        private void listView6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                cn = getSGBDConnection();
                if (!verifySGBDConnection())
                    return;
                ListViewItem item;
                try
                {
                    item = listView2.SelectedItems[0];
                }
                catch
                {
                    MessageBox.Show("Escolha um aluno");
                    return;
                }
                string nome = item.SubItems[1].Text;
                string nasc = item.SubItems[2].Text;
                try
                {
                    item = listView3.SelectedItems[0];
                }
                catch
                {
                    MessageBox.Show("Escolha uma visita");
                    return;
                }
                string data = item.SubItems[1].Text;
                SqlCommand cmd = new SqlCommand("agenda.insert_vai", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = nome;
                cmd.Parameters.Add("@nasc", SqlDbType.Date).Value = nasc;
                cmd.Parameters.Add("@visita", SqlDbType.Date).Value = data;
                cmd.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Adicionado com sucesso");
            }
            catch (SqlException ex)
            {
                StringBuilder errorMessages = new StringBuilder();
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                        "Message: " + ex.Errors[i].Message);
                }
                MessageBox.Show(errorMessages.ToString());
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(textBox3.Text) || string.IsNullOrWhiteSpace(textBox4.Text) 
                || string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("preencha todos os campos!");
                return;
            }
            try
            {
                cn = getSGBDConnection();
                if (!verifySGBDConnection())
                    return;
                
                SqlCommand cmd = new SqlCommand("agenda.insert_visita", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = textBox3.Text;
                cmd.Parameters.Add("@data", SqlDbType.Date).Value = dateTimePicker1.Value.ToString().Substring(0, 10);
                cmd.Parameters.Add("@partida", SqlDbType.Time).Value = DateTime.Parse(textBox4.Text).TimeOfDay;
                cmd.Parameters.Add("@chegada", SqlDbType.Time).Value = DateTime.Parse(textBox5.Text).TimeOfDay;
                cmd.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Adicionado com sucesso");
                Form4_Load(sender, e);
            }
            catch (FormatException)
            {
                MessageBox.Show("Formato partida/chagada: HH:MM:SS");
            }
            catch (SqlException ex)
            {
                StringBuilder errorMessages = new StringBuilder();
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                        "Message: " + ex.Errors[i].Message);
                }
                MessageBox.Show(errorMessages.ToString());
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                cn = getSGBDConnection();
                if (!verifySGBDConnection())
                    return;
                ListViewItem item;
                try
                {
                    item = listView4.SelectedItems[0];
                }
                catch
                {
                    MessageBox.Show("Escolha um aluno");
                    return;
                }
                string nome = item.SubItems[1].Text;
                string nasc = item.SubItems[2].Text;
                try
                {
                    item = listView5.SelectedItems[0];
                }
                catch
                {
                    MessageBox.Show("Escolha uma atividade");
                    return;
                }
                string atividade = item.SubItems[0].Text;
                SqlCommand cmd = new SqlCommand("agenda.delete_aluno_atividade", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = nome;
                cmd.Parameters.Add("@nasc", SqlDbType.Date).Value = nasc;
                cmd.Parameters.Add("@atividade", SqlDbType.VarChar).Value = atividade;
                cmd.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("removido com sucesso");
            }
            catch (SqlException ex)
            {
                StringBuilder errorMessages = new StringBuilder();
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                        "Message: " + ex.Errors[i].Message);
                }
                MessageBox.Show(errorMessages.ToString());
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                cn = getSGBDConnection();
                if (!verifySGBDConnection())
                    return;
                ListViewItem item;
                try
                {
                    item = listView4.SelectedItems[0];
                }
                catch
                {
                    MessageBox.Show("Escolha um aluno");
                    return;
                }
                string nome = item.SubItems[1].Text;
                string nasc = item.SubItems[2].Text;
                try
                {
                    item = listView5.SelectedItems[0];
                }
                catch
                {
                    MessageBox.Show("Escolha uma atividade");
                    return;
                }
                string atividade = item.SubItems[0].Text;
                SqlCommand cmd = new SqlCommand("agenda.insert_tem", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = nome;
                cmd.Parameters.Add("@nasc", SqlDbType.Date).Value = nasc;
                cmd.Parameters.Add("@atividade", SqlDbType.VarChar).Value = atividade;
                cmd.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Adicionado com sucesso");
            }
            catch (SqlException ex)
            {
                StringBuilder errorMessages = new StringBuilder();
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                        "Message: " + ex.Errors[i].Message);
                }
                MessageBox.Show(errorMessages.ToString());
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox6.Text))
            {
                MessageBox.Show("preencha o nome da atividade");
                return;
            }
            try
            {
                cn = getSGBDConnection();
                if (!verifySGBDConnection())
                    return;

                SqlCommand cmd = new SqlCommand("agenda.insert_atividade", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = textBox6.Text;
                cmd.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Adicionado com sucesso");
                Form4_Load(sender, e);
            }
            catch (SqlException ex)
            {
                StringBuilder errorMessages = new StringBuilder();
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                        "Message: " + ex.Errors[i].Message);
                }
                MessageBox.Show(errorMessages.ToString());
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            cn = getSGBDConnection();
            if (!verifySGBDConnection())
                return;
            ListViewItem item;
            try
            {
                item = listView4.SelectedItems[0];
            }
            catch
            {
                MessageBox.Show("Escolha um aluno");
                return;
            }
            string nome = item.SubItems[1].Text;
            string nasc = item.SubItems[2].Text;
            SqlCommand cmd = new SqlCommand("agenda.student_atividade", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = nome;
            cmd.Parameters.Add("@nasc", SqlDbType.Date).Value = nasc;
            cmd.ExecuteNonQuery();
            string str = "";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                str += reader["nome"].ToString()+"\n";
            }
            reader.Close();
            cn.Close();
            MessageBox.Show("Atividades:\n" + str);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            cn = getSGBDConnection();
            if (!verifySGBDConnection())
                return;
            ListViewItem item;
            try
            {
                item = listView2.SelectedItems[0];
            }
            catch
            {
                MessageBox.Show("Escolha um aluno");
                return;
            }
            string nome = item.SubItems[1].Text;
            string nasc = item.SubItems[2].Text;
            SqlCommand cmd = new SqlCommand("agenda.student_visita", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = nome;
            cmd.Parameters.Add("@nasc", SqlDbType.Date).Value = nasc;
            cmd.ExecuteNonQuery();
            string str = "";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                str += reader["nome"].ToString() + "\t" + reader["data"].ToString().Substring(0,10) + "\n";
            }
            reader.Close();
            cn.Close();
            MessageBox.Show("Visitas:\n" + str);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                cn = getSGBDConnection();
                if (!verifySGBDConnection())
                    return;
                ListViewItem item;
                try
                {
                    item = listView6.SelectedItems[0];
                }
                catch
                {
                    MessageBox.Show("Escolha um professor");
                    return;
                }
                string mail = item.SubItems[3].Text;
                SqlCommand cmd = new SqlCommand("agenda.insert_turma", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ano", SqlDbType.VarChar).Value = textBox1.Text;
                cmd.Parameters.Add("@grau", SqlDbType.Int).Value = textBox2.Text;
                cmd.Parameters.Add("@prof_mail", SqlDbType.VarChar).Value = mail;
                cmd.ExecuteNonQuery();
                Form4_Load(sender, e);
            }
            catch (FormatException)
            {
                MessageBox.Show("Formato ano: yyyy/yyyy\nFormato grau: numero do ano da turma");
            }
            catch (SqlException ex)
            {
                StringBuilder errorMessages = new StringBuilder();
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                        "Message: " + ex.Errors[i].Message);
                }
                MessageBox.Show(errorMessages.ToString());
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                cn = getSGBDConnection();
                if (!verifySGBDConnection())
                    return;
                ListViewItem item;
                try
                {
                    item = listView6.SelectedItems[0];
                }
                catch
                {
                    MessageBox.Show("Escolha um professor");
                    return;
                }
                string mail = item.SubItems[3].Text;
                try
                {
                    item = listView7.SelectedItems[0];
                }
                catch
                {
                    MessageBox.Show("Escolha uma turma");
                    return;
                }
                string ano = item.SubItems[2].Text;
                string grau = item.SubItems[1].Text;
                SqlCommand cmd = new SqlCommand("agenda.insert_turma", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ano", SqlDbType.VarChar).Value = ano;
                cmd.Parameters.Add("@grau", SqlDbType.Int).Value = grau;
                cmd.Parameters.Add("@prof_mail", SqlDbType.VarChar).Value = mail;
                cmd.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Troca bem sucedida");
                Form4_Load(sender, e);
            }
            catch (SqlException ex)
            {
                StringBuilder errorMessages = new StringBuilder();
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                        "Message: " + ex.Errors[i].Message);
                }
                MessageBox.Show(errorMessages.ToString());
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                cn = getSGBDConnection();
                if (!verifySGBDConnection())
                    return;
                ListViewItem item;
                try
                {
                    item = listView6.SelectedItems[0];
                }
                catch
                {
                    MessageBox.Show("Escolha um professor");
                    return;
                }
                string mail = item.SubItems[3].Text;
                try
                {
                    item = listView8.SelectedItems[0];
                }
                catch
                {
                    MessageBox.Show("Escolha uma atividade");
                    return;
                }
                string nome = item.SubItems[0].Text;
                SqlCommand cmd = new SqlCommand("agenda.insert_orientado", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = nome;
                cmd.Parameters.Add("@prof_mail", SqlDbType.VarChar).Value = mail;
                cmd.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Troca bem sucedida");
                Form4_Load(sender, e);
            }
            catch (SqlException ex)
            {
                StringBuilder errorMessages = new StringBuilder();
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                        "Message: " + ex.Errors[i].Message);
                }
                MessageBox.Show(errorMessages.ToString());
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Form2 newAluno = new Form2();
            newAluno.ShowDialog();
            this.Show();
            Form4_Load(sender, e);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Form3 gerirAdulto = new Form3();
            gerirAdulto.ShowDialog();
            this.Show();
            Form4_Load(sender, e);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            ListViewItem item;
            try
            {
                item = listView9.SelectedItems[0];
            }
            catch
            {
                MessageBox.Show("Escolha um aluno");
                return;
            }
            string nome = item.SubItems[1].Text;
            string nasc = item.SubItems[2].Text;
            Form5 detalhes = new Form5(nome, nasc, u);
            detalhes.ShowDialog();
            this.Show();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            try
            {
                cn = getSGBDConnection();
                if (!verifySGBDConnection())
                    return;
                ListViewItem item;
                try
                {
                    item = listView9.SelectedItems[0];
                }
                catch
                {
                    MessageBox.Show("Escolha um aluno");
                    return;
                }
                string nome = item.SubItems[1].Text;
                string nasc = item.SubItems[2].Text;
                try
                {
                    item = listView10.SelectedItems[0];
                }
                catch
                {
                    MessageBox.Show("Escolha uma turma");
                    return;
                }
                string ano = item.SubItems[2].Text;
                string grau = item.SubItems[1].Text;
                SqlCommand cmd = new SqlCommand("agenda.insert_pertence_turma", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = nome;
                cmd.Parameters.Add("@nasc", SqlDbType.Date).Value = nasc;
                cmd.Parameters.Add("@ano", SqlDbType.VarChar).Value = ano;
                cmd.Parameters.Add("@grau", SqlDbType.Int).Value = grau;
                cmd.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Inserção/Troca bem sucedida");
                Form4_Load(sender, e);
            }
            catch (SqlException ex)
            {
                StringBuilder errorMessages = new StringBuilder();
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                        "Message: " + ex.Errors[i].Message);
                }
                MessageBox.Show(errorMessages.ToString());
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            try
            {
                cn = getSGBDConnection();
                if (!verifySGBDConnection())
                    return;
                SqlCommand cmd = new SqlCommand("agenda.insert_professor", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = textBox7.Text;
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = textBox8.Text;
                cmd.Parameters.Add("@phone", SqlDbType.Decimal).Value = textBox9.Text;
                cmd.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Inserção bem sucedida");
                Form4_Load(sender, e);
                textBox7.Clear();
                textBox8.Clear();
                textBox9.Clear();
            }
            catch (FormatException)
            {
                MessageBox.Show("Insira valores válidos");
            }
            catch (SqlException ex)
            {
                StringBuilder errorMessages = new StringBuilder();
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                        "Message: " + ex.Errors[i].Message);
                }
                MessageBox.Show(errorMessages.ToString());
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            try
            {
                cn = getSGBDConnection();
                if (!verifySGBDConnection())
                    return;
                ListViewItem item;
                try
                {
                    item = listView6.SelectedItems[0];
                }
                catch
                {
                    MessageBox.Show("Escolha um professor");
                    return;
                }
                string id = item.SubItems[0].Text;
                string nome = textBox7.Text;
                string email = textBox8.Text;
                string phone = textBox9.Text;
                SqlCommand cmd = new SqlCommand("agenda.update_professor", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = nome;
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = email;
                cmd.Parameters.Add("@phone", SqlDbType.Decimal).Value = phone;
                cmd.Parameters.Add("@ide", SqlDbType.Int).Value = id;
                cmd.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Atualização bem sucedida");
                Form4_Load(sender, e);
            }
            catch (FormatException)
            {
                MessageBox.Show("Insira valores válidos");
            }
            catch (SqlException ex)
            {
                StringBuilder errorMessages = new StringBuilder();
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                        "Message: " + ex.Errors[i].Message);
                }
                MessageBox.Show(errorMessages.ToString());
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            ListViewItem item;
            try
            {
                item = listView9.SelectedItems[0];
            }
            catch
            {
                MessageBox.Show("Escolha um aluno");
                return;
            }
            string nome = item.SubItems[1].Text;
            string nasc = item.SubItems[2].Text;
            try
            {
                cn = getSGBDConnection();
                if (!verifySGBDConnection())
                    return;
                SqlCommand cmd = new SqlCommand("agenda.delete_aluno", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = nome;
                cmd.Parameters.Add("@nasc", SqlDbType.Date).Value = nasc;
                cmd.ExecuteNonQuery();
                cn.Close();
                Form4_Load(sender, e);

            }
            catch (SqlException ex)
            {
                StringBuilder errorMessages = new StringBuilder();
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                        "Message: " + ex.Errors[i].Message);
                }
                MessageBox.Show(errorMessages.ToString());
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            try
            {
                int right = Int32.Parse(textBox10.Text.Split('/')[1]);
                int left = Int32.Parse(textBox10.Text.Split('/')[0]);
                if (right - left != 1)
                {
                    MessageBox.Show("Insira um ano válido");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Insira no formato: yyyy/YYYY");
                return;
            }
            try
            {
                cn = getSGBDConnection();
                if (!verifySGBDConnection())
                    return;
                SqlCommand cmd = new SqlCommand("agenda.insert_ano", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ano", SqlDbType.VarChar).Value = textBox10.Text;
                cmd.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Inserido com sucesso!");
                Form4_Load(sender, e);
            }
            catch (SqlException ex)
            {
                StringBuilder errorMessages = new StringBuilder();
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                        "Message: " + ex.Errors[i].Message);
                }
                MessageBox.Show(errorMessages.ToString());
            }
        }
    }
}
