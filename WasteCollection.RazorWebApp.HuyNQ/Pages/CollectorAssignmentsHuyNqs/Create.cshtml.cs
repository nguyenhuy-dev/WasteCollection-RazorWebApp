using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WasteCollection.Services.HuyNQ;
using WasteCollection.Services.HuyNQ.DTOs;

namespace WasteCollection.RazorWebApp.HuyNQ.Pages.CollectorAssignmentsHuyNqs;

public class CreateModel(ICollectorAssignmentsHuyNqService collectorAssignmentsService,
    ReportsHuyNqService reportsService) : PageModel
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
        {
            return Page();
        }

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

        return RedirectToPage("./Index");
    }
}
