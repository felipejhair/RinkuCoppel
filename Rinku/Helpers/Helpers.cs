using System.Data;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Rinku.Helpers
{
    /// <summary>
    /// Clase de helpers, para facilitar funcionalidades
    /// </summary>
    public class Helpers
    {
        /// <summary>
        /// Funcion para traer elementos del archivo de configuracion
        /// </summary>
        /// <param name="environmentName"></param>
        /// <param name="addUserSecrets"></param>
        /// <returns></returns>
        public static IConfigurationRoot GetConfiguration(string environmentName = null, bool addUserSecrets = false)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);

            if (!String.IsNullOrWhiteSpace(environmentName))
            {
                builder = builder.AddJsonFile($"appsettings.{environmentName}.json", optional: true);
            }

            builder = builder.AddEnvironmentVariables();

            return builder.Build();
        }

        /// <summary>
        /// Convierte los datos de una tabla en un String tipo JSON
        /// </summary>
        /// <param name="table">Tabla que se va a convertir</param>
        /// <param name="single">True para regresar un solo registro, False regresa un arreglo</param>
        /// <returns></returns>
        public static string DtToJson(DataTable table, bool single = false)
        {

            //Se obtienen los nombres de cada columna
            string[] columnNames = table.Columns.Cast<DataColumn>()
                                                .Select(x => x.ColumnName.ToLower())
                                                .ToArray();

            //Se recorre el string para hacer la sanitizacion
            foreach (string name in columnNames)
            {
                string newName = SanitizeString(name);
                //Se cambia el nombre de la columna
                table.Columns[name].ColumnName = newName;
            }

            string Json = "";

            if (single == true)
            {
                Json = JsonConvert.SerializeObject(table).Replace("[", "").Replace("]", "");
            }
            else
            {
                Json = JsonConvert.SerializeObject(table);
            }


            return Json;
        }

        public static string SanitizeString(string word)
        {
            string retWord = "";

            //Se eliminan espacios
            string newWord = word.Replace(" ", string.Empty);
            //Se convierte todo a miniscula
            //newWord = (CultureInfo.InvariantCulture.TextInfo.ToTitleCase(newWord.ToLower()));

            //Se eliminan acentos y caracteres especiales
            string[] newWordArr = newWord.Split($"/./");

            foreach (string letter in newWordArr)
            {
                string correct_letter = FormattedLetter(letter);
                retWord += correct_letter;
            }

            return retWord;
        }

        /// <summary>
        /// Formateo de letras para quitar simbolos extraos y dejar la palabra
        /// sanitizada
        /// </summary>
        /// <param name="letter"></param>
        /// <returns></returns>
        public static string FormattedLetter(string letter)
        {
            return letter switch
            {
                "'" => "",
                "ñ" => "n",
                "á" => "a",
                "é" => "e",
                "í" => "i",
                "ó" => "o",
                "ú" => "u",
                "à" => "a",
                "è" => "e",
                "ì" => "i",
                "ò" => "o",
                "ù" => "u",
                "*" => "",
                "#" => "",
                "+" => "",
                "%" => "",
                "-" => "",
                "," => "",
                "  " => "",
                "." => "",
                "/" => "",
                "" => "",
                "=" => "",
                "?" => "",
                "¿" => "",
                "!" => "",
                "¡" => "",
                "ü" => "u",
                "$" => "",
                "|" => "",
                "°" => "",
                "¬" => "",
                "<" => "",
                ">" => "",
                "[" => "",
                "]" => "",
                "{" => "",
                "}" => "",
                ")" => "",
                "(" => "",
                "^" => "",
                "`" => "",
                "´" => "",
                "&" => "",
                ";" => "",
                ":" => "",
                "~" => "",
                _ => letter,
            };
        }

        /// <summary>
        /// Funcion para guardar logs en caso de presentar errores
        /// en la pagina
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="ex"></param>
        public static void EventsLog(object obj, Exception ex)
        {
            try
            {
                string fecha = System.DateTime.Now.ToString("yyyyMMdd");
                string hora = System.DateTime.Now.ToString("HH:mm:ss");
                string path = Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Log");
                string nameLog = "Log-" + fecha + ".txt";
                //
                if (!File.Exists(path))
                    Directory.CreateDirectory(path);
                //
                path = Path.Combine(path, nameLog);
                StreamWriter sw = new StreamWriter(path, true);

                StackTrace stacktrace = new StackTrace();
                sw.WriteLine(fecha + " " + hora + " " + obj.GetType().FullName);
                sw.WriteLine(stacktrace.GetFrame(1).GetMethod().Name + " - " + ex.Message + "\n" + ex.ToString() + "\n");
                //sw.WriteLine("");
                sw.Flush();
                sw.Close();
            }
            catch (Exception)
            {
            }
        }
    }
}
