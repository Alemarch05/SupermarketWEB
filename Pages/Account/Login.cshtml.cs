using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;
using SupermarketWEB.Models;
using System.Security.Claims;

namespace SupermarketWEB.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SupermarketContext _context;
        public LoginModel(SupermarketContext _context)
        {
            this._context = _context;
        }
        [BindProperty]
        public User User { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
           if (!ModelState.IsValid) return Page();

            var userInDb = await _context.Users
           .FirstOrDefaultAsync(u => u.Email == User.Email && u.Password == User.Password);
            // me baso en el el index de user Users = await _context.Users.ToListAsync();
            //El método FirstOrDefaultAsync devuelve un objeto del mismo tipo que el conjunto (DbSet<User>),
            //es decir, un objeto de la clase User o null si no encuentra coincidencias.

            if (userInDb != null)
            {
                Console.Write("");

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role, userInDb.Role),
                    new Claim(ClaimTypes.Email,userInDb.Email),
                };

                var indetity = new ClaimsIdentity(claims, "MyCookieAuth");

                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(indetity);

                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);
                return RedirectToPage("/Index");    
            }
            return Page();
        }
    }
}
