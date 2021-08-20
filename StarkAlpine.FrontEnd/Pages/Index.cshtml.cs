using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace StarkAlpine.FrontEnd.Pages
{
    public class IndexModel : PageModel
    {
        public IEnumerable<Lift> Lifts;

        private readonly ILogger<IndexModel> _logger;
        private readonly LiftStatusClient _liftStatusClient;

        public IndexModel(
            ILogger<IndexModel> logger,
            LiftStatusClient liftStatusClient)
        {
            _logger = logger;
            _liftStatusClient = liftStatusClient;
        }

        public async Task OnGet()
        {
            Lifts = await _liftStatusClient.LiftStatusAsync();
        }
    }
}
