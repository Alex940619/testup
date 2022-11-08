using System;
using System.Collections.Generic;
using System.Data;
using UPAdmPre.BL;
using UPAdmPre.SL;

namespace UPAdmPre.Web.Helper
{
    public class UtilStepAdmision
    {
        public class StepsAdmision
        {
            public int Paso { get; set; }
            public string IdFormulario { get; set; }
            public string NombreFicha { get; set; }
            public string NombreFormulario { get; set; }
            public int IsComplete { get; set; }
            public int Visitado { get; set; }

            public ListStepsAdmision listStepsAdmision { get; set; }
        }

        public class ListStepsAdmision : List<StepsAdmision>
        {

        }

        public class Helper<TClass> where TClass : StepsAdmision, new()
        {
            private int? modalidadId;
            private int? applicationId;
            private string currentWebForm;
            TClass stepsAdmision;

            public static Helper<TClass> Instance
            {
                get { return new Helper<TClass>(); }
            }

            public Helper<TClass> Configure(int? modalidadId, int? applicationId, string currentWebForm)
            {
                this.modalidadId = UIConvertNull.Int32(modalidadId);
                this.applicationId = UIConvertNull.Int32(applicationId);
                this.currentWebForm = currentWebForm;
                stepsAdmision = new TClass()
                {
                    listStepsAdmision = new ListStepsAdmision()
                };

                return this;
            }

            public TClass Get()
            {
                try
                {
                    DataTable dt = new GeneralBL().GetPasosInscripcion(modalidadId, applicationId);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TClass item = new TClass();
                        item.Paso = Convert.ToInt32(dt.Rows[i]["Paso"]);
                        item.IdFormulario = dt.Rows[i]["IdFormulario"].ToString();
                        item.NombreFicha = dt.Rows[i]["NombreFicha"].ToString();
                        item.NombreFormulario = dt.Rows[i]["NombreFormulario"].ToString();
                        item.IsComplete = Convert.ToInt32(dt.Rows[i]["IsCompleto"]);
                        item.Visitado = Convert.ToInt32(dt.Rows[i]["Visitado"]);

                        if (item.NombreFormulario.Equals(currentWebForm))
                        {
                            stepsAdmision.Paso = item.Paso;
                            stepsAdmision.IdFormulario = item.IdFormulario;
                            stepsAdmision.NombreFicha = item.NombreFicha;
                            stepsAdmision.NombreFormulario = item.NombreFormulario;
                            stepsAdmision.IsComplete = item.IsComplete;
                            stepsAdmision.Visitado = item.Visitado;
                        }

                        stepsAdmision.listStepsAdmision.Add(item);
                    }

                    return stepsAdmision;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    modalidadId = null;
                    currentWebForm = null;
                    stepsAdmision = null;
                }
            }

            public Helper<TClass> GetForHtml()
            {
                try
                {
                    DataTable dt = new GeneralBL().GetPasosInscripcion(modalidadId, applicationId);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TClass item = new TClass();
                        item.Paso = Convert.ToInt32(dt.Rows[i]["Paso"]);
                        item.IdFormulario = dt.Rows[i]["IdFormulario"].ToString();
                        item.NombreFicha = dt.Rows[i]["NombreFicha"].ToString();
                        item.NombreFormulario = dt.Rows[i]["NombreFormulario"].ToString();
                        item.IsComplete = Convert.ToInt32(dt.Rows[i]["IsCompleto"]);
                        item.Visitado = Convert.ToInt32(dt.Rows[i]["Visitado"]);

                        if (item.NombreFormulario.Equals(currentWebForm))
                        {
                            stepsAdmision.Paso = item.Paso;
                            stepsAdmision.IdFormulario = item.IdFormulario;
                            stepsAdmision.NombreFicha = item.NombreFicha;
                            stepsAdmision.NombreFormulario = item.NombreFormulario;
                            stepsAdmision.IsComplete = item.IsComplete;
                            stepsAdmision.Visitado = item.Visitado;
                        }

                        stepsAdmision.listStepsAdmision.Add(item);
                    }

                    return this;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public string GetHtml()
            {
                string bodyHtml = "<div class='wrap'>";

                bodyHtml = string.Concat(bodyHtml, "<ul class='pager pager" + stepsAdmision.listStepsAdmision.Count + "'>");

                foreach (var item in stepsAdmision.listStepsAdmision)
                {
                    if (item.NombreFicha.Equals(stepsAdmision.NombreFicha))
                    {
                        bodyHtml = string.Concat(bodyHtml, "<li class='complete complete-current'> ");
                        bodyHtml = string.Concat(bodyHtml, "<a href='" + ((item.Visitado == 1) ? item.NombreFormulario : "#") + "' class='current'>" +
                                                                "<span class='step-current'>" + item.Paso + "</span>" +
                                                                ((item.IsComplete == 1) ? "<span class='checklist-complete'> <i class='fa fa-check' aria-hidden='true'></i> </span>" : string.Empty) +
                                                           "</a><p>" + item.NombreFicha + "</p>");
                        bodyHtml = string.Concat(bodyHtml, "</li>");
                    }
                    else
                    {
                        bodyHtml = string.Concat(bodyHtml, "<li class='" + ((item.Visitado == 1) ? "complete" : "incomplete") + "'> ");
                        bodyHtml = string.Concat(bodyHtml, "<a href='" + ((item.Visitado == 1) ? item.NombreFormulario : "#") + "' class='" + ((item.Visitado == 1) ? "current" : string.Empty) + "' data-tooltip title='" + item.NombreFicha + "'>" +
                                                                "<span class='step-complete'>" + item.Paso + "</span>" +
                                                                ((item.IsComplete == 1) ? "<span class='checklist-complete'> <i class='fa fa-check' aria-hidden='true'></i> </span>" : string.Empty) +
                                                           "</a>");
                        bodyHtml = string.Concat(bodyHtml, "</li>");
                    }
                }

                bodyHtml = string.Concat(bodyHtml, "</ul>");
                bodyHtml = string.Concat(bodyHtml, "</div>");

                return bodyHtml;
            }
        }
    }
}