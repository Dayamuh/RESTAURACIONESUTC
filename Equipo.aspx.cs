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
    public partial class Equipo : System.Web.UI.Page
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
                using (SqlCommand cmd = new SqlCommand("SELECT *  FROM Equipo"))
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
            int valor = Clases.Equipo.INSERTAR_EQUIPO(tTipE.Text, tModelo.Text, int.Parse(tIDU.Text));

            if (valor > 0)
            {
                alertas("El Equipo fue agregado con exito");
                LlenarGrid();
            }
            else
            {
                alertas("Error al ingresar Equipo");
            }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            int valor = Clases.Equipo.BORRAR_EQUIPO_ID(int.Parse(tID.Text));

            if (valor > 0)
            {
                alertas("El Equipo fue borrado con exito");
                LlenarGrid();
            }
            else
            {
                alertas("Error al borrar Equipo");
            }

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            int valor = Clases.Equipo.ACTUALIZAR_EQUIPO_ID( tTipE.Text, tModelo.Text, int.Parse(tIDU.Text));

            if (valor > 0)
            {
                alertas("El Equipo fue actualizado con exito");
                LlenarGrid();
            }
            else
            {
                alertas("Error al actualizar Equipo");
            }

        }

        protected void Bconsulta_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(tID.Text);
            string constr = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Equipo WHERE EquipoID ='" + ID + "'"))


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