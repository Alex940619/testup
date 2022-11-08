using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data;
using UPAdmPre.BL;
using UPAdmPre.SL;

namespace UPAdmPre.Web.Admision
{
    public partial class frmRpt_ImprimirVoucher : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UIHelper.SessionActiva(Page);
            try
            {
                if (!Page.IsPostBack)
                {
                    if (Request.QueryString["AplicanteId"] != null)
                    {
                        Int32? AplicanteId = int.Parse(Request.QueryString["AplicanteId"]);
                        LocalReport localReport = null;

                        // Set the processing mode for the ReportViewer to Local
                        ReportViewer1.ProcessingMode = ProcessingMode.Local;

                        localReport = this.ProcesarReporteFormularioAdmision(localReport, AplicanteId);

                        // Create the sales order number report parameter
                        ReportParameter rpParamApellidoReporte = new ReportParameter();
                        rpParamApellidoReporte.Name = "ApplicationID";
                        rpParamApellidoReporte.Values.Add(AplicanteId.ToString());

                        // Set the report parameters for the report
                        localReport.EnableExternalImages = true;
                        localReport.SetParameters(new ReportParameter[] { rpParamApellidoReporte });
                        if (Request.QueryString["genpdf"] != null)
                        {
                            bool generaPDF = (int.Parse(Request.QueryString["genpdf"].ToString()) > 0 ? true : false);

                            if (generaPDF)
                            {
                                //code to render report as pdf document
                                string encoding = String.Empty;
                                string mimeType = String.Empty;
                                string extension = String.Empty;
                                Warning[] warnings = null;
                                string[] streamids = null;
                                //
                                byte[] byteArray = localReport.Render("PDF", null,
                                out mimeType, out encoding, out extension, out streamids, out warnings);
                                //
                                HttpContext.Current.Response.ContentType = "Application/pdf";
                                HttpContext.Current.Response.AddHeader("Content-Disposition",
                                "attachment; filename=FichaImprimirVoucher.pdf");
                                HttpContext.Current.Response.AddHeader("Content-Length",
                                byteArray.Length.ToString());
                                HttpContext.Current.Response.ContentType = "application/octet-stream";
                                HttpContext.Current.Response.BinaryWrite(byteArray);
                                HttpContext.Current.Response.BufferOutput = false;
                                HttpContext.Current.Response.End();
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private LocalReport ProcesarReporteFormularioAdmision(LocalReport localReport, Int32? AplicanteId)
        {
            ReporteBL oReporteBL = null;
            List<DataSet> ldsReporte = null;
            ReportDataSource dsItemReporte = null;

            localReport = ReportViewer1.LocalReport;
            //localReport.ReportPath = "Admision\\FrmAdmisionI.rdlc";
            localReport.ReportPath = "~\\Reportes\\RptImprimirVoucher.rdlc";

            oReporteBL = new ReporteBL();
            ldsReporte = oReporteBL.ImprimirVoucherPago(AplicanteId);

            dsItemReporte = new ReportDataSource();
            if (ldsReporte[int.Parse(UIConstantes.ESTRUCTURA_REPORTE_APLICANTE.ALUMNOS.ToString("D"))].Tables.Count > 0)
            {
                dsItemReporte = new ReportDataSource();
                dsItemReporte.Name = "DataSet1";
                dsItemReporte.Value = ldsReporte[int.Parse(UIConstantes.ESTRUCTURA_REPORTE_APLICANTE.ALUMNOS.ToString("D"))].Tables[0];
                localReport.DataSources.Add(dsItemReporte);
            }
            return localReport;
        }
    }
}