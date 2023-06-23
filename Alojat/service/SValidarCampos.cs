using Alojat.interfa;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Alojat.service
{
    public class SValidarCampos : IValidarCampos
    {
        public bool ValidarCaractes(string numString)
        {
            char[] charArr = numString.ToCharArray();
            foreach (char cd in charArr)
            {
                if (!char.IsLetterOrDigit(cd) && !char.IsSeparator(cd))
                {
                    return false;
                }
            }
            return true;
        }
        public int ValidarEdad(string edad)
        {
            string request = edad;
            int result = Int32.Parse(request);
            return result;
        }
        public bool ValidarLetras(string numString)
        {

            string parte = numString.Trim();
            int count = parte.Count(s => s == ' ');
            if (parte == "")
            {
                return false;
            }
            else if (count > 1)
            {
                return false;
            }
            char[] charArr = parte.ToCharArray();
            foreach (char cd in charArr)
            {
                if (!char.IsLetter(cd) && !char.IsSeparator(cd))
                    return false;
            }
            return true;
        }
        public bool ValidarLetras2(string numString)
        {

            string parte = numString.Trim();

            int count = parte.Count(s => s == ' ');
            if (parte == "")
            {
                return false;
            }
            else if (count > 3)
            {
                return false;
            }
            char[] charArr = parte.ToCharArray();
            foreach (char cd in charArr)
            {
                if (!char.IsLetter(cd) && !char.IsSeparator(cd))
                    return false;
            }
            return true;
        }
        public bool ValidarnUMEROS(string numString)
        {
            char[] charArr = numString.ToCharArray();
            foreach (char cd in charArr)
            {
                if (!char.IsNumber(cd))
                    return false;
            }
            return true;
        }
        public bool ValidarUnaPalabra(string numString)
        {
            char[] charArr = numString.ToCharArray();
            foreach (char cd in charArr)
            {
                if (!char.IsLetter(cd))
                    return false;
            }
            return true;
        }
        public bool ValidarEmail(string email)
        {
            String expresion;

            expresion = "\\w+([-+.']\\w+)*@\\w+([-.])*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public bool ValidarDomicilio(string email)
        {
            String expresion;

            expresion = "^.*(?=.*[0-9])(?=.*[a-zA-ZñÑ\\s]).*$";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public bool ValidarPrecio(string precio)
        {
            var regex = new Regex(@"^[0-9]*(?:\.[0-9]*)?$");

            if (!regex.IsMatch(precio))
            {
                return false;
            }
            else { return true; }
        }
        public bool Precio(string precio)
        {
            float val = Convert.ToSingle(precio, CultureInfo.CreateSpecificCulture("en-US"));

            if (val > 2000.00 || val < 5.00)
            {
                return false;
            }

            return true;
        }
        public bool ValidarNombreExpreciones(string nombre)
        {
            String expresion;

            expresion = "[a-zA-ZñÑ\\s]{2,50}";
            if (Regex.IsMatch(nombre, expresion))
            {
                if (Regex.Replace(nombre, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public bool ValidarStock(int nombre)
        {
            string stoc = nombre.ToString();

            String expresion;

            expresion = "[0-9]{1,9}(\\.[0-9]{0,2})?$";
            if (Regex.IsMatch(stoc, expresion))
            {
                if (Regex.Replace(stoc, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public bool ValidarDireccion(string direccion)
        {
            String expresion;

            expresion = "^.*(?=.*[0-9])(?=.*[a-zA-ZñÑ\\s]).*$";
            if (Regex.IsMatch(direccion, expresion))
            {
                if (Regex.Replace(direccion, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool ValidarCoordenadas(string coordenadas)
        {
            Regex regex = new(@"^[-+]?([1-8]?\d(\.\d+)?|90(\.0+)?),\s*[-+]?(180(\.0+)?|((1[0-7]\d)|([1-9]?\d))(\.\d+)?)$");

            if (!regex.IsMatch(coordenadas))
            {
                return false;
            }

            return true;
        }

        public bool ValidarLetrasNumeros(string piso)
        {
            Regex regex = new(@"^[a-zA-Z0-9ñÑ ]+$");

            if (!regex.IsMatch(piso))
            {
                return false;
            }

            return true;
        }

        public bool ValidarSoloLetras(string numString)
        {
            Regex regex = new(@"^[a-zA-Z]+$");

            if (!regex.IsMatch(numString))
            {
                return false;
            }

            return true;
        }

        public bool ValidarFecha(string fecha)
        {
            Regex regex = new(@"\b(?<month>\d{2})/(?<day>\d{2})/(?<year>\d{4})\b");

            if (!regex.IsMatch(fecha))
            {
                return false;
            }

            return true;
        }
    }
}
