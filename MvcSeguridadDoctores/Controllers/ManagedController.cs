using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MvcSeguridadDoctores.Models;
using MvcSeguridadDoctores.Repositories;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace MvcSeguridadDoctores.Controllers
{
    public class ManagedController : Controller
    {
        private RepositoryHospital repo;

        public ManagedController(RepositoryHospital repo)
        {
            this.repo = repo;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            Doctor doctor = await this.repo.ExisteDoctor(username, int.Parse(password));
            if (doctor != null)
            {
                ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                Claim claimName = new Claim(ClaimTypes.Name, username);
                identity.AddClaim(claimName);
                Claim claimId = new Claim(ClaimTypes.NameIdentifier, doctor.Doctor_NO.ToString());
                identity.AddClaim(claimId);
                Claim claimOficio = new Claim(ClaimTypes.Role, doctor.Especialidad);
                identity.AddClaim(claimOficio);
                Claim claimApellido = new Claim("Apellido", doctor.Apellido);
                identity.AddClaim(claimApellido);
                Claim claimSalario = new Claim("Salario", doctor.Salario.ToString());
                identity.AddClaim(claimSalario);
                Claim claimHospital = new Claim("Hospital", doctor.Hospital_COD.ToString());
                identity.AddClaim(claimHospital);

                if (doctor.Doctor_NO == 982)
                {
                    identity.AddClaim(new Claim("ADMINISTRADOR", "Soy el admin"));
                }

                identity.AddClaim(new Claim("SALARIO", doctor.Salario.ToString()));

                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal);
                string controller = TempData["controller"].ToString();
                string action = TempData["action"].ToString();
                string id = TempData["id"].ToString();
                return RedirectToAction(action, controller, new { id = id });
            }
            else
            {
                ViewData["MENSAJE"] = "Usuario/Password incorrectos";
                return View();
            }
        }
        public IActionResult ErrorAcceso()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
