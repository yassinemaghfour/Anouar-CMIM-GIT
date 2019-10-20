using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Validation;
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
            companies.Add("VIVO ENERGY MAROC", "VEM");
            companies.Add( "SHELL ET VIVO LUBRIFIANTS DU MAROC", "SVL");
            companies.Add("VIVO ENERGY AFRICA SERVICES", "VEAS");
            companies.Add( "SHELL ET VIVOLUB AFRICA SERVICE (SVLAS)", "SVLAS");
        }

        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            using(var SFD = new OpenFileDialog() { Filter = "Fichier Excel | *xlsx" , CheckFileExists = true, CheckPathExists = true, Title = "Chosissez un fichier"})
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
            txt_ConsoleLog.Invoke(new MethodInvoker(delegate
            {
                txt_ConsoleLog.AppendText(Environment.NewLine + "********************* Fin de l'opération d'importation *************************");
            }));
            
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            btnImporter.Invoke(new MethodInvoker(delegate
            {
                btnImporter.Enabled = false;
            }));
            txt_ConsoleLog.Invoke(new MethodInvoker(delegate
            {
                txt_ConsoleLog.ResetText();
                txt_ConsoleLog.AppendText("************************** Début d'importation  **************************");
            }));
            if (rd_ImportEmployees.Checked)
                ImporterEmployees();
            else
                ImporterRembourssement();
        }

        private void ImporterRembourssement()
        {
            try
            {

                Dictionary<string, int> columns = new Dictionary<string, int>();

                using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(txtFile.Text)))
                {
                    var db = new CMIMEntities();

                    var myWorksheet = excelPackage.Workbook.Worksheets[1];
                    var totalRows = myWorksheet.Dimension.End.Row;
                    var totalColumns = myWorksheet.Dimension.End.Column;
                    List<string> row;
                    bool isAddedRemourssement = false;
                    Remboursser remboursser = null;
                    Dossiers dossiers;
                    list list = null;
                    string referenceDoss;


                    row = myWorksheet.Cells[1, 1, 1, totalColumns].Select(c => c.Value == null ? string.Empty : c.Value.ToString()).ToList();

                    foreach (string s in row)
                    {
                        columns.Add(s, row.IndexOf(s));
                    }


                    for (int i = 2; i <= totalRows; i++)
                    {
                        
                        if (!isAddedRemourssement)
                        {
                            remboursser = new Remboursser()
                            {
                                DateRembourssement = DateTime.Now
                            };
                            db.Remboursser.Add(remboursser);
                            db.SaveChanges();
                            txt_ConsoleLog.Invoke(new MethodInvoker(delegate
                            {
                                txt_ConsoleLog.AppendText(Environment.NewLine + "Info: Ajout du rembourssement à la base sous numéro:  " + remboursser.rembourssementId + " .");
                            }));
                            isAddedRemourssement = true;
                        }
                        row = myWorksheet.Cells[i, 1, i, totalColumns].Select(c => c.Value == null ? string.Empty : c.Value.ToString()).ToList();

                        referenceDoss = row[columns["Dossier"]];

                        dossiers = db.Dossiers.Where(D => D.referance == referenceDoss).FirstOrDefault();
                        if (dossiers != null)
                        {
                            if (dossiers.etat == "Envoyé à la CMIM")
                            {
                                dossiers.rembourse = double.Parse(row[columns["Montant"]].Trim());
                                dossiers.rembourssementId = remboursser.rembourssementId;
                                dossiers.etat = "Remboursé";

                                list = new list()
                                {
                                    Dossierreferance = dossiers.referance,
                                    montant = dossiers.rembourse,
                                    RembouserembourssementId = remboursser.rembourssementId
                                };

                                db.list.Add(list);

                               
                                db.SaveChanges();

                                txt_ConsoleLog.Invoke(new MethodInvoker(delegate
                                {
                                    txt_ConsoleLog.AppendText(Environment.NewLine + "Info: Modification du dossier: " + dossiers.referance + " au statut 'REMBOURSE' avec le montant de rembourssement = " + list.montant + " DHS.");
                                }));
                            }
                            else
                                txt_ConsoleLog.Invoke(new MethodInvoker(delegate
                                {
                                    txt_ConsoleLog.AppendText(Environment.NewLine + "Err: Le statut du dossier doit être 'Envoyé à la CMIM' avant d'importer le remboussement du dossier.");
                                }));

                        }
                        else
                        {
                            txt_ConsoleLog.Invoke(new MethodInvoker(delegate
                            {
                                txt_ConsoleLog.AppendText(Environment.NewLine + "Err: Le dossier " + row[columns["Dossier"]] + " n'existe pas dans la base de donnée.");
                            }));

                        }

                    }
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
    

        private void clearLog_Click(object sender, EventArgs e)
        {
            txt_ConsoleLog.ResetText();
        }

        private void ImporterEmployees()
        {
            try
            {

                Dictionary<string, int> columns = new Dictionary<string, int>();

                using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(txtFile.Text)))
                {
                    var db = new CMIMEntities();
                    employees e;

                    var myWorksheet = excelPackage.Workbook.Worksheets[1];
                    var totalRows = myWorksheet.Dimension.End.Row;
                    var totalColumns = myWorksheet.Dimension.End.Column;
                    List<string> row;
                    string matriculeEmployee;


                    row = myWorksheet.Cells[4, 1, 4, totalColumns].Select(c => c.Value == null ? string.Empty : c.Value.ToString()).ToList();

                    foreach (string s in row)
                    {
                        columns.Add(s, row.IndexOf(s));
                    }

                    Users users = db.Users.Where(U => U.Role == "Admin").FirstOrDefault();


                    for (int i = 5; i <= totalRows; i++)
                    {
                        row = myWorksheet.Cells[i, 1, i, totalColumns].Select(c => c.Value == null ? string.Empty : c.Value.ToString()).ToList();


                        matriculeEmployee = row[columns["Id Num Personne"]].Trim();

                        e = db.employees.Where(emp => emp.matricule == matriculeEmployee).FirstOrDefault();
                        if (e == null)
                        {
                            if (companies.ContainsKey(row[columns["Raison Sociale Col"]]))
                            {
                                e = new employees()
                                {
                                    matricule = matriculeEmployee,
                                    first_name = row[columns["Nom Per"]],
                                    last_name = row[columns["Prenom Per"]],
                                    matriculecmim = matriculeEmployee,
                                    PlaceplacdeId = 1,
                                    company = companies[row[columns["Raison Sociale Col"]]],
                                    UserId = users.Id
                                };
                                db.employees.Add(e);
                                txt_ConsoleLog.Invoke(new MethodInvoker(delegate
                                {
                                    txt_ConsoleLog.AppendText(Environment.NewLine + "Info: Ajout effectué de l'employé: " + e.last_name + " " + e.first_name + " .");
                                }));
                                db.SaveChanges();
                            }
                            else
                                txt_ConsoleLog.Invoke(new MethodInvoker(delegate
                                {
                                    txt_ConsoleLog.AppendText(Environment.NewLine + "Err: La société de l'employée n'est pas correct.");
                                }));
                           
                        }
                        else
                        {
                            txt_ConsoleLog.Invoke(new MethodInvoker(delegate
                            {
                                txt_ConsoleLog.AppendText(Environment.NewLine + "Err: L'employé " + row[columns["Nom Per"]] + " " + row[columns["Prenom Per"]] + " existe déjà dans la base de donnée.");
                            }));
                            
                        }

                    }
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
