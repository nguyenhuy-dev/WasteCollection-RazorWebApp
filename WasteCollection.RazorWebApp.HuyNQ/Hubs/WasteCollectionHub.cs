using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using WasteCollection.Services.HuyNQ.DTOs;

namespace WasteCollection.RazorWebApp.HuyNQ.Hubs;

public class WasteCollectionHub : Hub
{
    public async Task HubDelete_CollectorAssignments(string id)
    {
        await Clients.All.SendAsync("ReceiveDelete_CollectorAssignments", id);
    }

    public async Task HubCreate_CollectorAssignments(string asmJson)
    {
        var asm = JsonConvert.DeserializeObject<CollectorAssignmentsHuyNqGetAllDto>(asmJson);
        await Clients.All.SendAsync("ReceiveCreate_CollectorAssignments", asm);
    }
}
