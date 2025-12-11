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
using System.Security.Cryptography;

namespace Agenda
{
    public partial class Form1 : Form
    {
        SqlConnection cn;

        public Form1()
        {
            InitializeComponent();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(TextBox1.Text) || string.IsNullOrWhiteSpace(TextBox2.Text))
                {
                    MessageBox.Show("Preencha username ou password");
                    return;
                }
                cn = getSGBDConnection();
                if (!verifySGBDConnection())
                    return;

                User u = new User();
                u.Usr = TextBox1.Text;
                u.Pass = TextBox2.Text;
                SqlCommand cmd = new SqlCommand("agenda.autorization", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = TextBox1.Text;
                cmd.Parameters.Add("@pas", SqlDbType.VarChar).Value = CalculateHash(TextBox2.Text);
                cmd.ExecuteNonQuery();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["tipo"].ToString() == "mandachuva")
                    {
                        u.UsrType = "mandachuva";
                    }
                    else if (reader["tipo"].ToString() == "normal")
                    {
                        u.UsrType = "normal";
                    }
                    else
                    {
                        MessageBox.Show("Username ou Password incorretas!");
                        return;    
                    }
                }
                reader.Close();
                cn.Close();
                if (u.UsrType != null)
                {
                    Form4 turmas = new Form4(u);
                    turmas.Closed += (s, args) => this.Close();
                    turmas.Show();
                    this.Hide();
                }
                else {
                    MessageBox.Show("Username ou Password incorretas!");
                    return;
                }
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
        private static string CalculateHash(string input)
        {
            //Convert the input to a byte array using specified encoding
            var InputBuffer = Encoding.Unicode.GetBytes(input);
            //Hash the input
            byte[] HashedBytes;
            using (var Hasher = new SHA256Managed())
            {
                HashedBytes = Hasher.ComputeHash(InputBuffer);
            }
            //Return
            return BitConverter.ToString(HashedBytes).Replace("-", string.Empty);
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
    }
}
