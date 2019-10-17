using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Service_Import_and_Export
{
    public partial class Import : Form
    {
        BackgroundWorker worker;
        Dictionary<string, string> companies = new Dictionary<string, string>();
        public Import()
        {
            InitializeComponent();
            companies.Add("VEM", "VIVO ENERGY MAROC");
            companies.Add("SVL", "SHELL ET VIVO LUBRIFIANTS DU MAROC");
            companies.Add("VEAS", "VIVO ENERGY AFRICA SERVICES");
            companies.Add("SVLAS", "SHELL ET VIVOLUB AFRICA SERVICE (SVLAS)");
        }

        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            using(var SFD = new SaveFileDialog() { Filter = "Fichier Excel | *xlsx" , CheckFileExists = true, CheckPathExists = true, Title = "Chosissez un fichier"})
            {
                if(SFD.ShowDialog() == DialogResult.OK)
                {
                    txtFile.Text = SFD.FileName;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtFile.Text.Length > 0)
            {
                worker = new BackgroundWorker();
                worker.DoWork += Worker_DoWork;
                worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
                worker.RunWorkerAsync();
            }
            else
                MessageBox.Show("Veuillez choisir le fichier et le type d'importation",
                    "Fichier non choisi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnImporter.Invoke(new MethodInvoker(delegate
            {
                btnImporter.Enabled = true;
            }));
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            btnImporter.Invoke(new MethodInvoker(delegate
            {
                btnImporter.Enabled = false;
            }));
        }

        private void clearLog_Click(object sender, EventArgs e)
        {
            txt_ConsoleLog.Clear();
        }

        private void ImporterEmployees()
        {
            try
            {

                Dictionary<int, int> columns = new Dictionary<int, int>();

                using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(txtFile.Text)))
                {
                    var db = new CMIMEntities();
                    employees e;

                    var myWorksheet = excelPackage.Workbook.Worksheets[1];
                    var totalRows = myWorksheet.Dimension.End.Row;
                    var totalColumns = myWorksheet.Dimension.End.Column;
                    List<string> row;
                    int AdrNumber;

                    //  MessageBox.Show(totalRows.ToString());

                    row = myWorksheet.Cells[1, 1, 1, totalColumns].Select(c => c.Value == null ? string.Empty : c.Value.ToString()).ToList();
                    foreach (string s in row)
                    {
                        columns.Add(s, row.IndexOf(s));
                    }


                    for (int i = 2; i <= totalRows; i++)
                    {
                        row = myWorksheet.Cells[i, 1, i, totalColumns].Select(c => c.Value == null ? string.Empty : c.Value.ToString()).ToList();

                        // MessageBox.Show(row.Count.ToString());

                        AdrNumber = row[columns["ABAN8"]].Trim();
                        lbllAchieve.Invoke(new MethodInvoker(delegate
                        {
                            lbllAchieve.Text = (i - 1) + " / " + (totalRows - 1);
                        }));

                        C = db.Clients.Where(Cl => Cl.Address_Nbr == AdrNumber).FirstOrDefault();
                        if (C == null)
                        {
                            C = new Client();
                            C.Address_Nbr = AdrNumber;
                            C.Alphaname = row[columns["ABALPH"]];
                            if (row[columns["ABMCU"]] != null && row[columns["ABMCU"]].Trim().Length != 0)
                                C.Business_Unite = int.Parse(row[columns["ABMCU"]].Trim());
                            C.PaymentTerms = row[columns["A5TRAR01"]];
                            C.TypeDocAccepte = row[columns["A5RYIN01"]];
                            C.Adress1 = row[columns["ALADD1"]];
                            C.Adress2 = row[columns["ALADD2"]];
                            C.Adress3 = row[columns["ALADD3"]];
                            C.CodePostal = row[columns["ALADDZ"]];
                            C.Ville = row[columns["ALCTY1"]];
                            C.TaxRate = row[columns["A5TXA1"]];
                            C.TypeClient = row[columns["ABAC0102"]];
                            C.ParentNumber = row[columns["ABPA8"]];
                            C.ICE = row[columns["ABTX2"]];
                            C.ABAT1 = row[columns["ABAT1"]];
                            db.Clients.Add(C);
                        }
                        else
                        {
                            C.Alphaname = row[columns["ABALPH"]];
                            if (row[columns["ABMCU"]] != null && row[columns["ABMCU"]].Trim().Length != 0)
                                C.Business_Unite = int.Parse(row[columns["ABMCU"]].Trim());
                            C.PaymentTerms = row[columns["A5TRAR01"]];
                            C.TypeDocAccepte = row[columns["A5RYIN01"]];
                            C.Adress1 = row[columns["ALADD1"]];
                            C.Adress2 = row[columns["ALADD2"]];
                            C.Adress3 = row[columns["ALADD3"]];
                            C.CodePostal = row[columns["ALADDZ"]];
                            C.Ville = row[columns["ALCTY1"]];
                            C.TaxRate = row[columns["A5TXA1"]];
                            C.TypeClient = row[columns["ABAC0102"]];
                            C.ParentNumber = row[columns["ABPA8"]];
                            C.ICE = row[columns["ABTX2"]];
                            C.ABAT1 = row[columns["ABAT1"]];
                        }

                    }
                    db.SaveChanges();
                }

                using (var db = new VivoEnergy_FacturationEntities())
                {
                    Client client;
                    foreach (Depot D in db.Depots.ToList())
                    {
                        client = db.Clients.Where(C => C.Address_Nbr == D.CodeDepot).FirstOrDefault();
                        if (client == null)
                        {
                            client = new Client { Address_Nbr = D.CodeDepot, Alphaname = D.DepotName, isClient = false };
                            db.Clients.Add(client);
                        }
                        else
                        {
                            client.Address_Nbr = D.CodeDepot;
                            client.Alphaname = D.DepotName;
                            client.isClient = false;
                        }
                    }
                    db.SaveChanges();
                }


            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        MessageBox.Show("Msg: " + ve.ErrorMessage + "\n" + ve.PropertyName);
                    }
                }
            }
            catch (IOException exept)
            {
                MessageBox.Show(exept.Message, "Un Erreur a été survenue lors de l'ouverture et lecture du fichier", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Err)
            {
                MessageBox.Show(Err.Message.ToString(), "Un Erreur s'est produit lors de la mise à jour des clients", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }


}
