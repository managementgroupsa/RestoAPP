using Syncfusion.XForms.Pickers;
using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using Xamarin.Forms;
using System.Text;

namespace RestoAPP.Extensions
{

    public class Token
    {
        public string AccessToken { get; set; }

        public int ExpiresIn { get; set; }

        public string TokenType { get; set; }

        public string Scope { get; set; }
    }

    public class Estado_Documento
    {
        public string cStatVtaElect { get; set; }
        public string cValResElect { get; set; }
    }

    public static class Estado_Sunat
    {
        public static string Aceptado = "ACEPTADO";
        public static string NoExiste = "NO EXISTE";
        public static string Rechazado= "RECHAZADO";
    }


    public static class RepuestaVentanaFlotante
    {
        public static string Aceptado = "1";
        public static string Cancelado = "0";
    }


    public static class Resultado
    {
        public static string Encontrado = "1";
        public static string NoEncontrado = "0";
        public static string Error = "-1";
    }

    public static class Opciones
    {
        public static string Nuevo = "1";
        public static string Consulta = "2";
        public static string Editar = "3";
        public static string Eliminar = "4";
        public static string Imprimir = "5";
    }

    public static class Propiedades
    {

        public static System.Data.DataTable CreateEntityToDataTable(Type oObbjeto, string cNombreTabla)
        {
            System.Data.DataTable return_Datatable = new System.Data.DataTable();
            foreach (PropertyInfo info in oObbjeto.GetProperties())
            {
                return_Datatable.Columns.Add(new System.Data.DataColumn(info.Name, info.PropertyType));
            }
            return_Datatable.TableName = cNombreTabla;
            return return_Datatable;
        }
        public static void SoloLecturaPicker(Picker Objeto, bool Condicion)
        {
            Objeto.IsEnabled = !Condicion;

            Color cColor;

            if (Condicion == true)
                cColor = Color.DarkGray;
            else
                cColor = Color.Black;

            Objeto.TextColor = cColor;
        }

        public static void SoloLecturaEntry(Entry Objeto, bool Condicion)
        {
            Objeto.IsReadOnly = Condicion;

            Color cColor;

            if (Condicion == true)
                cColor = Color.Black;// DarkGray;
            else
                cColor = Color.Black;

            Objeto.TextColor = cColor;
        }


        public static void SoloLecturaEditor(Editor Objeto, bool Condicion)
        {
            Objeto.IsReadOnly = Condicion;

            Color cColor;

            if (Condicion == true)
                cColor = Color.DarkGray;
            else
                cColor = Color.Black;

            Objeto.TextColor = cColor;
        }

    }
    public static class General
    {


        public static int RetornaIndicePicker(Picker Objeto, string valor)
        {
            int indice = 0;

            foreach (var item in Objeto.Items)
            {
                if (General.PrimerValor(Objeto.Items[indice].ToString()) == valor)
                {
                    break;
                }

                indice = indice + 1;
            }

            return indice;
        }

        public static string PrimerValor(string Valor)
        {
            string cRetorno = "";
            try
            {
                char c = '-';
                int index = Valor.IndexOf(c);

                if (index > 0)
                    cRetorno = CE(Left(Valor, index - 1));
                else
                    cRetorno = CE(Valor);

            }
            catch (Exception)
            {
            }
            return cRetorno;
        }

        public static string ValidaJSON(string Valor)
        {
            string cRetorno = "";
            try
            {
                if ( General.Left(Valor,1) != "[")
                {
                    cRetorno="[" + Valor + "]"; 
                }

                else
                {
                    cRetorno =  Valor ;
                }

            }
            catch (Exception)
            {
            }
            return cRetorno;
        }

        public static string SegundoValor(string Valor)
        {
            string cRetorno = "";
            try
            {
                char c = '-';
                int index = Valor.IndexOf(c);
                Valor = CE(Valor);
                cRetorno = Mid(Valor, index + 2, Valor.Length);

            }
            catch (Exception)
            {
            }
            return cRetorno;

        }


        public static Double NE(object Valor)
        {
            if (Valor == null || Valor == "")
            {
                return 0;
            }
            else
            {
                return Convert.ToDouble(Valor);
            }
        }

        public static string CE(object Valor)
        {
            if (Valor == null)
            {
                return "";
            }
            else
            {
                return Convert.ToString(Valor);
            }
        }

        public static string FechaISO(string Valor)
        {
            Valor = General.Left(Valor, 10);

            return General.Right(Valor, 4)  + General.Right(General.Left(Valor, 5), 2) +  General.Left(Valor, 2);
        }

        public static string ISOFecha(string Valor)
        {
            Valor = General.Left(Valor, 10);

            return   General.Right(Valor, 2) + '/' + General.Right(General.Left(Valor, 6), 2)  + '/' + General.Left(Valor, 4);
        }

        public static DateTime ISO_To_DateTime(string Valor)
        {
            DateTime _dFecha= DateTime.Now;

            try
            {
                Valor = General.Left(Valor, 10);
               
                DateTime.TryParseExact(Valor, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _dFecha);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
                
            }

            return _dFecha;
        }

        public static string Mid(this string strSource, int intFirst, int intLength)
        {
            string cCadena = "";
            try
            {
                if (string.IsNullOrEmpty(strSource))
                    cCadena ="";
                else if (intFirst > strSource.Length)
                    cCadena ="";
                else
                {
                    if (intFirst + intLength > strSource.Length)
                        intLength = strSource.Length - intFirst;
                    cCadena =strSource.Substring(intFirst, intLength);
                }
            }
            catch (Exception ex)
            {
            }
            return cCadena;
        }

        public static DateTime FE(object Valor)
        {
            if (Valor == null || Valor == "")
            {
                return Convert.ToDateTime("01/01/1900");
            }
            else if (Convert.ToDateTime(Valor).Year <= 1900)
            {
                return Convert.ToDateTime("01/01/1900");
            }
            else
            {
                return Convert.ToDateTime(Valor);
            }
        }

        public static string Right(this string input, int count)
        {
            return input.Substring(Math.Max(input.Length - count, 0), Math.Min(count, input.Length));
        }

        public static string Left(this string input, int count)
        {
            return input.Substring(0, Math.Min(input.Length, count));
        }

    }
}
