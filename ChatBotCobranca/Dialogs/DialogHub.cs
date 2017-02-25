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
        

        //Luis intent
        [LuisIntent("VerFatura")]
        //"IDialogContext context" is like some data's activity
        //"LuisResult result" is your Luis.ai JSON
        public async Task VerFatura(IDialogContext context, LuisResult result)
        {
            //sending a message
            await context.PostAsync("Só um minuto por favor, estou consultando.");
            //seting a time only for the bot looks like it is typing
            Thread.Sleep(3000);
            await context.PostAsync($"Sua fatura está em R$ R$ 500,00 senhor.");
            Thread.Sleep(3000);
            await context.PostAsync("Posso ajudar em mais alguma coisa senhor?");

            //waitting a another message
            context.Wait(MessageReceived);

        }

        //another intent
        [LuisIntent("EnviaEmail")]
        public async Task EnviaEmail(IDialogContext context, LuisResult result)
        {
           
            await context.PostAsync("Sua fatura foi enviada para o seu email");
            context.Wait(MessageReceived);
        }








    }
}