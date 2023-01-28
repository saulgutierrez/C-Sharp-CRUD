using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using Microsoft.Reporting.WinForms;

namespace Sol_Almacen.Presentacion
{
    public partial class Frm_Rpt_Articulos : Form
    {
        public Frm_Rpt_Articulos()
        {
            InitializeComponent();
        }
        #region "Mis Métodos"
        private void Listado()
        {
            MySqlConnection SqlCon = new MySqlConnection();
            try
            {                
                SqlCon = Conexion.getInstancia().CrearConexion();
                string sql_tarea = "select a.codigo_ar," +
                                   " a.descripcion_ar," +
                                   " a.marca_ar," +
                                   " b.descripcion_um," +
                                   " c.descripcion_ca," +
                                   " a.stock_actual" +                                  
                                   " from tb_articulos a " +
                                   " inner join tb_unidades_medidas b on a.codigo_um=b.codigo_um " +
                                   " inner join tb_categorias c on a.codigo_ca=c.codigo_ca " +                                 
                                   " where a.estado=1 " +
                                   " order by a.codigo_ar";
                MySqlDataAdapter da = new MySqlDataAdapter(sql_tarea,SqlCon);
                DataSet ds = new DataSet();
                da.Fill(ds);
                ReportDataSource fuente = new ReportDataSource("DataSet1", ds.Tables[0]);
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(fuente);
                reportViewer1.LocalReport.ReportEmbeddedResource = "Sol_Almacen.Presentacion.Rpt_articulos.rdlc";
                reportViewer1.LocalReport.Refresh();
                reportViewer1.Refresh();
                reportViewer1.RefreshReport();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if(SqlCon.State== ConnectionState.Open) SqlCon.Close();
            }
        }
        #endregion

        private void Frm_Rpt_Articulos_Load(object sender, EventArgs e)
        {

            //this.reportViewer1.RefreshReport();
            this.Listado();
        }
    }
}
