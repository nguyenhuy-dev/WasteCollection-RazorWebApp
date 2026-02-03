using Microsoft.AspNetCore.Mvc.RazorPages;
using WasteCollection.Services.HuyNQ;
using WasteCollection.Services.HuyNQ.DTOs;

namespace WasteCollection.RazorWebApp.HuyNQ.Pages.CollectorAssignmentsHuyNqs
{
    public class IndexModel(ICollectorAssignmentsHuyNqService collectorAssignmentsService, 
        ReportsHuyNqService reportsService) : PageModel
    {
        /*
        private readonly WasteCollection.Entities.HuyNQ.Models.WasteCollectionDbContext _context;

        public IndexModel(WasteCollection.Entities.HuyNQ.Models.WasteCollectionDbContext context)
        {
            _context = context;
        }
        */

        private readonly ICollectorAssignmentsHuyNqService _collectorAssignmentsService = collectorAssignmentsService;

        private readonly ReportsHuyNqService _reportsService = reportsService;

        public IList<CollectorAssignmentsHuyNqGetAllDto> CollectorAssignmentsHuyNq { get;set; } = default!;

        public async Task OnGetAsync()
        {
            //CollectorAssignmentsHuyNq = await _context.CollectorAssignmentsHuyNqs
            //    .Include(c => c.ReportHuyNq).ToListAsync();

            CollectorAssignmentsHuyNq = await _collectorAssignmentsService.GetAllAsync();
        }
    }
}
