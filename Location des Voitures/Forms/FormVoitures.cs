using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Location_des_Voitures.Forms
{
    public partial class FormVoitures : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeft, int nTop, int nRight, int nBottom, int nWidthEllipse, int nHeightEllipse);
        int position;
        public FormVoitures()
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
            label13.ForeColor = ThemeColor.PrimaryColor;
            btnRechercher.BackColor = ThemeColor.PrimaryColor;
            btnRechercher.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;
            btnRechercher.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnRechercher.Width, btnRechercher.Height, 20, 20));
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
            GLB.Cmd = new SqlCommand("select * from Voitures", GLB.Connection);
            GLB.dr = GLB.Cmd.ExecuteReader();
            while (GLB.dr.Read())
            {
                dgvVoitures.Rows.Add(GLB.dr[0], GLB.dr[1], GLB.dr[2], GLB.dr[3], GLB.dr[4], GLB.dr[5], GLB.dr[6], GLB.dr[7], GLB.dr[8], GLB.dr[9], GLB.dr[10], GLB.dr[11]);
            }
            GLB.dr.Close();
            GLB.Connection.Close();
            Marquedgv();
            Categoriedgv();
            Carburantdgv();
            Modeledgv();
        }
        private void Marquedgv()
        {
            
            if (GLB.Connection.State == ConnectionState.Open)
                GLB.Connection.Close();
            GLB.Connection.Open();
            for (int c = 0; c < dgvVoitures.Rows.Count; c++)
            {
                GLB.Cmd = new SqlCommand($"select Marque from Marques where Id_Marque = {dgvVoitures.Rows[c].Cells[6].Value}", GLB.Connection);
                string marque = GLB.Cmd.ExecuteScalar().ToString();

                dgvVoitures.Rows[c].Cells[6].Value = marque;
            }
            GLB.Connection.Close();
        }
        private void Modeledgv()
        {

            if (GLB.Connection.State == ConnectionState.Open)
                GLB.Connection.Close();
            GLB.Connection.Open();
            for (int c = 0; c < dgvVoitures.Rows.Count; c++)
            {
                GLB.Cmd = new SqlCommand($"select Modele from Modeles where Id_Model  = {dgvVoitures.Rows[c].Cells[5].Value}", GLB.Connection);
                string marque = GLB.Cmd.ExecuteScalar().ToString();

                dgvVoitures.Rows[c].Cells[5].Value = marque;
            }
            GLB.Connection.Close();
        }
        private void Carburantdgv()
        {

            if (GLB.Connection.State == ConnectionState.Open)
                GLB.Connection.Close();
            GLB.Connection.Open();
            for (int c = 0; c < dgvVoitures.Rows.Count; c++)
            {
                GLB.Cmd = new SqlCommand($"select TypeC from Carburants where Id_Carburant = {dgvVoitures.Rows[c].Cells[7].Value}", GLB.Connection);
                string marque = GLB.Cmd.ExecuteScalar().ToString();

                dgvVoitures.Rows[c].Cells[7].Value = marque;
            }
            GLB.Connection.Close();
        }
        private void Categoriedgv()
        {

            if (GLB.Connection.State == ConnectionState.Open)
                GLB.Connection.Close();
            GLB.Connection.Open();
            for (int c = 0; c < dgvVoitures.Rows.Count; c++)
            {
                GLB.Cmd = new SqlCommand($"select Categorie from Categories where Id_Categorie= {dgvVoitures.Rows[c].Cells[9].Value}", GLB.Connection);
                string marque = GLB.Cmd.ExecuteScalar().ToString();

                dgvVoitures.Rows[c].Cells[9].Value = marque;
            }
            GLB.Connection.Close();
        }
        private void Charger_cmbMarque()
        {

            if (GLB.ds.Tables["marque"] != null)
            {
                GLB.ds.Tables["marque"].Rows.Clear();
            }
            GLB.da = new SqlDataAdapter("select * from Marques", GLB.Connection);
            GLB.da.Fill(GLB.ds, "marque");
            cmbMarque.DataSource = GLB.ds.Tables["marque"];
            cmbMarque.DisplayMember = GLB.ds.Tables["marque"].Columns[1].ColumnName;
            cmbMarque.ValueMember = GLB.ds.Tables["marque"].Columns[0].ColumnName;
        }
        private void Charger_cmbModel()
        {

            if (GLB.ds.Tables["model"] != null)
            {
                GLB.ds.Tables["model"].Rows.Clear();
            }
            GLB.da = new SqlDataAdapter("select * from Modeles", GLB.Connection);
            GLB.da.Fill(GLB.ds, "model");
            cmbModele.DataSource = GLB.ds.Tables["model"];
            cmbModele.DisplayMember = GLB.ds.Tables["model"].Columns[1].ColumnName;
            cmbModele.ValueMember = GLB.ds.Tables["model"].Columns[0].ColumnName;
        }
        private void Charger_cmbCategorie()
        {

            if (GLB.ds.Tables["Categories"] != null)
            {
                GLB.ds.Tables["Categories"].Rows.Clear();
            }
            GLB.da = new SqlDataAdapter("select * from Categories", GLB.Connection);
            GLB.da.Fill(GLB.ds, "Categories");
            cmbCategorie.DataSource = GLB.ds.Tables["Categories"];
            cmbCategorie.DisplayMember = GLB.ds.Tables["Categories"].Columns[1].ColumnName;
            cmbCategorie.ValueMember = GLB.ds.Tables["Categories"].Columns[0].ColumnName;
        }
        private void Charger_cmbCarburant()
        {

            if (GLB.ds.Tables["Carburants"] != null)
            {
                GLB.ds.Tables["Carburants"].Rows.Clear();
            }
            GLB.da = new SqlDataAdapter("select * from Carburants", GLB.Connection);
            GLB.da.Fill(GLB.ds, "Carburants");
            cmbCarburant.DataSource = GLB.ds.Tables["Carburants"];
            cmbCarburant.DisplayMember = GLB.ds.Tables["Carburants"].Columns[1].ColumnName;
            cmbCarburant.ValueMember = GLB.ds.Tables["Carburants"].Columns[0].ColumnName;
        }

        private void FormVoitures_Load(object sender, EventArgs e)
        {
            LaodTheme();
            Charger_db();
            Charger_cmbMarque();
            Charger_cmbModel();
            Charger_cmbCarburant();
            Charger_cmbCategorie();
            //Marquedgv();

        }

        private bool check()
        {
            if (txtMatricule.Text == "" || txtAnne_Prod.Text == "" || txtCouleur.Text == "" || txtPuissance.Text == ""
                || txtCout_par_jour.Text == "" || cmbModele.Text == "" || cmbMarque.Text == ""
                || cmbCarburant.Text == "" || txtKilometrage.Text == "" || cmbCategorie.Text == "" || txtEtat.Text == "")
                return false;
            return true;
        }
        private void dgvVoitures_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            txtMatricule.Text = dgvVoitures.CurrentRow.Cells[0].Value.ToString();
            txtAnne_Prod.Text = dgvVoitures.CurrentRow.Cells[1].Value.ToString();
            txtCouleur.Text = dgvVoitures.CurrentRow.Cells[2].Value.ToString();
            txtPuissance.Text = dgvVoitures.CurrentRow.Cells[3].Value.ToString();
            txtCout_par_jour.Text = dgvVoitures.CurrentRow.Cells[4].Value.ToString();
            cmbModele.Text = dgvVoitures.CurrentRow.Cells[5].Value.ToString();
            cmbMarque.Text = dgvVoitures.CurrentRow.Cells[6].Value.ToString();
            cmbCarburant.Text = dgvVoitures.CurrentRow.Cells[7].Value.ToString();
            txtKilometrage.Text = dgvVoitures.CurrentRow.Cells[8].Value.ToString();
            cmbCategorie.Text = dgvVoitures.CurrentRow.Cells[9].Value.ToString();
            txtEtat.Text = dgvVoitures.CurrentRow.Cells[10].Value.ToString();
            position = dgvVoitures.CurrentRow.Index;
        }
        private void Select_dgv(int i)
        {
            txtMatricule.Text = dgvVoitures.Rows[i].Cells[0].Value.ToString();
            txtAnne_Prod.Text = dgvVoitures.Rows[i].Cells[1].Value.ToString();
            txtCouleur.Text = dgvVoitures.Rows[i].Cells[2].Value.ToString();
            txtPuissance.Text = dgvVoitures.Rows[i].Cells[3].Value.ToString();
            txtCout_par_jour.Text = dgvVoitures.Rows[i].Cells[4].Value.ToString();
            cmbModele.Text = dgvVoitures.Rows[i].Cells[5].Value.ToString();
            cmbMarque.Text = dgvVoitures.Rows[i].Cells[6].Value.ToString();
            cmbCarburant.Text = dgvVoitures.Rows[i].Cells[7].Value.ToString();
            txtKilometrage.Text = dgvVoitures.Rows[i].Cells[8].Value.ToString();
            cmbCategorie.Text = dgvVoitures.Rows[i].Cells[9].Value.ToString();
            txtEtat.Text = dgvVoitures.Rows[i].Cells[10].Value.ToString();

        }

        private void btnPremier_Click(object sender, EventArgs e)
        {
            if (dgvVoitures.Rows.Count > 0)
            {
                dgvVoitures.Rows[0].Selected = true;
                position = 0;
            }
            Select_dgv(position);
        }

        private void btnPrecedent_Click(object sender, EventArgs e)
        {
            if (dgvVoitures.Rows.Count > 0)
            {
                int pos = position - 1;
                if (pos == -1)
                    return;
                dgvVoitures.ClearSelection();

                dgvVoitures.Rows[pos].Selected = true;
                position = pos;
            }
            Select_dgv(position);
        }

        private void btnSuivant_Click(object sender, EventArgs e)
        {
            if (dgvVoitures.Rows.Count > 0)
            {
                int pos = position + 1;
                if (pos == dgvVoitures.Rows.Count)
                    return;
                dgvVoitures.ClearSelection();

                dgvVoitures.Rows[pos].Selected = true;
                position = pos;
            }
            Select_dgv(position);
        }

        private void btnDernier_Click(object sender, EventArgs e)
        {
            if (dgvVoitures.Rows.Count > 0)
            {
                int row = dgvVoitures.Rows.Count - 1;
                dgvVoitures.Rows[row].Selected = true;
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
                    GLB.Cmd = new SqlCommand($"insert into Voitures values('{txtMatricule.Text.Trim()}'," +
                        $"'{txtAnne_Prod.Text.Trim()}','{txtCouleur.Text.Trim()}','{txtPuissance.Text.Trim()}'," +
                        $"{txtCout_par_jour.Text.Trim()},{cmbModele.SelectedValue},{cmbMarque.SelectedValue},{cmbCarburant.SelectedValue},{txtKilometrage.Text.Trim()}," +
                        $"{cmbCategorie.SelectedValue},'{txtEtat.Text.Trim()}','Disponible')", GLB.Connection);
                    GLB.Cmd.ExecuteNonQuery();
                    MessageBox.Show("La Voiture a été bien ajouté", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GLB.Connection.Close();
                    dgvVoitures.Rows.Clear();
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
            //catch (Exception ex)
            //{

            //    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
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
                    GLB.Cmd = new SqlCommand($"update Voitures set Annee_Procution = '{txtAnne_Prod.Text.Trim()}' , Couleur ='{txtCouleur.Text.Trim()}',Puissance = '{txtPuissance.Text.Trim()}'" +
                        $",Cout_par_Jour ={txtCout_par_jour.Text.Trim()} ,Modele ={cmbModele.SelectedValue} ,Marque = {cmbMarque.SelectedValue},Carburant={cmbCarburant.SelectedValue} " +
                        $",Kilometrage = {txtKilometrage.Text.Trim()}, Categorie= {cmbCategorie.SelectedValue},Etat = '{txtEtat.Text.Trim()}' where Matricule ='{txtMatricule.Text.Trim()} '", GLB.Connection);
                    GLB.Cmd.ExecuteNonQuery();
                    MessageBox.Show("La Voiture a été bien modifié", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GLB.Connection.Close();
                    dgvVoitures.Rows.Clear();
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
                if(txtMatricule.Text != "")
                {
                    if (GLB.Connection.State == ConnectionState.Open)
                        GLB.Connection.Close();
                    GLB.Connection.Open();
                    GLB.Cmd = new SqlCommand($"delete from Reservations where Matricule = '{txtMatricule.Text.Trim()}'", GLB.Connection);
                    GLB.Cmd.ExecuteNonQuery();
                    GLB.Cmd = new SqlCommand($"delete from Voitures where Matricule = '{txtMatricule.Text.Trim()}'", GLB.Connection);
                    GLB.Cmd.ExecuteNonQuery();
                    MessageBox.Show("La Voiture a été bien supprimé", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GLB.Connection.Close();
                    dgvVoitures.Rows.Clear();
                    Charger_db();
                }
                else
                {
                    MessageBox.Show("Selectionnez une Voiture.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                


            }
            catch (Exception)
            {

                MessageBox.Show("Quelque chose s'est mal passé", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvVoitures.Rows.Count > 0)
                {

                    Microsoft.Office.Interop.Excel.Application xcelApp = new Microsoft.Office.Interop.Excel.Application();
                    xcelApp.Application.Workbooks.Add(Type.Missing);

                    for (int i = 1; i < dgvVoitures.Columns.Count + 1; i++)
                    {
                        xcelApp.Cells[1, i] = dgvVoitures.Columns[i - 1].HeaderText;
                    }

                    for (int i = 0; i < dgvVoitures.Rows.Count; i++)
                    {
                        for (int j = 0; j < dgvVoitures.Columns.Count; j++)
                        {
                            xcelApp.Cells[i + 2, j + 1] = dgvVoitures.Rows[i].Cells[j].Value.ToString();
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
        
        private void btnRechercher_Click(object sender, EventArgs e)
        {
            dgvVoitures.Rows.Clear();
            Charger_db();
            bool find = false;
            try
            {
                if (txtRechercher.Text != "")
                {
                    
                    foreach (DataGridViewRow item in dgvVoitures.Rows)
                    {
                        if (item.Cells[0].Value.ToString() == txtRechercher.Text.Trim())
                        {
                            MessageBox.Show($"\tMatricule : {item.Cells[0].Value}\n\tAnnee de Production : {item.Cells[1].Value} \n\tCouleur : {item.Cells[2].Value}" +
                            $"\n\tPuissance : {item.Cells[3].Value}\n\tCout par Jour:{item.Cells[4].Value}\n\tModele : {item.Cells[5].Value}" +
                            $"\n\tMarque : {item.Cells[6].Value}\n\tCarburants : {item.Cells[7].Value}\n\tKilometrage : {item.Cells[8].Value}" +
                            $"\n\tCatgorie : {item.Cells[9].Value}\n\tEtat : {item.Cells[10].Value}\n\tDisponibilité : {item.Cells[11].Value} "
                            , "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            int index = dgvVoitures.Rows.IndexOf(item);
                            Select_dgv(index);
                            find = true;
                            break;
                        }
                    }
                    if(!find)
                    {
                        MessageBox.Show($"La Voiture n'existe pas", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Saisie Le CIN du Client.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            txtMatricule.Clear();
            txtAnne_Prod.Clear();
            txtCouleur.Clear();
            txtPuissance.Clear();
            txtCout_par_jour.Clear();
            cmbModele.Text = "";
            cmbMarque.Text = "";cmbCarburant.Text = "";
            txtKilometrage.Clear();
            cmbCategorie.Text = "";
            txtEtat.Clear();
        }

        
    }
}
