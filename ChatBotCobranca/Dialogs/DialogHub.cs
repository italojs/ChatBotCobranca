using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Luis.Models;
using System.Threading;
using ChatBotCobranca.Model;

namespace ChatBotCobranca.Dialogs
{
    //using Microsoft.Bot.Builder.Luis;
    //AppId,SubtringKey
    [LuisModel("57447048-5ac1-41bd-be43-159e6b4d64ca", "91d3b7c64d9e41c18eab664da8d28b67")]
    [Serializable]
    //LuisDialog<object> = using Microsoft.Bot.Builder.Dialogs
    public class DialogHub : LuisDialog<object>
    {
        
        private static QueryclientInfo client;
        public DialogHub(QueryclientInfo _client)
        {
            //Simulate some query
            client = _client;
        }

        [LuisIntent("VerFatura")]
        public async Task VerFatura(IDialogContext context, LuisResult result)
        {
            
            await context.PostAsync("Só um minuto por favor, estou consultando.");
            Thread.Sleep(3000);
            await context.PostAsync($"Sua fatura está em R$ {client.Fatura} senhor.");
            Thread.Sleep(3000);
            await context.PostAsync("Posso ajudar em mais alguma coisa senhor?");

            context.Wait(MessageReceived);

        }

        [LuisIntent("EnviaEmail")]
        public async Task EnviaEmail(IDialogContext context, LuisResult result)
        {
            EntityRecommendation entidade;
            if (result.TryFindEntity("TraitAnexo::Fatura", out entidade))
            {
                await context.PostAsync("Sua fatura foi enviada para o seu email");
            }
            else if (result.TryFindEntity("TraitAnexo::Protocolo", out entidade))
            {
                await context.PostAsync("Seu protocolo foi enviada para o seu email");
            }
            else
            {
                await context.PostAsync("Desculpe senho, pode repetir a frase com oque você quer enviar por email?");
            }
            
            context.Wait(MessageReceived);
        }








    }
}