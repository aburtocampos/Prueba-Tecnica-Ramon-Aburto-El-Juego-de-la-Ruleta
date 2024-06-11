using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Entities
{
    public class RuletaEntity
    {
        [Key]
        public int id { get; set; }

        public string nombre { get; set; }

        public int monto { get; set; }

        [NotMapped]
        public int apuesta { get; set; }
    }
}
