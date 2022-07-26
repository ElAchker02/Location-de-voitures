﻿using System;
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
    public partial class FormCategorie : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn(int nLeft, int nTop, int nRight, int nBottom, int nWidthEllipse, int nHeightEllipse);
        int position;
        public FormCategorie()
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
            //label1.ForeColor = ThemeColor.PrimaryColor;
            //Color SecondaryColor = ThemeColor.ChangeColorBrightness(ThemeColor.SecondaryColor, -0.7);
            //this.BackColor = SecondaryColor;

        }
        private void Charger_db()
        {
            if (GLB.Connection.State == ConnectionState.Open)
                GLB.Connection.Close();
            GLB.Connection.Open();
            GLB.Cmd = new SqlCommand("select * from Categories", GLB.Connection);
            GLB.dr = GLB.Cmd.ExecuteReader();
            while (GLB.dr.Read())
            {
                dgvCategorie.Rows.Add(GLB.dr[0], GLB.dr[1]);
            }
            GLB.dr.Close();
            GLB.Connection.Close();
        }
        private void Select_dgv(int i)
        {
            txtCategorie.Text = dgvCategorie.Rows[i].Cells[1].Value.ToString();
        }
        private void FormCategorie_Load(object sender, EventArgs e)
        {
            Charger_db();
            LaodTheme();
        }

        private void dgvCategorie_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCategorie.Text = dgvCategorie.CurrentRow.Cells[1].Value.ToString();
            position = dgvCategorie.CurrentRow.Index;

        }

        private void btnPremier_Click(object sender, EventArgs e)
        {
            if (dgvCategorie.Rows.Count > 0)
            {
                dgvCategorie.Rows[0].Selected = true;
                position = 0;
            }
            Select_dgv(position);
        }

        private void btnPrecedent_Click(object sender, EventArgs e)
        {
            if (dgvCategorie.Rows.Count > 0)
            {
                int pos = position - 1;
                if (pos == -1)
                    return;
                dgvCategorie.ClearSelection();

                dgvCategorie.Rows[pos].Selected = true;
                position = pos;
            }
            Select_dgv(position);
        }

        private void btnSuivant_Click(object sender, EventArgs e)
        {
            if (dgvCategorie.Rows.Count > 0)
            {
                int pos = position + 1;
                if (pos == dgvCategorie.Rows.Count)
                    return;
                dgvCategorie.ClearSelection();

                dgvCategorie.Rows[pos].Selected = true;
                position = pos;
            }
            Select_dgv(position);
        }

        private void btnDernier_Click(object sender, EventArgs e)
        {
            if (dgvCategorie.Rows.Count > 0)
            {
                int row = dgvCategorie.Rows.Count - 1;
                dgvCategorie.Rows[row].Selected = true;
                position = row;
            }
            Select_dgv(position);
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCategorie.Text != "")
                {
                    if (GLB.Connection.State == ConnectionState.Open)
                        GLB.Connection.Close();
                    GLB.Connection.Open();
                    GLB.Cmd = new SqlCommand($"insert into Categories values('{txtCategorie.Text.Trim()}')", GLB.Connection);
                    GLB.Cmd.ExecuteNonQuery();
                    MessageBox.Show("Catégorie a été bien ajouté", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GLB.Connection.Close();
                    dgvCategorie.Rows.Clear();
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

        private void btnModifer_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCategorie.Text != "")
                {
                    if (GLB.Connection.State == ConnectionState.Open)
                        GLB.Connection.Close();
                    GLB.Connection.Open();
                    GLB.Cmd = new SqlCommand($"update Categories set Categorie = '{txtCategorie.Text.Trim()}' where Id_Categorie = {dgvCategorie.Rows[position].Cells[0].Value}", GLB.Connection);
                    GLB.Cmd.ExecuteNonQuery();
                    MessageBox.Show("Catégorie a été bien modifié", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    GLB.Connection.Close();
                    dgvCategorie.Rows.Clear();
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
                GLB.Cmd = new SqlCommand($"delete from Voitures where Categorie  = {dgvCategorie.Rows[position].Cells[0].Value}", GLB.Connection);
                GLB.Cmd.ExecuteNonQuery();
                GLB.Cmd = new SqlCommand($"delete from Categories where Id_Categorie = {dgvCategorie.Rows[position].Cells[0].Value}", GLB.Connection);
                GLB.Cmd.ExecuteNonQuery();
                MessageBox.Show("La Marque a été bien supprimé", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GLB.Connection.Close();
                dgvCategorie.Rows.Clear();
                Charger_db();


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
                if (dgvCategorie.Rows.Count > 0)
                {

                    Microsoft.Office.Interop.Excel.Application xcelApp = new Microsoft.Office.Interop.Excel.Application();
                    xcelApp.Application.Workbooks.Add(Type.Missing);

                    for (int i = 1; i < dgvCategorie.Columns.Count + 1; i++)
                    {
                        xcelApp.Cells[1, i] = dgvCategorie.Columns[i - 1].HeaderText;
                    }

                    for (int i = 0; i < dgvCategorie.Rows.Count; i++)
                    {
                        for (int j = 0; j < dgvCategorie.Columns.Count; j++)
                        {
                            xcelApp.Cells[i + 2, j + 1] = dgvCategorie.Rows[i].Cells[j].Value.ToString();
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
    }
}
