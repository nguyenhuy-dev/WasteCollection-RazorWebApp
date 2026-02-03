using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WasteCollection.Entities.HuyNQ.Models;
using WasteCollection.Services.HuyNQ;

namespace WasteCollection.RazorWebApp.HuyNQ.Pages.CollectorAssignmentsHuyNqs;

public class DetailsModel(ICollectorAssignmentsHuyNqService collectorAssignmentsService) : PageModel
{
    /*
    private readonly WasteCollection.Entities.HuyNQ.Models.WasteCollectionDbContext _context;

    public DetailsModel(WasteCollection.Entities.HuyNQ.Models.WasteCollectionDbContext context)
    {
        _context = context;
    }
    */

    private readonly ICollectorAssignmentsHuyNqService _collectorAssignmentsService = collectorAssignmentsService;

    public CollectorAssignmentsHuyNq CollectorAssignmentsHuyNq { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null)
            return NotFound();

        //var collectorassignmentshuynq = await _context.CollectorAssignmentsHuyNqs.FirstOrDefaultAsync(m => m.AssignmentId == id);

        var collectorassignmentshuynq = await _collectorAssignmentsService.GetByIdAsync(id.Value);

        if (collectorassignmentshuynq is not null)
        {
            CollectorAssignmentsHuyNq = collectorassignmentshuynq;

            return Page();
        }

        return NotFound();
    }
}
