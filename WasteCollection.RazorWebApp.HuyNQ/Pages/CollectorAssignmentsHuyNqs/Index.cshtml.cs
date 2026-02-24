using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WasteCollection.Repositories.HuyNQ.Models;
using WasteCollection.Services.HuyNQ;
using WasteCollection.Services.HuyNQ.DTOs;

namespace WasteCollection.RazorWebApp.HuyNQ.Pages.CollectorAssignmentsHuyNqs;

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

    public PaginatedResult<CollectorAssignmentsHuyNqGetAllDto> PagedResult { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public CollectorAssignmentsHuyNqSearchOptions SearchOptions { get; set; } = new();

    public bool IsSearching => !string.IsNullOrWhiteSpace(SearchOptions.Status)
                               || SearchOptions.CollectedWeight.HasValue
                               || SearchOptions.AssignedDate.HasValue;

    public async Task OnGetAsync()
    {
        PagedResult = await _collectorAssignmentsService.SearchAsync(SearchOptions);
    }
}
