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

namespace Location_des_Voitures.Forms
{
    public partial class FormRetour : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeft, int nTop, int nRight, int nBottom, int nWidthEllipse, int nHeightEllipse);
        public FormRetour()
        {
            InitializeComponent();
        }
        private void LaodTheme()
        {
            btnRechercher.BackColor = ThemeColor.PrimaryColor;
            btnRechercher.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;
            btnRechercher.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnRechercher.Width, btnRechercher.Height, 20, 20));



            btnImprimer.BackColor = ThemeColor.PrimaryColor;
            btnImprimer.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;
            btnImprimer.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnImprimer.Width, btnImprimer.Height, 20, 20));

        }
        
        private void FormRetour_Load(object sender, EventArgs e)
        {
            
            
            LaodTheme();
        }

        private void btnRechercher_Click(object sender, EventArgs e)
        {
            dgvReservations.Rows.Clear();
            try
            {
                if (txtClient.Text != "" || txtMatricule.Text == "" )
                {
                    if (GLB.Connection.State == ConnectionState.Open)
                        GLB.Connection.Close();
                    GLB.Connection.Open();
                    GLB.Cmd = new SqlCommand($"select * from Reservations where Date_Debut = '{Datedebut.Value}' and Client= '{txtClient.Text}' and Matricule='{txtMatricule.Text}'", GLB.Connection);
                    GLB.dr = GLB.Cmd.ExecuteReader();

                    while (GLB.dr.Read())
                    {
                        dgvReservations.Rows.Add(GLB.dr[0], GLB.dr[1], GLB.dr[2], GLB.dr[3], GLB.dr[4], GLB.dr[5], GLB.dr[6], GLB.dr[7], GLB.dr[8], GLB.dr[9]);
                    }
                    GLB.dr.Close();
                    GLB.Connection.Close();
                }
                else
                {
                    MessageBox.Show("Saisie Tous les informations.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

            }
            catch (Exception )
            {

                MessageBox.Show("Quelque chose s'est mal passé", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            
        }

        private void dgvReservations_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Datedebut.Text = dgvReservations.CurrentRow.Cells[0].Value.ToString();
            txtClient.Text = dgvReservations.CurrentRow.Cells[2].Value.ToString();
            txtMatricule.Text = dgvReservations.CurrentRow.Cells[3].Value.ToString();
        }

        private void btnImprimer_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtClient.Text != "" || txtMatricule.Text != "" || txtKmFinal.Text !="" || txtCoutDeDegat.Text !="" || richtxtEtatRetour.Text !="")
                {
                    if (GLB.Connection.State == ConnectionState.Open)
                        GLB.Connection.Close();
                    GLB.Connection.Open();
                    GLB.Cmd = new SqlCommand($"update Reservations set Km_Final ='{txtKmFinal.Text}',Etat_retour ='{richtxtEtatRetour.Text}',Cout_degat='{txtCoutDeDegat.Text}' where Date_Debut ='{Datedebut.Value}'and Client='{txtClient.Text}' and Matricule='{txtMatricule.Text}'", GLB.Connection);
                    GLB.Cmd.ExecuteNonQuery();
                    GLB.Cmd = new SqlCommand($"update Voitures set Disponibilite = 'Disponible' where Matricule = '{txtMatricule.Text}'", GLB.Connection);
                    GLB.Cmd.ExecuteNonQuery();
                    GLB.Cmd = new SqlCommand($"update Voitures set Kilometrage = '{txtKmFinal.Text}' where Matricule = '{txtMatricule.Text}'", GLB.Connection);
                    GLB.Cmd.ExecuteNonQuery();
                    GLB.Connection.Close();
                    btnRechercher_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Saisie Tous les informations.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

            }
            catch (Exception)
            {

                MessageBox.Show("Quelque chose s'est mal passé", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            float Km_Final = 1;
            float Km_Initiale = 1;
            float Supp_Km = 1;
            if (GLB.Connection.State == ConnectionState.Open)
                GLB.Connection.Close();
            GLB.Connection.Open();
            GLB.Cmd = new SqlCommand($"select Cout_par_Jour from Voitures where  Matricule='{txtMatricule.Text}'", GLB.Connection);
            float cout_par_jour = float.Parse(GLB.Cmd.ExecuteScalar().ToString());
            GLB.Cmd = new SqlCommand($"select Caution from Reservations where  Matricule='{txtMatricule.Text}'", GLB.Connection);
            float Caution = float.Parse(GLB.Cmd.ExecuteScalar().ToString());



            GLB.Cmd = new SqlCommand($"select (datediff (DAY,Date_Debut,Date_Fin)) as test from Reservations where Date_Debut='{this.Datedebut.Value}' and Matricule='{txtMatricule.Text}' and Client='{txtClient.Text}' ", GLB.Connection);
            int nbjour = int.Parse(GLB.Cmd.ExecuteScalar().ToString())+1;
            GLB.Cmd = new SqlCommand($"select Km_Final,Km_Initiale,Supp_Km from Reservations where Date_Debut='{this.Datedebut.Value}' and Matricule='{txtMatricule.Text}' and Client='{txtClient.Text}' ", GLB.Connection);
            GLB.dr = GLB.Cmd.ExecuteReader();
            if (GLB.dr.HasRows)
            {
                while (GLB.dr.Read())
                {
                    Km_Final = float.Parse(GLB.dr[0].ToString());
                    Km_Initiale = float.Parse(GLB.dr[1].ToString());
                    Supp_Km = float.Parse(GLB.dr[2].ToString());

                }
                GLB.dr.Close();
            } 
            /*(Km_Final - Km_Initiale - (200 * nbjour) * Supp_Km)*/
            float a = Km_Final - Km_Initiale;
            int b = 200 * nbjour;
           
            float c = float.Parse(((a - b) * Supp_Km).ToString());
            float d = nbjour * cout_par_jour;
           
            float totale1 = c + d + float.Parse(txtCoutDeDegat.Text);
            GLB.Total = Caution - totale1 ;
            
            GLB.Connection.Close();
            Impression_Retour impression_Retour = new Impression_Retour(Datedebut.Value, txtClient.Text, txtMatricule.Text);
            impression_Retour.Show();
        }
    }
}
