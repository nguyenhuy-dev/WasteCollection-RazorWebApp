using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using WasteCollection.Services.HuyNQ;
using LoginRequest = WasteCollection.Services.HuyNQ.DTOs.LoginRequest;

namespace WasteCollection.RazorWebApp.HuyNQ.Pages.Auth
{
    public class LoginModel(SystemUserAccountService systemUserAccountService) : PageModel
    {
        public IActionResult OnGet() => Page();

        [BindProperty]
        public LoginRequest LoginRequest { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await systemUserAccountService.LoginAsync(LoginRequest);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid username or password");
                return Page();
            }

            // Create Claims
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.UserAccountId.ToString()),
                new(ClaimTypes.Name, user.FullName),
            };

            // Create Identity
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // Sign-in user
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            return RedirectToPage("/Index");
        }
    }
}
