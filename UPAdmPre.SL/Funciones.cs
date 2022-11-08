using System.Web.UI.WebControls;

namespace UPAdmPre.SL
{
    public class Funciones
    {
        public static void cargarComboYSeleccione(DropDownList pobjDDL, object objFuente, string strColumnaTexto, string strColumnaValor, string strMsjeNinguno)
        {
            cargarCombo(pobjDDL, objFuente, strColumnaTexto, strColumnaValor);
            pobjDDL.Items.Insert(0, new ListItem(strMsjeNinguno, "0"));
        }

        public static void cargarCombo(DropDownList pobjDDL, object objFuente, string strColumnaTexto, string strColumnaValor)
        {
            pobjDDL.Items.Clear();
            pobjDDL.DataValueField = strColumnaValor;
            pobjDDL.DataTextField = strColumnaTexto;
            pobjDDL.DataSource = objFuente;
            pobjDDL.DataBind();
        }

        /*Ini:Christian Ramirez - REQ91569*/
        public static void CargarRadioButtonList(RadioButtonList radioButtonList, object dataSource
            , string dataValueField, string dataTextField)
        {
            radioButtonList.Items.Clear();
            radioButtonList.DataSource = dataSource;
            radioButtonList.DataValueField = dataValueField;
            radioButtonList.DataTextField = dataTextField;
            radioButtonList.DataBind();
        }
        /*Fin:Christian Ramirez - REQ91569*/
    }
}
