using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestauracionesUTC
{
    public partial class Tecnicos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LlenarGrid();
            }
        }


        public void alertas(String texto)
        {
            string message = texto;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload=function(){");
            sb.Append("alert('");
            sb.Append(message);
            sb.Append("')};");
            sb.Append("</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());

        }

        protected void LlenarGrid()
        {
            string constr = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT *  FROM Tecnicos"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            datagrid.DataSource = dt;
                            datagrid.DataBind();  // actualizar el grid view
                        }
                    }
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int valor = Clases.Tecnicos.INSERTAR_TECNICO(tNombre.Text, tEspecialidad.Text);

            if (valor > 0)
            {
                alertas("El Tecnico fue agregado con exito");
                LlenarGrid();
            }
            else
            {
                alertas("Error al ingresar Tecnico");
            }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            int valor = Clases.Tecnicos.BORRAR_TECNICOS_ID(int.Parse(tID.Text));

            if (valor > 0)
            {
                alertas("El Tecnico fue borrado con exito");
                LlenarGrid();
            }
            else
            {
                alertas("Error al borrar Tecnico");
            }

        }

        protected void Button3_Click(object sender, EventArgs e)
        {

            int valor = Clases.Tecnicos.ACTUALIZAR_TECNICO_ID(int.Parse(tID.Text), tNombre.Text, tEspecialidad.Text);

            if (valor > 0)
            {
                alertas("El Tecnico fue actualizado con exito");
                LlenarGrid();
            }
            else
            {
                alertas("Error al actualizar Tecnico");
            }

        }

        protected void Bconsulta_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(tID.Text);
            string constr = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Tecnicos WHERE TecnicoID ='" + ID + "'"))


                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        datagrid.DataSource = dt;
                        datagrid.DataBind();  // actualizar el grid view
                    }
                }

            }
        }
    }

}
