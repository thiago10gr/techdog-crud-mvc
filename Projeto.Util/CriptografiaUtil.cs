using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Projeto.Util.Contratos;
using System.Security.Cryptography;

namespace Projeto.Util
{
    public class CriptografiaUtil : ICriptografiaUtil
    {
        public string EncriptarSenha(string senha)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            return BitConverter.ToString(md5.ComputeHash(
                Encoding.UTF8.GetBytes(senha))).Replace("-", string.Empty);
        }
    }
}
