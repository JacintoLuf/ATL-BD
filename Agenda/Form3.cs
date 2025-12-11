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
    public partial class Form3 : Form
    {
        private SqlConnection cn;

        public Form3()
        {
            InitializeComponent();
            this.Load += Form3_Load;
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            cn = getSGBDConnection();
            if (!verifySGBDConnection())
                return;
            listView1.Items.Clear();
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;

            SqlCommand cmd = new SqlCommand("agenda.todos_alunos", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
            List<ListViewItem> list1 = new List<ListViewItem>();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ListViewItem item = new ListViewItem(reader["id"].ToString());
                item.SubItems.Add(reader["nome"].ToString());
                item.SubItems.Add(reader["nasc"].ToString().Substring(0, 10));
                list1.Add(item);
            }
            reader.Close();
            listView1.Items.AddRange(list1.ToArray());

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

        private void Button1_Click(object sender, EventArgs e)
        {
            TextBox3.Clear();
            TextBox4.Clear();
            TextBox5.Clear();
            TextBox6.Clear();
            TextBox7.Clear();
            TextBox8.Clear();
            TextBox9.Clear();
            TextBox10.Clear();
            TextBox11.Clear();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewItem item;
            try
            {
                item = listView1.SelectedItems[0];
            }
            catch
            {
                return;
            }
            TextBox1.Text = item.SubItems[1].Text;
            TextBox2.Text = item.SubItems[2].Text;

            cn = getSGBDConnection();
            if (!verifySGBDConnection())
                return;

            listView2.Items.Clear();
            listView2.View = View.Details;
            listView2.FullRowSelect = true;
            listView2.GridLines = true;

            SqlCommand cmd = new SqlCommand("agenda.student_related", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = TextBox1.Text;
            cmd.Parameters.Add("@birthday", SqlDbType.Date).Value = TextBox2.Text;
            cmd.ExecuteNonQuery();
            List<ListViewItem> list = new List<ListViewItem>();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ListViewItem item2 = new ListViewItem(reader["adulto"].ToString());
                item2.SubItems.Add(reader["phone"].ToString());
                SqlCommand cmd2 = new SqlCommand("select agenda.adulto_pl(@nome,@nasc,@phone)", cn);
                cmd2.Parameters.Add("@nome", SqlDbType.VarChar).Value = TextBox1.Text;
                cmd2.Parameters.Add("@nasc", SqlDbType.Date).Value = TextBox2.Text;
                cmd2.Parameters.Add("@phone", SqlDbType.Decimal).Value = reader["phone"].ToString();
                int res = (int)cmd2.ExecuteScalar();
                if (res == 1) { item2.SubItems.Add("sim"); } else { item.SubItems.Add("não"); }
                cmd2 = new SqlCommand("select agenda.adulto_p(@nome,@nasc,@phone)", cn);
                cmd2.Parameters.Add("@nome", SqlDbType.VarChar).Value = TextBox1.Text;
                cmd2.Parameters.Add("@nasc", SqlDbType.Date).Value = TextBox2.Text;
                cmd2.Parameters.Add("@phone", SqlDbType.Decimal).Value = reader["phone"].ToString();
                try
                {
                    string res2 = (string)cmd2.ExecuteScalar();
                    if (res2 != null) { item2.SubItems.Add(res2); }
                }
                catch { }
                list.Add(item2);
            }
            listView2.Items.AddRange(list.ToArray());
            reader.Close();
            cn.Close();
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Button1_Click(sender, e);
            ListViewItem item;
            try
            {
                item = listView2.SelectedItems[0];
            }
            catch
            {
                return;
            }
            string phone = item.SubItems[1].Text;
            string nome = TextBox1.Text;
            string nasc = TextBox2.Text;
            cn = getSGBDConnection();
            if (!verifySGBDConnection())
                return;
            SqlCommand cmd = new SqlCommand("agenda.info_adulto", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@phone", SqlDbType.Decimal).Value = phone;
            cmd.ExecuteNonQuery();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                TextBox3.Text = reader["nome"].ToString();
                TextBox4.Text = reader["morada"].ToString();
                TextBox5.Text = reader["phone"].ToString();
                TextBox6.Text = reader["hphone"].ToString();
                TextBox7.Text = reader["wphone"].ToString();
                TextBox8.Text = reader["email"].ToString();
                TextBox9.Text = reader["ltrabalho"].ToString();
                TextBox10.Text = reader["profissao"].ToString();
            }
            reader.Close();
            
            cmd = new SqlCommand("agenda.info_parente", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = nome;
            cmd.Parameters.Add("@nasc", SqlDbType.Date).Value = nasc;
            cmd.Parameters.Add("@phone", SqlDbType.Decimal).Value = phone;
            cmd.ExecuteNonQuery();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                TextBox11.Text = reader["parentesco"].ToString();
            }
            reader.Close();

            cmd = new SqlCommand("select agenda.adulto_e(@nome,@nasc,@phone)", cn);
            cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = nome;
            cmd.Parameters.Add("@nasc", SqlDbType.Date).Value = nasc;
            cmd.Parameters.Add("@phone", SqlDbType.Decimal).Value = phone;
            int res2 = (int)cmd.ExecuteScalar();
            if (res2 == 1) { checkBox2.Checked = true; } else { checkBox2.Checked = false; }
            cn.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBox3.Text) || string.IsNullOrWhiteSpace(TextBox4.Text) || string.IsNullOrWhiteSpace(TextBox5.Text))
            {
                MessageBox.Show("Preencha os campos obrigatórios");
                return;
            }
            if(TextBox1.Text == null || TextBox2.Text == null)
            {
                MessageBox.Show("Escolha um aluno");
                return;
            }
            try
            {
                ListViewItem item;
                try
                {
                    item = listView2.SelectedItems[0];
                }
                catch
                {
                    MessageBox.Show("Escolha um adulto");
                    return;
                }
                string uphone = item.SubItems[1].Text;
                string nome = TextBox1.Text;
                string nasc = TextBox2.Text;

                cn = getSGBDConnection();
                if (!verifySGBDConnection())
                    return;

                SqlCommand cmd = new SqlCommand("agenda.update_adulto", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@uphone", SqlDbType.Decimal).Value = uphone;
                cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = TextBox3.Text;
                cmd.Parameters.Add("@morada", SqlDbType.VarChar).Value = TextBox4.Text;
                cmd.Parameters.Add("@phone", SqlDbType.Decimal).Value = TextBox5.Text;
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = TextBox8.Text;
                cmd.Parameters.Add("@ltrabalho", SqlDbType.VarChar).Value = TextBox9.Text;
                cmd.Parameters.Add("@profissao", SqlDbType.VarChar).Value = TextBox10.Text;
                if (!string.IsNullOrWhiteSpace(TextBox6.Text))
                {
                    cmd.Parameters.Add("@hphone", SqlDbType.Decimal).Value = TextBox6.Text;
                }
                if (!string.IsNullOrWhiteSpace(TextBox7.Text))
                {
                    cmd.Parameters.Add("@wphone", SqlDbType.Decimal).Value = TextBox7.Text;
                }
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("select agenda.adulto_p(@nome,@nasc,@phone)", cn);
                cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = TextBox1.Text;
                cmd.Parameters.Add("@nasc", SqlDbType.Date).Value = TextBox2.Text;
                cmd.Parameters.Add("@phone", SqlDbType.Decimal).Value = uphone;
                string res2 = null;
                try { 
                    res2 = (string)cmd.ExecuteScalar();
                }
                catch { }
                if (res2 != null)
                {
                    if (string.IsNullOrWhiteSpace(TextBox11.Text))
                    {
                        cmd = new SqlCommand("agenda.delete_parente", cn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = TextBox1.Text;
                        cmd.Parameters.Add("@nasc", SqlDbType.Date).Value = TextBox2.Text;
                        cmd.Parameters.Add("@phone", SqlDbType.Decimal).Value = uphone;
                        cmd.ExecuteNonQuery();
                    }
                    if (!string.IsNullOrWhiteSpace(TextBox11.Text))
                    {
                        cmd = new SqlCommand("agenda.insert_adulto_parente", cn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = TextBox1.Text;
                        cmd.Parameters.Add("@nasc", SqlDbType.Date).Value = TextBox2.Text;
                        cmd.Parameters.Add("@phone", SqlDbType.Decimal).Value = TextBox5.Text;
                        cmd.Parameters.Add("@parente", SqlDbType.VarChar).Value = TextBox11.Text;
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(TextBox11.Text))
                    {
                        cmd = new SqlCommand("agenda.insert_adulto_parente", cn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = TextBox1.Text;
                        cmd.Parameters.Add("@nasc", SqlDbType.Date).Value = TextBox2.Text;
                        cmd.Parameters.Add("@phone", SqlDbType.Decimal).Value = TextBox5.Text;
                        cmd.Parameters.Add("@parente", SqlDbType.VarChar).Value = TextBox11.Text;
                        cmd.ExecuteNonQuery();
                    }
                }

                if (checkBox2.Checked == true)
                {
                    cmd = new SqlCommand("agenda.insert_adulto_encarregado", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = TextBox1.Text;
                    cmd.Parameters.Add("@nasc", SqlDbType.Date).Value = TextBox2.Text;
                    cmd.Parameters.Add("@phone", SqlDbType.Decimal).Value = TextBox5.Text;
                    cmd.ExecuteNonQuery();
                }
                cn.Close();
                MessageBox.Show("Atualizado com sucesso!");
                listView1_SelectedIndexChanged(sender, e);
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

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBox3.Text) || string.IsNullOrWhiteSpace(TextBox4.Text) || string.IsNullOrWhiteSpace(TextBox5.Text))
            {
                MessageBox.Show("Preencha os campos obrigatórios");
                return;
            }
            try
            {
                ListViewItem item;
                try
                {
                    item = listView1.SelectedItems[0];
                }
                catch
                {
                    return;
                }
                string nome = item.SubItems[1].Text;
                string nasc = item.SubItems[2].Text;

                cn = getSGBDConnection();
                if (!verifySGBDConnection())
                    return;

                SqlCommand cmd = new SqlCommand("agenda.insert_adulto", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = TextBox3.Text;
                cmd.Parameters.Add("@morada", SqlDbType.VarChar).Value = TextBox4.Text;
                cmd.Parameters.Add("@phone", SqlDbType.Decimal).Value = TextBox5.Text;
                if (!string.IsNullOrWhiteSpace(TextBox6.Text))
                {
                    cmd.Parameters.Add("@hphone", SqlDbType.Decimal).Value = TextBox6.Text;
                }
                if (!string.IsNullOrWhiteSpace(TextBox7.Text))
                {
                    cmd.Parameters.Add("@wphone", SqlDbType.Decimal).Value = TextBox7.Text;
                }
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = TextBox8.Text;
                cmd.Parameters.Add("@ltrabalho", SqlDbType.VarChar).Value = TextBox9.Text;
                cmd.Parameters.Add("@profissao", SqlDbType.VarChar).Value = TextBox10.Text;
                cmd.ExecuteNonQuery();

                if (TextBox11.Text != null)
                {
                    cmd = new SqlCommand("agenda.insert_adulto_parente", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = nome;
                    cmd.Parameters.Add("@nasc", SqlDbType.Date).Value = nasc;
                    cmd.Parameters.Add("@phone", SqlDbType.Decimal).Value = TextBox5.Text;
                    cmd.Parameters.Add("@parente", SqlDbType.VarChar).Value = TextBox11.Text;
                    cmd.ExecuteNonQuery();
                }
                cmd = new SqlCommand("agenda.insert_adulto_levantado", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = nome;
                cmd.Parameters.Add("@nasc", SqlDbType.Date).Value = nasc;
                cmd.Parameters.Add("@phone", SqlDbType.Decimal).Value = TextBox5.Text;
                cmd.ExecuteNonQuery();
                if (checkBox2.Checked == true)
                {
                    cmd = new SqlCommand("agenda.insert_adulto_encarregado", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = TextBox1.Text;
                    cmd.Parameters.Add("@nasc", SqlDbType.Date).Value = TextBox2.Text;
                    cmd.Parameters.Add("@phone", SqlDbType.Decimal).Value = TextBox5.Text;
                    cmd.ExecuteNonQuery();
                }
                cn.Close();
                MessageBox.Show("Inserido com sucesso!");
                listView1_SelectedIndexChanged(sender, e);
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
    }
}
