using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using ClosedXML.Excel;
using Microsoft.Office.Interop.Excel;

namespace Location_des_Voitures.Forms
{
    public partial class FormClients : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeft, int nTop, int nRight, int nBottom, int nWidthEllipse, int nHeightEllipse);
        int position;
        public FormClients()
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
            btnRechercher.BackColor = ThemeColor.PrimaryColor;
            btnRechercher.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;
            btnRechercher.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnRechercher.Width, btnRechercher.Height, 20, 20));
            btnImport.BackColor = ThemeColor.PrimaryColor;
            btnImport.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;
            btnImport.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnImport.Width, btnImport.Height, 10, 10));
            btnExport.BackColor = ThemeColor.PrimaryColor;
            btnExport.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;
            btnExport.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnExport.Width, btnExport.Height, 10, 10));
            btnClear.BackColor = ThemeColor.PrimaryColor;
            btnClear.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;
            btnClear.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnExport.Width, btnExport.Height, 10, 10));
            label14.ForeColor = ThemeColor.SecondaryColor;
            //Color SecondaryColor = ThemeColor.ChangeColorBrightness(ThemeColor.SecondaryColor, -0.7);
            //this.BackColor = SecondaryColor;

        }
        private void Charger_DB()
        {
            if (GLB.Connection.State == ConnectionState.Open)
                GLB.Connection.Close();
            GLB.Connection.Open();
            GLB.Cmd = new SqlCommand("select * from Clients",GLB.Connection);
            GLB.dr = GLB.Cmd.ExecuteReader();
            while (GLB.dr.Read())
            {
                dgvClient.Rows.Add(GLB.dr[0], GLB.dr[1], GLB.dr[2], GLB.dr[3], GLB.dr[4], GLB.dr[5], GLB.dr[6]);
            }
            GLB.dr.Close();
            GLB.Connection.Close();
        }
        private bool check()
        {
            if (txtCIN.Text == "" || txtNom.Text == "" || txtPermis.Text == "" || cmbSexe.Text == ""
                || txtAdress.Text == "" || txttelphone.Text == "" || txtEmail.Text == "")
                return false;
            return true;
        }

        private void FormClients_Load(object sender, EventArgs e)
        {
            LaodTheme();
            Charger_DB();
            
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
                    GLB.Cmd = new SqlCommand($"insert into Clients values ('{txtCIN.Text.Trim()}','{txtNom.Text.Trim()}','{txtPermis.Text.Trim()}'," +
                        $"'{cmbSexe.Text.Trim()}','{txtAdress.Text.Trim()}','{txttelphone.Text.Trim()}','{txtEmail.Text.Trim()}')", GLB.Connection);
                    GLB.Cmd.ExecuteNonQuery();
                    MessageBox.Show("Le Client a été bien ajouté", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GLB.Connection.Close();
                    dgvClient.Rows.Clear();
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

                MessageBox.Show("Quelque chose s'est mal passé", "Message",MessageBoxButtons.OK,MessageBoxIcon.Error);
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
                    GLB.Cmd = new SqlCommand($"update Clients set Nom_Complet = '{txtNom.Text.Trim()}' ,Permis ='{txtPermis.Text.Trim()}' " +
                        $",Sexe ='{cmbSexe.Text.Trim()}',Adress='{txtAdress.Text.Trim()}' ,Telephone='{txttelphone.Text.Trim()}',Email='{txtEmail.Text.Trim()}' where CIN = '{txtCIN.Text.Trim()}'", GLB.Connection);
                    GLB.Cmd.ExecuteNonQuery();
                    MessageBox.Show("Le Client a été bien modifié", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GLB.Connection.Close();
                    dgvClient.Rows.Clear();
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
                    GLB.Cmd = new SqlCommand($"delete from Reservations where Client  = '{txtCIN.Text.Trim()}'", GLB.Connection);
                    GLB.Cmd.ExecuteNonQuery();
                    GLB.Cmd = new SqlCommand($"delete From Clients where CIN = '{txtCIN.Text.Trim()}'", GLB.Connection);
                    GLB.Cmd.ExecuteNonQuery();
                    MessageBox.Show("Le Client a été bien supprimé", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GLB.Connection.Close();
                    dgvClient.Rows.Clear();
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

        private void dgvClient_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCIN.Text = dgvClient.CurrentRow.Cells[0].Value.ToString();
            txtNom.Text = dgvClient.CurrentRow.Cells[1].Value.ToString();
            txtPermis.Text = dgvClient.CurrentRow.Cells[2].Value.ToString();
            cmbSexe.Text = dgvClient.CurrentRow.Cells[3].Value.ToString();
            txtAdress.Text = dgvClient.CurrentRow.Cells[4].Value.ToString();
            txttelphone.Text = dgvClient.CurrentRow.Cells[5].Value.ToString();
            txtEmail.Text = dgvClient.CurrentRow.Cells[6].Value.ToString();
            position = dgvClient.CurrentRow.Index;
        }
        private void Select_dgv(int i)
        {
            txtCIN.Text = dgvClient.Rows[i].Cells[0].Value.ToString();
            txtNom.Text = dgvClient.Rows[i].Cells[1].Value.ToString();
            txtPermis.Text = dgvClient.Rows[i].Cells[2].Value.ToString();
            cmbSexe.Text = dgvClient.Rows[i].Cells[3].Value.ToString();
            txtAdress.Text = dgvClient.Rows[i].Cells[4].Value.ToString();
            txttelphone.Text = dgvClient.Rows[i].Cells[5].Value.ToString();
            txtEmail.Text = dgvClient.Rows[i].Cells[6].Value.ToString();
        }
        private void btnPremier_Click(object sender, EventArgs e)
        {
            if (dgvClient.Rows.Count > 0)
            {
                dgvClient.Rows[0].Selected = true;
                position = 0;
            }
            Select_dgv(position);
        }

        private void btnPrecedent_Click(object sender, EventArgs e)
        {
            if (dgvClient.Rows.Count > 0)
            {
                int pos = position - 1;
                if (pos == -1)
                    return;
                dgvClient.ClearSelection();

                dgvClient.Rows[pos].Selected = true;
                position = pos;
            }
            Select_dgv(position);

        }

        private void btnSuivant_Click(object sender, EventArgs e)
        {
            if (dgvClient.Rows.Count > 0)
            {
                int pos = position + 1;
                if (pos == dgvClient.Rows.Count)
                    return;
                dgvClient.ClearSelection();

                dgvClient.Rows[pos].Selected = true;
                position = pos;
            }
            Select_dgv(position);

        }

        private void btnDernier_Click(object sender, EventArgs e)
        {
            if (dgvClient.Rows.Count > 0)
            {
                int row = dgvClient.Rows.Count - 1;
                dgvClient.Rows[row].Selected = true;
                position = row;
            }
            Select_dgv(position);

        }

        private void btnRechercher_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRechercher.Text !="")
                {
                    if (GLB.Connection.State == ConnectionState.Open)
                        GLB.Connection.Close();
                    GLB.Connection.Open();
                    GLB.Cmd = new SqlCommand($"select * from Clients where CIN = '{txtRechercher.Text.Trim()}'", GLB.Connection);
                    GLB.dr = GLB.Cmd.ExecuteReader();
                    
                    while (GLB.dr.Read())
                    {
                        MessageBox.Show($"\tCIN : {GLB.dr[0]}\n\tNom Et Prenom : {GLB.dr[1]}\n\tPermis : {GLB.dr[2]}" +
                            $"\n\tSexe : {GLB.dr[3]}\n\tAdresse : {GLB.dr[4]}\n\tTelephone : {GLB.dr[5]}\n\tEmail : {GLB.dr[6]}", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCIN.Text = GLB.dr[0].ToString();
                        txtNom.Text = GLB.dr[1].ToString();
                        txtPermis.Text = GLB.dr[2].ToString();
                        cmbSexe.Text = GLB.dr[3].ToString();
                        txtAdress.Text = GLB.dr[4].ToString();
                        txttelphone.Text = GLB.dr[5].ToString();
                        txtEmail.Text = GLB.dr[6].ToString();

                    }
                    GLB.dr.Close();
                    GLB.Connection.Close();
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

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvClient.Rows.Count > 0)
                {

                    Microsoft.Office.Interop.Excel.Application xcelApp = new Microsoft.Office.Interop.Excel.Application();
                    xcelApp.Application.Workbooks.Add(Type.Missing);

                    for (int i = 1; i < dgvClient.Columns.Count + 1; i++)
                    {
                        xcelApp.Cells[1, i] = dgvClient.Columns[i - 1].HeaderText;
                    }

                    for (int i = 0; i < dgvClient.Rows.Count; i++)
                    {
                        for (int j = 0; j < dgvClient.Columns.Count; j++)
                        {
                            xcelApp.Cells[i + 2, j + 1] = dgvClient.Rows[i].Cells[j].Value.ToString();
                        }
                    }
                    xcelApp.Columns.AutoFit();
                    xcelApp.Visible = true;
                    MessageBox.Show("Vous avez réussi à exporter vos données vers un fichier excel","Meesage",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }


            }
            catch (Exception)
            {
                MessageBox.Show("Quelque chose s'est mal passé", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            _Application importExceldatagridViewApp;
            _Workbook importExceldatagridViewworkbook;
            _Worksheet importExceldatagridViewworksheet;
            Range importdatagridviewRange;
            try
            {
                importExceldatagridViewApp = new Microsoft.Office.Interop.Excel.Application();
                OpenFileDialog importOpenDialoge = new OpenFileDialog();
                importOpenDialoge.Title = "Import Excel File";
                importOpenDialoge.Filter = "Import Excel File|*.xlsx;*xls;*xlm";
                if (importOpenDialoge.ShowDialog() == DialogResult.OK)
                {
                    if (GLB.Connection.State == ConnectionState.Open)
                        GLB.Connection.Close();
                    GLB.Connection.Open();
                    
                    importExceldatagridViewworkbook = importExceldatagridViewApp.Workbooks.Open(importOpenDialoge.FileName);
                    importExceldatagridViewworksheet = importExceldatagridViewworkbook.ActiveSheet;
                    importdatagridviewRange = importExceldatagridViewworksheet.UsedRange;
                    for (int excelWorksheetIndex = 2 ; excelWorksheetIndex < importdatagridviewRange.Rows.Count + 1; excelWorksheetIndex++)
                    {
                        GLB.Cmd = new SqlCommand($"insert into Clients values ('{importExceldatagridViewworksheet.Cells[excelWorksheetIndex, 1].value}','{importExceldatagridViewworksheet.Cells[excelWorksheetIndex,2].value}','{importExceldatagridViewworksheet.Cells[excelWorksheetIndex, 3].value}'," +
                        $"'{importExceldatagridViewworksheet.Cells[excelWorksheetIndex, 4].value}','{importExceldatagridViewworksheet.Cells[excelWorksheetIndex, 5].value}','{importExceldatagridViewworksheet.Cells[excelWorksheetIndex, 6].value}','{importExceldatagridViewworksheet.Cells[excelWorksheetIndex, 7].value}')", GLB.Connection);
                        GLB.Cmd.ExecuteNonQuery();
                        dgvClient.Rows.Add(importExceldatagridViewworksheet.Cells[excelWorksheetIndex,1].value,
                            importExceldatagridViewworksheet.Cells[excelWorksheetIndex, 2].value,
                            importExceldatagridViewworksheet.Cells[excelWorksheetIndex, 3].value,
                            importExceldatagridViewworksheet.Cells[excelWorksheetIndex, 4].value,
                            importExceldatagridViewworksheet.Cells[excelWorksheetIndex, 5].value,
                            importExceldatagridViewworksheet.Cells[excelWorksheetIndex, 6].value,
                            importExceldatagridViewworksheet.Cells[excelWorksheetIndex, 7].value);
                    }
                    GLB.Connection.Close();
                }
                

            }
            catch (Exception)
            {
                MessageBox.Show("Quelque chose s'est mal passé", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCIN.Clear();
            txtNom.Clear();
            txtPermis.Clear();
            cmbSexe.Text = "";
            txtAdress.Clear();
            txttelphone.Clear();
            txtEmail.Clear();

        }
    }
}
