using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using System.IO;

namespace Cotizaciones
{
    public static class PDFHelper
    {
        
        private static Font fntTitle = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 16, BaseColor.BLACK);
        private static Font fntHead = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, BaseColor.BLACK);
        private static Font fntHeaders = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, BaseColor.BLACK);
        private static Font fntTable = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, BaseColor.BLACK);
        private static Font fntClienteHeader = FontFactory.GetFont(FontFactory.TIMES_BOLD, 6, BaseColor.BLACK);
        private static Font fntClienteTable = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 7, BaseColor.BLACK);
        private static Font fntClienteTableLeyenda = FontFactory.GetFont(FontFactory.TIMES_BOLD, 8, BaseColor.BLACK);
        private static Font fntWhiteSpace = FontFactory.GetFont(FontFactory.TIMES_BOLD, 3, BaseColor.BLACK);
        private static Document doc;
        private static PdfWriter writer;
        private static List<Sucursales> sucursales;
        private static Sucursales currentSucursal;
        public static void creaPDF(List<Cotizacion> Cotizaciones,string sucursal, string subtotal = "error", string iva = "error", string total = "error")
        {
           
            creaElArchivo();
            encabezado(sucursal);
            tablaCliente(Cotizaciones[0]);
            tablaProductos(Cotizaciones);
            tablaTotales(subtotal, iva, total);
            tablaFirma();
            tablaSucursales();
            terminaDocumento();
        }

        private static void tablaSucursales()
        {
            
            
            PdfPTable tablaSucursales = createTable(4, 100, Element.ALIGN_LEFT, 0);
            foreach(Sucursales sucursal in sucursales)
            {
                PdfPTable columnaSucursal = createTable(1, 100, Element.ALIGN_LEFT, 0);
                celda(sucursal.sucnom, fntClienteHeader, columnaSucursal, Element.ALIGN_CENTER);
                celda(sucursal.direccion, fntClienteHeader, columnaSucursal, Element.ALIGN_CENTER);
                celda(sucursal.colonia, fntClienteHeader, columnaSucursal, Element.ALIGN_CENTER);
                celda(sucursal.lugar, fntClienteHeader, columnaSucursal, Element.ALIGN_CENTER);
                celda("Tel. "+sucursal.telefono, fntClienteHeader, columnaSucursal, Element.ALIGN_CENTER);
                celda("Cel. "+sucursal.celular, fntClienteHeader, columnaSucursal, Element.ALIGN_CENTER);
                tablaSucursales.AddCell(columnaSucursal);
            }

            doc.Add(tablaSucursales);

        }

        private static void tablaFirma()
        {
            PdfPTable tablaFirma = createTable(3, 100, Element.ALIGN_LEFT, 0);
            tablaFirma.SetWidths(new float[] { 8f, 9f, 8f });
            PdfPTable columnaFirma = createTable(1, 100, Element.ALIGN_LEFT, 0);
            celda("________________________________", fntHead, columnaFirma, Element.ALIGN_CENTER, padding: 3);
            celda(currentSucursal.nombre, fntHead, columnaFirma, Element.ALIGN_CENTER, padding: 3);
            celda(currentSucursal.email, fntHead, columnaFirma, Element.ALIGN_CENTER, padding: 3);
            celda("Tel. "+currentSucursal.telefono, fntHead, columnaFirma, Element.ALIGN_CENTER, padding: 3);
            tablaFirma.AddCell(" ");
            tablaFirma.AddCell(columnaFirma);
            tablaFirma.AddCell(" ");
            doc.Add(tablaFirma);
            doc.Add(Chunk.NEWLINE);
            doc.Add(Chunk.NEWLINE);
            doc.Add(Chunk.NEWLINE);
            doc.Add(Chunk.NEWLINE);
        }

        private static void tablaTotales(string subtotal, string iva, string total)
        {
            PdfPTable tablaTotales = createTable(3, 100, Element.ALIGN_LEFT, 0);
            tablaTotales.DefaultCell.Border = PdfPCell.NO_BORDER;
            tablaTotales.DefaultCell.Padding = 0;
            tablaTotales.SetWidths(new float[] { 20f, 4f, 4f });
            PdfPTable tablaInfo = createTable(1, 100, Element.ALIGN_LEFT,0);
            celda("* PRECIO SUJETO A CAMBIOS SIN PREVIO AVISO", fntClienteHeader, tablaInfo, Element.ALIGN_LEFT, padding: 3);
            celda("* PRECIOS CON IVA INCLUIDO", fntClienteHeader, tablaInfo, Element.ALIGN_LEFT, padding: 3);
            celda("* PRECIOS SUJETOS A DISPONIBILIDAD DE MATERIALES EN INVENTARIO", fntClienteHeader, tablaInfo, Element.ALIGN_LEFT, padding: 3);
            celda("* PRECIO ESPECIAL A MAYORISTA", fntClienteHeader, tablaInfo, Element.ALIGN_LEFT, padding: 3);

            PdfPTable tablaTitulos = createTable(1, 100, Element.ALIGN_LEFT, 0.5f);
            celda("SUBTOTAL:", fntTable, tablaTitulos, Element.ALIGN_RIGHT, padding: 3);
            celda("IVA:", fntTable, tablaTitulos, Element.ALIGN_RIGHT, padding: 3);
            celda("TOTAL:", fntTable, tablaTitulos, Element.ALIGN_RIGHT, padding: 3);
            celda(" ", fntClienteHeader, tablaTitulos, Element.ALIGN_RIGHT, padding: 3);

            PdfPTable tablaDatos = createTable(1, 100, Element.ALIGN_LEFT, 0.5f);
            celda(subtotal, fntTable, tablaDatos, Element.ALIGN_CENTER, padding: 3, borde: true);
            celda(iva, fntTable, tablaDatos, Element.ALIGN_CENTER, padding: 3, borde: true);
            celda(total, fntTable, tablaDatos, Element.ALIGN_CENTER, padding: 3, borde: true);
            celda(" ", fntClienteHeader, tablaDatos, Element.ALIGN_CENTER, padding: 3);

            tablaTotales.AddCell(tablaInfo);
            tablaTotales.AddCell(tablaTitulos);
            tablaTotales.AddCell(tablaDatos);
            doc.Add(tablaTotales);
            doc.Add(Chunk.NEWLINE);
            doc.Add(Chunk.NEWLINE);
        }

        private static void tablaProductos(List<Cotizacion> Cotizaciones)
        {
            PdfPTable tablaProductos = createTable(5, 100, Element.ALIGN_LEFT, 0.5f);
            tablaProductos.SetWidths(new float[] { 4f, 12f, 4f, 4f, 4f });
            celda("CODIGO", fntTable, tablaProductos, Element.ALIGN_CENTER, padding: 3, borde:true);
            celda("DESCRIPCION", fntTable, tablaProductos, Element.ALIGN_CENTER, padding: 3, borde: true);
            celda("CANTIDAD", fntTable, tablaProductos, Element.ALIGN_CENTER, padding: 3, borde: true);
            celda("PRECIO UNI", fntTable, tablaProductos, Element.ALIGN_CENTER, padding: 3, borde: true);
            celda("TOTAL", fntTable, tablaProductos, Element.ALIGN_CENTER, padding: 3, borde: true);

            PdfPTable columnaCodigos = createTable(1, 100, Element.ALIGN_LEFT, 0.3f);
            PdfPTable columnaDescripcion = createTable(1, 100, Element.ALIGN_LEFT, 0.3f);
            PdfPTable columnaCantidad = createTable(1, 100, Element.ALIGN_LEFT, 0.3f);
            PdfPTable columnaPrecios = createTable(1, 100, Element.ALIGN_LEFT, 0.3f);
            PdfPTable columnaTotal = createTable(1, 100, Element.ALIGN_LEFT, 0.3f);

            foreach(Cotizacion cotizacion in Cotizaciones)
            {
                Producto producto = new Producto();
                producto = DBHelper.getProducto(cotizacion.codigopro.Replace(" ", ""));
                celda(cotizacion.codigopro, fntTable, columnaCodigos, Element.ALIGN_LEFT, paddingLeft: 3);
                celda(producto.CNOMBREPRODUCTO, fntTable, columnaDescripcion, Element.ALIGN_LEFT, paddingLeft: 3);
                celda(cotizacion.cantidad.ToString(), fntTable, columnaCantidad, Element.ALIGN_CENTER, paddingLeft: 3);
                celda(producto.CPRECIO1, fntTable, columnaPrecios, Element.ALIGN_CENTER, paddingLeft: 3);
                celda((cotizacion.cantidad*float.Parse(producto.CPRECIO1)).ToString(), fntTable, columnaTotal, Element.ALIGN_CENTER, paddingLeft: 3);
            }
            if (Cotizaciones.Count < 20)
            {
                for(int i=0; i < (20 - Cotizaciones.Count); i++)
                {
                    celda(" ", fntTable, columnaCodigos, Element.ALIGN_LEFT, paddingLeft: 3);
                    celda(" ", fntTable, columnaDescripcion, Element.ALIGN_LEFT, paddingLeft: 3);
                    celda(" ", fntTable, columnaCantidad, Element.ALIGN_CENTER, paddingLeft: 3);
                    celda(" ", fntTable, columnaPrecios, Element.ALIGN_CENTER, paddingLeft: 3);
                    celda(" ", fntTable, columnaTotal, Element.ALIGN_CENTER, paddingLeft: 3);
                }
            }

            tablaProductos.AddCell(columnaCodigos);
            tablaProductos.AddCell(columnaDescripcion);
            tablaProductos.AddCell(columnaCantidad);
            tablaProductos.AddCell(columnaPrecios);
            tablaProductos.AddCell(columnaTotal);

            doc.Add(tablaProductos);
        }

        private static void tablaCliente(Cotizacion cliente)
        {
            PdfPTable tablaCliente = createTable(2, 100, Element.ALIGN_LEFT, 0.3f);
            PdfPTable columnaCliente = createTable(1, 100, Element.ALIGN_LEFT, 0);
            celda("CLIENTE:", fntClienteHeader, columnaCliente, Element.ALIGN_LEFT, paddingLeft: 3);
            celda(cliente.nombre, fntClienteTable, columnaCliente, Element.ALIGN_LEFT, paddingLeft: 10);
            tablaCliente.AddCell(columnaCliente);
            PdfPTable columnaTelefono = createTable(1, 100, Element.ALIGN_LEFT, 0);
            celda("TELEFONO:", fntClienteHeader, columnaTelefono, Element.ALIGN_LEFT, paddingLeft: 3);
            celda(cliente.telefono, fntClienteTable, columnaTelefono, Element.ALIGN_LEFT, paddingLeft: 10);
            tablaCliente.AddCell(columnaTelefono);

            PdfPTable columnaDireccion = createTable(1, 100, Element.ALIGN_LEFT, 0);
            celda("DIRECCION:", fntClienteHeader, columnaDireccion, Element.ALIGN_LEFT, paddingLeft: 3);
            celda(cliente.direccion, fntClienteTable, columnaDireccion, Element.ALIGN_LEFT, paddingLeft: 10);
            tablaCliente.AddCell(columnaDireccion);
            PdfPTable columnaEmail = createTable(1, 100, Element.ALIGN_LEFT, 0);
            celda("E-MAIL:", fntClienteHeader, columnaEmail, Element.ALIGN_LEFT, paddingLeft: 3);
            celda(cliente.mail, fntClienteTable, columnaEmail, Element.ALIGN_LEFT, paddingLeft: 10);
            tablaCliente.AddCell(columnaEmail);

            PdfPTable columnaAtencion = createTable(1, 100, Element.ALIGN_LEFT, 0);
            celda("ATENCION:", fntClienteHeader, columnaAtencion, Element.ALIGN_LEFT, paddingLeft: 3);
            celda(cliente.atencion, fntClienteTable, columnaAtencion, Element.ALIGN_LEFT, paddingLeft: 10);
            tablaCliente.AddCell(columnaAtencion);
            PdfPTable columnaSolicito = createTable(1, 100, Element.ALIGN_LEFT, 0);
            celda("SOLICITO:", fntClienteHeader, columnaSolicito, Element.ALIGN_LEFT, paddingLeft: 3);
            celda(cliente.solicito, fntClienteTable, columnaSolicito, Element.ALIGN_LEFT, paddingLeft: 10);
            tablaCliente.AddCell(columnaSolicito);
            PdfPTable tablaClienteLeyenda = createTable(1, 100, Element.ALIGN_LEFT, 0.3f);
            PdfPTable columnaLeyenda = createTable(1, 100, Element.ALIGN_LEFT, 0);
            celda("POR MEDIO DE LA PRESENTE PONEMOS A SU CONSIDERACION LA SIGUIENTE COTIZACION", fntClienteTableLeyenda, columnaLeyenda, Element.ALIGN_LEFT, paddingLeft: 20);
            tablaClienteLeyenda.AddCell(columnaLeyenda);

            doc.Add(tablaCliente);
            doc.Add(tablaClienteLeyenda);
            doc.Add(Chunk.NEWLINE);
            doc.Add(Chunk.NEWLINE);
        }

        private static void terminaDocumento()
        {
            doc.Close();
            writer.Close();
        }

        private static void encabezado(string sucursal)
        {
            sucursales = DBHelper.getDatosSucursales();
            for (int i = 0; i < sucursales.Count; i++)
            {
                if (sucursales[i].sucursal.Equals(sucursal))
                {
                    currentSucursal = sucursales[i];
                    sucursales.RemoveAt(i);
                }
            }

            PdfPTable encabezado = createTable(3, 100, Element.ALIGN_LEFT, 0);
            encabezado.SetWidths(new float[] { 4f, 8f, 4f });

            //Logo
            string imageURL = "./image2.png";
            Image png = Image.GetInstance(imageURL);
            png.ScaleToFit(140f, 120f);
            PdfPCell logo = new PdfPCell(png);
            logo.Border = PdfPCell.NO_BORDER;
            logo.HorizontalAlignment = Element.ALIGN_LEFT;

            //Centro
            PdfPTable centro = createTable(1, 100, Element.ALIGN_LEFT, 1);
            celda("COTIZACION", fntTitle, centro, Element.ALIGN_CENTER);
            celda(currentSucursal.sucnom, fntHead, centro, Element.ALIGN_CENTER);
            celda(currentSucursal.direccion, fntHead, centro, Element.ALIGN_CENTER);
            celda(currentSucursal.colonia, fntHead, centro, Element.ALIGN_CENTER);
            celda(currentSucursal.lugar, fntHead, centro, Element.ALIGN_CENTER);
            celda("Tel. "+ currentSucursal.telefono, fntHead, centro, Element.ALIGN_CENTER);
            celda("Cel. "+ currentSucursal.celular, fntHead, centro, Element.ALIGN_CENTER);

            //Fecha
            PdfPTable ContenedorFolioYFecha = createTable(1, 100, Element.ALIGN_RIGHT, 0);

            PdfPTable tablaFolio = createTable(1, 100, Element.ALIGN_RIGHT, 0.8f);
            tablaFolio.DefaultCell.Border = PdfPCell.NO_BORDER;
            tablaFolio.DefaultCell.CellEvent = new RoundedBorder();

            PdfPTable folio = createTable(1, 100, Element.ALIGN_RIGHT, 0.8f);
            celda("Folio", fntHead, folio, Element.ALIGN_CENTER, true);
            celda("1", fntHead, folio, Element.ALIGN_CENTER, padding:3);

            PdfPTable tablaFecha = createTable(1, 100, Element.ALIGN_RIGHT, 0.8f);
            tablaFecha.DefaultCell.Border = PdfPCell.NO_BORDER;
            tablaFecha.DefaultCell.CellEvent = new RoundedBorder();

            PdfPTable fecha = createTable(1, 100, Element.ALIGN_RIGHT, 0.8f);
            celda("Fecha", fntHead, fecha, Element.ALIGN_CENTER, true);
            celda(System.DateTime.Now.ToString("dd/MM/yyyy"), fntHead, fecha, Element.ALIGN_CENTER, padding: 3);


            PdfPTable espacioEnBlancoEnContenedorFYH = createTable(1, 100, Element.ALIGN_RIGHT, 0);
            celda(" ", fntWhiteSpace, espacioEnBlancoEnContenedorFYH, Element.ALIGN_CENTER);



            tablaFolio.AddCell(folio);
            tablaFecha.AddCell(fecha);
            ContenedorFolioYFecha.AddCell(tablaFolio);
            ContenedorFolioYFecha.AddCell(espacioEnBlancoEnContenedorFYH);
            ContenedorFolioYFecha.AddCell(tablaFecha);
            ContenedorFolioYFecha.AddCell(espacioEnBlancoEnContenedorFYH);

            encabezado.AddCell(logo);
            encabezado.AddCell(centro);
            encabezado.AddCell(ContenedorFolioYFecha);

            doc.Add(encabezado);
            doc.Add(new Paragraph(" ", fntWhiteSpace));
        }

        private static void celda(string texto, Font fuente, PdfPTable tabla, int posicion_del_texto, bool borde = false, float paddingLeft = 0, float padding = 0, bool bordeIzq = false, bool bordeDer = false, bool bordeAbajo = false, bool cliente = false)
        {
            PdfPCell Celda = new PdfPCell(new Paragraph(texto, fuente));
            Celda.HorizontalAlignment = posicion_del_texto;
            if (!borde)
            {
                Celda.Border = PdfPCell.NO_BORDER;
            }
            if (cliente)
            {
                Celda.BorderWidthBottom = 0;
            }
            Celda.PaddingLeft = paddingLeft;
            if (padding > 0)
            {
                Celda.Padding = padding;
            }
            tabla.AddCell(Celda);

        }

        private static PdfPTable createTable(int columns, int width, int alignment, float border)
        {
            PdfPTable table = new PdfPTable(columns);
            table.WidthPercentage = width;
            table.HorizontalAlignment = alignment;
            table.DefaultCell.BorderWidth = border;
            return table;
        }

        private static void creaElArchivo()
        {
            doc = new Document(PageSize.LETTER);
            // Indicamos donde vamos a guardar el documento
            //Ventana para guardar el archivo
            SaveFileDialog dialog = new SaveFileDialog();

            dialog.Title = "Guardar como...";
            dialog.Filter = "Text files (*.PDF)|*pdf|All files (*.*)|*.*";
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Guardado en: " + dialog.FileName);
            }

            //Guarda el archivo en la ruta y crea el archivo
            writer = PdfWriter.GetInstance(doc,
                                        new System.IO.FileStream(dialog.FileName + ".PDF", System.IO.FileMode.Create));
            // Abrimos el archivo
            doc.Open();
        }
    }
    class RoundedBorder : IPdfPCellEvent
    {
        public void CellLayout(PdfPCell cell, Rectangle rect, PdfContentByte[] canvas)
        {
            PdfContentByte cb = canvas[PdfPTable.BACKGROUNDCANVAS];
            cb.RoundRectangle(
              rect.Left + 1.5f,
              rect.Bottom + 1.5f,
              rect.Width - 3,
              rect.Height - 3, 4
            );
            cb.Stroke();
        }
    }
}
