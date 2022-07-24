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
    public partial class FormReservations : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeft, int nTop, int nRight, int nBottom, int nWidthEllipse, int nHeightEllipse);
        int position;
        public FormReservations()
        {
            InitializeComponent();
        }
        private void LaodTheme()
        {
            btn_print.BackColor = ThemeColor.PrimaryColor;
            btn_print.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;
            btn_print.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btn_print.Width, btn_print.Height, 10, 10));
            
            btnAjouter.BackColor = ThemeColor.PrimaryColor;
            btnAjouter.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;
            btnAjouter.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnAjouter.Width, btnAjouter.Height, 20, 20));

            btnModifer.BackColor = ThemeColor.PrimaryColor;
            btnModifer.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;
            btnModifer.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnModifer.Width, btnModifer.Height, 20, 20));

            btnSupprimer.BackColor = ThemeColor.PrimaryColor;
            btnSupprimer.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;
            btnSupprimer.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnSupprimer.Width, btnSupprimer.Height, 20, 20));

            btnPremier.BackColor = ThemeColor.PrimaryColor;
            btnPremier.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;
            btnPremier.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnPremier.Width, btnPremier.Height, 20, 20));
            btnPrecedent.BackColor = ThemeColor.PrimaryColor;
            btnPrecedent.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;
            btnPrecedent.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnPrecedent.Width, btnPrecedent.Height, 20, 20));
            btnSuivant.BackColor = ThemeColor.PrimaryColor;
            btnSuivant.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;
            btnSuivant.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnSuivant.Width, btnSuivant.Height, 20, 20));
            btnDernier.BackColor = ThemeColor.PrimaryColor;
            btnDernier.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;
            btnDernier.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnDernier.Width, btnDernier.Height, 20, 20));
            label1.ForeColor = ThemeColor.PrimaryColor;
            btnExport.BackColor = ThemeColor.PrimaryColor;
            btnExport.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;
            btnExport.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnExport.Width, btnExport.Height, 10, 10));
            btnClear.BackColor = ThemeColor.PrimaryColor;
            btnClear.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;
            btnClear.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnClear.Width, btnClear.Height, 10, 10));
            //Color SecondaryColor = ThemeColor.ChangeColorBrightness(ThemeColor.SecondaryColor, -0.7);
            //this.BackColor = SecondaryColor;

        }
        private void Charger_db()
        {
            if (GLB.Connection.State == ConnectionState.Open)
                GLB.Connection.Close();
            GLB.Connection.Open();
            GLB.Cmd = new SqlCommand("select * from Reservations", GLB.Connection);
            GLB.dr = GLB.Cmd.ExecuteReader();
            while (GLB.dr.Read())
            {
                dgvReservations.Rows.Add(GLB.dr[0], GLB.dr[1], GLB.dr[2], GLB.dr[3], GLB.dr[4], GLB.dr[5], GLB.dr[6], GLB.dr[7], GLB.dr[8], GLB.dr[9]);
            }
            GLB.dr.Close();
            GLB.Connection.Close();
        }
        private void FormReservations_Load(object sender, EventArgs e)
        {
            LaodTheme();
            Charger_db();

        }

        private void dgvReservations_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            datedebut.Text = dgvReservations.CurrentRow.Cells[0].Value.ToString();
            dateFin.Text = dgvReservations.CurrentRow.Cells[1].Value.ToString();
            txtClient.Text = dgvReservations.CurrentRow.Cells[2].Value.ToString();
            txtMatricule.Text = dgvReservations.CurrentRow.Cells[3].Value.ToString();
            txtKmIntiale.Text = dgvReservations.CurrentRow.Cells[4].Value.ToString();
            txtCaution.Text = dgvReservations.CurrentRow.Cells[6].Value.ToString();
            txtSupp_km.Text = dgvReservations.CurrentRow.Cells[7].Value.ToString();
            position = dgvReservations.CurrentRow.Index;
        }
        private void Select_dgv(int i)
        {

            datedebut.Text = dgvReservations.Rows[i].Cells[0].Value.ToString();
            dateFin.Text = dgvReservations.Rows[i].Cells[1].Value.ToString();
            txtClient.Text = dgvReservations.Rows[i].Cells[2].Value.ToString();
            txtMatricule.Text = dgvReservations.Rows[i].Cells[3].Value.ToString();
            txtKmIntiale.Text = dgvReservations.Rows[i].Cells[4].Value.ToString();
            txtCaution.Text = dgvReservations.Rows[i].Cells[6].Value.ToString();
            txtSupp_km.Text = dgvReservations.Rows[i].Cells[7].Value.ToString();

        }

        private bool Check()
        {
            if ( dateFin.Value == DateTime.Now || txtClient.Text == "" || txtMatricule.Text == ""
                || txtKmIntiale.Text == "" || txtCaution.Text == "" || txtSupp_km.Text == "")
                return false;
            return true;
        }
        private void btnAjouter_Click(object sender, EventArgs e)
        {
            try
            {
                if (Check())
                {
                    if (GLB.Connection.State == ConnectionState.Open)
                        GLB.Connection.Close();
                    GLB.Connection.Open();
                    GLB.Cmd = new SqlCommand($"select Count(*) from Voitures where Matricule = '{txtMatricule.Text}'and Disponibilite='Disponible'", GLB.Connection);
                    int a=Convert.ToInt32(GLB.Cmd.ExecuteScalar());
                    if (a != 0)
                    {
                        
                        GLB.Cmd = new SqlCommand($"insert into Reservations(Date_Debut,Date_Fin,Client,Matricule,Km_Initiale ,Caution,Supp_Km ) " +
                            $"values ('{datedebut.Value}','{dateFin.Value}','{txtClient.Text}','{txtMatricule.Text}',{txtKmIntiale.Text},{txtCaution.Text},{txtSupp_km.Text})", GLB.Connection);
                        GLB.Cmd.ExecuteNonQuery();
                        GLB.Cmd = new SqlCommand($"update Voitures set Disponibilite = 'Non disponible' where Matricule = '{txtMatricule.Text}'", GLB.Connection);
                        GLB.Cmd.ExecuteNonQuery();
                        MessageBox.Show("La Reservation a été bien ajouté", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GLB.Connection.Close();
                        dgvReservations.Rows.Clear();
                        Charger_db();
                    }
                    else
                    {
                        MessageBox.Show("Cette voiture n'exist pas ou n'est pas disponible pour le moment.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }

                }
                else
                {
                    MessageBox.Show("Saisie Correctement votre informations.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //catch (Exception)
            //{

            //    MessageBox.Show("Quelque chose s'est mal passé", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            


        }

        private void btnModifer_Click(object sender, EventArgs e)
        {
            try
            {
                if (Check())
                {
                    if (GLB.Connection.State == ConnectionState.Open)
                        GLB.Connection.Close();
                    GLB.Connection.Open();
                    GLB.Cmd = new SqlCommand($"update Reservations set Date_Fin ='{dateFin.Value}',Km_Initiale ={txtKmIntiale.Text} ,Caution ={txtCaution.Text} " +
                        $",Supp_Km = {txtSupp_km.Text} where Date_Debut ='{datedebut.Value}' and Client='{txtClient.Text}' and Matricule='{txtMatricule.Text}' ", GLB.Connection);
                    GLB.Cmd.ExecuteNonQuery();
                    MessageBox.Show("La Reservation a été bien modifié", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GLB.Connection.Close();
                    dgvReservations.Rows.Clear();
                    Charger_db();
                }
                else
                {
                    MessageBox.Show("Saisie Correctement votre informations.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

            }
            catch (Exception)
            {

                MessageBox.Show("Quelque chose s'est mal passé", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            try
            {

                if (GLB.Connection.State == ConnectionState.Open)
                    GLB.Connection.Close();
                GLB.Connection.Open();
                GLB.Cmd = new SqlCommand($"update Voitures set Disponibilite = 'Disponible' where Matricule = '{txtMatricule.Text}'", GLB.Connection);
                GLB.Cmd.ExecuteNonQuery();
                GLB.Cmd = new SqlCommand($"delete from Reservations where Date_Debut ='{datedebut.Value}'and Client='{txtClient.Text}' and Matricule='{txtMatricule.Text}'", GLB.Connection);
                GLB.Cmd.ExecuteNonQuery();
                MessageBox.Show("La Reservation a été bien supprimé", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GLB.Connection.Close();
                dgvReservations.Rows.Clear();
                Charger_db();


            }
            catch (Exception)
            {

                MessageBox.Show("Quelque chose s'est mal passé", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //catch (Exception ex)
            //{

            //    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void btnPremier_Click(object sender, EventArgs e)
        {
            if (dgvReservations.Rows.Count > 0)
            {
                dgvReservations.Rows[0].Selected = true;
                position = 0;
            }
            Select_dgv(position);
        }

        private void btnPrecedent_Click(object sender, EventArgs e)
        {
            if (dgvReservations.Rows.Count > 0)
            {
                int pos = position - 1;
                if (pos == -1)
                    return;
                dgvReservations.ClearSelection();

                dgvReservations.Rows[pos].Selected = true;
                position = pos;
            }
            Select_dgv(position);
        }

        private void btnSuivant_Click(object sender, EventArgs e)
        {
            if (dgvReservations.Rows.Count > 0)
            {
                int pos = position + 1;
                if (pos == dgvReservations.Rows.Count)
                    return;
                dgvReservations.ClearSelection();

                dgvReservations.Rows[pos].Selected = true;
                position = pos;
            }
            Select_dgv(position);

        }

        private void btnDernier_Click(object sender, EventArgs e)
        {
            if (dgvReservations.Rows.Count > 0)
            {
                int row = dgvReservations.Rows.Count - 1;
                dgvReservations.Rows[row].Selected = true;
                position = row;
            }
            Select_dgv(position);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvReservations.Rows.Count > 0)
                {

                    Microsoft.Office.Interop.Excel.Application xcelApp = new Microsoft.Office.Interop.Excel.Application();
                    xcelApp.Application.Workbooks.Add(Type.Missing);

                    for (int i = 1; i < dgvReservations.Columns.Count + 1; i++)
                    {
                        xcelApp.Cells[1, i] = dgvReservations.Columns[i - 1].HeaderText;
                    }

                    for (int i = 0; i < dgvReservations.Rows.Count; i++)
                    {
                        for (int j = 0; j < dgvReservations.Columns.Count; j++)
                        {
                            xcelApp.Cells[i + 2, j + 1] = dgvReservations.Rows[i].Cells[j].Value.ToString();
                        }
                    }
                    xcelApp.Columns.AutoFit();
                    xcelApp.Visible = true;
                    MessageBox.Show("Vous avez réussi à exporter vos données vers un fichier excel", "Meesage", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }
            catch (Exception)
            {
                MessageBox.Show("Quelque chose s'est mal passé", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            datedebut.Value = DateTime.Now;
            dateFin.Value = DateTime.Now;
            txtClient.Text = "";
            txtMatricule.Text = "";
            txtKmIntiale.Text = "";
            txtCaution.Text = "";
            txtSupp_km.Text = "";
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            if (txtClient.Text!=""&& txtMatricule.Text!=""&& dateFin.Value!=DateTime.Now&&txtKmIntiale.Text!=""&&txtCaution.Text!=""&&txtSupp_km.Text!="" )
            {
                Impression_Reservation impression_Reservation = new Impression_Reservation(datedebut.Value,txtClient.Text,txtMatricule.Text);
                impression_Reservation.Show();
            }
            else
            {
                MessageBox.Show("Sélectionner une réservation", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private void txtMatricule_TextChanged(object sender, EventArgs e)
        {
            if (GLB.Connection.State == ConnectionState.Open)
                GLB.Connection.Close();
            GLB.Connection.Open();
            GLB.Cmd = new SqlCommand($"select Kilometrage from Voitures where Matricule = '{txtMatricule.Text}'", GLB.Connection);
            int a = Convert.ToInt32(GLB.Cmd.ExecuteScalar());
            if (a != 0)
            {
                txtKmIntiale.Text = a.ToString();

            }
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
