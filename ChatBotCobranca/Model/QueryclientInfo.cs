using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatBotCobranca.Model
{
    public class QueryclientInfo
    {
        
        public string Name { get; private set; }
        public string Endereco { get; private set; }
        public float Fatura { get; private set; }
        public string Email { get; private set; }

        public  QueryclientInfo(string name, string endereco, float fatura,string email)
        {
            Name = name;
            Endereco = endereco;
            Fatura = fatura;
            Email = email;
        }
        public void updateEmail(string _email)
        {
            Email = _email;
        }


    }
}