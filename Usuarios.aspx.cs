using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RestauracionesUTC.Clases;

namespace RestauracionesUTC
{
    public partial class Usuarios : System.Web.UI.Page
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
                using (SqlCommand cmd = new SqlCommand("SELECT *  FROM Usuarios"))
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
            int valor = Clases.Usuarios.INSERTAR_USUARIO(tNombre.Text, tCore.Text, tTel.Text);

            if (valor > 0)
            {
                alertas("El usuario fue agregado con exito");
                LlenarGrid();
            }
            else
            {
                alertas("Error al ingresar usuario");
            }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            int valor = Clases.Usuarios.BORRAR_USUARIOS_ID(int.Parse(tID.Text));

            if (valor > 0)
            {
                alertas("El usuario fue borrado con exito");
                LlenarGrid();
            }
            else
            {
                alertas("Error al borrar usuario");
            }

        }

        protected void Button3_Click(object sender, EventArgs e)
        {

            int valor = Clases.Usuarios.ACTUALIZAR_USUARIO_ID(int.Parse(tID.Text), tNombre.Text, tCore.Text, int.Parse(tTel.Text));

            if (valor > 0)
            {
                alertas("El usuario fue actualizado con exito");
                LlenarGrid();
            }
            else
            {
                alertas("Error al actualizar usuario");
            }

        }

        protected void Bconsulta_Click(object sender, EventArgs e)
        {
            int ID = int.Parse(tID.Text);
            string constr = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Usuarios WHERE UsuarioID ='" + ID + "'"))


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