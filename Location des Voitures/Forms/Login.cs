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
using System.Runtime.InteropServices;

namespace Location_des_Voitures.Forms
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeft, int nTop, int nRight, int nBottom, int nWidthEllipse, int nHeightEllipse);
        private void btnLogIn_Click(object sender, EventArgs e)
        {
            try
            {
                if (GLB.Connection.State == ConnectionState.Open)
                    GLB.Connection.Close();
                GLB.Connection.Open();
                GLB.Cmd = new SqlCommand($"select COUNT(*) from Log_in where username = '{txtuser.Text.Trim()}' and pass_word = '{txtpass.Text.Trim()}'", GLB.Connection);
                int nb = int.Parse(GLB.Cmd.ExecuteScalar().ToString());
                if (nb > 0)
                {
                    FormMainMenu frm = new FormMainMenu();
                    frm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Le nom d'utilisateur ou mot de pass est incorrect","Message",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                GLB.Connection.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            txtpass.Clear();
            txtuser.Clear();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Voulez vous vraiment Quitter ?", "Confirmation", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if(res == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 20, 20));
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtpass.UseSystemPasswordChar = true;
            }
            else
            {
                txtpass.UseSystemPasswordChar = false;
            }
        }
    }
}
