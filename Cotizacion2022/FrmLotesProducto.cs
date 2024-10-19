using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Cotizacion2022
{

    public partial class FrmLotesProducto : Form
    {
        double des;
        double CPRECIO_Pro;
        double CNETO_Pro;
        double cDescuentoGeneral_Pro;
        double cImpuesto1_Pro;
        double ImporteSI_Pro;
        double Importe_Pro;
        // Variable para el código del producto seleccionado
        private string CodP = PantallaProductos.CidProd;

        // Lista para guardar los datos seleccionados (número de lote, producto, cantidad)
        private List<Tuple<string, string, int, int, string, string, Tuple<string, string>>> lotesSeleccionados = new List<Tuple<string, string, int, int, string,string, Tuple<string, string>>>();
        private List<Tuple<double>> LotSelc = new List<Tuple<double>>();

        public FrmLotesProducto()
        {
            InitializeComponent();
            InicializarDataGridView();
            CargarDatosLotes();
        }

        // Método para configurar el DataGridView
        private void InicializarDataGridView()
        {
            MessageBox.Show("El valor de CodP es:" + CodP);
            // Limpiar columnas existentes para evitar duplicados
            dataGridView.Columns.Clear();

            // Crear y configurar las columnas del DataGridView
            DataGridViewTextBoxColumn codigoProductoColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "Código Producto",
                Name = "CCODIGOPRODUCTO",
                DataPropertyName = "CCODIGOPRODUCTO",
                ReadOnly = true
            };

            DataGridViewTextBoxColumn numeroLoteColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "Número de Lote",
                Name = "CNUMEROLOTE",
                DataPropertyName = "CNUMEROLOTE",  // Asegura que esta columna mapea con el campo correcto del DataTable
                ReadOnly = true
            };

            DataGridViewTextBoxColumn nombreProductoColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "Nombre Producto",
                Name = "CNOMBREPRODUCTO",
                DataPropertyName = "CNOMBREPRODUCTO",
                ReadOnly = true
            };

            DataGridViewTextBoxColumn nombreAlmacenColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "Nombre Almacén",
                Name = "CNOMBREALMACEN",
                DataPropertyName = "CNOMBREALMACEN",
                ReadOnly = true
            };
            DataGridViewTextBoxColumn PesoColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "Peso",
                Name = "peso",
                DataPropertyName = "peso",
                ReadOnly = true
            };
            DataGridViewTextBoxColumn PrecioColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "Precio",
                Name = "precio",
                DataPropertyName = "precio",
                ReadOnly = true
            };


            DataGridViewTextBoxColumn existenciaColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "Existencia",
                Name = "CEXISTENCIA",
                DataPropertyName = "CEXISTENCIA",
                ReadOnly = true
            };

            DataGridViewTextBoxColumn cantidadColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "Cantidad a Tomar",
                Name = "Cantidad"
            };
            
            DataGridViewTextBoxColumn totalRestanteColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "Total Restante",
                Name = "TotalRestante",
                ReadOnly = true
            };

            // Agregar las columnas al DataGridView
            dataGridView.Columns.Add(codigoProductoColumn);
            dataGridView.Columns.Add(numeroLoteColumn);
            dataGridView.Columns.Add(nombreProductoColumn);
            dataGridView.Columns.Add(nombreAlmacenColumn);
            dataGridView.Columns.Add(PesoColumn);
            dataGridView.Columns.Add(PrecioColumn);
            dataGridView.Columns.Add(existenciaColumn);
            dataGridView.Columns.Add(cantidadColumn);
            
            dataGridView.Columns.Add(totalRestanteColumn);

            // Configurar evento para validación al finalizar edición de la celda
            dataGridView.CellEndEdit += DataGridView1_CellEndEdit;
        }

        // Método para cargar los datos desde la base de datos
        private void CargarDatosLotes()
        {
            // Define tu cadena de conexión aquí
            string connectionString = @"Data Source=192.168.1.127\COMPAC;Initial Catalog=adAmsa_Pruebas no usar;Persist Security Info=True;User ID=sa;Password=AdminSql7639!";

            // Define tu consulta SQL con filtro basado en el código de producto
            string query = @"
                SELECT TOP (1000)
                    h.[CCODIGOPRODUCTO],
                    d.[CNUMEROLOTE],
                    h.[CNOMBREPRODUCTO],
                    a.[CNOMBREALMACEN],
                    h.CPRECIO10 as peso,
                    (h.CPRECIO1 / 1.16) as Precio,
                    d.[CEXISTENCIA]
                FROM [adAmsa_Pruebas no usar].[dbo].[admProductos] h
                LEFT JOIN admCapasProducto d ON h.CIDPRODUCTO = d.CIDPRODUCTO
                LEFT JOIN admExistenciaCosto e ON h.CIDPRODUCTO = e.CIDPRODUCTO AND d.CIDALMACEN = e.CIDALMACEN
                LEFT JOIN admAlmacenes a ON a.CIDALMACEN = d.CIDALMACEN AND a.CIDALMACEN = e.CIDALMACEN
                WHERE e.CIDALMACEN IS NOT NULL
                    AND e.CIDEJERCICIO = 7
                    AND h.CTIPOPRODUCTO = 1
                    AND h.CCONTROLEXISTENCIA = 17
                    AND d.CEXISTENCIA != 0
                    AND d.CIDPRODUCTO  = @CodProducto -- Filtro por código de producto
                ORDER BY h.CIDPRODUCTO;
            ";

            // Crear conexión y adaptador para ejecutar la consulta
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open(); // Abrir la conexión
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@CodProducto", CodP); // Usar el parámetro para evitar SQL Injection

                    SqlDataAdapter adapter = new SqlDataAdapter(command); // Crear adaptador de datos
                    DataTable dataTable = new DataTable(); // Crear DataTable para almacenar los resultados
                    adapter.Fill(dataTable); // Llenar el DataTable con los resultados de la consulta

                    // Asignar el DataTable como fuente de datos para el DataGridView
                    dataGridView.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los datos: " + ex.Message);
                }
            }
        }

        // Evento para validar la entrada del usuario en la columna "Cantidad"
        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView.Columns["Cantidad"].Index)
            {
                int cantidadTomar;
                if (int.TryParse(dataGridView.Rows[e.RowIndex].Cells["Cantidad"].Value?.ToString(), out cantidadTomar))
                {
                    int existencia = Convert.ToInt32(dataGridView.Rows[e.RowIndex].Cells["CEXISTENCIA"].Value);

                    if (cantidadTomar > existencia)
                    {
                        MessageBox.Show("No puede tomar más unidades de las disponibles.");
                        dataGridView.Rows[e.RowIndex].Cells["Cantidad"].Value = 0;
                        dataGridView.Rows[e.RowIndex].Cells["TotalRestante"].Value = existencia;
                    }
                    else
                    {
                        int totalRestante = existencia - cantidadTomar;
                        dataGridView.Rows[e.RowIndex].Cells["TotalRestante"].Value = totalRestante;

                        // Almacenar los valores en la lista
                        string numeroLote = dataGridView.Rows[e.RowIndex].Cells["CNUMEROLOTE"].Value.ToString();
                        string codigoProducto = dataGridView.Rows[e.RowIndex].Cells["CCODIGOPRODUCTO"].Value.ToString();
                        string nombre = dataGridView.Rows[e.RowIndex].Cells["CNOMBREPRODUCTO"].Value.ToString();
                        string peso = dataGridView.Rows[e.RowIndex].Cells["peso"].Value.ToString();
                        string precio = dataGridView.Rows[e.RowIndex].Cells["precio"].Value.ToString();
                        string Descto = "0";


                        // Revisar si ya existe un registro para ese lote y producto
                        var registroExistente = lotesSeleccionados.FirstOrDefault(lote => lote.Item1 == numeroLote && lote.Item2 == codigoProducto && lote.Item3 == cantidadTomar && lote.Item4 == existencia && lote.Item5 == nombre && lote.Item6 == peso && lote.Item7 ==  new Tuple<string, string>(precio, Descto));
                        var registroExistente2 = lotesSeleccionados.FirstOrDefault(lote => lote.Item1 == numeroLote && lote.Item2 == codigoProducto && lote.Item3 == cantidadTomar && lote.Item4 == existencia && lote.Item5 == nombre && lote.Item6 == peso && lote.Item7 == new Tuple<string, string>(precio, Descto));
                        if (registroExistente != null)
                        {
                            // Actualizar la cantidad del lote existente
                            int index = lotesSeleccionados.IndexOf(registroExistente);
                           
                            lotesSeleccionados[index] = new Tuple<string, string, int, int, string, string, Tuple<string, string>>(numeroLote, codigoProducto, cantidadTomar, existencia, nombre, peso, new Tuple<string, string>(precio, Descto));
                            
                        }
                        else
                        {
                            // Agregar un nuevo registro
                            lotesSeleccionados.Add(new Tuple<string, string, int, int, string, string, Tuple<string, string>>(numeroLote, codigoProducto, cantidadTomar, existencia, nombre, peso, new Tuple<string, string>(precio, Descto)));
                            
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, ingrese un número válido.");
                    dataGridView.Rows[e.RowIndex].Cells["Cantidad"].Value = 0;
                    dataGridView.Rows[e.RowIndex].Cells["TotalRestante"].Value = dataGridView.Rows[e.RowIndex].Cells["CEXISTENCIA"].Value;
                }
            }
            //else if (e.ColumnIndex == dataGridView.Columns["CDescuento"].Index)
            //{
            //    //Validar y aplicar el descuento
            //    double descuento;
            //    if(!double.TryParse(dataGridView.Rows[e.RowIndex].Cells["CDescuento"].Value?.ToString(), out descuento))
            //    {
            //        MessageBox.Show("Por favor, ingrese un valor de descuento válido.");
            //        dataGridView.Rows[e.RowIndex].Cells["CDescuento"].Value = "0";
            //    }
            //}
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FrmLotesProducto_Load(object sender, EventArgs e)
        {

        }
        //Boton Cerrar programa
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            MessageBox.Show("Cancelado con exito");
            
        }
        //Boton Realizar Pedido
        private void button2_Click(object sender, EventArgs e)
        {
            // Obtener la referencia de Form1
            Form1 form1 = (Form1)Application.OpenForms["Form1"];

            // Iterar sobre los lotes seleccionados
            foreach (var lote in lotesSeleccionados)
            {
                string numeroLote = lote.Item1;
                string codigoProducto = lote.Item2;
                int cantidad = lote.Item3;
                int existencia = lote.Item4;
                string nombre = lote.Item5;
                double peso = Convert.ToDouble(lote.Item6);
                double precio = Convert.ToDouble(lote.Item7.Item1);
                double desc = Convert.ToDouble(lote.Item7.Item2);

                // Obtener el descuento (si lo gestionas de alguna manera)
                double descuento = 0; // Obtener el descuento de la columna o agregar una lógica para el descuento

                // Realizar los cálculos antes de insertar en el DataGridView
                Calculos(precio, desc, cantidad);

                // Verificar si los cálculos son válidos (no deben ser nulos o vacíos)
                if (CPRECIO_Pro == 0 || CNETO_Pro == 0 || ImporteSI_Pro == 0 || Importe_Pro == 0 || cImpuesto1_Pro == 0)
                {
                    MessageBox.Show("Error: Los cálculos resultaron en valores no válidos.");
                    return;  // Salimos del bucle si los cálculos fallan
                }

                // Verificar si ya existe un producto con el mismo código y lote en el DataGridView
                bool existe = false;
                foreach (DataGridViewRow row in form1.dataGridView1.Rows)
                {
                    if (row.Cells["Codigo"].Value != null &&
                        row.Cells["Codigo"].Value.ToString() == codigoProducto &&
                        row.Cells["NumLote"].Value.ToString() == numeroLote)
                    {
                        existe = true;
                        break;
                    }
                }
                form1.Calculos(precio, desc, cantidad);
                // Si no existe, agrega una nueva fila con el producto y lote
                if (!existe)
                {
                    int rowIndex = form1.dataGridView1.Rows.Add();
                    DataGridViewRow newRow = form1.dataGridView1.Rows[rowIndex];

                    newRow.Cells["IdPro"].Value = PantallaProductos.IdProPasa;
                    newRow.Cells["codigo"].Value = codigoProducto;
                    newRow.Cells["nombre"].Value = nombre;
                    newRow.Cells["NumLote"].Value = numeroLote;
                    newRow.Cells["Observa"].Value = PantallaProductos.ObservaPasa;
                    newRow.Cells["clasificacion"].Value = PantallaProductos.ClasificacionPasa;
                    newRow.Cells["exis"].Value = existencia;
                    newRow.Cells["Cantidad"].Value = cantidad;
                    newRow.Cells["Descuento"].Value = desc;
                    newRow.Cells["peso"].Value = peso;

                   // PantallaProductos. 

                    // Realizar cálculos específicos para el formulario principal41
                    form1.Calculos(precio, desc, cantidad);

                    newRow.Cells["CPRECIO"].Value = Math.Round(Convert.ToDouble(PantallaProductos.precio), 4);
                    newRow.Cells["CNETO"].Value = CNETO_Pro.ToString();
                    newRow.Cells["Cdescuento1"].Value = cDescuentoGeneral_Pro;
                    newRow.Cells["CIMPUESTO1"].Value = cImpuesto1_Pro;
                    newRow.Cells["ImporteSI"].Value = ImporteSI_Pro;
                    newRow.Cells["Importe"].Value = Importe_Pro;
                    newRow.Cells["Eliminar"].Value = "X"; // Clasificación

                    form1.calculaTotales();
                    form1.Refresh();

                    /* // Insertar en el DataGridView los valores calculados previamente

              form1.dataGridView1.Rows[form1.dataGridView1.CurrentRow.Index].Cells["IdPro"].Value = PantallaProductos.IdProPasa,
              form1.dataGridView1.Rows[form1.dataGridView1.CurrentRow.Index].Cells["codigo"].Value = codigoProducto,
              form1.dataGridView1.Rows[form1.dataGridView1.CurrentRow.Index].Cells["nombre"].Value = nombre,
              form1.dataGridView1.Rows[form1.dataGridView1.CurrentRow.Index].Cells["NumLote"].Value = numeroLote,

              form1.dataGridView1.Rows[form1.dataGridView1.CurrentRow.Index].Cells["Observa"].Value = PantallaProductos.ObservaPasa,
              form1.dataGridView1.Rows[form1.dataGridView1.CurrentRow.Index].Cells["clasificacion"].Value = PantallaProductos.ClasificacionPasa,
              form1.dataGridView1.Rows[form1.dataGridView1.CurrentRow.Index].Cells["exis"].Value = existencia,

              form1.dataGridView1.Rows[form1.dataGridView1.CurrentRow.Index].Cells["Cantidad"].Value = cantidad,
              form1.dataGridView1.Rows[form1.dataGridView1.CurrentRow.Index].Cells["Descuento"].Value = desc,
              form1.dataGridView1.Rows[form1.dataGridView1.CurrentRow.Index].Cells["peso"].Value = peso,

                     //form1.Calculos(precio, desc,cantidad);

                      form1.dataGridView1.Rows[form1.dataGridView1.CurrentRow.Index].Cells["CPRECIO"].Value = Math.Round(Convert.ToDouble(precio), 4),
              form1.dataGridView1.Rows[form1.dataGridView1.CurrentRow.Index].Cells["CNETO"].Value = CNETO_Pro.ToString(),
              form1.dataGridView1.Rows[form1.dataGridView1.CurrentRow.Index].Cells["Cdescuento1"].Value = cDescuentoGeneral_Pro,
              form1.dataGridView1.Rows[form1.dataGridView1.CurrentRow.Index].Cells["CIMPUESTO1"].Value = cImpuesto1_Pro,
              form1.dataGridView1.Rows[form1.dataGridView1.CurrentRow.Index].Cells["ImporteSI"].Value = ImporteSI_Pro,
              form1.dataGridView1.Rows[form1.dataGridView1.CurrentRow.Index].Cells["Importe"].Value = Importe_Pro,
              form1.dataGridView1.Rows[form1.dataGridView1.CurrentRow.Index].Cells["Eliminar"].Value = "X"        // Clasificación
                      );

                      form1.calculaTotales();*/
                }
            }

            // Actualizar los totales generales
            //form1.calculaTotales();

            // Cerrar el formulario de lotes
            this.Close();
        }
        
    
    private void Calculos(double precio, double descuento, double cantidad)
        {
            // Aplicar el descuento
            double porcentajeDescuento = descuento / 100;
            double precioConDescuento = precio * (1 - porcentajeDescuento);

            // Cálculo del precio neto
            double neto = precioConDescuento * cantidad;

            // IVA
            double iva = neto * 0.16;

            // Importe total con IVA
            double importeTotal = neto + iva;

            // Asignar los valores calculados
            CPRECIO_Pro = precio; // Precio original
            CNETO_Pro = precioConDescuento; // Precio con descuento aplicado
            ImporteSI_Pro = neto; // Precio sin IVA
            cImpuesto1_Pro = iva; // IVA calculado
            Importe_Pro = importeTotal; // Importe total con IVA
        }



        public void Calculos2(double PrecioNormal, double Descuento, double Cantidad)
        {

            double precioNormal = PrecioNormal;
            double Cunidades = Cantidad;
            double descuento = Math.Round(Descuento / 100, 4);

            double CPRECIO = 0;
            double CNETO = 0;
            double CNETO2 = 0;
            double cImpuesto1 = 0;
            double cDescuentoGeneral = 0;

            double Importe = 0;

            if (descuento > 0)
            {
                CPRECIO = Math.Round(PrecioNormal, 6);
                Importe = Math.Round(CPRECIO * Cunidades, 2);
                CNETO = Math.Round(Importe / 1.16, 2);
                cDescuentoGeneral = Math.Round(CNETO * descuento, 2);
                CNETO2 = Math.Round(CNETO / Cunidades, 2);
                cImpuesto1 = Math.Round((CNETO - cDescuentoGeneral) * 0.16, 2);

                double ImporteDescuento = Math.Round((CNETO - cDescuentoGeneral) + cImpuesto1, 2);

                CPRECIO_Pro = CPRECIO;
                CNETO_Pro = CNETO2;
                ImporteSI_Pro = CNETO;
                cDescuentoGeneral_Pro = cDescuentoGeneral;
                cImpuesto1_Pro = cImpuesto1;
                Importe_Pro = ImporteDescuento;

            }
            else
            {
                CPRECIO = Math.Round(PrecioNormal, 6);
                Importe = Math.Round(CPRECIO * Cunidades, 2);
                CNETO = Math.Round(Importe / 1.16, 2);
                cImpuesto1 = Math.Round(Importe - CNETO, 2);
                CNETO2 = Math.Round(CNETO / Cunidades, 2);


                CPRECIO_Pro = CPRECIO;
                CNETO_Pro = CNETO2;
                ImporteSI_Pro = CNETO;
                cDescuentoGeneral_Pro = cDescuentoGeneral;
                cImpuesto1_Pro = cImpuesto1;
                Importe_Pro = Importe;

            }


            //CPRECIO = Math.Round(PrecioNormal / 1.16, 6);
            //CNETO = Math.Round(CPRECIO * Cunidades, 2);
            //CNETO2 = Math.Round(CPRECIO,2);
            //cDescuentoGeneral = Math.Round(CNETO * descuento, 2);
            //cImpuesto1 = Math.Round((CNETO - cDescuentoGeneral) * 0.16f, 2);
            //Importe = (CNETO - cDescuentoGeneral) + cImpuesto1;



            //CPRECIO_Pro = CPRECIO;
            //CNETO_Pro = CNETO2;
            //ImporteSI_Pro = CNETO;
            //cDescuentoGeneral_Pro = cDescuentoGeneral;
            //cImpuesto1_Pro = cImpuesto1;
            //Importe_Pro = Importe;


        }
    }
}

