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
    public partial class Form2 : Form
    {
        SqlConnection cn;
        private Aluno a = new Aluno();
        private string uphone;
        public Form2()
        {
            InitializeComponent();
        }
        public Form2(string nome, string nasc)
        {
            InitializeComponent();
            a.Name = nome;
            a.Nasc = nasc;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if(a.Name != null && a.Nasc != null)
            {
                cn = getSGBDConnection();
                if (!verifySGBDConnection())
                    return;

                TextBox1.Text = a.Name;
                DateTime nasc = DateTime.Parse(a.Nasc);
                DateTimePicker1.Value = nasc;
                SqlCommand cmd = new SqlCommand("agenda.info_aluno", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = a.Name;
                cmd.Parameters.Add("@nasc", SqlDbType.Date).Value = a.Nasc;
                cmd.ExecuteNonQuery();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TextBox2.Text = reader["morada"].ToString();
                    TextBox3.Text = reader["problemas"].ToString();
                    TextBox4.Text = reader["restricao"].ToString();
                    TextBox5.Text = reader["medicacao"].ToString();
                    TextBox6.Text = reader["antipiretico"].ToString();
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
                    TextBox7.Text = reader["nome"].ToString();
                    TextBox9.Text = reader["phone"].ToString();
                    uphone = reader["phone"].ToString();
                }
                reader.Close();
                cmd = new SqlCommand("agenda.info_adulto", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@phone", SqlDbType.Decimal).Value = uphone;
                cmd.ExecuteNonQuery();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TextBox8.Text = reader["morada"].ToString();
                    TextBox10.Text = reader["hphone"].ToString();
                    TextBox11.Text = reader["wphone"].ToString();
                    TextBox12.Text = reader["email"].ToString();
                    TextBox13.Text = reader["ltrabalho"].ToString();
                    TextBox14.Text = reader["profissao"].ToString();
                }
                reader.Close();

                cmd = new SqlCommand("agenda.info_parente", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = a.Name;
                cmd.Parameters.Add("@nasc", SqlDbType.Date).Value = a.Nasc;
                cmd.Parameters.Add("@phone", SqlDbType.Decimal).Value = uphone;
                cmd.ExecuteNonQuery();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TextBox15.Text = reader["parentesco"].ToString();
                }
                reader.Close();

                cn.Close();
            }
        }

        private SqlConnection getSGBDConnection()
        {
            //return new SqlConnection("data source= DESKTOP-J3KGJ50;integrated security=true;initial catalog=Agenda;");
            return new SqlConnection("data source=tcp:mednat.ieeta.pt\\SQLSERVER,8101;Initial Catalog=p3g2;User ID=p3g2;Password=mukua-limao2;");
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
            TextBox1.Clear();
            TextBox2.Clear();
            TextBox3.Clear();
            TextBox4.Clear();
            TextBox5.Clear();
            TextBox6.Clear();
            TextBox7.Clear();
            TextBox8.Clear();
            TextBox9.Clear();
            TextBox10.Clear();
            TextBox11.Clear();
            TextBox12.Clear();
            TextBox13.Clear();
            TextBox14.Clear();
            TextBox15.Clear();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBox7.Text) ||
                string.IsNullOrWhiteSpace(TextBox8.Text) ||
                string.IsNullOrWhiteSpace(TextBox9.Text) ||
                string.IsNullOrWhiteSpace(TextBox1.Text) ||
                string.IsNullOrWhiteSpace(TextBox2.Text))
            {
                MessageBox.Show("Preencha os campos obrigatórios");
                return;
            }
            try
            {
                cn = getSGBDConnection();
                if (!verifySGBDConnection())
                    return;

                SqlCommand cmd = new SqlCommand("agenda.insert_aluno", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = TextBox1.Text;
                cmd.Parameters.Add("@morada", SqlDbType.VarChar).Value = TextBox2.Text;
                cmd.Parameters.Add("@nasc", SqlDbType.Date).Value = DateTimePicker1.Value.ToString().Substring(0, 10);
                cmd.Parameters.Add("@problemas", SqlDbType.VarChar).Value = TextBox3.Text;
                cmd.Parameters.Add("@medicacao", SqlDbType.VarChar).Value = TextBox4.Text;
                cmd.Parameters.Add("@restricao", SqlDbType.VarChar).Value = TextBox5.Text;
                cmd.Parameters.Add("@antipiretico", SqlDbType.VarChar).Value = TextBox6.Text;
                cmd.ExecuteNonQuery();

                if (a.Name != null && a.Nasc != null)
                {
                    cmd = new SqlCommand("agenda.update_adulto", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@uphone", SqlDbType.Decimal).Value = uphone;
                    cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = TextBox7.Text;
                    cmd.Parameters.Add("@morada", SqlDbType.VarChar).Value = TextBox8.Text;
                    cmd.Parameters.Add("@phone", SqlDbType.Decimal).Value = TextBox9.Text;
                    if (!string.IsNullOrWhiteSpace(TextBox10.Text))
                    {
                        cmd.Parameters.Add("@hphone", SqlDbType.Decimal).Value = TextBox10.Text;
                    }
                    if (!string.IsNullOrWhiteSpace(TextBox11.Text))
                    {
                        cmd.Parameters.Add("@wphone", SqlDbType.Decimal).Value = TextBox11.Text;
                    }
                    cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = TextBox12.Text;
                    cmd.Parameters.Add("@ltrabalho", SqlDbType.VarChar).Value = TextBox13.Text;
                    cmd.Parameters.Add("@profissao", SqlDbType.VarChar).Value = TextBox14.Text;
                    cmd.ExecuteNonQuery();
                    
                    if (!string.IsNullOrWhiteSpace(TextBox15.Text))
                    {
                        cmd = new SqlCommand("agenda.insert_adulto_parente", cn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = TextBox1.Text;
                        cmd.Parameters.Add("@nasc", SqlDbType.Date).Value = DateTimePicker1.Value.ToString().Substring(0, 10);
                        cmd.Parameters.Add("@phone", SqlDbType.Decimal).Value = TextBox9.Text;
                        cmd.Parameters.Add("@parente", SqlDbType.VarChar).Value = TextBox15.Text;
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        cmd = new SqlCommand("agenda.delete_parente", cn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = TextBox1.Text;
                        cmd.Parameters.Add("@nasc", SqlDbType.Date).Value = DateTimePicker1.Value.ToString().Substring(0, 10);
                        cmd.Parameters.Add("@phone", SqlDbType.Decimal).Value = TextBox9.Text;
                    }
                }
                else
                {
                    cmd = new SqlCommand("agenda.insert_adulto", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = TextBox7.Text;
                    cmd.Parameters.Add("@morada", SqlDbType.VarChar).Value = TextBox8.Text;
                    cmd.Parameters.Add("@phone", SqlDbType.Decimal).Value = TextBox9.Text;
                    if (!string.IsNullOrWhiteSpace(TextBox10.Text))
                    {
                        cmd.Parameters.Add("@hphone", SqlDbType.Decimal).Value = TextBox10.Text;
                    }
                    if (!string.IsNullOrWhiteSpace(TextBox11.Text))
                    {
                        cmd.Parameters.Add("@wphone", SqlDbType.Decimal).Value = TextBox11.Text;
                    }
                    cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = TextBox12.Text;
                    cmd.Parameters.Add("@ltrabalho", SqlDbType.VarChar).Value = TextBox13.Text;
                    cmd.Parameters.Add("@profissao", SqlDbType.VarChar).Value = TextBox14.Text;
                    cmd.ExecuteNonQuery();
                
                    cmd = new SqlCommand("agenda.insert_adulto_levantado", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = TextBox1.Text;
                    cmd.Parameters.Add("@nasc", SqlDbType.Date).Value = DateTimePicker1.Value.ToString().Substring(0, 10);
                    cmd.Parameters.Add("@phone", SqlDbType.Decimal).Value = TextBox9.Text;
                    cmd.ExecuteNonQuery();

                    if (!string.IsNullOrWhiteSpace(TextBox15.Text))
                    {
                        cmd = new SqlCommand("agenda.insert_adulto_parente", cn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = TextBox1.Text;
                        cmd.Parameters.Add("@nasc", SqlDbType.Date).Value = DateTimePicker1.Value.ToString().Substring(0, 10);
                        cmd.Parameters.Add("@phone", SqlDbType.Decimal).Value = TextBox9.Text;
                        cmd.Parameters.Add("@parente", SqlDbType.VarChar).Value = TextBox15.Text;
                        cmd.ExecuteNonQuery();
                    }
                }

                cmd = new SqlCommand("agenda.insert_adulto_encarregado", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = TextBox1.Text;
                cmd.Parameters.Add("@nasc", SqlDbType.Date).Value = DateTimePicker1.Value.ToString().Substring(0, 10);
                cmd.Parameters.Add("@phone", SqlDbType.Decimal).Value = TextBox9.Text;
                cmd.ExecuteNonQuery();

                cn.Close();
                MessageBox.Show("Submetido com sucesso!");
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
