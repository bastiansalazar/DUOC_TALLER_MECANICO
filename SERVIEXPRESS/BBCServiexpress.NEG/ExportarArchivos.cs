using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OfficeExcel = Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using Xceed.Words.NET;
using System.Drawing;
using System.Diagnostics;
using BBCServiexpress.DAL.Vistas;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace BBCServiexpress.NEG
{
    public class ExportarArchivos
    {
        public string OrdenesPedidos_TextoPlano(DataTable tabla,string nombre)
        {
            try
            {
                String CadenaDataTable = "";
                string mdoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string ruta = mdoc+"/" + nombre + ".txt";
                StreamWriter file = new StreamWriter(ruta, true);
                file.WriteLine("ID;FOLIO;FECHA CREACION;SUCURSAL;RUT PROVEEDOR;PROVEEDOR;MONTO TOTAL;CANTIDAD ART;FECHA ENTREGA;ESTADO;EMPLEADO RESPONSABLE;MONEDA;TOTAL MONEDA;FECHA ACTUALIZACION");
                foreach (DataRow row in tabla.Rows)
                {
                    CadenaDataTable = row["ID"] + ";" + row["FOLIO"] + ";" + row["FECHA CREACION"] + ";" + row["SUCURSAL"] + ";" + row["RUT PROVEEDOR"] + 
                        ";" + row["MONTO TOTAL"]+ ";" + row["CANTIDAD ART"] + ";" + row["FECHA ENTREGA"] + ";" + row["ESTADO"] + ";" + row["EMPLEADO RESPONSABLE"] +
                        ";" + row["MONEDA"] + ";" + row["TOTAL MONEDA"] + ";" + row["FECHA ACTUALIZACION"];
                    file.WriteLine(CadenaDataTable);
                }             
                
                file.Close();
                return "guardado";
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public string OrdenesPedidos_Word(DataTable tabla, string nombre)
        {
            try
            {
                string filename = nombre + ".docx";
                var document = DocX.Create(filename);
                document.PageLayout.Orientation = Orientation.Landscape;
                var titleParagraph = document.InsertParagraph();
                titleParagraph.Append("ORDENES DE PEDIDOS").Heading(HeadingType.Heading1);

                Formatting textParagraphFormat = new Formatting();
                textParagraphFormat.FontFamily = new Xceed.Words.NET.Font("Arial Narrow");
                textParagraphFormat.Size = 8;

                int cantidadFilas = tabla.Rows.Count+1;
                Table t = document.AddTable(cantidadFilas, 14);
                // Specify some properties for this Table.
                t.Alignment = Alignment.center;
                t.Design = TableDesign.MediumGrid1Accent2;
                // Add content to this Table.
                t.Rows[0].Cells[0].Paragraphs.First().Append("ID");
                t.Rows[0].Cells[1].Paragraphs.First().Append("FOLIO");
                t.Rows[0].Cells[2].Paragraphs.First().Append("FECHA CREACION");
                t.Rows[0].Cells[3].Paragraphs.First().Append("SUCURSAL");
                t.Rows[0].Cells[4].Paragraphs.First().Append("RUT PROVEEDOR");
                t.Rows[0].Cells[5].Paragraphs.First().Append("PROVEEDOR");
                t.Rows[0].Cells[6].Paragraphs.First().Append("MONTO TOTAL");
                t.Rows[0].Cells[7].Paragraphs.First().Append("CANTIDAD ART");
                t.Rows[0].Cells[8].Paragraphs.First().Append("FECHA ENTREGA");
                t.Rows[0].Cells[9].Paragraphs.First().Append("ESTADO");
                t.Rows[0].Cells[10].Paragraphs.First().Append("EMPLEADO RESPONSABLE");
                t.Rows[0].Cells[11].Paragraphs.First().Append("MONEDA");
                t.Rows[0].Cells[12].Paragraphs.First().Append("TOTAL MONEDA");
                t.Rows[0].Cells[13].Paragraphs.First().Append("FECHA ACTUALIZACION");

                int contador = 0;
                foreach (DataRow fila in tabla.Rows)
                {
                    contador = contador + 1;
                    t.Rows[contador].Cells[0].Paragraphs.First().Append(fila.ItemArray[0].ToString());
                    t.Rows[contador].Cells[1].Paragraphs.First().Append(fila.ItemArray[1].ToString());
                    t.Rows[contador].Cells[2].Paragraphs.First().Append(fila.ItemArray[2].ToString());
                    t.Rows[contador].Cells[3].Paragraphs.First().Append(fila.ItemArray[3].ToString());
                    t.Rows[contador].Cells[4].Paragraphs.First().Append(fila.ItemArray[4].ToString());
                    t.Rows[contador].Cells[5].Paragraphs.First().Append(fila.ItemArray[5].ToString());
                    t.Rows[contador].Cells[6].Paragraphs.First().Append(fila.ItemArray[6].ToString());
                    t.Rows[contador].Cells[7].Paragraphs.First().Append(fila.ItemArray[7].ToString());
                    t.Rows[contador].Cells[8].Paragraphs.First().Append(fila.ItemArray[8].ToString());
                    t.Rows[contador].Cells[9].Paragraphs.First().Append(fila.ItemArray[9].ToString());
                    t.Rows[contador].Cells[10].Paragraphs.First().Append(fila.ItemArray[10].ToString());
                    t.Rows[contador].Cells[11].Paragraphs.First().Append(fila.ItemArray[11].ToString());
                    t.Rows[contador].Cells[12].Paragraphs.First().Append(fila.ItemArray[12].ToString());
                    t.Rows[contador].Cells[13].Paragraphs.First().Append(fila.ItemArray[13].ToString());
                }

                // Insert the Table into the document.
                document.InsertTable(t);

                string mdoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string ruta = mdoc + "/" + filename;

                document.SaveAs(ruta);

                return "guardado";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DevolucionPedidoPDF(PDF_ModeloDevolucion msolicitud)
        {
            try
            {
                DateTime ahora = DateTime.Now;
                string _dia = ahora.Day.ToString();
                string _mes = ahora.Month.ToString();
                string _anio = ahora.Year.ToString();
                string _hora = ahora.Hour.ToString();
                string _minuto = ahora.Minute.ToString();
                string _segundos = ahora.Second.ToString();
                for (int i = 0; i < 2; i++)
                {
                    if (_dia.Length < 2)
                        _dia = "0" + _dia;
                }
                for (int i = 0; i < 2; i++)
                {
                    if (_mes.Length < 2)
                        _mes = "0" + _mes;
                }
                for (int i = 0; i < 4; i++)
                {
                    if (_anio.Length < 4)
                        _anio = "0" + _anio;
                }
                string nombre = _anio + _mes + _dia + _hora + _minuto + _segundos + " DEVOLUCION PEDIDO N" + msolicitud.Folio;
                //Se instancia lo necesario
                string mdoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string nombreDoc = mdoc + "/" + nombre + ".pdf";

                // Creamos el documento con el tamaño de página carta y se definen los margenes
                Document doc = new Document(PageSize.LETTER, 40, 40, 40, 40);
                // Indicamos donde vamos a guardar el documento
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(nombreDoc, FileMode.CreateNew));

                // Le colocamos el título y el autor
                // **Nota: Esto no será visible en el documento
                doc.AddTitle("Ddevolución de Pedido");
                doc.AddCreator("SERVIEXPRESS");

                // Abrimos el archivo
                doc.Open();

                // Escribimos el encabezamiento en el documento
                // Creamos la imagen y le ajustamos el tamaño
                iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance("C:\\Serviexpress\\AppServiexpress\\Contenidos\\Imagenes\\Serviexpress_logo.png");
                imagen.BorderWidth = 0;
                imagen.Alignment = Element.ALIGN_LEFT;
                float percentage = 0.0f;
                percentage = 90 / imagen.Width;
                imagen.ScalePercent(percentage * 100);
                // Insertamos la imagen en el documento
                doc.Add(imagen);

                //Se intancia la clase que solicita la libreria
                iTextSharp.text.Paragraph paragraph = new iTextSharp.text.Paragraph();
                Phrase phrase = new Phrase();


                //Creamos e insertamos las secciones del documento                  
                //titulo
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Font = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD);
                paragraph.Add("FORMULARIO DE DEVOLUCIÓN");
                doc.Add(paragraph);
                paragraph.Clear();

                //contenido central
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Font = FontFactory.GetFont("arial", 10);
                paragraph.Add("\nProveedor: " + msolicitud.NombreProveedor + "  Rol: " + msolicitud.RolProveedor);
                doc.Add(paragraph);
                paragraph.Clear();
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Font = FontFactory.GetFont("arial", 10);
                paragraph.Add("Fecha Creación Formulario: " + DateTime.Now);
                doc.Add(paragraph);
                paragraph.Clear();
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Font = FontFactory.GetFont("arial", 10);
                paragraph.Add("Sucursal Recepción: " + msolicitud.Sucursal);
                doc.Add(paragraph);
                paragraph.Clear();
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Font = FontFactory.GetFont("arial", 10);
                paragraph.Add("Numero de Orden de Pedido: " + msolicitud.Folio);
                doc.Add(paragraph);
                paragraph.Clear();
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Font = FontFactory.GetFont("arial", 10);
                paragraph.Add("Estimado Proveedor, hemos generado un formulario de devolución de productos, ya que el pedido no fue recepcionado por los motivos que se detallan a continuación:");
                doc.Add(paragraph);
                paragraph.Clear();
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Font = FontFactory.GetFont("arial", 10);
                paragraph.Add(msolicitud.Motivo);
                doc.Add(paragraph);
                paragraph.Clear();

                paragraph.Add("\nProductos Solicitados en la Orden de Pedido \n");
                doc.Add(paragraph);
                paragraph.Clear();

                //tabla datos funcionario
                PdfPTable table = new PdfPTable(5);//CANTIDAD DE COLUMNAS
                                                   //definir tabaño de las columnas
                float[] widths = new float[] { 30f, 270f, 30f, 80f, 80f };
                table.SetWidths(widths);
                // anchura real de la tabla en puntos
                table.TotalWidth = 500f;
                // fijar el ancho absoluto de la tabla
                table.LockedWidth = true;

                /*datos*/
                //1° Tabla Productos
                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD));
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Add("Id Prod.");
                PdfPCell cell = new PdfPCell();
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                table.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD));
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Add("Nombre Producto");
                cell = new PdfPCell();
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                table.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD));
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Add("Cant.");
                cell = new PdfPCell();
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                table.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD));
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Add("Precio Compra");
                cell = new PdfPCell();
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                table.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD));
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Add("Precio Total");
                cell = new PdfPCell();
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                table.AddCell(cell);
                paragraph.Clear();

                foreach (var fila in msolicitud.DetalleOrdenPedido)
                {
                    paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 10));
                    paragraph.Alignment = Element.ALIGN_RIGHT;
                    paragraph.Add(fila.PRODUCTO_ID.ToString());
                    cell = new PdfPCell();
                    cell.PaddingTop = -7;
                    cell.AddElement(paragraph);
                    table.AddCell(cell);
                    paragraph.Clear();

                    paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 10));
                    paragraph.Alignment = Element.ALIGN_LEFT;
                    paragraph.Add(fila.NOMBRE_PRODUCTO.ToString());
                    cell = new PdfPCell();
                    cell.PaddingTop = -7;
                    cell.AddElement(paragraph);
                    table.AddCell(cell);
                    paragraph.Clear();

                    paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 10));
                    paragraph.Alignment = Element.ALIGN_CENTER;
                    paragraph.Add(fila.CANTIDAD.ToString());
                    cell = new PdfPCell();
                    cell.PaddingTop = -7;
                    cell.AddElement(paragraph);
                    table.AddCell(cell);
                    paragraph.Clear();

                    paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 10));
                    paragraph.Alignment = Element.ALIGN_RIGHT;
                    paragraph.Add(string.Format("{0:n2}", fila.PRECIO_COMPRA.ToString()));
                    cell = new PdfPCell();
                    cell.PaddingTop = -7;
                    cell.AddElement(paragraph);
                    table.AddCell(cell);
                    paragraph.Clear();

                    paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 10));
                    paragraph.Alignment = Element.ALIGN_RIGHT;
                    paragraph.Add(string.Format("{0:n2}", fila.MONTO_TOTAL.ToString()));
                    cell = new PdfPCell();
                    cell.PaddingTop = -7;
                    cell.AddElement(paragraph);
                    table.AddCell(cell);
                    paragraph.Clear();
                }

                doc.Add(table);
                paragraph.Add("\n\n\n");
                doc.Add(paragraph);
                paragraph.Clear();

                //tabla datos funcionario
                PdfPTable tabla = new PdfPTable(1);//CANTIDAD DE COLUMNAS
                                                   //definir tabaño de las columnas
                widths = new float[] { 500f };
                tabla.SetWidths(widths);
                // anchura real de la tabla en puntos
                tabla.TotalWidth = 500f;
                // fijar el ancho absoluto de la tabla
                tabla.LockedWidth = true;

                /*datos*/
                //1° linea
                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 12));
                paragraph.Alignment = Element.ALIGN_RIGHT;
                paragraph.Add("Atentamente le saluda, La Administración de la Sucursal");
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tabla.AddCell(cell);
                paragraph.Clear();

                doc.Add(tabla);
                paragraph.Add("\n");
                doc.Add(paragraph);
                paragraph.Clear();
                               
                //finaliza la escritura del documento
                doc.Close();
                writer.Close();

                return nombreDoc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Ventas_Excel(DataTable tabla, string nombre)
        {
            try
            {
                OfficeExcel.Application application = new OfficeExcel.Application();
                OfficeExcel.Workbook workbook = application.Workbooks.Add(Type.Missing);
                OfficeExcel.Worksheet worksheet = null;
                worksheet = workbook.Sheets["Hoja1"];
                worksheet = workbook.ActiveSheet;
                worksheet.Name = "VENTAS";

                worksheet.Cells[1, 1] = "ID";
                worksheet.Cells[1, 2] = "FOLIO";
                worksheet.Cells[1, 3] = "TIPO VENTA";
                worksheet.Cells[1, 4] = "FECHA VENTA";
                worksheet.Cells[1, 5] = "SUCURSAL";
                worksheet.Cells[1, 6] = "EMPLEADO";
                worksheet.Cells[1, 7] = "CANTIDAD ART";
                worksheet.Cells[1, 8] = "MONTO TOTAL";
                worksheet.Cells[1, 9] = "MONEDA";
                worksheet.Cells[1, 10] = "CLIENTE";
                worksheet.Cells[1, 11] = "ESTADO";

                int contador = 1;

                foreach (DataRow fila in tabla.Rows)
                {
                    contador = contador + 1;
                    worksheet.Cells[contador, 1] = fila.ItemArray[0].ToString();
                    worksheet.Cells[contador, 2] = fila.ItemArray[1].ToString();
                    worksheet.Cells[contador, 3] = fila.ItemArray[2].ToString();
                    worksheet.Cells[contador, 4] = fila.ItemArray[3].ToString();
                    worksheet.Cells[contador, 5] = fila.ItemArray[4].ToString();
                    worksheet.Cells[contador, 6] = fila.ItemArray[5].ToString();
                    worksheet.Cells[contador, 7] = fila.ItemArray[6].ToString();
                    worksheet.Cells[contador, 8] = fila.ItemArray[7].ToString();
                    worksheet.Cells[contador, 9] = fila.ItemArray[8].ToString();
                    worksheet.Cells[contador, 10] = fila.ItemArray[9].ToString();
                    worksheet.Cells[contador, 11] = fila.ItemArray[10].ToString();
                }

                var saveFileDialoge = new SaveFileDialog();
                saveFileDialoge.FileName = nombre;
                saveFileDialoge.DefaultExt = ".xlsx";
                workbook.SaveAs(nombre, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, OfficeExcel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                application.Quit();
                return "guardado";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Ventas_Word(DataTable tabla, string nombre)
        {
            try
            {
                string filename = nombre + ".docx";
                var document = DocX.Create(filename);
                document.PageLayout.Orientation = Orientation.Landscape;
                var titleParagraph = document.InsertParagraph();
                titleParagraph.Append("VENTAS").Heading(HeadingType.Heading1);

                Formatting textParagraphFormat = new Formatting();
                textParagraphFormat.FontFamily = new Xceed.Words.NET.Font("Arial Narrow");
                textParagraphFormat.Size = 8;

                int cantidadFilas = tabla.Rows.Count + 1;
                Table t = document.AddTable(cantidadFilas, 11);
                // Specify some properties for this Table.
                t.Alignment = Alignment.center;
                t.Design = TableDesign.MediumGrid1Accent2;
                // Add content to this Table.
                t.Rows[0].Cells[0].Paragraphs.First().Append("ID");
                t.Rows[0].Cells[1].Paragraphs.First().Append("FOLIO");
                t.Rows[0].Cells[2].Paragraphs.First().Append("TIPO VENTA");
                t.Rows[0].Cells[3].Paragraphs.First().Append("FECHA VENTA");
                t.Rows[0].Cells[4].Paragraphs.First().Append("SUCURSAL");
                t.Rows[0].Cells[5].Paragraphs.First().Append("EMPLEADO");
                t.Rows[0].Cells[6].Paragraphs.First().Append("CANTIDAD ART");
                t.Rows[0].Cells[7].Paragraphs.First().Append("MONTO TOTAL");
                t.Rows[0].Cells[8].Paragraphs.First().Append("MONEDA");
                t.Rows[0].Cells[9].Paragraphs.First().Append("CLIENTE");
                t.Rows[0].Cells[10].Paragraphs.First().Append("ESTADO");

                int contador = 0;
                foreach (DataRow fila in tabla.Rows)
                {
                    contador = contador + 1;
                    t.Rows[contador].Cells[0].Paragraphs.First().Append(fila.ItemArray[0].ToString());
                    t.Rows[contador].Cells[1].Paragraphs.First().Append(fila.ItemArray[1].ToString());
                    t.Rows[contador].Cells[2].Paragraphs.First().Append(fila.ItemArray[2].ToString());
                    t.Rows[contador].Cells[3].Paragraphs.First().Append(fila.ItemArray[3].ToString());
                    t.Rows[contador].Cells[4].Paragraphs.First().Append(fila.ItemArray[4].ToString());
                    t.Rows[contador].Cells[5].Paragraphs.First().Append(fila.ItemArray[5].ToString());
                    t.Rows[contador].Cells[6].Paragraphs.First().Append(fila.ItemArray[6].ToString());
                    t.Rows[contador].Cells[7].Paragraphs.First().Append(fila.ItemArray[7].ToString());
                    t.Rows[contador].Cells[8].Paragraphs.First().Append(fila.ItemArray[8].ToString());
                    t.Rows[contador].Cells[9].Paragraphs.First().Append(fila.ItemArray[9].ToString());
                    t.Rows[contador].Cells[10].Paragraphs.First().Append(fila.ItemArray[10].ToString());
                }

                // Insert the Table into the document.
                document.InsertTable(t);

                string mdoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string ruta = mdoc + "/" + filename;

                document.SaveAs(ruta);

                return "guardado";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Ventas_TextoPlano(DataTable tabla, string nombre)
        {
            try
            {
                String CadenaDataTable = "";
                string mdoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string ruta = mdoc + "/" + nombre + ".txt";
                StreamWriter file = new StreamWriter(ruta, true);
                file.WriteLine("ID;FOLIO;TIPO VENTA;FECHA VENTA;SUCURSAL;EMPLEADO;CANTIDAD ART;MONTO TOTAL;MONEDA;CLIENTE;ESTADO");
                foreach (DataRow row in tabla.Rows)
                {
                    CadenaDataTable = row["ID"] + ";" + row["FOLIO"] + ";" + row["TIPO VENTA"] + ";" + row["FECHA VENTA"] + ";" + row["SUCURSAL"] +
                        ";" + row["EMPLEADO"] + ";" + row["CANTIDAD ART"] + ";" + row["MONTO TOTAL"] + ";" + row["MONEDA"] + ";" + row["CLIENTE"] +
                        ";" + row["ESTADO"];
                    file.WriteLine(CadenaDataTable);
                }

                file.Close();
                return "guardado";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Requerimientos_Excel(DataTable tabla, string nombre)
        {
            try
            {
                OfficeExcel.Application application = new OfficeExcel.Application();
                OfficeExcel.Workbook workbook = application.Workbooks.Add(Type.Missing);
                OfficeExcel.Worksheet worksheet = null;
                worksheet = workbook.Sheets["Hoja1"];
                worksheet = workbook.ActiveSheet;
                worksheet.Name = "REQUERIMIENTOS";

                worksheet.Cells[1, 1] = "ID";
                worksheet.Cells[1, 2] = "FOLIO";
                worksheet.Cells[1, 3] = "ESTADO";
                worksheet.Cells[1, 4] = "FECHA RESERVA";
                worksheet.Cells[1, 5] = "SUCURSAL";
                worksheet.Cells[1, 6] = "PATENTE";
                worksheet.Cells[1, 7] = "TIPO VEHICULO";
                worksheet.Cells[1, 8] = "MARCA";
                worksheet.Cells[1, 9] = "CLIENTE SOLICITANTE";
                worksheet.Cells[1, 10] = "EMPLEADO RECEPCIONISTA";
                worksheet.Cells[1, 11] = "TOTAL PRESUPUESTO";

                int contador = 1;

                foreach (DataRow fila in tabla.Rows)
                {
                    contador = contador + 1;
                    worksheet.Cells[contador, 1] = fila.ItemArray[0].ToString();
                    worksheet.Cells[contador, 2] = fila.ItemArray[1].ToString();
                    worksheet.Cells[contador, 3] = fila.ItemArray[2].ToString();
                    worksheet.Cells[contador, 4] = fila.ItemArray[3].ToString();
                    worksheet.Cells[contador, 5] = fila.ItemArray[4].ToString();
                    worksheet.Cells[contador, 6] = fila.ItemArray[5].ToString();
                    worksheet.Cells[contador, 7] = fila.ItemArray[6].ToString();
                    worksheet.Cells[contador, 8] = fila.ItemArray[7].ToString();
                    worksheet.Cells[contador, 9] = fila.ItemArray[8].ToString();
                    worksheet.Cells[contador, 10] = fila.ItemArray[9].ToString();
                    worksheet.Cells[contador, 11] = fila.ItemArray[10].ToString();
                }

                var saveFileDialoge = new SaveFileDialog();
                saveFileDialoge.FileName = nombre;
                saveFileDialoge.DefaultExt = ".xlsx";
                workbook.SaveAs(nombre, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, OfficeExcel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                application.Quit();
                return "guardado";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Requerimientos_Word(DataTable tabla, string nombre)
        {
            try
            {
                string filename = nombre + ".docx";
                var document = DocX.Create(filename);
                document.PageLayout.Orientation = Orientation.Landscape;
                var titleParagraph = document.InsertParagraph();
                titleParagraph.Append("REQUERIMIENTOS").Heading(HeadingType.Heading1);

                int cantidadFilas = tabla.Rows.Count + 1;

                Table t = document.AddTable(cantidadFilas, 11);
                // Specify some properties for this Table.
                t.Alignment = Alignment.center;
                t.Design = TableDesign.MediumGrid1Accent2;
                // Add content to this Table.
                t.Rows[0].Cells[0].Paragraphs.First().Append("ID");
                t.Rows[0].Cells[1].Paragraphs.First().Append("FOLIO");
                t.Rows[0].Cells[2].Paragraphs.First().Append("ESTADO");
                t.Rows[0].Cells[3].Paragraphs.First().Append("FECHA RESERVA");
                t.Rows[0].Cells[4].Paragraphs.First().Append("SUCURSAL");
                t.Rows[0].Cells[5].Paragraphs.First().Append("PATENTE");
                t.Rows[0].Cells[6].Paragraphs.First().Append("TIPO VEHICULO");
                t.Rows[0].Cells[7].Paragraphs.First().Append("MARCA");
                t.Rows[0].Cells[8].Paragraphs.First().Append("CLIENTE SOLICITANTE");
                t.Rows[0].Cells[9].Paragraphs.First().Append("EMPLEADO RECEPCIONISTA");
                t.Rows[0].Cells[10].Paragraphs.First().Append("TOTAL PRESUPUESTO");
                int contador = 0;
                foreach (DataRow fila in tabla.Rows)
                {
                    contador = contador + 1;
                    t.Rows[contador].Cells[0].Paragraphs.First().Append(fila.ItemArray[0].ToString());
                    t.Rows[contador].Cells[1].Paragraphs.First().Append(fila.ItemArray[1].ToString());
                    t.Rows[contador].Cells[2].Paragraphs.First().Append(fila.ItemArray[2].ToString());
                    t.Rows[contador].Cells[3].Paragraphs.First().Append(fila.ItemArray[3].ToString());
                    t.Rows[contador].Cells[4].Paragraphs.First().Append(fila.ItemArray[4].ToString());
                    t.Rows[contador].Cells[5].Paragraphs.First().Append(fila.ItemArray[5].ToString());
                    t.Rows[contador].Cells[6].Paragraphs.First().Append(fila.ItemArray[6].ToString());
                    t.Rows[contador].Cells[7].Paragraphs.First().Append(fila.ItemArray[7].ToString());
                    t.Rows[contador].Cells[8].Paragraphs.First().Append(fila.ItemArray[8].ToString());
                    t.Rows[contador].Cells[9].Paragraphs.First().Append(fila.ItemArray[9].ToString());
                    t.Rows[contador].Cells[10].Paragraphs.First().Append(fila.ItemArray[10].ToString());
                }

                // Insert the Table into the document.
                document.InsertTable(t);              

                string mdoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string ruta = mdoc + "/" + filename;

                document.SaveAs(ruta);

                return "guardado";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DocumentoPagoPDF(DatosDocumentoPagoVIEW msolicitud, string folio)
        {
            try
            {
                DateTime ahora = DateTime.Now;
                string _dia = ahora.Day.ToString();
                string _mes = ahora.Month.ToString();
                string _anio = ahora.Year.ToString();
                string _hora = ahora.Hour.ToString();
                string _minuto = ahora.Minute.ToString();
                string _segundos = ahora.Second.ToString();
                for (int i = 0; i < 2; i++)
                {
                    if (_dia.Length < 2)
                        _dia = "0" + _dia;
                }
                for (int i = 0; i < 2; i++)
                {
                    if (_mes.Length < 2)
                        _mes = "0" + _mes;
                }
                for (int i = 0; i < 4; i++)
                {
                    if (_anio.Length < 4)
                        _anio = "0" + _anio;
                }
                string nombre = _anio + _mes + _dia + _hora + _minuto + _segundos + " DOCUMENTO PAGO N" + folio;
                //Se instancia lo necesario
                string mdoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string nombreDoc = mdoc + "/" + nombre + ".pdf";

                // Creamos el documento con el tamaño de página carta y se definen los margenes
                Document doc = new Document(PageSize.LETTER, 40, 40, 40, 40);
                // Indicamos donde vamos a guardar el documento
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(nombreDoc, FileMode.CreateNew));

                // Le colocamos el título y el autor
                // **Nota: Esto no será visible en el documento
                doc.AddTitle("Documento de Pago");
                doc.AddCreator("SERVIEXPRESS");

                // Abrimos el archivo
                doc.Open();

                // Escribimos el encabezamiento en el documento
                // Creamos la imagen y le ajustamos el tamaño
                iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance("C:\\Serviexpress\\AppServiexpress\\Contenidos\\Imagenes\\Serviexpress_logo.png");
                imagen.BorderWidth = 0;
                imagen.Alignment = Element.ALIGN_LEFT;
                float percentage = 0.0f;
                percentage = 90 / imagen.Width;
                imagen.ScalePercent(percentage * 100);
                // Insertamos la imagen en el documento
                doc.Add(imagen);

                //Se intancia la clase que solicita la libreria
                iTextSharp.text.Paragraph paragraph = new iTextSharp.text.Paragraph();
                Phrase phrase = new Phrase();


                //Creamos e insertamos las secciones del documento                  
               
                PdfPTable tabla = new PdfPTable(2);//CANTIDAD DE COLUMNAS
                                                   //definir tabaño de las columnas
                float[] widths = new float[] { 350f, 200f };
                tabla.SetWidths(widths);
                // anchura real de la tabla en puntos
                tabla.TotalWidth = 550f;
                // fijar el ancho absoluto de la tabla
                tabla.LockedWidth = true;

                /*datos*/
                //1° linea
                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD));
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Add(msolicitud.NOMBRE_MULTIEMPRESA);
                PdfPCell cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tabla.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD));
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Add("R.U.T.:"+msolicitud.RUT_MULTIEMPRESA);
                cell = new PdfPCell();
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tabla.AddCell(cell);
                paragraph.Clear();
                //
                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 7));
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Add(msolicitud.DIRECCION_MULTIEMPRESA);
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tabla.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD));
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Add(msolicitud.TIPO_DOCUMENTO);
                cell = new PdfPCell();
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tabla.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 7));
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Add("TELEFONO "+msolicitud.TELEFONO_MULTIEMPRESA);
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tabla.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD));
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Add("N° "+folio);
                cell = new PdfPCell();
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tabla.AddCell(cell);
                paragraph.Clear();               

                doc.Add(tabla);
                paragraph.Add("\n");
                doc.Add(paragraph);
                paragraph.Clear();

                //tabla datos funcionario
                PdfPTable tablaDatosCliente = new PdfPTable(4);//CANTIDAD DE COLUMNAS
                                                               //definir tabaño de las columnas
                widths = new float[] { 50f, 300f, 50f, 100f };
                tablaDatosCliente.SetWidths(widths);
                // anchura real de la tabla en puntos
                tablaDatosCliente.TotalWidth = 550f;
                // fijar el ancho absoluto de la tabla
                tablaDatosCliente.LockedWidth = true;

                /*datos*/
                //1° linea
                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 6));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add("Sr.(a)");
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaDatosCliente.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 6));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add(msolicitud.NOMBRE_CLIENTE+" "+msolicitud.APELLIDO_CLIENTE);
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaDatosCliente.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 6));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add("FECHA EMISIÓN");
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaDatosCliente.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 6));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add(msolicitud.FECHA_EMISION.ToString());
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaDatosCliente.AddCell(cell);
                paragraph.Clear();

                //2
                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 6));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add("DIRECCIÓN");
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaDatosCliente.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 6));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add(msolicitud.DIRECCION_CLIENTE);
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaDatosCliente.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 6));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add("RUT");
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaDatosCliente.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 6));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add(msolicitud.NUM_CLIENTE.ToString()+"-"+msolicitud.DIV_CLIENTE);
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaDatosCliente.AddCell(cell);
                paragraph.Clear();
                //3
                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 6));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add("COMUNA");
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaDatosCliente.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 6));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add(msolicitud.COMUNA_CLIENTE);
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaDatosCliente.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 6));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add("TELEFONO");
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaDatosCliente.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 6));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add(msolicitud.TELEFONO1_CLIENTE.ToString());
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaDatosCliente.AddCell(cell);
                paragraph.Clear();
                //4
                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 6));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add("TELEFONO 2");
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaDatosCliente.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add(msolicitud.TELEFONO_CLIENTE.ToString());
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaDatosCliente.AddCell(cell);
                paragraph.Clear();
                              

                doc.Add(tablaDatosCliente);
                paragraph.Add("\n");
                doc.Add(paragraph);
                paragraph.Clear();

                //Fin tabla

                //tabla datos funcionario
                PdfPTable tableServicios = new PdfPTable(4);//CANTIDAD DE COLUMNAS
                                                            //definir tabaño de las columnas
                widths = new float[] { 50f, 400f, 50f, 50f };
                tableServicios.SetWidths(widths);
                // anchura real de la tabla en puntos
                tableServicios.TotalWidth = 550f;
                // fijar el ancho absoluto de la tabla
                tableServicios.LockedWidth = true;

                /*datos*/
                //1° Tabla Productos
                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 6, iTextSharp.text.Font.BOLD));
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Add("CANTIDAD");
                cell = new PdfPCell();
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tableServicios.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 6, iTextSharp.text.Font.BOLD));
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Add("DETALLE");
                cell = new PdfPCell();
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tableServicios.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 6, iTextSharp.text.Font.BOLD));
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Add("P UNITARIO");
                cell = new PdfPCell();
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tableServicios.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 6, iTextSharp.text.Font.BOLD));
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Add("TOTAL");
                cell = new PdfPCell();
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tableServicios.AddCell(cell);
                paragraph.Clear();

                int total = 0;
                foreach (var fila in msolicitud.DETALLE_BOLETA)
                {
                    paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 6));
                    paragraph.Alignment = Element.ALIGN_CENTER;
                    paragraph.Add(fila.CANTIDAD.ToString());
                    cell = new PdfPCell();
                    cell.PaddingTop = -7;
                    cell.AddElement(paragraph);
                    tableServicios.AddCell(cell);
                    paragraph.Clear();

                    paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 6));
                    paragraph.Alignment = Element.ALIGN_LEFT;
                    paragraph.Add(fila.DESCRIPCION.ToString());
                    cell = new PdfPCell();
                    cell.PaddingTop = -7;
                    cell.AddElement(paragraph);
                    tableServicios.AddCell(cell);
                    paragraph.Clear();

                    paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 6));
                    paragraph.Alignment = Element.ALIGN_LEFT;
                    paragraph.Add(string.Format("{0:n2}", fila.PRECIO_UNITARIO.ToString()));
                    cell = new PdfPCell();
                    cell.PaddingTop = -7;
                    cell.AddElement(paragraph);
                    tableServicios.AddCell(cell);
                    paragraph.Clear();

                    paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 6));
                    paragraph.Alignment = Element.ALIGN_RIGHT;
                    paragraph.Add(string.Format("{0:n2}", fila.TOTAL.ToString()));
                    cell = new PdfPCell();
                    cell.PaddingTop = -7;
                    cell.AddElement(paragraph);
                    tableServicios.AddCell(cell);
                    paragraph.Clear();
                    total = total + int.Parse(fila.TOTAL.ToString());
                }

                doc.Add(tableServicios);
                paragraph.Add("\n");
                doc.Add(paragraph);
                paragraph.Clear();
                //fin tabla
                               

                //tabla datos funcionario
                PdfPTable tablaTotal = new PdfPTable(4);//CANTIDAD DE COLUMNAS
                                                        //definir tabaño de las columnas
                widths = new float[] { 50f, 400f, 50f, 50f };
                tablaTotal.SetWidths(widths);
                // anchura real de la tabla en puntos
                tablaTotal.TotalWidth = 550f;
                // fijar el ancho absoluto de la tabla
                tablaTotal.LockedWidth = true;

                /*datos*/
                //1° linea
                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 6, iTextSharp.text.Font.BOLD));
                paragraph.Alignment = Element.ALIGN_RIGHT;
                paragraph.Add(" ");
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaTotal.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 6, iTextSharp.text.Font.BOLD));
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Add("CANCELADO");
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaTotal.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 6, iTextSharp.text.Font.BOLD));
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Add("NETO");
                cell = new PdfPCell();
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaTotal.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 6));
                paragraph.Alignment = Element.ALIGN_RIGHT;
                double iva = total * 0.19;
                double neto = total;
                paragraph.Add(string.Format("{0:n2}", neto.ToString()));
                cell = new PdfPCell();
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaTotal.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 6, iTextSharp.text.Font.BOLD));
                paragraph.Alignment = Element.ALIGN_RIGHT;
                paragraph.Add(" ");
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaTotal.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 6, iTextSharp.text.Font.BOLD));
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Add(DateTime.Now.ToLongDateString());
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaTotal.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 6, iTextSharp.text.Font.BOLD));
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Add("19 % I.V.A.");
                cell = new PdfPCell();
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaTotal.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 6));
                paragraph.Alignment = Element.ALIGN_RIGHT;
                paragraph.Add(string.Format("{0:n2}", iva.ToString()));
                cell = new PdfPCell();
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaTotal.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 6, iTextSharp.text.Font.BOLD));
                paragraph.Alignment = Element.ALIGN_RIGHT;
                paragraph.Add(" ");
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaTotal.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 6, iTextSharp.text.Font.BOLD));
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Add(" ");
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaTotal.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 6, iTextSharp.text.Font.BOLD));
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Add("TOTAL");
                cell = new PdfPCell();
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaTotal.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 6));
                paragraph.Alignment = Element.ALIGN_RIGHT;
                double algo = neto + iva;
                paragraph.Add(string.Format("{0:n2}", algo.ToString()));
                cell = new PdfPCell();
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaTotal.AddCell(cell);
                paragraph.Clear();



                ////

                doc.Add(tablaTotal);
                paragraph.Add("\n");
                doc.Add(paragraph);
                paragraph.Clear();

                // Escribimos el encabezamiento en el documento
                // Creamos la imagen y le ajustamos el tamaño
                imagen = iTextSharp.text.Image.GetInstance("C:\\Serviexpress\\AppServiexpress\\Contenidos\\Imagenes\\timbre.jpg");
                imagen.BorderWidth = 0;
                imagen.Alignment = Element.ALIGN_CENTER;
                percentage = 0.0f;
                percentage = 90 / imagen.Width;
                imagen.ScalePercent(percentage * 200);
                // Insertamos la imagen en el documento
                doc.Add(imagen);
                //finaliza la escritura del documento
                doc.Close();
                writer.Close();

                return nombreDoc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Requerimientos_TextoPlano(DataTable tabla, string nombre)
        {
            try
            {
                String CadenaDataTable = "";
                string mdoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string ruta = mdoc + "/" + nombre + ".txt";
                StreamWriter file = new StreamWriter(ruta, true);
                file.WriteLine("ID;FOLIO;ESTADO;FECHA RESERVA;SUCURSAL;PATENTE;TIPO VEHICULO;MARCA;CLIENTE SOLICITANTE;EMPLEADO RECEPCIONISTA;EMPLEADO RESPONSABLE;TOTAL PRESUPUESTO");
                foreach (DataRow row in tabla.Rows)
                {
                    CadenaDataTable = row["ID"] + ";" + row["FOLIO"] + ";" + row["ESTADO"] + ";" + row["FECHA RESERVA"] + ";" + row["SUCURSAL"] +
                        ";" + row["PATENTE"] + ";" + row["TIPO VEHICULO"] + ";" + row["MARCA"] + ";" + row["CLIENTE SOLICITANTE"] + ";" + row["EMPLEADO RECEPCIONISTA"] +
                        ";" + row["TOTAL PRESUPUESTO"];
                    file.WriteLine(CadenaDataTable);
                }                            
                
                file.Close();
                return "guardado";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string OrdenTrabajoPedidoPDF(PDF_ModeloOrdenTrabajo msolicitud)
        {
            try
            {
                DateTime ahora = DateTime.Now;
                string _dia = ahora.Day.ToString();
                string _mes = ahora.Month.ToString();
                string _anio = ahora.Year.ToString();
                string _hora = ahora.Hour.ToString();
                string _minuto = ahora.Minute.ToString();
                string _segundos = ahora.Second.ToString();
                for (int i = 0; i < 2; i++)
                {
                    if (_dia.Length < 2)
                        _dia = "0" + _dia;
                }
                for (int i = 0; i < 2; i++)
                {
                    if (_mes.Length < 2)
                        _mes = "0" + _mes;
                }
                for (int i = 0; i < 4; i++)
                {
                    if (_anio.Length < 4)
                        _anio = "0" + _anio;
                }
                string nombre = _anio + _mes + _dia + _hora + _minuto + _segundos + " ORDEN DE TRABAJO N" + msolicitud.Folio;
                //Se instancia lo necesario
                string mdoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string nombreDoc = mdoc + "/" + nombre + ".pdf";

                // Creamos el documento con el tamaño de página carta y se definen los margenes
                Document doc = new Document(PageSize.LETTER, 40, 40, 40, 40);
                // Indicamos donde vamos a guardar el documento
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(nombreDoc, FileMode.CreateNew));

                // Le colocamos el título y el autor
                // **Nota: Esto no será visible en el documento
                doc.AddTitle("Orden de Trabajo");
                doc.AddCreator("SERVIEXPRESS");

                // Abrimos el archivo
                doc.Open();

                // Escribimos el encabezamiento en el documento
                // Creamos la imagen y le ajustamos el tamaño
                iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance("C:\\Serviexpress\\AppServiexpress\\Contenidos\\Imagenes\\Serviexpress_logo.png");
                imagen.BorderWidth = 0;
                imagen.Alignment = Element.ALIGN_LEFT;
                float percentage = 0.0f;
                percentage = 90 / imagen.Width;
                imagen.ScalePercent(percentage * 100);
                // Insertamos la imagen en el documento
                doc.Add(imagen);

                //Se intancia la clase que solicita la libreria
                iTextSharp.text.Paragraph paragraph = new iTextSharp.text.Paragraph();
                Phrase phrase = new Phrase();


                //Creamos e insertamos las secciones del documento                  
                //titulo
                //titulo
                paragraph.Alignment = Element.ALIGN_RIGHT;
                paragraph.Font = FontFactory.GetFont("Arial", 9);
                paragraph.Add(DateTime.Now.ToLongDateString());
                doc.Add(paragraph);
                paragraph.Clear();
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Font = FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.BOLD);
                paragraph.Add("ORDEN DE TRABAJO");
                doc.Add(paragraph);
                paragraph.Clear();

                paragraph.Add("\n");
                doc.Add(paragraph);
                paragraph.Clear();

                //tabla datos funcionario
                PdfPTable tabla = new PdfPTable(4);//CANTIDAD DE COLUMNAS
                                                   //definir tabaño de las columnas
                float[] widths = new float[] { 75f, 285f, 75f, 100f };
                tabla.SetWidths(widths);
                // anchura real de la tabla en puntos
                tabla.TotalWidth = 550f;
                // fijar el ancho absoluto de la tabla
                tabla.LockedWidth = true;

                /*datos*/
                //1° linea
                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add("Nro. OT");
                PdfPCell cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tabla.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add(msolicitud.Folio);
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tabla.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add("Fecha Reserva");
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tabla.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add(msolicitud.FechaReserva);
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tabla.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9));
                paragraph.Alignment = Element.ALIGN_RIGHT;
                paragraph.Add(" ");
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tabla.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9));
                paragraph.Alignment = Element.ALIGN_RIGHT;
                paragraph.Add(" ");
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tabla.AddCell(cell);
                paragraph.Clear();
                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add("Fecha Ingreso");
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tabla.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add(msolicitud.FechaIngreso);
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tabla.AddCell(cell);
                paragraph.Clear();

                doc.Add(tabla);
                paragraph.Add("\n");
                doc.Add(paragraph);
                paragraph.Clear();

                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Font = FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.BOLD);
                paragraph.Add("INFORMACIÓN DEL INGRESO");
                doc.Add(paragraph);
                paragraph.Clear();

                paragraph.Add("\n");
                doc.Add(paragraph);
                paragraph.Clear();

                //tabla datos funcionario
                PdfPTable tablaDatosCliente = new PdfPTable(4);//CANTIDAD DE COLUMNAS
                                                               //definir tabaño de las columnas
                widths = new float[] { 75f, 285f, 75f, 100f };
                tablaDatosCliente.SetWidths(widths);
                // anchura real de la tabla en puntos
                tablaDatosCliente.TotalWidth = 550f;
                // fijar el ancho absoluto de la tabla
                tablaDatosCliente.LockedWidth = true;

                /*datos*/
                //1° linea
                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add("Sr.(a)");
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaDatosCliente.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add(msolicitud.NombreCliente + " " + msolicitud.ApellidoCliente);
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaDatosCliente.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add("RUT");
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaDatosCliente.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add(msolicitud.NumCliente + "-" + msolicitud.DivCliente);
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaDatosCliente.AddCell(cell);
                paragraph.Clear();

                //2
                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add("DIRECCIÓN");
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaDatosCliente.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add(msolicitud.DireccionCliente);
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaDatosCliente.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add("COMUNA");
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaDatosCliente.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add(msolicitud.ComunaCliente);
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaDatosCliente.AddCell(cell);
                paragraph.Clear();
                //3
                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add("TELEFONO 1");
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaDatosCliente.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add(msolicitud.TelCliente);
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaDatosCliente.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add("TELEFONO 2");
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaDatosCliente.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add(msolicitud.Tel2Cliente);
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaDatosCliente.AddCell(cell);
                paragraph.Clear();
                //4
                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add("TIPO VEHICULO");
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaDatosCliente.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add(msolicitud.TipoVehiculo);
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaDatosCliente.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add("MARCA V");
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaDatosCliente.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add(msolicitud.MarcaVehiculo);
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaDatosCliente.AddCell(cell);
                paragraph.Clear();
                //5
                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add("PATENTE V.");
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaDatosCliente.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add(msolicitud.PatenteVehiculo);
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaDatosCliente.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9));
                paragraph.Alignment = Element.ALIGN_RIGHT;
                paragraph.Add(" ");
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaDatosCliente.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9));
                paragraph.Alignment = Element.ALIGN_RIGHT;
                paragraph.Add(" ");
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaDatosCliente.AddCell(cell);
                paragraph.Clear();
                //6
                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add("SUCURSAL");
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaDatosCliente.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add(msolicitud.NombreSucursal);
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaDatosCliente.AddCell(cell);
                paragraph.Clear();
                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add("RECEPCION");
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaDatosCliente.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add(msolicitud.NombreEmpleado);
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaDatosCliente.AddCell(cell);
                paragraph.Clear();


                //7
                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add("DIR. SUCURSAL");
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaDatosCliente.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add(msolicitud.DireccionSucursal);
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaDatosCliente.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add("TEL. SUCURSAL");
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaDatosCliente.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add(msolicitud.TelefonoSucursal);
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaDatosCliente.AddCell(cell);
                paragraph.Clear();

                doc.Add(tablaDatosCliente);
                paragraph.Add("\n");
                doc.Add(paragraph);
                paragraph.Clear();

                //Fin tabla

                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Font = FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.BOLD);
                paragraph.Add("SERVICIOS UTILIZADOS");
                doc.Add(paragraph);
                paragraph.Clear();

                paragraph.Add("\n");
                doc.Add(paragraph);
                paragraph.Clear();

                //tabla datos funcionario
                PdfPTable tableServicios = new PdfPTable(4);//CANTIDAD DE COLUMNAS
                                                            //definir tabaño de las columnas
                widths = new float[] { 50f, 300f, 100f,100f };
                tableServicios.SetWidths(widths);
                // anchura real de la tabla en puntos
                tableServicios.TotalWidth = 550f;
                // fijar el ancho absoluto de la tabla
                tableServicios.LockedWidth = true;

                /*datos*/
                //1° Tabla Productos
                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.BOLD));
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Add("ID SERV.");
                cell = new PdfPCell();
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tableServicios.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.BOLD));
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Add("NOMBRE DEL SERVICIO");
                cell = new PdfPCell();
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tableServicios.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.BOLD));
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Add("ESTADO");
                cell = new PdfPCell();
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tableServicios.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.BOLD));
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Add("COSTO");
                cell = new PdfPCell();
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tableServicios.AddCell(cell);
                paragraph.Clear();

                int total = 0;
                foreach (var fila in msolicitud.ListaServicios)
                {
                    paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9));
                    paragraph.Alignment = Element.ALIGN_CENTER;
                    paragraph.Add(fila.IdServicio.ToString());
                    cell = new PdfPCell();
                    cell.PaddingTop = -7;
                    cell.AddElement(paragraph);
                    tableServicios.AddCell(cell);
                    paragraph.Clear();

                    paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9));
                    paragraph.Alignment = Element.ALIGN_LEFT;
                    paragraph.Add(fila.NombreServicio.ToString());
                    cell = new PdfPCell();
                    cell.PaddingTop = -7;
                    cell.AddElement(paragraph);
                    tableServicios.AddCell(cell);
                    paragraph.Clear();

                    paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9));
                    paragraph.Alignment = Element.ALIGN_LEFT;
                    paragraph.Add(fila.EstadoServicio.ToString());
                    cell = new PdfPCell();
                    cell.PaddingTop = -7;
                    cell.AddElement(paragraph);
                    tableServicios.AddCell(cell);
                    paragraph.Clear();

                    paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9));
                    paragraph.Alignment = Element.ALIGN_RIGHT;
                    paragraph.Add(string.Format("{0:n2}", fila.CostoServicio));
                    cell = new PdfPCell();
                    cell.PaddingTop = -7;
                    cell.AddElement(paragraph);
                    tableServicios.AddCell(cell);
                    paragraph.Clear();
                    total = total+int.Parse(fila.CostoServicio.ToString());
                }

                doc.Add(tableServicios);
                paragraph.Add("\n");
                doc.Add(paragraph);
                paragraph.Clear();
                //fin tabla

                if (msolicitud.ListaProductos.Count > 0)
                {
                    paragraph.Alignment = Element.ALIGN_LEFT;
                    paragraph.Font = FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.BOLD);
                    paragraph.Add("PRODUCTOS UTILIZADOS");
                    doc.Add(paragraph);
                    paragraph.Clear();

                    paragraph.Add("\n");
                    doc.Add(paragraph);
                    paragraph.Clear();
                    //tabla datos funcionario
                    PdfPTable tableProductos = new PdfPTable(5);//CANTIDAD DE COLUMNAS
                                                                //definir tabaño de las columnas
                    widths = new float[] { 50f, 250f, 50f, 100f, 100f };
                    tableProductos.SetWidths(widths);
                    // anchura real de la tabla en puntos
                    tableProductos.TotalWidth = 550f;
                    // fijar el ancho absoluto de la tabla
                    tableProductos.LockedWidth = true;

                    /*datos*/
                    //1° Tabla Productos
                    paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.BOLD));
                    paragraph.Alignment = Element.ALIGN_CENTER;
                    paragraph.Add("ID PROD.");
                    cell = new PdfPCell();
                    cell.PaddingTop = -7;
                    cell.AddElement(paragraph);
                    tableProductos.AddCell(cell);
                    paragraph.Clear();

                    paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.BOLD));
                    paragraph.Alignment = Element.ALIGN_CENTER;
                    paragraph.Add("NOMBRE DEL PRODUCTO");
                    cell = new PdfPCell();
                    cell.PaddingTop = -7;
                    cell.AddElement(paragraph);
                    tableProductos.AddCell(cell);
                    paragraph.Clear();

                    paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.BOLD));
                    paragraph.Alignment = Element.ALIGN_CENTER;
                    paragraph.Add("CANT");
                    cell = new PdfPCell();
                    cell.PaddingTop = -7;
                    cell.AddElement(paragraph);
                    tableProductos.AddCell(cell);
                    paragraph.Clear();

                    paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.BOLD));
                    paragraph.Alignment = Element.ALIGN_CENTER;
                    paragraph.Add("COSTO U.");
                    cell = new PdfPCell();
                    cell.PaddingTop = -7;
                    cell.AddElement(paragraph);
                    tableProductos.AddCell(cell);
                    paragraph.Clear();

                    paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.BOLD));
                    paragraph.Alignment = Element.ALIGN_CENTER;
                    paragraph.Add("COSTO");
                    cell = new PdfPCell();
                    cell.PaddingTop = -7;
                    cell.AddElement(paragraph);
                    tableProductos.AddCell(cell);
                    paragraph.Clear();

                    foreach (var fila in msolicitud.ListaProductos)
                    {
                        paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9));
                        paragraph.Alignment = Element.ALIGN_CENTER;
                        paragraph.Add(fila.IdProducto.ToString());
                        cell = new PdfPCell();
                        cell.PaddingTop = -7;
                        cell.AddElement(paragraph);
                        tableProductos.AddCell(cell);
                        paragraph.Clear();

                        paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9));
                        paragraph.Alignment = Element.ALIGN_LEFT;
                        paragraph.Add(fila.NombreProdcuto.ToString());
                        cell = new PdfPCell();
                        cell.PaddingTop = -7;
                        cell.AddElement(paragraph);
                        tableProductos.AddCell(cell);
                        paragraph.Clear();

                        paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9));
                        paragraph.Alignment = Element.ALIGN_RIGHT;
                        paragraph.Add(fila.Cantidad.ToString());
                        cell = new PdfPCell();
                        cell.PaddingTop = -7;
                        cell.AddElement(paragraph);
                        tableProductos.AddCell(cell);
                        paragraph.Clear();

                        paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9));
                        paragraph.Alignment = Element.ALIGN_RIGHT;
                        paragraph.Add(string.Format("{0:n2}", fila.PrecioUni));
                        cell = new PdfPCell();
                        cell.PaddingTop = -7;
                        cell.AddElement(paragraph);
                        tableProductos.AddCell(cell);
                        paragraph.Clear();

                        paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9));
                        paragraph.Alignment = Element.ALIGN_RIGHT;
                        paragraph.Add(string.Format("{0:n2}", fila.PrecioTot));
                        cell = new PdfPCell();
                        cell.PaddingTop = -7;
                        cell.AddElement(paragraph);
                        tableProductos.AddCell(cell);
                        paragraph.Clear();
                        total = total + int.Parse(fila.PrecioTot.ToString());
                    }

                    doc.Add(tableProductos);
                    paragraph.Add("\n");
                    doc.Add(paragraph);
                    paragraph.Clear();
                }
               

                //tabla datos funcionario
                PdfPTable tablaTotal = new PdfPTable(2);//CANTIDAD DE COLUMNAS
                                                   //definir tabaño de las columnas
                widths = new float[] { 450f,100f };
                tablaTotal.SetWidths(widths);
                // anchura real de la tabla en puntos
                tablaTotal.TotalWidth = 550f;
                // fijar el ancho absoluto de la tabla
                tablaTotal.LockedWidth = true;

                /*datos*/
                //1° linea
                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.BOLD));
                paragraph.Alignment = Element.ALIGN_RIGHT;
                paragraph.Add("TOTAL PRESUPUESTADO");
                cell = new PdfPCell();
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaTotal.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 10));
                paragraph.Alignment = Element.ALIGN_RIGHT;
                paragraph.Add(string.Format("{0:n2}", total));
                cell = new PdfPCell();
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tablaTotal.AddCell(cell);
                paragraph.Clear();

                doc.Add(tablaTotal);
                paragraph.Add("\n");
                doc.Add(paragraph);
                paragraph.Clear();

                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Font = FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.BOLD);
                paragraph.Add("OBSERVACIONES");
                doc.Add(paragraph);
                paragraph.Clear();
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Font = FontFactory.GetFont("Arial", 9);
                paragraph.Add(msolicitud.Observacion);
                doc.Add(paragraph);
                paragraph.Clear();

                paragraph.Add("\n\n\n");
                doc.Add(paragraph);
                paragraph.Clear();

                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Font = FontFactory.GetFont("Arial", 8);
                paragraph.Add("**Durante el desarrollo de su solicitud pueden ser incluidos productos según sean requeridos para concretar la Orden de Trabajo");
                doc.Add(paragraph);
                paragraph.Clear();

                //finaliza la escritura del documento
                doc.Close();
                writer.Close();

                return nombreDoc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string OrdenesPedidos_Excel(DataTable tabla, string nombre)
        {
            try
            {
                OfficeExcel.Application application = new OfficeExcel.Application();
                OfficeExcel.Workbook workbook = application.Workbooks.Add(Type.Missing);
                OfficeExcel.Worksheet worksheet = null;
                worksheet = workbook.Sheets["Hoja1"];
                worksheet = workbook.ActiveSheet;
                worksheet.Name = "ORDENES DE PEDIDOS";

                worksheet.Cells[1, 1]="ID";
                worksheet.Cells[1, 2]="FOLIO";
                worksheet.Cells[1, 3]="FECHA CREACION";
                worksheet.Cells[1, 4]="SUCURSAL";
                worksheet.Cells[1, 5]="RUT PROVEEDOR";
                worksheet.Cells[1, 6]="PROVEEDOR";
                worksheet.Cells[1, 7]="MONTO TOTAL";
                worksheet.Cells[1, 8]="CANTIDAD ART";
                worksheet.Cells[1, 9]="FECHA ENTREGA";
                worksheet.Cells[1, 10]="ESTADO";
                worksheet.Cells[1, 11]="EMPLEADO RESPONSABLE";
                worksheet.Cells[1, 12]="MONEDA";
                worksheet.Cells[1, 13]="TOTAL MONEDA";
                worksheet.Cells[1, 14]="FECHA ACTUALIZACION";                

                int contador = 1;

                foreach (DataRow fila in tabla.Rows)
                {
                    contador = contador + 1;
                    worksheet.Cells[contador, 1] = fila.ItemArray[0].ToString();
                    worksheet.Cells[contador, 2] = fila.ItemArray[1].ToString();
                    worksheet.Cells[contador, 3] = fila.ItemArray[2].ToString();
                    worksheet.Cells[contador, 4] = fila.ItemArray[3].ToString();
                    worksheet.Cells[contador, 5] = fila.ItemArray[4].ToString();
                    worksheet.Cells[contador, 6] = fila.ItemArray[5].ToString();
                    worksheet.Cells[contador, 7] = fila.ItemArray[6].ToString();
                    worksheet.Cells[contador, 8] = fila.ItemArray[7].ToString();
                    worksheet.Cells[contador, 9] = fila.ItemArray[8].ToString();
                    worksheet.Cells[contador, 10] = fila.ItemArray[9].ToString();
                    worksheet.Cells[contador, 11] = fila.ItemArray[10].ToString();
                    worksheet.Cells[contador, 12] = fila.ItemArray[11].ToString();
                    worksheet.Cells[contador, 13] = fila.ItemArray[12].ToString();
                    worksheet.Cells[contador, 14] = fila.ItemArray[13].ToString();
                }

                var saveFileDialoge = new SaveFileDialog();
                saveFileDialoge.FileName = nombre;
                saveFileDialoge.DefaultExt = ".xlsx";
                workbook.SaveAs(nombre, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,OfficeExcel.XlSaveAsAccessMode.xlExclusive, Type.Missing,Type.Missing,Type.Missing,Type.Missing, Type.Missing);
                application.Quit();
                return "guardado";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ControlRecpecion_TextoPlano(DataTable tabla, string nombre)
        {
            try
            {
                String CadenaDataTable = "";
                string mdoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string ruta = mdoc + "/" + nombre + ".txt";
                StreamWriter file = new StreamWriter(ruta, true);
                file.WriteLine("ID;FOLIO ORDEN;FECHA RECEPCION;ESTADO RECEPCION;EMPLEADO RESPONSABLE;COMENTARIO");
                foreach (DataRow row in tabla.Rows)
                {
                    CadenaDataTable = row["ID"] + ";" + row["FOLIO ORDEN"] + ";" + row["FECHA RECEPCION"] + ";" + row["ESTADO RECEPCION"] + ";" + row["EMPLEADO RESPONSABLE"] +
                        ";" + row["COMENTARIO"];
                    file.WriteLine(CadenaDataTable);
                }                
                
                file.Close();
                return "guardado";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ControlRecepcion_Word(DataTable tabla, string nombre)
        {
            try
            {
                string filename = nombre + ".docx";
                var document = DocX.Create(filename);
                document.PageLayout.Orientation = Orientation.Landscape;
                var titleParagraph = document.InsertParagraph();
                titleParagraph.Append("CONTROL DE RECEPCION").Heading(HeadingType.Heading1);

                Formatting textParagraphFormat = new Formatting();
                textParagraphFormat.FontFamily = new Xceed.Words.NET.Font("Arial Narrow");
                textParagraphFormat.Size = 8;

                int cantidadFilas = tabla.Rows.Count + 1;
                Table t = document.AddTable(cantidadFilas, 6);
                // Specify some properties for this Table.
                t.Alignment = Alignment.center;
                t.Design = TableDesign.MediumGrid1Accent2;
                // Add content to this Table.
                t.Rows[0].Cells[0].Paragraphs.First().Append("ID");
                t.Rows[0].Cells[1].Paragraphs.First().Append("FOLIO ORDEN");
                t.Rows[0].Cells[2].Paragraphs.First().Append("FECHA RECEPCION");
                t.Rows[0].Cells[3].Paragraphs.First().Append("ESTADO RECEPCION");
                t.Rows[0].Cells[4].Paragraphs.First().Append("EMPLEADO RESPONSABLE");
                t.Rows[0].Cells[5].Paragraphs.First().Append("COMENTARIO");

                int contador = 0;
                foreach (DataRow fila in tabla.Rows)
                {
                    contador = contador + 1;
                    t.Rows[contador].Cells[0].Paragraphs.First().Append(fila.ItemArray[0].ToString());
                    t.Rows[contador].Cells[1].Paragraphs.First().Append(fila.ItemArray[1].ToString());
                    t.Rows[contador].Cells[2].Paragraphs.First().Append(fila.ItemArray[2].ToString());
                    t.Rows[contador].Cells[3].Paragraphs.First().Append(fila.ItemArray[3].ToString());
                    t.Rows[contador].Cells[4].Paragraphs.First().Append(fila.ItemArray[4].ToString());
                    t.Rows[contador].Cells[5].Paragraphs.First().Append(fila.ItemArray[5].ToString());
                }

                // Insert the Table into the document.
                document.InsertTable(t);

                string mdoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string ruta = mdoc + "/" + filename;

                document.SaveAs(ruta);

                return "guardado";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ControlRecepcion_Excel(DataTable tabla, string nombre)
        {
            try
            {
                OfficeExcel.Application application = new OfficeExcel.Application();
                OfficeExcel.Workbook workbook = application.Workbooks.Add(Type.Missing);
                OfficeExcel.Worksheet worksheet = null;
                worksheet = workbook.Sheets["Hoja1"];
                worksheet = workbook.ActiveSheet;
                worksheet.Name = "CONtROL DE RECEPCION";

                worksheet.Cells[1, 1] = "ID";
                worksheet.Cells[1, 2] = "FOLIO ORDEN";
                worksheet.Cells[1, 3] = "FECHA RECEPCION";
                worksheet.Cells[1, 4] = "ESTADO RECEPCION";
                worksheet.Cells[1, 5] = "EMPLEADO RESPONSABLE";
                worksheet.Cells[1, 6] = "COMENTARIO";

                int contador = 1;

                foreach (DataRow fila in tabla.Rows)
                {
                    contador = contador + 1;
                    worksheet.Cells[contador, 1] = fila.ItemArray[0].ToString();
                    worksheet.Cells[contador, 2] = fila.ItemArray[1].ToString();
                    worksheet.Cells[contador, 3] = fila.ItemArray[2].ToString();
                    worksheet.Cells[contador, 4] = fila.ItemArray[3].ToString();
                    worksheet.Cells[contador, 5] = fila.ItemArray[4].ToString();
                    worksheet.Cells[contador, 6] = fila.ItemArray[5].ToString();
                }

                var saveFileDialoge = new SaveFileDialog();
                saveFileDialoge.FileName = nombre;
                saveFileDialoge.DefaultExt = ".xlsx";
                workbook.SaveAs(nombre, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, OfficeExcel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                application.Quit();
                return "guardado";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string OrdenPedidoPDF(PDF_ModeloOrdenPedido msolicitud)
        {
            try
            {
                DateTime ahora = DateTime.Now;
                string _dia = ahora.Day.ToString();
                string _mes = ahora.Month.ToString();
                string _anio = ahora.Year.ToString();
                string _hora = ahora.Hour.ToString();
                string _minuto = ahora.Minute.ToString();
                string _segundos = ahora.Second.ToString();
                for (int i = 0; i < 2; i++)
                {
                    if (_dia.Length < 2)
                        _dia = "0" + _dia;
                }
                for (int i = 0; i < 2; i++)
                {
                    if (_mes.Length < 2)
                        _mes = "0" + _mes;
                }
                for (int i = 0; i < 4; i++)
                {
                    if (_anio.Length < 4)
                        _anio = "0" + _anio;
                }
                string nombre = _anio + _mes + _dia + _hora + _minuto + _segundos + " ORDEN DE PEDIDO N"+msolicitud.Folio;
                //Se instancia lo necesario
                string mdoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string nombreDoc = mdoc + "/" + nombre + ".pdf";

                // Creamos el documento con el tamaño de página carta y se definen los margenes
                Document doc = new Document(PageSize.LETTER, 40, 40, 40, 40);
                // Indicamos donde vamos a guardar el documento
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(nombreDoc, FileMode.CreateNew));

                // Le colocamos el título y el autor
                // **Nota: Esto no será visible en el documento
                doc.AddTitle("Orden de Pedido");
                doc.AddCreator("SERVIEXPRESS");

                // Abrimos el archivo
                doc.Open();

                // Escribimos el encabezamiento en el documento
                // Creamos la imagen y le ajustamos el tamaño
                iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance("C:\\Serviexpress\\AppServiexpress\\Contenidos\\Imagenes\\Serviexpress_logo.png");
                imagen.BorderWidth = 0;
                imagen.Alignment = Element.ALIGN_LEFT;
                float percentage = 0.0f;
                percentage = 90 / imagen.Width;
                imagen.ScalePercent(percentage * 100);
                // Insertamos la imagen en el documento
                doc.Add(imagen);

                //Se intancia la clase que solicita la libreria
                iTextSharp.text.Paragraph paragraph = new iTextSharp.text.Paragraph();
                Phrase phrase = new Phrase();


                //Creamos e insertamos las secciones del documento  
                //folio
                paragraph.Alignment = Element.ALIGN_RIGHT;
                paragraph.Font = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD);
                paragraph.Add("FOLIO:  "+msolicitud.Folio);
                doc.Add(paragraph);
                paragraph.Clear();
                //titulo
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Font = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD);
                paragraph.Add("ORDEN DE PEDIDO");
                doc.Add(paragraph);
                paragraph.Clear();

                //contenido central
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Font = FontFactory.GetFont("arial", 10);
                paragraph.Add("\nProveedor: "+msolicitud.NombreProveedor+"  Rol: "+msolicitud.RolProveedor);
                doc.Add(paragraph);
                paragraph.Clear();
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Font = FontFactory.GetFont("arial", 10);
                paragraph.Add("Fecha Ingreso Solicitud: " + msolicitud.FechaSolicitud + "\nFecha Modificación Solicitud: " + msolicitud.FechaModificacion);
                doc.Add(paragraph);
                paragraph.Clear();
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Font = FontFactory.GetFont("arial", 10);
                paragraph.Add("Sucursal Solicitante: " + msolicitud.Sucursal);
                doc.Add(paragraph);
                paragraph.Clear();
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Font = FontFactory.GetFont("arial", 10);
                paragraph.Add("Dirección Despacho: " + msolicitud.Direccion);
                doc.Add(paragraph);
                paragraph.Clear();
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Font = FontFactory.GetFont("arial", 10);
                paragraph.Add("Telefono: " + msolicitud.Telefono + "     Email Sucursal: " + msolicitud.EmailSucursal);
                doc.Add(paragraph);
                paragraph.Clear();

               
                paragraph.Add("\n");
                doc.Add(paragraph);
                paragraph.Clear();

                //tabla datos funcionario
                PdfPTable table = new PdfPTable(5);//CANTIDAD DE COLUMNAS
                                                   //definir tabaño de las columnas
                float[] widths = new float[] { 30f, 270f,30f,80f,80f };
                table.SetWidths(widths);                
                // anchura real de la tabla en puntos
                table.TotalWidth = 500f;
                // fijar el ancho absoluto de la tabla
                table.LockedWidth = true;

                /*datos*/
                //1° Tabla Productos
                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD));
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Add("Id Prod.");
                PdfPCell cell = new PdfPCell();
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                table.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD));
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Add("Nombre Producto");
                cell = new PdfPCell();
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                table.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD));
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Add("Cant.");
                cell = new PdfPCell();
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                table.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD));
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Add("Precio Compra");
                cell = new PdfPCell();
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                table.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD));
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Add("Precio Total");
                cell = new PdfPCell();
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                table.AddCell(cell);
                paragraph.Clear();

                foreach (var fila in msolicitud.DetalleOrdenPedido)
                {
                    paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 10));
                    paragraph.Alignment = Element.ALIGN_RIGHT;
                    paragraph.Add(fila.PRODUCTO_ID.ToString());
                    cell = new PdfPCell();
                    cell.PaddingTop = -7;
                    cell.AddElement(paragraph);
                    table.AddCell(cell);
                    paragraph.Clear();

                    paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 10));
                    paragraph.Alignment = Element.ALIGN_LEFT;
                    paragraph.Add(fila.NOMBRE_PRODUCTO.ToString());
                    cell = new PdfPCell();
                    cell.PaddingTop = -7;
                    cell.AddElement(paragraph);
                    table.AddCell(cell);
                    paragraph.Clear();

                    paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 10));
                    paragraph.Alignment = Element.ALIGN_CENTER;
                    paragraph.Add(fila.CANTIDAD.ToString());
                    cell = new PdfPCell();
                    cell.PaddingTop = -7;
                    cell.AddElement(paragraph);
                    table.AddCell(cell);
                    paragraph.Clear();

                    paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 10));
                    paragraph.Alignment = Element.ALIGN_RIGHT;
                    paragraph.Add(string.Format("{0:n2}", fila.PRECIO_COMPRA.ToString()));
                    cell = new PdfPCell();
                    cell.PaddingTop = -7;
                    cell.AddElement(paragraph);
                    table.AddCell(cell);
                    paragraph.Clear();

                    paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 10));
                    paragraph.Alignment = Element.ALIGN_RIGHT;
                    paragraph.Add(string.Format("{0:n2}", fila.MONTO_TOTAL.ToString()));
                    cell = new PdfPCell();
                    cell.PaddingTop = -7;
                    cell.AddElement(paragraph);
                    table.AddCell(cell);
                    paragraph.Clear();
                }                              

                doc.Add(table);
                paragraph.Add("\n");
                doc.Add(paragraph);
                paragraph.Clear();

                //tabla datos funcionario
                PdfPTable tabla = new PdfPTable(4);//CANTIDAD DE COLUMNAS
                                                   //definir tabaño de las columnas
                widths = new float[] { 50f, 200f, 170f, 80f };
                tabla.SetWidths(widths);
                // anchura real de la tabla en puntos
                tabla.TotalWidth = 500f;
                // fijar el ancho absoluto de la tabla
                tabla.LockedWidth = true;

                /*datos*/
                //1° linea
                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 10));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add(" ");
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tabla.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 10));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add(" ");
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tabla.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 10));
                paragraph.Alignment = Element.ALIGN_RIGHT;
                paragraph.Add("Costo Total: $");
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tabla.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 10));
                paragraph.Alignment = Element.ALIGN_RIGHT;
                paragraph.Add(string.Format("{0:n2}",msolicitud.CostoTotal));
                cell = new PdfPCell();
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tabla.AddCell(cell);
                paragraph.Clear();

                //2° linea
                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 10));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add("Moneda:");
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tabla.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 10));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add(msolicitud.Moneda);
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tabla.AddCell(cell);
                paragraph.Clear();
                
                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 10));
                paragraph.Alignment = Element.ALIGN_RIGHT;
                paragraph.Add("Costo Total(Segun Moneda): $");
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tabla.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 10));
                paragraph.Alignment = Element.ALIGN_RIGHT;
                paragraph.Add(string.Format("{0:n2}", msolicitud.ConstoMoneda));
                cell = new PdfPCell();
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tabla.AddCell(cell);
                paragraph.Clear();

                doc.Add(tabla);
                paragraph.Add("\n");
                doc.Add(paragraph);
                paragraph.Clear();

                PdfPTable tabla2 = new PdfPTable(1);//CANTIDAD DE COLUMNAS
                                                   //definir tabaño de las columnas
                widths = new float[] { 540f };
                tabla2.SetWidths(widths);
                // anchura real de la tabla en puntos
                tabla2.TotalWidth = 540f;
                // fijar el ancho absoluto de la tabla
                tabla2.LockedWidth = true;

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 11));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add("Empleado Solicitante: "+ msolicitud.CodigoEmpleado + " " + msolicitud.NombreEMpleado);
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tabla2.AddCell(cell);
                paragraph.Clear();
                //
                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 11));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add("Email Proveedor: "+ msolicitud.EmailProveedor);
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tabla2.AddCell(cell);
                paragraph.Clear();
                doc.Add(tabla2);
                //finaliza la escritura del documento
                doc.Close();
                writer.Close();

                return nombreDoc;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string ControlRecepcionPedidoPDF(PDF_ModeloControlRecepcion modelo)
        {
            try
            {
                DateTime ahora = DateTime.Now;
                string _dia = ahora.Day.ToString();
                string _mes = ahora.Month.ToString();
                string _anio = ahora.Year.ToString();
                string _hora = ahora.Hour.ToString();
                string _minuto = ahora.Minute.ToString();
                string _segundos = ahora.Second.ToString();
                for (int i = 0; i < 2; i++)
                {
                    if (_dia.Length < 2)
                        _dia = "0" + _dia;
                }
                for (int i = 0; i < 2; i++)
                {
                    if (_mes.Length < 2)
                        _mes = "0" + _mes;
                }
                for (int i = 0; i < 4; i++)
                {
                    if (_anio.Length < 4)
                        _anio = "0" + _anio;
                }
                string nombre = _anio + _mes + _dia + _hora + _minuto + _segundos + " CONTROL RECEPCION PEDIDO N" + modelo.Folio;
                //Se instancia lo necesario
                string mdoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string nombreDoc = mdoc + "/" + nombre + ".pdf";

                // Creamos el documento con el tamaño de página carta y se definen los margenes
                Document doc = new Document(PageSize.LETTER, 40, 40, 40, 40);
                // Indicamos donde vamos a guardar el documento
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(nombreDoc, FileMode.CreateNew));

                // Le colocamos el título y el autor
                // **Nota: Esto no será visible en el documento
                doc.AddTitle("Control de Recepcion");
                doc.AddCreator("SERVIEXPRESS");

                // Abrimos el archivo
                doc.Open();

                // Escribimos el encabezamiento en el documento
                // Creamos la imagen y le ajustamos el tamaño
                iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance("C:\\Serviexpress\\AppServiexpress\\Contenidos\\Imagenes\\Serviexpress_logo.png");
                imagen.BorderWidth = 0;
                imagen.Alignment = Element.ALIGN_LEFT;
                float percentage = 0.0f;
                percentage = 90 / imagen.Width;
                imagen.ScalePercent(percentage * 100);
                // Insertamos la imagen en el documento
                doc.Add(imagen);

                //Se intancia la clase que solicita la libreria
                iTextSharp.text.Paragraph paragraph = new iTextSharp.text.Paragraph();
                Phrase phrase = new Phrase();


                //Creamos e insertamos las secciones del documento  
                //folio
                paragraph.Alignment = Element.ALIGN_RIGHT;
                paragraph.Font = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD);
                paragraph.Add("FOLIO PEDIDO:  " + modelo.Folio);
                doc.Add(paragraph);
                paragraph.Clear();
                //titulo
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Font = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD);
                paragraph.Add("CONTROL DE RECEPCIÓN DE PEDIDO");
                doc.Add(paragraph);
                paragraph.Clear();

                //contenido central
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Font = FontFactory.GetFont("arial", 10);
                paragraph.Add("\nProveedor: " + modelo.NombreProveedor + "  Rol: " + modelo.RolProveedor);
                doc.Add(paragraph);
                paragraph.Clear();
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Font = FontFactory.GetFont("arial", 10);
                paragraph.Add("Sucursal: " + modelo.NombreSucursal);
                doc.Add(paragraph);
                paragraph.Clear();
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Font = FontFactory.GetFont("arial", 10);
                paragraph.Add("Fecha de Recepción: " + modelo.FechaRecepcion);
                doc.Add(paragraph);
                paragraph.Clear();
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Font = FontFactory.GetFont("arial", 10);
                paragraph.Add("Empleado Responsable: " + modelo.CodigoEmpleado + " "+modelo.NombreEmpleado);
                doc.Add(paragraph);
                paragraph.Clear();
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Font = FontFactory.GetFont("arial", 10);
                paragraph.Add("Estado de Conformidad: " + modelo.EstadoControl);
                doc.Add(paragraph);
                paragraph.Clear();


                paragraph.Add("\n");
                doc.Add(paragraph);
                paragraph.Clear();

                //tabla datos funcionario
                PdfPTable table = new PdfPTable(4);//CANTIDAD DE COLUMNAS
                                                   //definir tabaño de las columnas
                float[] widths = new float[] { 50f, 300f, 50f, 100f};
                table.SetWidths(widths);
                // anchura real de la tabla en puntos
                table.TotalWidth = 500f;
                // fijar el ancho absoluto de la tabla
                table.LockedWidth = true;

                /*datos*/
                //1° Tabla Productos
                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD));
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Add("Id Prod.");
                PdfPCell cell = new PdfPCell();
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                table.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD));
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Add("Nombre Producto");
                cell = new PdfPCell();
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                table.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD));
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Add("Cant.");
                cell = new PdfPCell();
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                table.AddCell(cell);
                paragraph.Clear();

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD));
                paragraph.Alignment = Element.ALIGN_CENTER;
                paragraph.Add("Precio Uni");
                cell = new PdfPCell();
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                table.AddCell(cell);
                paragraph.Clear();

                foreach (var fila in modelo.DetalleControlRecepcion)
                {
                    paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 10));
                    paragraph.Alignment = Element.ALIGN_RIGHT;
                    paragraph.Add(fila.PRODUCTO_ID.ToString());
                    cell = new PdfPCell();
                    cell.PaddingTop = -7;
                    cell.AddElement(paragraph);
                    table.AddCell(cell);
                    paragraph.Clear();

                    paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 10));
                    paragraph.Alignment = Element.ALIGN_LEFT;
                    paragraph.Add(fila.NOMBRE_PRODUCTO.ToString());
                    cell = new PdfPCell();
                    cell.PaddingTop = -7;
                    cell.AddElement(paragraph);
                    table.AddCell(cell);
                    paragraph.Clear();

                    paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 10));
                    paragraph.Alignment = Element.ALIGN_CENTER;
                    paragraph.Add(fila.CANTIDAD_INGRESADA.ToString());
                    cell = new PdfPCell();
                    cell.PaddingTop = -7;
                    cell.AddElement(paragraph);
                    table.AddCell(cell);
                    paragraph.Clear();

                    paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 10));
                    paragraph.Alignment = Element.ALIGN_RIGHT;
                    paragraph.Add(string.Format("{0:n2}", fila.PRECIO_COMPRA.ToString()));
                    cell = new PdfPCell();
                    cell.PaddingTop = -7;
                    cell.AddElement(paragraph);
                    table.AddCell(cell);
                    paragraph.Clear();
                }

                doc.Add(table);
                paragraph.Add("\n");
                doc.Add(paragraph);
                paragraph.Clear();               

                PdfPTable tabla2 = new PdfPTable(1);//CANTIDAD DE COLUMNAS
                                                    //definir tabaño de las columnas
                widths = new float[] { 540f };
                tabla2.SetWidths(widths);
                // anchura real de la tabla en puntos
                tabla2.TotalWidth = 540f;
                // fijar el ancho absoluto de la tabla
                tabla2.LockedWidth = true;

                paragraph.Font = new iTextSharp.text.Font(FontFactory.GetFont("Arial", 11));
                paragraph.Alignment = Element.ALIGN_LEFT;
                paragraph.Add("Comentario: " + modelo.Comentario);
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.PaddingTop = -7;
                cell.AddElement(paragraph);
                tabla2.AddCell(cell);
                paragraph.Clear();
                doc.Add(tabla2);
                //finaliza la escritura del documento
                doc.Close();
                writer.Close();

                return "creado";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
