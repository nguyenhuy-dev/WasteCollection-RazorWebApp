using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WasteCollection.Entities.HuyNQ.Models;
using WasteCollection.Services.HuyNQ;

namespace WasteCollection.RazorWebApp.HuyNQ.Pages.CollectorAssignmentsHuyNqs
{
    public class EditModel(ICollectorAssignmentsHuyNqService collectorAssignmentsService,
        ReportsHuyNqService reportsService) : PageModel
    {
        /*
        private readonly WasteCollection.Entities.HuyNQ.Models.WasteCollectionDbContext _context;

        public EditModel(WasteCollection.Entities.HuyNQ.Models.WasteCollectionDbContext context)
        {
            _context = context;
        }
        */
        private readonly ICollectorAssignmentsHuyNqService _collectorAssignmentsService = collectorAssignmentsService;
        private readonly ReportsHuyNqService _reportsService = reportsService;

        [BindProperty]
        public CollectorAssignmentsHuyNq CollectorAssignmentsHuyNq { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // var collectorassignmentshuynq =  await _context.CollectorAssignmentsHuyNqs.FirstOrDefaultAsync(m => m.AssignmentId == id);
            var collectorassignmentshuynq = await _collectorAssignmentsService.GetByIdAsync(id.Value);
            if (collectorassignmentshuynq == null)
                return NotFound();

            CollectorAssignmentsHuyNq = collectorassignmentshuynq;

            var reports = await _reportsService.GetAllAsync();

            ViewData["ReportHuyNqid"] = new SelectList(reports, "ReportId", "Address", collectorassignmentshuynq.ReportHuyNqid);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.Attach(CollectorAssignmentsHuyNq).State = EntityState.Modified;

            try
            {
                //await _context.SaveChangesAsync();
                var result = await _collectorAssignmentsService.UpdateAsync(CollectorAssignmentsHuyNq);

                if (result > 0)
                    return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                /*
                if (!CollectorAssignmentsHuyNqExists(CollectorAssignmentsHuyNq.AssignmentId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
                */

                ModelState.AddModelError(string.Empty, $"An error occurred while updating the Collector Assignment: {ex.Message}");
            }

            return RedirectToPage("./Index");
        }

        //private bool CollectorAssignmentsHuyNqExists(Guid id)
        //{
        //    //return _context.CollectorAssignmentsHuyNqs.Any(e => e.AssignmentId == id);
        //    return CollectorAssignmentsHuyNq != null;
        //}
    }
}
