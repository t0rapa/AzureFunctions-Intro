using System.Net;
using Microsoft.Bot.Connector;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    log.Info($"C# HTTP trigger function processed a request. RequestUri={req.RequestUri}");
    Activity activity = await req.Content.ReadAsAsync<Activity>();
    
    if (activity.Type == ActivityTypes.Message)
    {
        ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
        int length = (activity.Text ?? string.Empty).Length;

        // return our reply to the user
        Activity reply = activity.CreateReply($"You sent {activity.Text} which was {length} characters");
        await connector.Conversations.ReplyToActivityAsync(reply);
    }
    else
    {
        HandleSystemMessage(activity);
    }

    return req.CreateResponse(HttpStatusCode.OK);
}

public static Activity HandleSystemMessage(Activity message)
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
