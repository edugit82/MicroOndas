using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroOndas.Business
{
    public static class LogDeErros
    {
        public static void Log(ref RetornoAquecimentoViewModel viewmodel,Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                GravaExcecao(ref viewmodel,ex);
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
        private static void GravaExcecao(ref RetornoAquecimentoViewModel viewmodel, Exception ex)
        {
            try
            {
                string texto = "";
                texto = string.Format("   Hora: {0:HH:mm:ss}{1}", DateTime.Now, Environment.NewLine);
                texto += string.Format("   Mensagem: {0}{1}{1}", ex.Message, Environment.NewLine);
                do
                {
                    texto += ex.StackTrace + Environment.NewLine;
                    ex = ex.InnerException;
                }
                while (!(ex is null));

                viewmodel.Mensagem = texto;

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

