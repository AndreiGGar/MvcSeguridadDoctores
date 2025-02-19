﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcSeguridadDoctores.Models
{
    [Table("DOCTOR")]
    public class Doctor
    {
        [Key]
        [Column("DOCTOR_NO")]
        public int Doctor_NO { get; set; }
        [Column("HOSPITAL_COD")]
        public int Hospital_COD { get; set; }
        [Column("APELLIDO")]
        public string? Apellido { get; set; }
        [Column("ESPECIALIDAD")]
        public string? Especialidad { get; set; }
        [Column("SALARIO")]
        public int Salario { get; set; }
    }
}
