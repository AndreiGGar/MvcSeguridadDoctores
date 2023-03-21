using Microsoft.AspNetCore.Mvc;
using MvcSeguridadDoctores.Filters;
using MvcSeguridadDoctores.Models;
using MvcSeguridadDoctores.Repositories;
using System.Security.Claims;

namespace MvcCoreSeguridadEmpleados.Controllers
{
    public class DoctoresController : Controller
    {
        private RepositoryHospital repo;

        public DoctoresController(RepositoryHospital repo)
        {
            this.repo = repo;
        }
        public async Task<IActionResult> Index()
        {
            List<Enfermo> enfermos = await this.repo.GetEnfermosAsync();
            return View(enfermos);
        }

        [AuthorizeDoctors]
        public async Task<IActionResult> DeleteEnfermo(int id)
        {
            Enfermo enfermo = await this.repo.FindEnfermo(id);
            return View(enfermo);
        }

        [AuthorizeDoctors(Policy = "PERMISOSELEVADOS")]
        public async Task<IActionResult> Delete(int id)
        {
            await this.repo.DeleteEnfermoAsync(id);
            return RedirectToAction("Index", "Doctores");
        }

        [AuthorizeDoctors]
        public async Task<IActionResult> PerfilDoctor()
        {
            string data =
                HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Doctor doctor =
                await this.repo.FindDoctorAsync(int.Parse(data));
            return View(doctor);
        }

        [AuthorizeDoctors(Policy = "ADMIN")]
        public IActionResult AdminDoctores()
        {
            return View();
        }
    }
}
