using CadRamm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClnRamm
{
    public class SerieCln
    {
        public static int insertar(Serie serie)
        {
            using (var contexto = new Parcial2RammEntities())
            {
                contexto.Serie.Add(serie);
                contexto.SaveChanges();
                return serie.id;
            }
        }

        public static int actualizar(Serie serie)
        {
            using (var contexto = new Parcial2RammEntities())
            {
                var existente = contexto.Serie.Find(serie.id);
                existente.titulo = serie.titulo;
                existente.sinopsis = serie.sinopsis;
                existente.director = serie.director;
                existente.duracion= serie.duracion;
                existente.fechaEstreno = serie.fechaEstreno;
                return contexto.SaveChanges();
            }
        }

        public static int eliminar(int id)
        {
            using (var contexto = new Parcial2RammEntities())
            {
                var existente = contexto.Serie.Find(id);
                existente.registroActivo = false;
                return contexto.SaveChanges();
            }
        }

        public static Serie get(int id)
        {
            using (var contexto = new Parcial2RammEntities())
            {
                return contexto.Serie.Find(id);
            }
        }

        public static List<Serie> listar()
        {
            using (var contexto = new Parcial2RammEntities())
            {
                return contexto.Serie.Where(x => x.registroActivo.Value).ToList();
            }
        }

        public static List<paSerieListar_Result> listarPa(string parametro)
        {
            using (var contexto = new Parcial2RammEntities())
            {
                return contexto.paSerieListar(parametro).ToList();
            }
        }
    }
}
