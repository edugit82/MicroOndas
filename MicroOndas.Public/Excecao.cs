using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroOndas.Public
{    
    public static class Excecao
    {        
        public static void GravaExcecao(Exception? ex)
        {
            try
            {
                string texto = "";
                texto = string.Format("   Hora: {0:dd-MM-yyyy HH:mm:ss}{1}", DateTime.Now, Environment.NewLine);
                texto += string.Format("   Mensagem: {0}{1}{1}", ex?.Message, Environment.NewLine);
                do
                {
                    texto += ex?.StackTrace + Environment.NewLine;
                    ex = ex?.InnerException;
                }
                while (ex is not null);                

                string folder = Environment.CurrentDirectory + @"\Error_Folder";
                string file = folder + @"\Error.txt";

                Directory.CreateDirectory(folder);

                if (!File.Exists(file))
                    File.Create(file);

                if (File.Exists(file))
                    using (StreamReader sr = new StreamReader(file))
                    {
                        texto += string.Format("{0}{1}{1}", "--------------Erro--------------", Environment.NewLine);
                        texto += sr.ReadToEnd();
                    }

                using (StreamWriter sw = new StreamWriter(file))
                    sw.Write(texto);
            }
            catch
            {
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
    }
}
