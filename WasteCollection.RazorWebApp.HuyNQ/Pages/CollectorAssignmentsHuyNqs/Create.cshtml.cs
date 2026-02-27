using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using WasteCollection.RazorWebApp.HuyNQ.Hubs;
using WasteCollection.Services.HuyNQ;
using WasteCollection.Services.HuyNQ.DTOs;

namespace WasteCollection.RazorWebApp.HuyNQ.Pages.CollectorAssignmentsHuyNqs;

[Authorize]
public class CreateModel(ICollectorAssignmentsHuyNqService collectorAssignmentsService,
    ReportsHuyNqService reportsService, IHubContext<WasteCollectionHub> hubContext) : PageModel
{
    /*
    private readonly WasteCollection.Entities.HuyNQ.Models.WasteCollectionDbContext _context;

    public CreateModel(WasteCollection.Entities.HuyNQ.Models.WasteCollectionDbContext context)
    {
        _context = context;
    }
    */

    private readonly ICollectorAssignmentsHuyNqService _collectorAssignmentsService = collectorAssignmentsService;

    private readonly ReportsHuyNqService _reportsService = reportsService;

    private readonly IHubContext<WasteCollectionHub> _hubContext = hubContext;

    public async Task<IActionResult> OnGet()
    {
        //ViewData["ReportHuyNqid"] = new SelectList(_context.ReportsHuyNqs, "ReportId", "ReportId");

        var items = await _reportsService.GetAllAsync();

        ViewData["ReportHuyNqid"] = new SelectList(items, "ReportId", "Address");

        return Page();
    }

    [BindProperty]
    public CollectorAssignmentsHuyNqCreatedDto CollectorAssignmentsHuyNq { get; set; } = default!;

    // For more information, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        /*
        _context.CollectorAssignmentsHuyNqs.Add(CollectorAssignmentsHuyNq);
        await _context.SaveChangesAsync();
        */

        var result = await _collectorAssignmentsService.CreateAsync(CollectorAssignmentsHuyNq);

        if (result <= 0)
        {
            ModelState.AddModelError(string.Empty, "An error occured while creating the Collector Assignment");
            return Page();
        }

        //if (!string.IsNullOrEmpty(hubCreate))
        //    await _hubContext.Clients.All.SendAsync("ReceiveCreate_CollectorAssignments", createdAssignment);

        return RedirectToPage("./Index");
    }

    public async Task<IActionResult> OnPostHubCreateAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        /*
        _context.CollectorAssignmentsHuyNqs.Add(CollectorAssignmentsHuyNq);
        await _context.SaveChangesAsync();
        */

        var result = await _collectorAssignmentsService.CreateAsync(CollectorAssignmentsHuyNq);

        var createdAssignment = await _collectorAssignmentsService.GetByIdAsync(CollectorAssignmentsHuyNq.AssignmentId);

        await _hubContext.Clients.All.SendAsync("ReceiveCreate_CollectorAssignments", createdAssignment);

        if (result <= 0)
        {
            ModelState.AddModelError(string.Empty, "An error occured while creating the Collector Assignment");
            return Page();
        }

        //if (!string.IsNullOrEmpty(hubCreate))
        //    await _hubContext.Clients.All.SendAsync("ReceiveCreate_CollectorAssignments", createdAssignment);

        return RedirectToPage("./Index");
    }
}
