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
    public partial class Export : Form
    {
        private void setVisibility(RadioButton radioButton)
        {
            if(radioButton.Name == "rd_DossiersEnvoyee")
            {
                lblId.Enabled = false;
                txtId.Enabled = false;
            }
            else
            {
                lblId.Enabled = true;
                txtId.Enabled = true;
            }
        }
        public Export()
        {
            InitializeComponent();
        }

        private void clearLog_Click(object sender, EventArgs e)
        {
            txt_ConsoleLog.ResetText();
            output_Grid.Rows.Clear();
        }

        private void Options_CheckedChanged(object sender, EventArgs e)
        {
            setVisibility((RadioButton)sender);
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (GetChecked_RadioButton().Name != "rd_DossiersEnvoyee")
                if (txtId.Text.Trim().Length == 0)
                    MessageBox.Show("Veuillez saisir l'identifiant pour pouvoir générer le fichier", "ID manquant", MessageBoxButtons.OK, MessageBoxIcon.Error);
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.RunWorkerAsync(GetChecked_RadioButton());
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            txt_ConsoleLog.Invoke(new MethodInvoker(delegate
            {
                txt_ConsoleLog.AppendText(Environment.NewLine + "************ Fin de l'opération d'exportation ************");
            }));
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            txt_ConsoleLog.Invoke(new MethodInvoker(delegate
            {
                txt_ConsoleLog.ResetText();
                txt_ConsoleLog.AppendText("****************** Début d'exportation ******************");
            }));
            RadioButton chk_temp = (RadioButton)e.Argument;
            switch (chk_temp.Name)
            {
                case "rd_DossiersEnvoyee":
                    Export_EnvoyéACmim();
                    break;
                case "rd_Bordereau":
                    Export_Bordereau(int.Parse(txtId.Text));
                    break;
                case "rd_rembourssement":
                    Export_Rembourssement(int.Parse(txtId.Text));
                    break;
                case "rd_Regularisation":
                    Export_Regularisation(int.Parse(txtId.Text));
                    break;
            }
        }

        private RadioButton GetChecked_RadioButton()
        {
            if (rd_Bordereau.Checked)
                return rd_Bordereau;
            if (rd_DossiersEnvoyee.Checked)
                return rd_DossiersEnvoyee;
            if (rd_Regularisation.Checked)
                return rd_Regularisation;
            if (rd_rembourssement.Checked)
                return rd_rembourssement;
            return null;
        }

        private void CreateExcelFile(List<string[]> headerRow, List<object[]> data, string WorksheetName, string FileName)
        {
            if(!Directory.Exists(@"C:\CMIM Exportation"))
            {
                Directory.CreateDirectory(@"C:\CMIM Exportation");
                txt_ConsoleLog.Invoke(new MethodInvoker(delegate
                {
                    txt_ConsoleLog.AppendText(Environment.NewLine + @"Info: Création du répertoire 'C:\CMIM Exportation'");
                }));
            }
            

            string headerRange = "A1:" + Char.ConvertFromUtf32(headerRow[0].Length + 64) + "1";

            using (ExcelPackage excel = new ExcelPackage())
            {
                excel.Workbook.Worksheets.Add(WorksheetName);

                var excelWorkSheet = excel.Workbook.Worksheets[WorksheetName];

                excelWorkSheet.Cells[headerRange].LoadFromArrays(headerRow);
                excelWorkSheet.Cells[headerRange].Style.Font.Bold = true;
                excelWorkSheet.Cells[headerRange].Style.Font.Size = 9;
                excelWorkSheet.Cells[headerRange].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                excelWorkSheet.Cells[headerRange].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Bottom;
                excelWorkSheet.Cells[headerRange].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                excelWorkSheet.Cells[headerRange].Style.WrapText = true;
                excelWorkSheet.Cells[headerRange].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                excelWorkSheet.Cells[headerRange].Style.Fill.BackgroundColor.SetColor(1, 244, 176, 132);
                excelWorkSheet.Row(1).Height = 37;

                excelWorkSheet.Cells[2, 1].LoadFromArrays(data);

                string ExcelFileName = @"C:\CMIM Exportation\" + FileName + "____" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute +
                     "_" + DateTime.Now.Second + ".xlsx";

                FileInfo excelFile = new FileInfo(ExcelFileName);
                excel.SaveAs(excelFile);

                output_Grid.Invoke(new MethodInvoker(delegate
                {
                    output_Grid.Rows.Add(FileName, DateTime.Now.ToString("dd/MM/yyyy HH:mm"), "Excel", ExcelFileName);
                }));

                if(File.Exists(ExcelFileName))
                System.Diagnostics.Process.Start(ExcelFileName);
                else
                    txt_ConsoleLog.Invoke(new MethodInvoker(delegate
                    {
                        txt_ConsoleLog.AppendText(Environment.NewLine + "Err: Le fichier a été supprimé ou déplacé du répertoire --> " + ExcelFileName);
                    }));


            }

        }

        private void Export_Rembourssement(int idRembourssment)
        {
            var db = new CMIMEntities();
            var remboursseement = db.Remboursser
                .Where(R => R.rembourssementId == idRembourssment)
                .Select(R => new RembourssementVue
                {
                    remboursser = R,
                    Dossiers = R.list.Select(L => L.Dossiers)
                })
                .FirstOrDefault();

            if(remboursseement == null)
            {
                txt_ConsoleLog.Invoke(new MethodInvoker(delegate
                {
                    txt_ConsoleLog.AppendText(Environment.NewLine + "Err: Le code de rembourssement n'existe pas dans la base");
                }));
                return;
            }

            txt_ConsoleLog.Invoke(new MethodInvoker(delegate
            {
                txt_ConsoleLog.AppendText(Environment.NewLine + "Info: Récupération des informations du remboursement N° " + idRembourssment);
            }));

            var headerRow = new List<string[]>()
                        {
                            new string[] {"N° Rembourssement", "Date Rembourssement", "Numéro Dossier",
                                "Montant Total", "Avance", "MT Remboursé" }
                        };

            var cellData = new List<object[]>();

            foreach (var item in remboursseement.Dossiers)
            {
                txt_ConsoleLog.Invoke(new MethodInvoker(delegate
                {
                    txt_ConsoleLog.AppendText(Environment.NewLine + "Info: Ajout du dossier N° " + item.referance + " au fichier");
                }));
                object[] objet = new object[]
                {
                    remboursseement.remboursser.rembourssementId, remboursseement.remboursser.DateRembourssement.ToLongDateString(),
                    item.referance, item.montant, item.avance, item.rembourse
                };
                cellData.Add(objet);
            }

            CreateExcelFile(headerRow, cellData, "Rembourssement", "Rembourssement_N_" + idRembourssment);

        }

        private void Export_EnvoyéACmim()
        {
            var db = new CMIMEntities();
            var Dossiers = db.Dossiers.Where(B => B.etat == "Envoyé à la CMIM").ToList();

            if (Dossiers == null || Dossiers.Count == 0)
            {
                txt_ConsoleLog.Invoke(new MethodInvoker(delegate
                {
                    txt_ConsoleLog.AppendText(Environment.NewLine + "Err: Aucun dossier(s) envoyé(s) à la CMIM");
                }));
                return;
            }

            txt_ConsoleLog.Invoke(new MethodInvoker(delegate
            {
                txt_ConsoleLog.AppendText(Environment.NewLine + "Info: Nombre des dossiers envoyés à la CMIM est: " + Dossiers.Count);
            }));

            var headerRow = new List<string[]>()
                        {
                            new string[] {"N° Dossier", "Date création" , "Date Dossier", "Etat", "Employé",
                                "Montant Total", "Avance", "MT Radio", "MT Analyse", "MT Soins", "MT Prothèse",
                                "Devis", "MT Visite M", "MT Pharmacie", "MT Visite L", "MT Cadre",
                            "Avance", "Rembourse"}
                        };

            var cellData = new List<object[]>();

            foreach (var item in Dossiers)
            {
                txt_ConsoleLog.Invoke(new MethodInvoker(delegate
                {
                    txt_ConsoleLog.AppendText(Environment.NewLine + "Info: Ajout du dossier N° " + item.referance + " au fichier");
                }));
                object[] objet = new object[]
                {
                    item.referance, item.date.ToString("dd/MM/yyyy HH:mm"), item.dateDossier.ToString("dd/MM/yyyy HH:mm"),
                     item.etat, item.employeematricule, item.montant, item.avance, item.Mt_radio, item.Mt_analyse,
                    item.MtSoins_D, item.MtProthese_D, item.Devis_D, item.MtVisite_M, item.MtPharmacie_M, item.MtVisite_L,
                    item.MtCadre_L, item.avance, item.rembourse
                };
                cellData.Add(objet);
            }

            CreateExcelFile(headerRow, cellData, "Dossiers envoyés à la CMIM", "ENVOYES_A_CMIM");

        }

        private void Export_Regularisation(int idRegularisation)
        {
            var db = new CMIMEntities();
            var Regularisation = db.Regularisation
                .Where(R => R.RegularisationID == idRegularisation)
                .FirstOrDefault();

            if (Regularisation == null)
            {
                txt_ConsoleLog.Invoke(new MethodInvoker(delegate
                {
                    txt_ConsoleLog.AppendText(Environment.NewLine + "Err: Le code du régularisation n'existe pas dans la base");
                }));
                return;
            }

            txt_ConsoleLog.Invoke(new MethodInvoker(delegate
            {
                txt_ConsoleLog.AppendText(Environment.NewLine + "Info: Récupération des informations du régularisation N° " + idRegularisation);
            }));

            var headerRow = new List<string[]>()
                        {
                            new string[] {"N° Régularisation", "Date Régularisation", "Mois", "Total régularisation",
                                "Matricule employée", "Total régularisé de l'employé"}
                        };

            var cellData = new List<object[]>();

            foreach (var item in Regularisation.Regul_Emp)
            {
                txt_ConsoleLog.Invoke(new MethodInvoker(delegate
                {
                    txt_ConsoleLog.AppendText(Environment.NewLine + "Info: Ajout de l'employé N° " + item.EmployeeMatricule + " au fichier");
                }));
                object[] objet = new object[]
                {
                    idRegularisation, Regularisation.dateRegularisation.ToString("dd-MM-yyyy HH:mm"), Regularisation.Mois,
                    Regularisation.Total, item.EmployeeMatricule, item.Total
                };
                cellData.Add(objet);
            }

            CreateExcelFile(headerRow, cellData, "Régularisation", "Régularisation_N_" + idRegularisation);
        }

        private void Export_Bordereau(int idBordereau)
        {
            var db = new CMIMEntities();
            var bordereau = db.Bordereau.Where(B => B.BordereauId == idBordereau).FirstOrDefault();

            if (bordereau == null)
            {
                txt_ConsoleLog.Invoke(new MethodInvoker(delegate
                {
                    txt_ConsoleLog.AppendText(Environment.NewLine + "Err: Le code du bordereau n'existe pas dans la base");
                }));
                return;
            }

            txt_ConsoleLog.Invoke(new MethodInvoker(delegate
            {
                txt_ConsoleLog.AppendText(Environment.NewLine + "Info: Récupération des informations du bordereau N° " + idBordereau);
            }));

            var headerRow = new List<string[]>()
                        {
                            new string[] {"N° Bordereau", "Société", "Date création", "N° Dossier", "Etat", "Employé",
                                "Montant Total", "Avance", "MT Radio", "MT Analyse", "MT Soins", "MT Prothèse",
                                "Devis", "MT Visite M", "MT Pharmacie", "MT Visite L", "MT Cadre",
                            "Avance", "Rembourse"}
                        };

            var cellData = new List<object[]>();

            foreach (var item in bordereau.Dossiers)
            {
                txt_ConsoleLog.Invoke(new MethodInvoker(delegate
                {
                    txt_ConsoleLog.AppendText(Environment.NewLine + "Info: Ajout du dossier N° " + item.referance + " au fichier");
                }));
                object[] objet = new object[]
                {
                    item.BordereauId, bordereau.Company, bordereau.DateCreation.ToString("dd/MM/yyyy HH:mm"),
                    item.referance, item.etat, item.employeematricule, item.montant, item.avance, item.Mt_radio, item.Mt_analyse,
                    item.MtSoins_D, item.MtProthese_D, item.Devis_D, item.MtVisite_M, item.MtPharmacie_M, item.MtVisite_L,
                    item.MtCadre_L, item.avance, item.rembourse
                };
                cellData.Add(objet);
            }

            CreateExcelFile(headerRow, cellData, "Bordereau", "Bordereau_N_" + idBordereau);

        }

        private void output_Grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                System.Diagnostics.Process.Start(output_Grid.Rows[e.RowIndex].Cells[3].Value.ToString());
            }
        }
    }

    class RembourssementVue
    {
        public Remboursser remboursser;
        public IEnumerable<Dossiers> Dossiers;
    }

}

