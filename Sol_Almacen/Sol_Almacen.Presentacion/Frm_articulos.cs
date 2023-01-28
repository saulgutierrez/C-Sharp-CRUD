using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sol_Almacen.Presentacion
{
    public partial class Frm_articulos : Form
    {
        public Frm_articulos()
        {
            InitializeComponent();
        }
        #region "Mis variables"
        int nCodigo_ar = 0;
        int nEstadoguarda = 0; // Conocer si estamos creando un nuevo registro, o actualizando uno existente
        int nCodigo_um = 0;
        int nCodigo_ca = 0;
        #endregion

        #region "Mis métodos"

        private void Formato_ar()
        {
            // Personalizar propiedades de cada columna del DataGridView
            Dgv_articulos.Columns[0].Width = 80;
            Dgv_articulos.Columns[0].HeaderText = "CÓDIGO";
            Dgv_articulos.Columns[1].Width = 250;
            Dgv_articulos.Columns[1].HeaderText = "ARTÍCULO";
            Dgv_articulos.Columns[2].Width = 150;
            Dgv_articulos.Columns[2].HeaderText = "MARCA";
            Dgv_articulos.Columns[3].Width = 80;
            Dgv_articulos.Columns[3].HeaderText = "MEDIDA";
            Dgv_articulos.Columns[4].Width = 150;
            Dgv_articulos.Columns[4].HeaderText = "CATEGORÍA";
            Dgv_articulos.Columns[5].Width = 150;
            Dgv_articulos.Columns[5].HeaderText = "STOCK ACTUAL";
            Dgv_articulos.Columns[6].Visible = false;
            Dgv_articulos.Columns[7].Visible = false;
        }
        // Provee informacion al DataGridView
        private void Listado_ar(string cTexto)
        {
            D_Articulos Datos = new D_Articulos();
            Dgv_articulos.DataSource = Datos.Listado_ar(cTexto);
            this.Formato_ar(); // Llamada a la funcion formato
        }

        private void Estado_texto(bool lEstado)
        {
            Txt_descripcion_ar.ReadOnly = !lEstado;
            Txt_marca_ar.ReadOnly = !lEstado;
            Txt_stock_actual.ReadOnly = !lEstado;
        }

        private void Estado_botones_procesos(bool lEstado)
        {
            // Se habilita el boton de busqueda, se visualizan los botones de guardado de datos, y permite introducir texto en la barra de busqueda
            // dependiendo del evento que lo desencadene
            // Si se hace clic en el boton "Nuevo", se visualizaran los botones de cancelar y guardar
            Btn_lupa_um.Enabled = lEstado;
            Btn_lupa_ca.Enabled = lEstado;

            Btn_cancelar.Visible = lEstado;
            Btn_guardar.Visible = lEstado;

            //Otros detalles
            Txt_buscar.ReadOnly = lEstado;
            Btn_buscar.Enabled = !lEstado;
            Dgv_articulos.Enabled = !lEstado;
        }

        private void Estado_botones_principales(bool lEstado)
        {
            // Dependiendo del valor del parametro pasado por la funcion que le invoque, cada boton se habilitara o deshabilitara, segun sea
            // el caso
            Btn_nuevo.Enabled = lEstado;
            Btn_actualizar.Enabled = lEstado;
            Btn_eliminar.Enabled = lEstado;
            Btn_reporte.Enabled = lEstado;
            Btn_salir.Enabled = lEstado;
        }

        private void Limpia_texto()
        {
            Txt_descripcion_ar.Text = "";
            Txt_marca_ar.Text = "";
            Txt_descripcion_um.Text = "";
            Txt_descripcion_ca.Text = "";
            Txt_stock_actual.Text = "0";
        }

        private  void Selecciona_item()
        {
            // Si se hace clic en el boton actualizar, sin haber seleccionado una celda del DataGridView
            if (string.IsNullOrEmpty(Convert.ToString(Dgv_articulos.CurrentRow.Cells["codigo_ar"].Value)))
            {
                MessageBox.Show("Selecciona un registro",
                                "Aviso del Sistema",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
            }
            else
            {
                // Si se ha hecho clic en el boton actualizar, seleccionando una celda, se copian los valores en las cajas de texto
                this.nCodigo_ar = Convert.ToInt32(Dgv_articulos.CurrentRow.Cells["codigo_ar"].Value);
                Txt_descripcion_ar.Text = Convert.ToString(Dgv_articulos.CurrentRow.Cells["descripcion_ar"].Value);
                Txt_marca_ar.Text = Convert.ToString(Dgv_articulos.CurrentRow.Cells["marca_ar"].Value);
                Txt_descripcion_um.Text = Convert.ToString(Dgv_articulos.CurrentRow.Cells["descripcion_um"].Value);
                Txt_descripcion_ca.Text = Convert.ToString(Dgv_articulos.CurrentRow.Cells["descripcion_ca"].Value);
                Txt_stock_actual.Text = Convert.ToString(Dgv_articulos.CurrentRow.Cells["stock_actual"].Value);
                this.nCodigo_um= Convert.ToInt32(Dgv_articulos.CurrentRow.Cells["codigo_um"].Value);
                this.nCodigo_ca = Convert.ToInt32(Dgv_articulos.CurrentRow.Cells["codigo_ca"].Value);
            }
        }
        #endregion

        #region  "Métodos para Medidas y Categorías"
        private void Formato_um()
        {
            Dgv_um.Columns[0].Width = 200;
            Dgv_um.Columns[0].HeaderText = "MEDIDAS";
            Dgv_um.Columns[1].Visible = false;
        }
        private void Listado_um()
        {
            D_Articulos Datos = new D_Articulos();
            Dgv_um.DataSource = Datos.Listado_um();
            this.Formato_um();
        }

        private void Formato_ca()
        {
            Dgv_ca.Columns[0].Width = 200;
            Dgv_ca.Columns[0].HeaderText = "CATEGORÍAS";
            Dgv_ca.Columns[1].Visible = false;
        }
        private void Listado_ca()
        {
            D_Articulos Datos = new D_Articulos();
            Dgv_ca.DataSource = Datos.Listado_ca();
            this.Formato_ca();
        }

        private void Selecciona_item_um()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Dgv_um.CurrentRow.Cells["codigo_um"].Value)))
            {
                MessageBox.Show("Selecciona un elemento de la lista",
                                "Aviso del Sistema",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
            }
            else
            {
                this.nCodigo_um = Convert.ToInt32(Dgv_um.CurrentRow.Cells["codigo_um"].Value);
                Txt_descripcion_um.Text = Convert.ToString(Dgv_um.CurrentRow.Cells["descripcion_um"].Value);

            }
        }

        private void Selecciona_item_ca()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Dgv_ca.CurrentRow.Cells["codigo_ca"].Value)))
            {
                MessageBox.Show("Selecciona un elemento de la lista",
                                "Aviso del Sistema",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
            }
            else
            {
                this.nCodigo_ca = Convert.ToInt32(Dgv_ca.CurrentRow.Cells["codigo_ca"].Value);
                Txt_descripcion_ca.Text = Convert.ToString(Dgv_ca.CurrentRow.Cells["descripcion_ca"].Value);

            }
        }
        #endregion

        private void Frm_articulos_Load(object sender, EventArgs e)
        {
            // Cargar la informacion que contiene el dataGridView en cuanto se inicie la aplicacion
            this.Listado_ar("%");
            this.Listado_um();
            this.Listado_ca();
        }

        private void Btn_buscar_Click(object sender, EventArgs e)
        {
            // Concatena la entrada en la caja de texto con todo el contenido del dataGridView para buscar coincidencias en esta
            this.Listado_ar("%"+Txt_buscar.Text.Trim()+"%");
        }

        private void Btn_nuevo_Click(object sender, EventArgs e)
        {
            nEstadoguarda = 1; //Nuevo Registro
            this.Limpia_texto();
            // Activa los campos y botones correspondientes para permitir la entrada de un registro
            this.Estado_texto(true);
            this.Estado_botones_procesos(true);
            this.Estado_botones_principales(false);
            Txt_descripcion_ar.Focus();
        }

        private void Btn_cancelar_Click(object sender, EventArgs e)
        {
            nCodigo_ar = 0;
            nEstadoguarda = 0;
            // Se activan y desactivan los botones correspondientes, para reingresar um registro, y se limpian las cajas de texto
            this.Limpia_texto();
            this.Estado_texto(false);
            this.Estado_botones_procesos(false);
            this.Estado_botones_principales(true);
            Txt_buscar.Focus();
        }

        private void Btn_guardar_Click(object sender, EventArgs e)
        {
            // Cargar informacion desde los campos de texto
            string Rpta = "";
            P_Articulos oAr = new P_Articulos();
            oAr.Codigo_ar = nCodigo_ar;
            oAr.Descripcion_ar = Txt_descripcion_ar.Text.Trim();
            oAr.Marca_ar = Txt_marca_ar.Text.Trim();
            oAr.Codigo_um = this.nCodigo_um;
            oAr.Codigo_ca = this.nCodigo_ca;
            // Cast de tipos de texto a decimal
            oAr.Stock_actual =Convert.ToDecimal(Txt_stock_actual.Text);
            // Obtener la fecha del sistema, en el mismo formato que ofrece MySQL
            oAr.Fecha_crea = DateTime.Now.ToString("yyyy-MM-dd");
            oAr.Fecha_modifica = DateTime.Now.ToString("yyyy-MM-dd");

            D_Articulos Datos = new D_Articulos();
            Rpta = Datos.Guardar_ar(nEstadoguarda,oAr);
            if (Rpta.Equals("OK"))
            {
                // Ocultar y desactivar las funciones innecesarias
                this.Estado_texto(false);
                this.Estado_botones_procesos(false);
                this.Estado_botones_principales(true);
                this.Listado_ar("%"); // Refrescar el listado de registros con la nueva informacion ingresada
                nCodigo_ar = 0;
                nEstadoguarda = 0;
                nCodigo_um = 0;
                nCodigo_ca = 0;
                
                MessageBox.Show("Los datos han sido guardados correctamente",
                                "Aviso del Sistema",
                                MessageBoxButtons.OK, // Crear boton de Ok anexo a la ventana de mensaje
                                MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(Rpta,
                                "Aviso del Sistema",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void Btn_actualizar_Click(object sender, EventArgs e)
        {
            nEstadoguarda = 2; //Actualizar Registro           
            this.Estado_texto(true);
            this.Estado_botones_procesos(true);
            this.Estado_botones_principales(false);
            Txt_descripcion_ar.Focus();
        }

        private void Dgv_articulos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.Selecciona_item();
        }

        private void Btn_eliminar_Click(object sender, EventArgs e)
        {
            if (nCodigo_ar > 0)
            {
                string Rpta = "";
                D_Articulos Datos = new D_Articulos();
                Rpta = Datos.Eliminar_ar(nCodigo_ar);
                if (Rpta.Equals("OK"))
                {
                    this.Limpia_texto();
                    this.Listado_ar("%");
                    nCodigo_ar = 0;
                    MessageBox.Show("Registro eliminado correctamente",
                                    "Aviso del Sistema",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("No se tiene seleccionado ningún registro",
                                "Aviso del Sistema",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
            }
        }

        private void Btn_salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_reporte_Click(object sender, EventArgs e)
        {
            Frm_Rpt_Articulos oRpt = new Frm_Rpt_Articulos();
            oRpt.ShowDialog();
        }

        private void Btn_lupa_um_Click(object sender, EventArgs e)
        {
            Pnl_um.Location = Btn_lupa_um.Location;
            Pnl_um.Visible = true;
        }

        private void Btn_lupa_ca_Click(object sender, EventArgs e)
        {
            Pnl_ca.Location = Btn_lupa_ca.Location;
            Pnl_ca.Visible = true;
        }

        private void Btn_retornar_um_Click(object sender, EventArgs e)
        {
            Pnl_um.Visible = false;
        }

        private void Btn_retornar_ca_Click(object sender, EventArgs e)
        {
            Pnl_ca.Visible = false;
        }

        private void Dgv_um_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.Selecciona_item_um();
            Pnl_um.Visible = false;
        }

        private void Dgv_ca_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.Selecciona_item_ca();
            Pnl_ca.Visible = false;
        }
    }
}
