using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.Entities;

namespace WebApplication1.Services
{
    public interface IRuleta
    {


        List<RuletaEntity> GetAll();

        RuletaEntity GetByName(string name);

        void Save(RuletaEntity rule);

    }


    public class Ruleta : IRuleta
    {
        private readonly DataContext _context;

        public Ruleta(DataContext context)
        {
            _context = context;
        }

        public void Save(RuletaEntity rule)
        {
            try
            {
                var res = _context.Ruletas.AsNoTracking().FirstOrDefault(p => p.nombre == rule.nombre);
             
                bool encontrada = res != null;
                if (encontrada)
                {
                    rule.id = res.id;
                    rule.monto += rule.apuesta;
                    _context.Update(rule);
                }
                else
                {
                    rule.monto += rule.apuesta;
                    _context.Add(rule);
                }
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<RuletaEntity> GetAll()
        {
            var respuesta = _context.Ruletas.ToList();
            return respuesta;
        }

        public RuletaEntity GetByName(string name)
        {
            var respuesta = _context.Ruletas.FirstOrDefault(n => n.nombre == name);
            return respuesta;
        }




    }


  


}
