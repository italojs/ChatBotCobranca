﻿using System;
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
        QueryclientInfo client;

        public DialogHub()
        {
            //Simulate some query
            client = new QueryclientInfo("italo jose", "rua jaquitiba, 1771", (float)200.35, "italo.i@live.com");
           
        }
       

        //Intent
        [LuisIntent("VerFatura")]
        public async Task VerFatura(IDialogContext context)
        { 

            await context.PostAsync($"Só um minuto por favor, estou consultando.");
            Thread.Sleep(3000);
            await context.PostAsync($"Sua fatura está em R$ {client.Fatura} senhor.");
            Thread.Sleep(3000);
            await context.PostAsync($"Posso ajudar em mais alguma coisa senhor?");

            context.Wait(MessageReceived);

        }






    }
}