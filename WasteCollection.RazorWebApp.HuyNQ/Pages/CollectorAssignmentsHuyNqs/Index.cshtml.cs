using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WasteCollection.Services.HuyNQ;
using WasteCollection.Services.HuyNQ.DTOs;

namespace WasteCollection.RazorWebApp.HuyNQ.Pages.CollectorAssignmentsHuyNqs
{
    [Authorize]
    public class IndexModel(ICollectorAssignmentsHuyNqService collectorAssignmentsService) : PageModel
    {
        /*
        private readonly WasteCollection.Entities.HuyNQ.Models.WasteCollectionDbContext _context;

        public IndexModel(WasteCollection.Entities.HuyNQ.Models.WasteCollectionDbContext context)
        {
            _context = context;
        }
        */

        private readonly ICollectorAssignmentsHuyNqService _collectorAssignmentsService = collectorAssignmentsService;

        public IList<CollectorAssignmentsHuyNqGetAllDto> CollectorAssignmentsHuyNq { get;set; } = default!;

        public async Task OnGetAsync()
        {
            //CollectorAssignmentsHuyNq = await _context.CollectorAssignmentsHuyNqs
            //    .Include(c => c.ReportHuyNq).ToListAsync();

            CollectorAssignmentsHuyNq = await _collectorAssignmentsService.GetAllAsync();
        }
    }
}
