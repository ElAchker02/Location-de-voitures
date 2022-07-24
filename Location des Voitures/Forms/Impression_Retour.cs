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

namespace Location_des_Voitures.Forms
{
    public partial class Impression_Retour : Form
    {
        DateTime d;
        string cin;
        string matricule;
        public Impression_Retour(DateTime date, string client, string voiture)
        {
            InitializeComponent();
            d = date;
            cin = client;
            matricule = voiture;
        }

        private void Impression_Retour_Load(object sender, EventArgs e)
        {
            if (GLB.Total > 0)
            {
                if(GLB.Connection.State == ConnectionState.Open)
                    GLB.Connection.Close();
                GLB.Connection.Open();
                GLB.Cmd = new SqlCommand($"delete from print_retour ", GLB.Connection);
                GLB.Cmd.ExecuteNonQuery();
                GLB.Cmd = new SqlCommand($"insert into print_retour (Totale,Retour_Caution) values ({0 },{GLB.Total})", GLB.Connection);
                GLB.Cmd.ExecuteNonQuery();
                GLB.da = new SqlDataAdapter($"select  Reservations.Date_Debut,Reservations.Date_Fin,Clients.CIN,Clients.Nom_Complet,Clients.Sexe,Clients.Permis,Clients.Adress, Clients.Telephone, Clients.Email, Voitures.Matricule, Voitures.Annee_Procution, Voitures.Couleur, Voitures.Puissance,Voitures.Cout_par_Jour, Marques.Marque, Modeles.Modele, Carburants.TypeC, Voitures.Kilometrage,Categories.Categorie, Reservations.Km_Initiale, Reservations.Caution, Reservations.Supp_Km, print_retour.Totale, print_retour.Retour_Caution from Reservations, Voitures, Clients, Marques, Modeles, Carburants, Categories, print_retour where Clients.CIN = Reservations.Client and Voitures.Matricule = Reservations.Matricule and Voitures.Marque = Marques.Id_Marque and Modeles.Id_Model = Voitures.Modele and Carburants.Id_Carburant = Voitures.Carburant and Categories.Id_Categorie = Voitures.Categorie and Reservations.Date_Debut = '{d}' and Reservations.Client = '{cin}' and Reservations.Matricule = '{matricule}'", GLB.Connection);
                GLB.dtRetour.Rows.Clear();
                GLB.da.Fill(GLB.dtRetour);


                CrystalReport2_ cr = new CrystalReport2_();
                cr.SetDataSource(GLB.dtRetour);
                crystalReportViewer1.ReportSource = cr;
                crystalReportViewer1.Refresh();
            }
            else if (GLB.Total < 0)
            {
                if (GLB.Connection.State == ConnectionState.Open)
                    GLB.Connection.Close();
                GLB.Connection.Open();
                GLB.Cmd = new SqlCommand($"delete from print_retour ",GLB.Connection);
                GLB.Cmd.ExecuteNonQuery();
                GLB.Cmd = new SqlCommand($"insert into print_retour (Totale,Retour_Caution) values ({ GLB.Total*-1},{0})", GLB.Connection);
                GLB.Cmd.ExecuteNonQuery();
                GLB.da = new SqlDataAdapter($"select  Reservations.Date_Debut,Reservations.Date_Fin,Clients.CIN,Clients.Nom_Complet,Clients.Sexe,Clients.Permis,Clients.Adress, Clients.Telephone, Clients.Email, Voitures.Matricule, Voitures.Annee_Procution, Voitures.Couleur, Voitures.Puissance,Voitures.Cout_par_Jour, Marques.Marque, Modeles.Modele, Carburants.TypeC, Voitures.Kilometrage,Categories.Categorie, Reservations.Km_Initiale, Reservations.Caution, Reservations.Supp_Km, print_retour.Totale, print_retour.Retour_Caution from Reservations, Voitures, Clients, Marques, Modeles, Carburants, Categories, print_retour where Clients.CIN = Reservations.Client and Voitures.Matricule = Reservations.Matricule and Voitures.Marque = Marques.Id_Marque and Modeles.Id_Model = Voitures.Modele and Carburants.Id_Carburant = Voitures.Carburant and Categories.Id_Categorie = Voitures.Categorie and Reservations.Date_Debut = '{d}' and Reservations.Client = '{cin}' and Reservations.Matricule = '{matricule}'", GLB.Connection);
                GLB.dtRetour.Rows.Clear();
                GLB.da.Fill(GLB.dtRetour);
               
                //MessageBox.Show(GLB.dtRetour.Columns.Count.ToString());
                
                CrystalReport2_ cr = new CrystalReport2_();
                cr.SetDataSource(GLB.dtRetour);
                crystalReportViewer1.ReportSource = cr;
                crystalReportViewer1.Refresh();
            }
            else if (GLB.Total == 0)
            {
                if (GLB.Connection.State == ConnectionState.Open)
                    GLB.Connection.Close();
                GLB.Connection.Open();
                GLB.Cmd = new SqlCommand($"delete from print_retour ", GLB.Connection);
                GLB.Cmd.ExecuteNonQuery();
                GLB.Cmd = new SqlCommand($"insert into print_retour (Totale,Retour_Caution) values ({0},{0})", GLB.Connection);
                GLB.Cmd.ExecuteNonQuery();
                GLB.da = new SqlDataAdapter($"select  Reservations.Date_Debut,Reservations.Date_Fin,Clients.CIN,Clients.Nom_Complet,Clients.Sexe,Clients.Permis,Clients.Adress, Clients.Telephone, Clients.Email, Voitures.Matricule, Voitures.Annee_Procution, Voitures.Couleur, Voitures.Puissance,Voitures.Cout_par_Jour, Marques.Marque, Modeles.Modele, Carburants.TypeC, Voitures.Kilometrage,Categories.Categorie, Reservations.Km_Initiale, Reservations.Caution, Reservations.Supp_Km, print_retour.Totale, print_retour.Retour_Caution from Reservations, Voitures, Clients, Marques, Modeles, Carburants, Categories, print_retour where Clients.CIN = Reservations.Client and Voitures.Matricule = Reservations.Matricule and Voitures.Marque = Marques.Id_Marque and Modeles.Id_Model = Voitures.Modele and Carburants.Id_Carburant = Voitures.Carburant and Categories.Id_Categorie = Voitures.Categorie and Reservations.Date_Debut = '{d}' and Reservations.Client = '{cin}' and Reservations.Matricule = '{matricule}'", GLB.Connection);
                GLB.dtRetour.Rows.Clear();
                GLB.da.Fill(GLB.dtRetour);


                CrystalReport2_ cr = new CrystalReport2_();
                cr.SetDataSource(GLB.dtRetour);
                crystalReportViewer1.ReportSource = cr;
                crystalReportViewer1.Refresh();
            }
            GLB.Connection.Close();
        }
    }
}
