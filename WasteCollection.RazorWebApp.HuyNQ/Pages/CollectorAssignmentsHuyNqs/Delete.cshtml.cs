using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WasteCollection.Entities.HuyNQ.Models;
using WasteCollection.Services.HuyNQ;

namespace WasteCollection.RazorWebApp.HuyNQ.Pages.CollectorAssignmentsHuyNqs
{
    public class DeleteModel(ICollectorAssignmentsHuyNqService collectorAssignmentsService) : PageModel
    {
        /*
        private readonly WasteCollection.Entities.HuyNQ.Models.WasteCollectionDbContext _context;

        public DeleteModel(WasteCollection.Entities.HuyNQ.Models.WasteCollectionDbContext context)
        {
            _context = context;
        }
        */
        private readonly ICollectorAssignmentsHuyNqService _collectorAssignmentsService = collectorAssignmentsService;

        [BindProperty]
        public CollectorAssignmentsHuyNq CollectorAssignmentsHuyNq { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var collectorassignmentshuynq = await _context.CollectorAssignmentsHuyNqs.FirstOrDefaultAsync(m => m.AssignmentId == id);

            var collectorassignmentshuynq = await _collectorAssignmentsService.GetByIdAsync(id.Value);

            if (collectorassignmentshuynq is not null)
            {
                CollectorAssignmentsHuyNq = collectorassignmentshuynq;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
                return NotFound();

            /*
            var collectorassignmentshuynq = await _context.CollectorAssignmentsHuyNqs.FindAsync(id);
            if (collectorassignmentshuynq != null)
            {
                CollectorAssignmentsHuyNq = collectorassignmentshuynq;
                _context.CollectorAssignmentsHuyNqs.Remove(CollectorAssignmentsHuyNq);
                await _context.SaveChangesAsync();
            }
            */

            var result = await _collectorAssignmentsService.DeleteAsync(id.Value);

            if (!result)
                return NotFound();

            return RedirectToPage("./Index");
        }
    }
}
