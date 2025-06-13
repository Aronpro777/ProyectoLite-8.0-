using SQLite;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ProyectoLite__8._0_.Models
{
    public class Receta
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Titulo { get; set; } = null!; // CS8618
        public string Imagen { get; set; } = null!; // CS8618 (assuming this is for the image URL/path)
        public string Categoria { get; set; } = null!; // CS8618
        public string Dificultad { get; set; } = null!; // CS8618
        public int TiempoEstimadoMin { get; set; }
        public string Descripcion { get; set; } = null!; // CS8618

        public string VariantesPorcionesJson
        {
            get => JsonConvert.SerializeObject(VariantesPorciones);
            // CORRECCIÓN: Usar '?? new Dictionary<int, VarianteReceta>()' para manejar la deserialización nula (CS8601)
            set => VariantesPorciones = JsonConvert.DeserializeObject<Dictionary<int, VarianteReceta>>(value) ?? new Dictionary<int, VarianteReceta>();
        }

        [Ignore]
        public Dictionary<int, VarianteReceta> VariantesPorciones { get; set; } = new Dictionary<int, VarianteReceta>();

        public class VarianteReceta
        {
            public int Porciones { get; set; }
            public List<Ingrediente> Ingredientes { get; set; } = new List<Ingrediente>();
            public List<string> Pasos { get; set; } = new List<string>();
        }

        public class Ingrediente
        {
            public string Nombre { get; set; } = null!;   // CS8618
            public string Cantidad { get; set; } = null!; // CS8618
        }
    }
}