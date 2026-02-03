using Microsoft.AspNetCore.SignalR;

namespace SignalRChat.Hubs;

public class ChatHub(ILogger<ChatHub> logger) : Hub
{
    private readonly ILogger<ChatHub> _logger = logger;

    public async Task SendMessage(string user, string message)
    {
        if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(message))
        {
            _logger.LogWarning("User must be identified and message cannot be empty");
            return;
        }
        
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}
