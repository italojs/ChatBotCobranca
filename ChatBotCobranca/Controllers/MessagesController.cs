using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;

namespace ChatBotCobranca
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                //In the ConnectorClient we need set who the bot will reply the message
                //the activity.ServiceUrl provide it, in this case our ServiceUrl are the localhost
                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));

                //our botMessage object
                Activity reply;

                //chosing the message wicth the bot will reply
                if (activity.Text.Contains("ola"))
                {
                    //setting bot message
                    reply = activity.CreateReply($"Olá, no que posso ajudar");
                }else if(activity.Text.Contains("tudo bem?"))
                {
                    reply = activity.CreateReply($"tudo sim, e você?");
                }
                else if (activity.Text.Contains("tudo sim"))
                {
                    reply = activity.CreateReply($"ai que ótimo");
                }
                else
                {
                    reply = activity.CreateReply($"ai, me desculpa eu não entedi");
                }

                // return our reply to the user
                await connector.Conversations.ReplyToActivityAsync(reply);
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}