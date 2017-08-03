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
using ChatBotCobranca.Utils;

namespace ChatBotCobranca.Dialogs
{
    //using Microsoft.Bot.Builder.Luis;
    //AppId,SubtringKey
    [LuisModel("57447048-5ac1-41bd-be43-159e6b4d64ca", "91d3b7c64d9e41c18eab664da8d28b67")]
    [Serializable]
    //LuisDialog<object> = using Microsoft.Bot.Builder.Dialogs
    public class DialogHub : LuisDialog<object>
    {
        
        private static QueryclientInfo _client;
        public DialogHub(QueryclientInfo client)
        {
            //Simulate some query
            _client = client;
        }

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
            await context.PostAsync($"Sua fatura está em R$ {_client.Fatura} senhor.");
            Thread.Sleep(3000);
            await context.PostAsync("Posso ajudar em mais alguma coisa senhor?");

            //context.PrivateConversationData.SetValue("TraitAnexo", "Fatura");

            //waitting a another message
            context.Wait(MessageReceived);

        }

        //another intent
        [LuisIntent("EnviaEmail")]
        public async Task EnviaEmail(IDialogContext context, LuisResult result)
        {
            //getting a EntityType
            if (result.TryFindEntity("TraitAnexo", out EntityRecommendation anexo))
            {
                if (result.TryFindEntity("Email", out EntityRecommendation email))
                {
                    var msg = EnviaAnexo(anexo.Entity, email.Entity);
                    await context.PostAsync(msg);
                    context.Wait(MessageReceived);
                }
                else
                {
                    context.PrivateConversationData.SetValue("TraitAnexo", anexo.Entity);
                    PromptDialog.Confirm(context, TrocarEmail, $"Seu email atual é:  {_client.Email} , você gostaria de trocá-lo?");
                }
            }
            else
            {
                await context.PostAsync("Desculpe, mas o que você deseja enviar por e-mail?");
                context.Wait(MessageReceived);
            }

        }

        private async Task TrocarEmail(IDialogContext context, IAwaitable<bool> confirmation)
        {
            if (await confirmation)
                PromptDialog.Text(context, AtualizaEmail, "Qual é o seu e-mail?");
            else
            {
                context.PrivateConversationData.TryGetValue("TraitAnexo", out string anexo);
                var msg = EnviaAnexo(anexo, _client.Email);
                await context.PostAsync(msg);
                context.Wait(MessageReceived);
            }
        }

        private async Task AtualizaEmail(IDialogContext context, IAwaitable<string> result)
        {
            var activity = context. Activity.AsMessageActivity();
            _client.updateEmail(activity.Text);

            if (Ultils.IsValidEmail(_client.Email))
                PromptDialog.Confirm(context, ConfirmarEmail, $"Ok, você confirma que esse e-mail {_client.Email} está correto?");
            else
                PromptDialog.Text(context, AtualizaEmail, $"Esse é um e-mail inválido, por favor entre com novo email valido.");

        }

        private async Task ConfirmarEmail(IDialogContext context, IAwaitable<bool> result)
        {
            if (await result)
            {
                context.PrivateConversationData.TryGetValue("TraitAnexo", out string anexo);
                var msg = EnviaAnexo(anexo, _client.Email);
                await context.PostAsync(msg);
                context.Wait(MessageReceived);
            }
            else
                PromptDialog.Text(context, AtualizaEmail, "Qual é o seu e-mail?");    
        }
        public string EnviaAnexo(string anexo, string email)
        {
            switch (anexo)
            {
                case "fatura":
                    return $"ok, sua fatura será enviada no email {email.Replace(" ", String.Empty)}";
                case "protocolo":
                    return $"ok, seu protocolo será enviada no email {email.Replace(" ", String.Empty)}";
                default:
                    return $"Desculpe, pode repetir com oque voce deseja envia no seu email?";
            }
        }

    }

        
    
}