using Microsoft.AspNetCore.SignalR;
using WasteCollection.Services.HuyNQ;

namespace WasteCollection.RazorWebApp.HuyNQ.Hubs;

public class WasteCollectionHub(ICollectorAssignmentsHuyNqService collectorAssignmentsService,
    ReportsHuyNqService reportsService) : Hub
{
    private readonly ICollectorAssignmentsHuyNqService _collectorAssignmentsService = collectorAssignmentsService;
    private readonly ReportsHuyNqService _reportsService = reportsService;

    public async Task HubDelete_CollectorAssignments(string id)
    {
        await Clients.All.SendAsync("ReceiveDelete_CollectorAssignments", id);
        // await _collectorAssignmentsService.DeleteAsync(Guid.Parse(id));
    }
}
