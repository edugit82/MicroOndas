using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroOndas.Business
{
    public class CadastrarLoginInvalidoException : Exception
    {
        public CadastrarLoginInvalidoException(string message) : base(message)
        {            
            
        }        
    }
    public class CadastrarSenhaInvalidoException : Exception
    {
        public CadastrarSenhaInvalidoException(string message) : base(message)
        {
            
        }
    }
    public class CadastrarRepetirSenhaInvalidoException : Exception
    {
        public CadastrarRepetirSenhaInvalidoException(string message) : base(message)
        {            
        }
    }
    public class CadastrarRepetirDiferenteSenhaInvalidoException : Exception
    {
        public CadastrarRepetirDiferenteSenhaInvalidoException(string message) : base(message)
        {
            
        }
    }
    public class CadastrarUsuarioJaCadastradoException : Exception
    {
        public CadastrarUsuarioJaCadastradoException(string message) : base(message)
        {            
        }
    }
    public class LogarLoginInvalidoException : Exception
    {
        public LogarLoginInvalidoException(string message) : base(message)
        {
        }
    }
    public class LogarSenhaInvalidoException : Exception
    {
        public LogarSenhaInvalidoException(string message) : base(message)
        {
        }
    }
    public class LogarUsuarioInvalidoException : Exception
    {
        public LogarUsuarioInvalidoException(string message) : base(message)
        {
        }
    }
    public class AquecimentoPonteciaInvalidaException : Exception
    {
        public AquecimentoPonteciaInvalidaException(string message) : base(message)
        {
        }
    }
    public class AquecimentoTempoExcedidoException : Exception
    {
        public AquecimentoTempoExcedidoException(string message) : base(message)
        {
        }
    }
    public class AquecimentoTempoInvalidoException : Exception
    {
        public AquecimentoTempoInvalidoException(string message) : base(message)
        {
        }
    }
    public class CadastrarProgramaNomeInvalidoException : Exception
    {
        public CadastrarProgramaNomeInvalidoException(string message) : base(message)
        {
        }
    }
    public class CadastrarProgramaAlimentoInvalidoException : Exception
    {
        public CadastrarProgramaAlimentoInvalidoException(string message) : base(message)
        {
        }
    }
    public class CadastrarProgramaTempoInvalidoException : Exception
    {
        public CadastrarProgramaTempoInvalidoException(string message) : base(message)
        {
        }
    }
    public class CadastrarProgramaTempoFormatoInvalidoException : Exception
    {
        public CadastrarProgramaTempoFormatoInvalidoException(string message) : base(message)
        {
        }
    }
    public class CadastrarProgramaPotenciaInvalidoException : Exception
    {
        public CadastrarProgramaPotenciaInvalidoException(string message) : base(message)
        {
        }
    }
    public class CadastrarProgramaProgressoInvalidoException : Exception
    {
        public CadastrarProgramaProgressoInvalidoException(string message) : base(message)
        {
        }
    }    
}
