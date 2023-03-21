using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcSeguridadDoctores.Context;
using MvcSeguridadDoctores.Models;

namespace MvcSeguridadDoctores.Repositories
{
    public class RepositoryHospital
    {
        private DataContext context;

        public RepositoryHospital(DataContext context)
        {
            this.context = context;
        }

        public List<Doctor> GetDoctores()
        {
            return this.context.Doctores.ToList();
        }

        public async Task<Doctor> FindDoctorAsync(int iddoctor)
        {
            Doctor doctor =
                await this.context.Doctores.FirstOrDefaultAsync
                (x => x.Doctor_NO == iddoctor);
            return doctor;
        }

        public async Task<Doctor> ExisteDoctor(string apellido, int iddoctor)
        {
            Doctor doctor = await this.context.Doctores.Where(x => x.Apellido == apellido && x.Doctor_NO == iddoctor).FirstOrDefaultAsync();
            return doctor;
        }

        public async Task<List<Enfermo>> GetEnfermosAsync()
        {
            return await this.context.Enfermos.ToListAsync();
        }

        public async Task DeleteEnfermoAsync(int inscripcion)
        {
           Enfermo enfermo = await this.context.Enfermos.FirstOrDefaultAsync(x => x.Inscripcion == inscripcion);
            if (enfermo != null)
            {
                this.context.Enfermos.Remove(enfermo);
                await this.context.SaveChangesAsync();
            }
        }

        public async Task<Enfermo> FindEnfermo(int idenfermo)
        {
            return await this.context.Enfermos.FirstOrDefaultAsync(x => x.Inscripcion == idenfermo);
        }

        /*public async Task<List<Doctor>> GetEmpleadosDepartamentoAsync(int iddept)
        {
            var consulta = from datos in this.context.Empleados
                           where datos.Dept_no == iddept
                           select datos;
            return await consulta.ToListAsync();
        }*/
    }
}
