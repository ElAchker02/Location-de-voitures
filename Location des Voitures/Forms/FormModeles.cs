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
    public partial class FormModeles : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeft, int nTop, int nRight, int nBottom, int nWidthEllipse, int nHeightEllipse);
        int position;
        public FormModeles()
        {
            InitializeComponent();
        }
        private void LaodTheme()
        {

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
            btnExport.BackColor = ThemeColor.PrimaryColor;
            btnExport.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;
            btnExport.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnExport.Width, btnExport.Height, 10, 10));
            btnClear.BackColor = ThemeColor.PrimaryColor;
            btnClear.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;
            btnClear.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnExport.Width, btnExport.Height, 10, 10));
            //label1.ForeColor = ThemeColor.PrimaryColor;
            //Color SecondaryColor = ThemeColor.ChangeColorBrightness(ThemeColor.SecondaryColor, -0.7);
            //this.BackColor = SecondaryColor;

        }
        private void Charger_cmb()
        {
            
            if (GLB.ds.Tables["marque"]!= null)
            {
                GLB.ds.Tables["marque"].Rows.Clear();
            }
            GLB.da = new SqlDataAdapter("select * from Marques",GLB.Connection);
            GLB.da.Fill(GLB.ds , "marque");
            cmbMarque.DataSource = GLB.ds.Tables["marque"];
            cmbMarque.DisplayMember = GLB.ds.Tables["marque"].Columns[1].ColumnName;
            cmbMarque.ValueMember = GLB.ds.Tables["marque"].Columns[0].ColumnName;
        }
        private void Charger_DB()
        {
            if (GLB.Connection.State == ConnectionState.Open)
                GLB.Connection.Close();
            GLB.Connection.Open();
            GLB.Cmd = new SqlCommand("select * from Modeles", GLB.Connection);
            GLB.dr = GLB.Cmd.ExecuteReader();
            while (GLB.dr.Read())
            {
               
                dgvModele.Rows.Add(GLB.dr[0], GLB.dr[1], GLB.dr[2]);
            }
            GLB.dr.Close();
            GLB.Connection.Close();
            //Remplacer le FK par sa valeur.
            int i = 0;
            if (GLB.Connection.State == ConnectionState.Open)
                GLB.Connection.Close();
            GLB.Connection.Open();
            for (int c = 0; c < dgvModele.Rows.Count ; c++)
            {
                GLB.Cmd = new SqlCommand($"select Marque from Marques where Id_Marque = {dgvModele.Rows[c].Cells[2].Value} ", GLB.Connection);
                string marque = GLB.Cmd.ExecuteScalar().ToString();
               
                dgvModele.Rows[c].Cells[2].Value = marque;
            }
            GLB.Connection.Close();
        }
        private bool check()
        {
            if (txtModele.Text == "" || cmbMarque.Text == "" )
                return false;
            return true;
        }
        private void FormModeles_Load(object sender, EventArgs e)
        {
            LaodTheme();
            Charger_DB();
            Charger_cmb();


        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvModele.Rows.Count > 0)
                {

                    Microsoft.Office.Interop.Excel.Application xcelApp = new Microsoft.Office.Interop.Excel.Application();
                    xcelApp.Application.Workbooks.Add(Type.Missing);

                    for (int i = 1; i < dgvModele.Columns.Count + 1; i++)
                    {
                        xcelApp.Cells[1, i] = dgvModele.Columns[i - 1].HeaderText;
                    }

                    for (int i = 0; i < dgvModele.Rows.Count; i++)
                    {
                        for (int j = 0; j < dgvModele.Columns.Count; j++)
                        {
                            xcelApp.Cells[i + 2, j + 1] = dgvModele.Rows[i].Cells[j].Value.ToString();
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

        private void dgvModele_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtModele.Text = dgvModele.CurrentRow.Cells[1].Value.ToString();
            
            cmbMarque.Text = dgvModele.CurrentRow.Cells[2].Value.ToString();
            position = dgvModele.CurrentRow.Index;

        }
        private void Select_dgv(int i)
        {
            txtModele.Text = dgvModele.Rows[i].Cells[1].Value.ToString();
            
            cmbMarque.Text = dgvModele.Rows[i].Cells[2].Value.ToString();
            
        }

        private void btnPremier_Click(object sender, EventArgs e)
        {
            if (dgvModele.Rows.Count > 0)
            {
                dgvModele.Rows[0].Selected = true;
                position = 0;
            }
            Select_dgv(position);
        }

        private void btnPrecedent_Click(object sender, EventArgs e)
        {
            if (dgvModele.Rows.Count > 0)
            {
                int pos = position - 1;
                if (pos == -1)
                    return;
                dgvModele.ClearSelection();

                dgvModele.Rows[pos].Selected = true;
                position = pos;
            }
            Select_dgv(position);
        }

        private void btnSuivant_Click(object sender, EventArgs e)
        {
            if (dgvModele.Rows.Count > 0)
            {
                int pos = position + 1;
                if (pos == dgvModele.Rows.Count)
                    return;
                dgvModele.ClearSelection();

                dgvModele.Rows[pos].Selected = true;
                position = pos;
            }
            Select_dgv(position);
        }

        private void btnDernier_Click(object sender, EventArgs e)
        {
            if (dgvModele.Rows.Count > 0)
            {
                int row = dgvModele.Rows.Count - 1;
                dgvModele.Rows[row].Selected = true;
                position = row;
            }
            Select_dgv(position);
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            try
            {
                if (check())
                {
                    if (GLB.Connection.State == ConnectionState.Open)
                        GLB.Connection.Close();
                    GLB.Connection.Open();
                    GLB.Cmd = new SqlCommand($"insert into  Modeles values ('{txtModele.Text}',{cmbMarque.SelectedValue})", GLB.Connection);
                    GLB.Cmd.ExecuteNonQuery();
                    MessageBox.Show("Le Modele a été bien ajouté", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GLB.Connection.Close();
                    dgvModele.Rows.Clear();
                    Charger_DB();
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

        private void btnModifer_Click(object sender, EventArgs e)
        {
            try
            {
                if (check())
                {
                    if (GLB.Connection.State == ConnectionState.Open)
                        GLB.Connection.Close();
                    GLB.Connection.Open();
                    GLB.Cmd = new SqlCommand($"update Modeles set Marque = {cmbMarque.SelectedValue}, Modele = '{txtModele.Text}' where Id_Model = {dgvModele.Rows[position].Cells[0].Value}", GLB.Connection);
                    GLB.Cmd.ExecuteNonQuery();
                    MessageBox.Show("Le Modele a été bien modifié", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GLB.Connection.Close();
                    dgvModele.Rows.Clear();
                    Charger_DB();
                }
                else
                {
                    MessageBox.Show("Saisie Correctement votre informations.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Quelque chose s'est mal passé", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            try
            {
                if (check())
                {
                    if (GLB.Connection.State == ConnectionState.Open)
                        GLB.Connection.Close();
                    GLB.Connection.Open();
                    GLB.Cmd = new SqlCommand($"delete from Voitures where Modele  =  {dgvModele.Rows[position].Cells[0].Value}", GLB.Connection);
                    GLB.Cmd.ExecuteNonQuery();
                    GLB.Cmd = new SqlCommand($"delete from Modeles where Id_Model = {dgvModele.Rows[position].Cells[0].Value}", GLB.Connection);
                    GLB.Cmd.ExecuteNonQuery();
                    MessageBox.Show("Le Modele a été bien supprimé", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GLB.Connection.Close();
                    dgvModele.Rows.Clear();
                    Charger_DB();
                }
                else
                {
                    MessageBox.Show("Saisie Correctement votre informations.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Quelque chose s'est mal passé", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtModele.Clear();
            cmbMarque.Text="";
        }
    }
}
