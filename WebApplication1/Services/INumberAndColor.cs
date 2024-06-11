using System;
using WebApplication1.Entities;

namespace WebApplication1.Services
{
   
    public interface INumberAndColor
    {
        Numberandcolor GetNumberAndColor();
    }

    public class Numberandcolor : INumberAndColor
    {
        public int Numero { get; set; } 
        public string Color { get; set; }


        private static readonly Random random = new Random();

        public Numberandcolor GetNumberAndColor()
        {
            int numero = random.Next(0, 37);
            string[] colors = { "rojo", "negro" };
            string color = colors[random.Next(colors.Length)];
            var respuesta = new Numberandcolor { Numero = numero, Color = color };
            return respuesta;
        }
    }
   

}
