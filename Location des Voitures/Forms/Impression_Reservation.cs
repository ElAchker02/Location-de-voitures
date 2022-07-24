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
    public partial class Impression_Reservation : Form
    {
        DateTime d;
        string cin ;
        string matricule ;
        public Impression_Reservation(DateTime date,string client,string voiture)
        {
            InitializeComponent();
             d = date;
             cin = client;
             matricule = voiture;
        }

        private void Impression_Reservation_Load(object sender, EventArgs e)
        {
            if (GLB.Connection.State == ConnectionState.Open)
                GLB.Connection.Close();
            GLB.Connection.Open();
            GLB.da = new SqlDataAdapter($"select  Reservations.Date_Debut,Reservations.Date_Fin,Clients.CIN,Nom_Complet,Sexe,Permis,Adress,Telephone,Email,Voitures.Matricule,Voitures.Annee_Procution,Couleur,Puissance,Cout_par_Jour,Marques.Marque,Modeles.Modele,Carburants.TypeC,Kilometrage,Categories.Categorie, Reservations.Km_Initiale,Reservations.Caution,Reservations.Supp_Km from Reservations,Voitures,Clients,Marques,Modeles,Carburants,Categories where Clients.CIN=Reservations.Client and Voitures.Matricule=Reservations.Matricule and Voitures.Marque=Marques.Id_Marque and Modeles.Id_Model=Voitures.Modele and Carburants.Id_Carburant=Voitures.Carburant and Categories.Id_Categorie=Voitures.Categorie and Reservations.Date_Debut='{d}' and Reservations.Client='{cin}' and Reservations.Matricule='{matricule}'", GLB.Connection);
            GLB.dtImperssion.Rows.Clear();
            GLB.da.Fill(GLB.dtImperssion);
            
            CrystalReport1 cr = new CrystalReport1();
            cr.SetDataSource(GLB.dtImperssion);
            crystalReportViewer1.ReportSource = cr;
            crystalReportViewer1.Refresh();
        }
    }
}
