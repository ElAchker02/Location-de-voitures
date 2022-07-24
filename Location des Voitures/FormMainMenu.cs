using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Location_des_Voitures
{
    public partial class FormMainMenu : Form
    {

        private Button currentButton;
        private Random random;
        private int tempIndex;
        private Form activeForm;
        public FormMainMenu()
        {
            InitializeComponent();
            random = new Random();
            btnCloseChildForm.Visible = false;
            this.Text = string.Empty;
            this.ControlBox = false;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }
        
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private Color SelectThemeColor()
        {
            // This Methode return a random color from ColorList in every call.
            int index = random.Next(ThemeColor.ColorList.Count);
            while (tempIndex == index)
            {
                index = random.Next(ThemeColor.ColorList.Count);
            }
            tempIndex = index;
            string color = ThemeColor.ColorList[index];
            return ColorTranslator.FromHtml(color);
        }    
        private void ActivateButton(object btnSender)
        {
            //This methode change the parameters of the button when we click it.
            if(btnSender != null)
            {
                if(currentButton != (Button)btnSender)
                {
                    DisableButton();
                    Color color = SelectThemeColor();
                    currentButton = (Button)btnSender;
                    currentButton.BackColor = color;
                    currentButton.ForeColor = Color.White;
                    //By activiting / highlighting a button , we increase the size of the font zoom effect .
                    currentButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    panelTitleBar.BackColor = color;
                    panelLogo.BackColor = ThemeColor.ChangeColorBrightness(color,-0.3);
                    ThemeColor.PrimaryColor = color;
                    ThemeColor.SecondaryColor = ThemeColor.ChangeColorBrightness(color, -0.3);
                    btnCloseChildForm.Visible = true;
                    

                }
            }
        }
       
        private void DisableButton()
        {
            //This Methode set the default settings of the button. 
            foreach (Control previousBtn in panelMenu.Controls)
            {
                if (previousBtn.GetType()==typeof(Button))
                {
                    previousBtn.BackColor = Color.FromArgb(51, 51, 76);
                    previousBtn.ForeColor = Color.Gainsboro;
                    previousBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }
        }
        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            ActivateButton(btnSender);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelDesktopPane.Controls.Add(childForm);
            this.panelDesktopPane.Tag = childForm;
            childForm.BringToFront();

            childForm.Show();
            lblTitle.Text = childForm.Text;

        }
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            //OpenChildForm(new FormMainMenu(), sender);

        }

        private void btnCars_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormVoitures(), sender);

        }

        private void btnReservations_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormReservations(), sender);


        }

        private void btnClients_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormClients(), sender);


        }

        private void btnMarques_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormMarques(), sender);


        }

        private void btnModeles_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormModeles(), sender);


        }

        private void btncarburants_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormCarburants(), sender);


        }

        private void btnCloseChildForm_Click(object sender, EventArgs e)
        {
            if (activeForm != null)
                activeForm.Close();
            Reset();
            refesh();
        }

        private void Reset()
        {
            DisableButton();
            lblTitle.Text = "Tableau de board";
            panelTitleBar.BackColor = Color.FromArgb(0, 150, 136);
            panelLogo.BackColor = Color.FromArgb(39, 39, 58);
            currentButton = null;
            btnCloseChildForm.Visible = false;
        }

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                this.WindowState = FormWindowState.Maximized;
            else
                this.WindowState = FormWindowState.Normal;
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void FormMainMenu_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            guna2GroupBox1.CustomBorderColor = Color.FromArgb(0, 150, 136);
            guna2GroupBox2.CustomBorderColor = Color.FromArgb(0, 150, 136);
            guna2GroupBox4.CustomBorderColor = Color.FromArgb(51, 51, 76);
            guna2GroupBox5.CustomBorderColor = Color.FromArgb(0, 150, 136);
            guna2GroupBox8.CustomBorderColor = Color.FromArgb(51, 51, 76);
            refesh();
        }

        

        private void btnRetournerLaVoiture_Click_1(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormRetour(), sender);
        }

        private void btnCategorie_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FormCategorie(), sender);
        }

        private void panelDesktopPane_Paint(object sender, PaintEventArgs e)
        {

        }
        private void refesh()
        {
            dgv_Voitures.Rows.Clear();
            dgvReservations.Rows.Clear();
            if (GLB.Connection.State == ConnectionState.Open)
                GLB.Connection.Close();
            GLB.Connection.Open();
            GLB.Cmd = new SqlCommand("select count(*) from Voitures", GLB.Connection);
            nbr_Voiture.Text = GLB.Cmd.ExecuteScalar().ToString();
            GLB.Cmd = new SqlCommand("select count(*) from Clients", GLB.Connection);
            nbr_CLient.Text = GLB.Cmd.ExecuteScalar().ToString();
            GLB.Cmd = new SqlCommand("select count(*) from Reservations", GLB.Connection);
            nbr_Reservation.Text = GLB.Cmd.ExecuteScalar().ToString();
            GLB.Cmd = new SqlCommand("select v.Matricule , m.Marque , md.Modele from Voitures v inner join Marques m on v.Marque = m.Id_Marque inner join Modeles md on v.Modele = md.Id_Model", GLB.Connection);
            GLB.dr = GLB.Cmd.ExecuteReader();
            while (GLB.dr.Read())
            {
                dgv_Voitures.Rows.Add(GLB.dr[0], GLB.dr[1], GLB.dr[2]);
            }
            GLB.dr.Close();
            GLB.Cmd = new SqlCommand("select * from Reservations where Km_Final!='' ", GLB.Connection);
            GLB.dr = GLB.Cmd.ExecuteReader();
            while (GLB.dr.Read())
            {
                dgvReservations.Rows.Add(GLB.dr[0], GLB.dr[1], GLB.dr[2], GLB.dr[3], GLB.dr[4], GLB.dr[5], GLB.dr[6], GLB.dr[7], GLB.dr[8], GLB.dr[9]);
            }
            GLB.dr.Close();
            GLB.Connection.Close();
        }

        private void btnrefesh_Click(object sender, EventArgs e)
        {
            refesh();
        }
    }
}
