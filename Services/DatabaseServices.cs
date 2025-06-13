using SQLite;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using ProyectoLite__8._0_.Models; // Fix for CS0246: Missing 'User' model namespace
using BCrypt.Net; // Fix for CS0103: Missing BCrypt.Net reference
using Newtonsoft.Json; // Assuming you need this for Receta.VariantesPorcionesJson

namespace ProyectoLite__8._0_.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection? _database; // Fix for CS8618: Declared as nullable for lazy initialization

        private static readonly Lazy<DatabaseService> _lazyInitializer = new Lazy<DatabaseService>(() => new DatabaseService());
        public static DatabaseService Instance => _lazyInitializer.Value;

        public DatabaseService()
        {
            // Constructor privado para Singleton
        }

        public async Task InitializeAsync()
        {
            if (_database is not null)
                return;

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "ProyectoLite.db3");
            _database = new SQLiteAsyncConnection(databasePath);

            await _database.CreateTableAsync<Usuario>(); // Fix for CS0246
            await _database.CreateTableAsync<Receta>();

            await SeedInitialData();
        }

        private async Task SeedInitialData()
        {
            if (_database == null) // Add null check for _database if it's not guaranteed non-null
                return;

            if (await _database.Table<Receta>().CountAsync() == 0)
            {
                // IMPORTANT: Your recetas list is currently empty.
                // If you had initial recipe data, you should re-add it here.
                var recetas = new List<Receta>
                
                {
                    // --- RECETAS DE CATEGORÍA: PAY (2) ---
                    new Receta
                    {
                        Titulo = "Pay de Queso",
                        Imagen = "pay_queso.jpg",
                        Categoria = "Pay",
                        Dificultad = "Medio",
                        TiempoEstimadoMin = 100,
                        Descripcion = "Un postre cremoso y suave con base crujiente de galleta. Su sabor equilibrado lo hace perfecto solo o con mermelada, caramelo o chocolate. Ideal para cualquier ocasión y un verdadero deleite para el paladar.",
                        VariantesPorciones = new Dictionary<int, Receta.VarianteReceta>
                        {
                            {
                                2, new Receta.VarianteReceta
                                {
                                    Porciones = 2,
                                    Ingredientes = new List<Receta.Ingrediente>
                                    {
                                        new Receta.Ingrediente { Nombre = "Galletas María molidas", Cantidad = "100 g" },
                                        new Receta.Ingrediente { Nombre = "Mantequilla sin sal derretida", Cantidad = "50 g" },
                                        new Receta.Ingrediente { Nombre = "Queso crema (a temperatura ambiente)", Cantidad = "200 g" },
                                        new Receta.Ingrediente { Nombre = "Leche condensada", Cantidad = "½ taza" },
                                        new Receta.Ingrediente { Nombre = "Huevo grande", Cantidad = "1" },
                                        new Receta.Ingrediente { Nombre = "Esencia de vainilla", Cantidad = "½ cucharadita" },
                                        new Receta.Ingrediente { Nombre = "Jugo de limón (fresco)", Cantidad = "1 cucharada (opcional)" },
                                        new Receta.Ingrediente { Nombre = "Mermelada de fresa o frutos rojos", Cantidad = "al gusto (para decorar)" }
                                    },
                                    Pasos = new List<string>
                                    {
                                        "Paso 1: Precalienta el horno a 160°C (325°F).",
                                        "Paso 2: Para la base, mezcla las galletas María molidas con la mantequilla derretida hasta obtener una pasta arenosa. Presiona firmemente esta mezcla en el fondo de un molde para pay de 15 cm (6 pulgadas).",
                                        "Paso 3: Para el relleno, en un tazón grande, bate el queso crema hasta que esté suave y cremoso. Agrega la leche condensada y continúa batiendo hasta integrar.",
                                        "Paso 4: Incorpora el huevo, la esencia de vainilla y el jugo de limón (si lo usas). Bate hasta que la mezcla sea homogénea, evitando batir en exceso para no incorporar demasiado aire.",
                                        "Paso 5: Vierte el relleno sobre la base de galleta en el molde.",
                                        "Paso 6: Hornea durante 30-35 minutos, o hasta que el centro del pay esté casi firme, pero aún se mueva ligeramente al agitarlo suavemente. El borde debe estar dorado.",
                                        "Paso 7: Apaga el horno, abre ligeramente la puerta y deja el pay dentro por 15 minutos. Luego, retíralo y deja enfriar completamente a temperatura ambiente.",
                                        "Paso 8: Refrigera el pay de queso por al menos 4 horas o idealmente toda la noche antes de servir. Decora con mermelada de fresa o frutos rojos si lo deseas."
                                    }
                                }
                            },
                            {
                                4, new Receta.VarianteReceta
                                {
                                    Porciones = 4,
                                    Ingredientes = new List<Receta.Ingrediente>
                                    {
                                        new Receta.Ingrediente { Nombre = "Galletas María molidas", Cantidad = "200 g" },
                                        new Receta.Ingrediente { Nombre = "Mantequilla sin sal derretida", Cantidad = "100 g" },
                                        new Receta.Ingrediente { Nombre = "Queso crema (a temperatura ambiente)", Cantidad = "400 g" },
                                        new Receta.Ingrediente { Nombre = "Leche condensada", Cantidad = "1 taza" },
                                        new Receta.Ingrediente { Nombre = "Huevos grandes", Cantidad = "2" },
                                        new Receta.Ingrediente { Nombre = "Esencia de vainilla", Cantidad = "1 cucharadita" },
                                        new Receta.Ingrediente { Nombre = "Jugo de limón (fresco)", Cantidad = "2 cucharadas (opcional)" },
                                        new Receta.Ingrediente { Nombre = "Mermelada de fresa o frutos rojos", Cantidad = "al gusto (para decorar)" }
                                    },
                                    Pasos = new List<string>
                                    {
                                        "Paso 1: Precalienta el horno a 170°C (340°F).",
                                        "Paso 2: Para la base, mezcla las galletas María molidas con la mantequilla derretida. Presiona en un molde para pay de 20 cm (8 pulgadas).",
                                        "Paso 3: Para el relleno, bate el queso crema. Agrega la leche condensada, huevos, vainilla y jugo de limón. Bate hasta que todo esté integrado.",
                                        "Paso 4: Vierte el relleno sobre la base de galleta.",
                                        "Paso 5: Hornea durante 40-45 minutos, o hasta que el centro esté casi firme. El borde debe estar dorado.",
                                        "Paso 6: Apaga el horno, abre ligeramente la puerta y deja el pay dentro por 20 minutos. Enfría completamente a temperatura ambiente.",
                                        "Paso 7: Refrigera por al menos 6 horas o toda la noche antes de servir. Decora con mermelada."
                                    }
                                }
                            },
                            {
                                6, new Receta.VarianteReceta
                                {
                                    Porciones = 6,
                                    Ingredientes = new List<Receta.Ingrediente>
                                    {
                                        new Receta.Ingrediente { Nombre = "Galletas María molidas", Cantidad = "300 g" },
                                        new Receta.Ingrediente { Nombre = "Mantequilla sin sal derretida", Cantidad = "150 g" },
                                        new Receta.Ingrediente { Nombre = "Queso crema (a temperatura ambiente)", Cantidad = "600 g" },
                                        new Receta.Ingrediente { Nombre = "Leche condensada", Cantidad = "1.5 tazas" },
                                        new Receta.Ingrediente { Nombre = "Huevos grandes", Cantidad = "3" },
                                        new Receta.Ingrediente { Nombre = "Esencia de vainilla", Cantidad = "1.5 cucharaditas" },
                                        new Receta.Ingrediente { Nombre = "Jugo de limón (fresco)", Cantidad = "3 cucharadas (opcional)" },
                                        new Receta.Ingrediente { Nombre = "Mermelada de fresa o frutos rojos", Cantidad = "al gusto (para decorar)" }
                                    },
                                    Pasos = new List<string>
                                    {
                                        "Paso 1: Precalienta el horno a 175°C (350°F).",
                                        "Paso 2: Para la base, mezcla las galletas María molidas con la mantequilla derretida. Presiona en un molde para pay de 23 cm (9 pulgadas).",
                                        "Paso 3: Para el relleno, bate el queso crema. Agrega la leche condensada, huevos, vainilla y jugo de limón. Bate hasta que todo esté integrado y cremoso.",
                                        "Paso 4: Vierte el relleno sobre la base de galleta de manera uniforme.",
                                        "Paso 5: Hornea durante 50-55 minutos, o hasta que el centro esté casi firme al tacto. Si los bordes se dora demasiado rápido, puedes cubrirlos con papel de aluminio.",
                                        "Paso 6: Apaga el horno, abre ligeramente la puerta y deja el pay dentro por 25 minutos. Esto ayuda a que el pay se asiente y evita que se agriete.",
                                        "Paso 7: Retíralo del horno y deja enfriar completamente a temperatura ambiente sobre una rejilla.",
                                        "Paso 8: Refrigera el pay de queso por al menos 8 horas o toda la noche para que adquiera la consistencia perfecta. Decora con mermelada al momento de servir."
                                    }
                                }
                            }
                        }
                    },
                    new Receta
                    {
                        Titulo = "Tarta de Manzana Clásica",
                        Imagen = "pay_manzana.jpg",
                        Categoria = "Pay",
                        Dificultad = "Medio",
                        TiempoEstimadoMin = 90,
                        Descripcion = "Una tarta de manzana clásica con un relleno de manzanas tiernas y especiadas, y una corteza dorada y crujiente. Un postre reconfortante y delicioso, ideal para compartir.",
                        VariantesPorciones = new Dictionary<int, Receta.VarianteReceta>
                        {
                            {
                                8, new Receta.VarianteReceta
                                {
                                    Porciones = 8,
                                    Ingredientes = new List<Receta.Ingrediente>
                                    {
                                        new Receta.Ingrediente { Nombre = "Masa de tarta (refrigerada)", Cantidad = "2 láminas" },
                                        new Receta.Ingrediente { Nombre = "Manzanas (peladas, sin corazón y en rodajas finas)", Cantidad = "6-8 medianas" },
                                        new Receta.Ingrediente { Nombre = "Azúcar granulada", Cantidad = "½ taza" },
                                        new Receta.Ingrediente { Nombre = "Harina de trigo", Cantidad = "¼ taza" },
                                        new Receta.Ingrediente { Nombre = "Canela molida", Cantidad = "1 cucharadita" },
                                        new Receta.Ingrediente { Nombre = "Nuez mosca.da molida", Cantidad = "¼ cucharadita" },
                                        new Receta.Ingrediente { Nombre = "Jugo de limón", Cantidad = "1 cucharada" },
                                        new Receta.Ingrediente { Nombre = "Mantequilla sin sal (en cubos pequeños)", Cantidad = "2 cucharadas" },
                                        new Receta.Ingrediente { Nombre = "Huevo (batido, para barnizar)", Cantidad = "1" }
                                    },
                                    Pasos = new List<string>
                                    {
                                        "Paso 1: Precalienta el horno a 200°C (400°F).",
                                        "Paso 2: En un tazón grande, mezcla las rodajas de manzana con el azúcar, la harina, la canela, la nuez mosca.da y el jugo de limón hasta que las manzanas estén bien cubiertas.",
                                        "Paso 3: Extiende una de las láminas de masa de tarta en un molde para tarta de 23 cm (9 pulgadas). Vierte el relleno de manzana sobre la masa y distribuye los cubos de mantequilla por encima.",
                                        "Paso 4: Coloca la segunda lámina de masa sobre el relleno. Puedes hacer un enrejado o simplemente cubrir la tarta. Recorta el exceso de masa y sella los bordes presionando o rizando.",
                                        "Paso 5: Haz pequeñas ranuras en la parte superior de la masa para que escape el vapor durante la cocción. Barniza la parte superior con el huevo batido.",
                                        "Paso 6: Hornea durante 15 minutos. Luego, reduce la temperatura del horno a 175°C (350°F) y hornea por 35-45 minutos más, o hasta que la corteza esté dorada y el relleno burbujee.",
                                        "Paso 7: Si la corteza se dora demasiado rápido, puedes cubrirla con papel de aluminio. Deja enfriar la tarta sobre una rejilla antes de servir. Es deliciosa tibia con helado."
                                    }
                                }
                            }
                        }
                    },
                    // --- RECETAS DE CATEGORÍA: PASTEL (2) ---
                    new Receta
                    {
                        Titulo = "Pastel Red Velvet",
                        Imagen = "red_velvet.jpg",
                        Categoria = "Pastel",
                        Dificultad = "Medio",
                        TiempoEstimadoMin = 120,
                        Descripcion = "Un clásico elegante con capas de terciopelo rojo y glaseado de queso crema. Su distintivo color y sabor lo hacen irresistible, con una textura suave y esponjosa que sorprenderá a todos.",
                        VariantesPorciones = new Dictionary<int, Receta.VarianteReceta>
                        {
                            {
                                4, new Receta.VarianteReceta
                                {
                                    Porciones = 4,
                                    Ingredientes = new List<Receta.Ingrediente>
                                    {
                                        new Receta.Ingrediente { Nombre = "Harina de trigo", Cantidad = "2 tazas" },
                                        new Receta.Ingrediente { Nombre = "Azúcar granulada", Cantidad = "1 ½ tazas" },
                                        new Receta.Ingrediente { Nombre = "Cacao en polvo sin azúcar", Cantidad = "¼ taza" },
                                        new Receta.Ingrediente { Nombre = "Bicarbonato de sodio", Cantidad = "1 cucharadita" },
                                        new Receta.Ingrediente { Nombre = "Sal", Cantidad = "½ cucharadita" },
                                        new Receta.Ingrediente { Nombre = "Huevos grandes", Cantidad = "2" },
                                        new Receta.Ingrediente { Nombre = "Aceite vegetal", Cantidad = "1 taza" },
                                        new Receta.Ingrediente { Nombre = "Suero de leche (buttermilk)", Cantidad = "1 taza" },
                                        new Receta.Ingrediente { Nombre = "Extracto de vainilla", Cantidad = "1 cucharadita" },
                                        new Receta.Ingrediente { Nombre = "Colorante rojo en gel", Cantidad = "2 cucharaditas" },
                                        new Receta.Ingrediente { Nombre = "Vinagre blanco", Cantidad = "1 cucharadita" },
                                        new Receta.Ingrediente { Nombre = "Queso crema (para el glaseado)", Cantidad = "226 g" },
                                        new Receta.Ingrediente { Nombre = "Mantequilla sin sal (para el glaseado)", Cantidad = "113 g" },
                                        new Receta.Ingrediente { Nombre = "Azúcar glas (para el glaseado)", Cantidad = "4 tazas" }
                                    },
                                    Pasos = new List<string>
                                    {
                                        "Paso 1: Precalienta el horno a 175°C (350°F). Engrasa y enharina dos moldes redondos de 20 cm.",
                                        "Paso 2: En un tazón grande, tamiza la harina, azúcar, cacao, bicarbonato y sal. Mezcla bien.",
                                        "Paso 3: En otro tazón, bate los huevos, aceite, suero de leche, vainilla y colorante rojo hasta combinar.",
                                        "Paso 4: Vierte los ingredientes húmedos sobre los secos y mezcla a baja velocidad hasta que estén apenas combinados. Agrega el vinagre y mezcla rápidamente.",
                                        "Paso 5: Divide la masa equitativamente en los moldes preparados. Hornea de 25 a 30 minutos, o hasta que un palillo insertado en el centro salga limpio.",
                                        "Paso 6: Deja enfriar los pasteles en los moldes por 10 minutos, luego desmóntalos sobre una rejilla para que se enfríen completamente.",
                                        "Paso 7: Para el glaseado, bate el queso crema y la mantequilla hasta que estén suaves. Agrega el azúcar glas poco a poco hasta obtener un glaseado cremoso y uniforme.",
                                        "Paso 8: Una vez que los pasteles estén completamente fríos, nivela las capas si es necesario y únelas con el glaseado. Cubre todo el pastel con una capa fina de glaseado (migas) y luego una capa final."
                                    }
                                }
                            }
                        }
                    },
                    new Receta
                    {
                        Titulo = "Pastel de Chocolate Húmedo",
                        Imagen = "pastel_chocolate.jpg",
                        Categoria = "Pastel",
                        Dificultad = "Medio",
                        TiempoEstimadoMin = 80,
                        Descripcion = "Un pastel de chocolate increíblemente húmedo y rico, con un intenso sabor a cacao. Cubierto con un suave glaseado de chocolate, es el postre perfecto para cualquier ocasión.",
                        VariantesPorciones = new Dictionary<int, Receta.VarianteReceta>
                        {
                            {
                                12, new Receta.VarianteReceta
                                {
                                    Porciones = 12,
                                    Ingredientes = new List<Receta.Ingrediente>
                                    {
                                        new Receta.Ingrediente { Nombre = "Harina de trigo", Cantidad = "1 ¾ tazas" },
                                        new Receta.Ingrediente { Nombre = "Azúcar granulada", Cantidad = "2 tazas" },
                                        new Receta.Ingrediente { Nombre = "Cacao en polvo sin azúcar", Cantidad = "¾ taza" },
                                        new Receta.Ingrediente { Nombre = "Bicarbonato de sodio", Cantidad = "1 ½ cucharaditas" },
                                        new Receta.Ingrediente { Nombre = "Polvo para hornear", Cantidad = "1 ½ cucharaditas" },
                                        new Receta.Ingrediente { Nombre = "Sal", Cantidad = "1 cucharadita" },
                                        new Receta.Ingrediente { Nombre = "Huevos grandes", Cantidad = "2" },
                                        new Receta.Ingrediente { Nombre = "Leche", Cantidad = "1 taza" },
                                        new Receta.Ingrediente { Nombre = "Aceite vegetal", Cantidad = "½ taza" },
                                        new Receta.Ingrediente { Nombre = "Extracto de vainilla", Cantidad = "2 cucharaditas" },
                                        new Receta.Ingrediente { Nombre = "Agua hirviendo", Cantidad = "1 taza" },
                                        new Receta.Ingrediente { Nombre = "Mantequilla sin sal (para glaseado)", Cantidad = "1 taza" },
                                        new Receta.Ingrediente { Nombre = "Cacao en polvo (para glaseado)", Cantidad = "½ taza" },
                                        new Receta.Ingrediente { Nombre = "Azúcar glas (para glaseado)", Cantidad = "4 tazas" },
                                        new Receta.Ingrediente { Nombre = "Leche (para glaseado)", Cantidad = "¼ taza" },
                                        new Receta.Ingrediente { Nombre = "Extracto de vainilla (para glaseado)", Cantidad = "1 cucharadita" }
                                    },
                                    Pasos = new List<string>
                                    {
                                        "Paso 1: Precalienta el horno a 175°C (350°F). Engrasa y enharina dos moldes redondos de 23 cm (9 pulgadas).",
                                        "Paso 2: En un tazón grande, combina la harina, el azúcar, el cacao en polvo, el bicarbonato de sodio, el polvo para hornear y la sal.",
                                        "Paso 3: Agrega los huevos, la leche, el aceite vegetal y el extracto de vainilla a los ingredientes secos. Bate a velocidad media hasta que se combinen. Luego, incorpora lentamente el agua hirviendo (la mezcla será líquida).",
                                        "Paso 4: Divide la masa uniformemente entre los dos moldes preparados. Hornea durante 30-35 minutos, o hasta que un palillo insertado en el centro salga limpio.",
                                        "Paso 5: Deja enfriar los pasteles en los moldes durante 10 minutos, luego desmóldalos y colócalos en una rejilla para que se enfríen completamente.",
                                        "Paso 6: Para el glaseado: En un tazón grande, bate la mantequilla hasta que esté suave. Agrega el cacao en polvo y mezcla. Poco a poco, incorpora el azúcar glas. Añade la leche y el extracto de vainilla, batiendo hasta obtener un glaseado suave y cremoso.",
                                        "Paso 7: Una vez que los pasteles estén completamente fríos, nivela las capas si es necesario. Cubre una capa con glaseado, coloca la segunda capa encima y cubre todo el pastel con el glaseado restante.",
                                        "Paso 8: Decora al gusto y sirve."
                                    }
                                }
                            }
                        }
                    },
                    // --- RECETAS DE CATEGORÍA: GELATINA (2) ---
                    new Receta
                    {
                        Titulo = "Gelatina de Frutas Mixtas",
                        Imagen = "gelatina_mixta.jpg",
                        Categoria = "Gelatina",
                        Dificultad = "Fácil",
                        TiempoEstimadoMin = 240, // Requiere refrigeración
                        Descripcion = "Una gelatina refrescante y colorida, repleta de trozos de frutas frescas. Perfecta para un postre ligero, una fiesta infantil o un día caluroso.",
                        VariantesPorciones = new Dictionary<int, Receta.VarianteReceta>
                        {
                            {
                                8, new Receta.VarianteReceta
                                {
                                    Porciones = 8,
                                    Ingredientes = new List<Receta.Ingrediente>
                                    {
                                        new Receta.Ingrediente { Nombre = "Gelatina en polvo (sabor fresa o cereza)", Cantidad = "1 sobre (85g)" },
                                        new Receta.Ingrediente { Nombre = "Gelatina en polvo (sabor limón o uva)", Cantidad = "1 sobre (85g)" },
                                        new Receta.Ingrediente { Nombre = "Gelatina en polvo (sabor piña o naranja)", Cantidad = "1 sobre (85g)" },
                                        new Receta.Ingrediente { Nombre = "Agua hirviendo", Cantidad = "3 tazas (1 taza por sabor)" },
                                        new Receta.Ingrediente { Nombre = "Agua fría", Cantidad = "3 tazas (1 taza por sabor)" },
                                        new Receta.Ingrediente { Nombre = "Frutas frescas picadas (fresa, kiwi, mango, uvas)", Cantidad = "2 tazas en total" }
                                    },
                                    Pasos = new List<string>
                                    {
                                        "Paso 1: En un tazón, disuelve el primer sobre de gelatina (ej. fresa) en 1 taza de agua hirviendo. Mezcla bien hasta que se disuelva por completo. Luego, agrega 1 taza de agua fría y mezcla. Vierte una capa delgada en un molde para gelatina o en vasos individuales y refrigera hasta que cuaje ligeramente.",
                                        "Paso 2: Una vez que la primera capa esté ligeramente cuajada, distribuye la mitad de las frutas picadas sobre ella.",
                                        "Paso 3: Repite el Paso 1 con el segundo sabor de gelatina (ej. limón): disuelve en 1 taza de agua hirviendo, agrega 1 taza de agua fría. Vierte con cuidado sobre la primera capa cuajada con frutas. Refrigera hasta que cuaje ligeramente.",
                                        "Paso 4: Distribuye el resto de las frutas picadas sobre la segunda capa.",
                                        "Paso 5: Repite el Paso 1 con el tercer sabor de gelatina (ej. piña): disuelve en 1 taza de agua hirviendo, agrega 1 taza de agua fría. Vierte con cuidado sobre la tercera capa cuajada con frutas.",
                                        "Paso 6: Refrigera la gelatina por al menos 4 horas, o idealmente toda la noche, hasta que esté completamente firme.",
                                        "Paso 7: Para desmoldar (si usas un molde grande): Sumerge el molde en agua tibia por unos segundos. Pasa un cuchillo delgado por los bordes y coloca un plato húmedo encima. Voltea rápidamente el molde. Si usaste vasos individuales, simplemente sírvelos con una cuchara.",
                                        "Paso 8: Sirve la gelatina fría. Puedes decorar con un poco de crema batida o hojas de menta si lo deseas."
                                    }
                                }
                            }
                        }
                    },
                    new Receta
                    {
                        Titulo = "Gelatina Mosaico",
                        Imagen = "gelatina_mosaico.png",
                        Categoria = "Gelatina",
                        Dificultad = "Medio",
                        TiempoEstimadoMin = 300, // Tiempo total incluyendo cuajado
                        Descripcion = "Una explosión de color y sabor, divertida de hacer y de comer. Perfecta para fiestas y reuniones por su atractivo visual.",
                        VariantesPorciones = new Dictionary<int, Receta.VarianteReceta>
                        {
                            {
                                2, new Receta.VarianteReceta
                                {
                                    Porciones = 2,
                                    Ingredientes = new List<Receta.Ingrediente>
                                    {
                                        new Receta.Ingrediente { Nombre = "Leche condensada", Cantidad = "1 lata" },
                                        new Receta.Ingrediente { Nombre = "Sobres de gelatina de sabores (diferentes colores)", Cantidad = "4" }
                                    },
                                    Pasos = new List<string>
                                    {
                                        "Paso 1: Prepara cada sobre de gelatina de sabor individualmente según las instrucciones del paquete, pero usando un poco menos de agua para que queden más firmes. Vierte cada sabor en moldes cuadrados o rectangulares poco profundos. Refrigera hasta que cuajen por completo (mínimo 2-3 horas).",
                                        "Paso 2: Una vez cuajadas, corta cada gelatina de color en cubos pequeños (aproximadamente 1x1 cm).",
                                        "Paso 3: En un tazón grande, disuelve 1 sobre de gelatina sin sabor en 1 taza de agua caliente. Agrega la lata de leche condensada y mezcla bien hasta que la mezcla esté homogénea. Deja enfriar a temperatura ambiente.",
                                        "Paso 4: En un molde grande para gelatina (puede ser un molde de bundt o uno rectangular), distribuye los cubos de gelatina de colores de manera uniforme en el fondo.",
                                        "Paso 5: Vierte lentamente la mezcla de gelatina de leche condensada y sin sabor sobre los cubos de colores. Asegúrate de que los cubos se distribuyan bien y no floten en exceso.",
                                        "Paso 6: Refrigera la gelatina mosaico por al menos 4 horas, o idealmente toda la noche, hasta que esté completamente firme.",
                                        "Paso 7: Para desmoldar, sumerge el molde en agua tibia por unos segundos. Pasa un cuchillo delgado por los bordes y coloca un plato grande boca abajo sobre el molde. Voltea rápidamente el molde para que la gelatina caiga sobre el plato.",
                                        "Paso 8: Sirve fría. Puedes decorar con un poco de crema batida o hojas de menta si lo deseas."
                                    }
                                }
                            },
                            {
                                4, new Receta.VarianteReceta
                                {
                                    Porciones = 4,
                                    Ingredientes = new List<Receta.Ingrediente>
                                    {
                                        new Receta.Ingrediente { Nombre = "Leche condensada", Cantidad = "1 lata" },
                                        new Receta.Ingrediente { Nombre = "Sobres de gelatina de sabores (diferentes colores)", Cantidad = "4" }
                                    },
                                    Pasos = new List<string>
                                    {
                                        "Paso 1: Prepara cada sobre de gelatina de sabor individualmente según las instrucciones del paquete, pero usando un poco menos de agua para que queden más firmes. Vierte cada sabor en moldes cuadrados o rectangulares poco profundos. Refrigera hasta que cuajen por completo (mínimo 2-3 horas).",
                                        "Paso 2: Una vez cuajadas, corta cada gelatina de color en cubos pequeños (aproximadamente 1x1 cm).",
                                        "Paso 3: En un tazón grande, disuelve 1 sobre de gelatina sin sabor en 1 taza de agua caliente. Agrega la lata de leche condensada y mezcla bien hasta que la mezcla esté homogénea. Deja enfriar a temperatura ambiente.",
                                        "Paso 4: En un molde grande para gelatina (puede ser un molde de bundt o uno rectangular), distribuye los cubos de gelatina de colores de manera uniforme en el fondo.",
                                        "Paso 5: Vierte lentamente la mezcla de gelatina de leche condensada y sin sabor sobre los cubos de colores. Asegúrate de que los cubos se distribuyan bien y no floten en exceso.",
                                        "Paso 6: Refrigera la gelatina mosaico por al menos 4 horas, o idealmente toda la noche, hasta que esté completamente firme.",
                                        "Paso 7: Para desmoldar, sumerge el molde en agua tibia por unos segundos. Pasa un cuchillo delgado por los bordes y coloca un plato grande boca abajo sobre el molde. Voltea rápidamente el molde para que la gelatina caiga sobre el plato.",
                                        "Paso 8: Sirve fría. Puedes decorar con un poco de crema batida o hojas de menta si lo deseas."
                                    }
                                }
                            },
                            {
                                6, new Receta.VarianteReceta
                                {
                                    Porciones = 6,
                                    Ingredientes = new List<Receta.Ingrediente>
                                    {
                                        new Receta.Ingrediente { Nombre = "Leche condensada", Cantidad = "1 lata" },
                                        new Receta.Ingrediente { Nombre = "Sobres de gelatina de sabores (diferentes colores)", Cantidad = "4" }
                                    },
                                    Pasos = new List<string>
                                    {
                                        "Paso 1: Prepara cada sobre de gelatina de sabor individualmente según las instrucciones del paquete, pero usando un poco menos de agua para que queden más firmes. Vierte cada sabor en moldes cuadrados o rectangulares poco profundos. Refrigera hasta que cuajen por completo (mínimo 2-3 horas).",
                                        "Paso 2: Una vez cuajadas, corta cada gelatina de color en cubos pequeños (aproximadamente 1x1 cm).",
                                        "Paso 3: En un tazón grande, disuelve 1 sobre de gelatina sin sabor en 1 taza de agua caliente. Agrega la lata de leche condensada y mezcla bien hasta que la mezcla esté homogénea. Deja enfriar a temperatura ambiente.",
                                        "Paso 4: En un molde grande para gelatina (puede ser un molde de bundt o uno rectangular), distribuye los cubos de gelatina de colores de manera uniforme en el fondo.",
                                        "Paso 5: Vierte lentamente la mezcla de gelatina de leche condensada y sin sabor sobre los cubos de colores. Asegúrate de que los cubos se distribuyan bien y no floten en exceso.",
                                        "Paso 6: Refrigera la gelatina mosaico por al menos 4 horas, o idealmente toda la noche, hasta que esté completamente firme.",
                                        "Paso 7: Para desmoldar, sumerge el molde en agua tibia por unos segundos. Pasa un cuchillo delgado por los bordes y coloca un plato grande boca abajo sobre el molde. Voltea rápidamente el molde para que la gelatina caiga sobre el plato.",
                                        "Paso 8: Sirve fría. Puedes decorar con un poco de crema batida o hojas de menta si lo deseas."
                                    }
                                }
                            }
                        }
                    },
                    // --- RECETAS DE CATEGORÍA: FLAN (2) ---
                    new Receta
                    {
                        Titulo = "Flan Napolitano",
                        Imagen = "flan_napo.jpg",
                        Categoria = "Flan",
                        Dificultad = "Medio",
                        TiempoEstimadoMin = 90,
                        Descripcion = "Un postre cremoso y suave, con una capa de caramelo dorado y una textura sedosa. El flan napolitano es un clásico que deleita con su sabor dulce y su elegante presentación.",
                        VariantesPorciones = new Dictionary<int, Receta.VarianteReceta>
                        {
                            {
                                8, new Receta.VarianteReceta
                                {
                                    Porciones = 8,
                                    Ingredientes = new List<Receta.Ingrediente>
                                    {
                                        new Receta.Ingrediente { Nombre = "Azúcar (para el caramelo)", Cantidad = "1 taza" },
                                        new Receta.Ingrediente { Nombre = "Agua (para el caramelo)", Cantidad = "¼ taza" },
                                        new Receta.Ingrediente { Nombre = "Leche evaporada", Cantidad = "1 lata (360 ml)" },
                                        new Receta.Ingrediente { Nombre = "Leche condensada", Cantidad = "1 lata (397 g)" },
                                        new Receta.Ingrediente { Nombre = "Huevos grandes", Cantidad = "5" },
                                        new Receta.Ingrediente { Nombre = "Extracto de vainilla", Cantidad = "1 cucharadita" },
                                        new Receta.Ingrediente { Nombre = "Queso crema (opcional, para mayor cremosidad)", Cantidad = "100 g" }
                                    },
                                    Pasos = new List<string>
                                    {
                                        "Paso 1: Para el caramelo: En una flanera o molde de 20 cm, combina el azúcar y el agua. Cocina a fuego medio-alto sin revolver hasta que el azúcar se disuelva y se forme un caramelo dorado. Retira del fuego y mueve el molde para cubrir el fondo y los lados. Ten cuidado, el caramelo está muy caliente.",
                                        "Paso 2: Precalienta el horno a 180°C (350°F).",
                                        "Paso 3: En una licuadora, combina la leche evaporada, la leche condensada, los huevos, el extracto de vainilla y el queso crema (si lo usas). Licúa hasta que la mezcla esté completamente homogénea y suave.",
                                        "Paso 4: Vierte lentamente la mezcla del flan sobre el caramelo en el molde.",
                                        "Paso 5: Coloca la flanera dentro de una bandeja para hornear más grande. Vierte agua caliente en la bandeja grande hasta que llegue a la mitad de la altura de la flanera (baño María).",
                                        "Paso 6: Hornea durante 60-75 minutos, o hasta que el flan esté cuajado pero aún un poco tembloroso en el centro. Un cuchillo insertado cerca del borde debe salir limpio.",
                                        "Paso 7: Retira el flan del baño María y deja enfriar a temperatura ambiente. Luego, refrigera por al menos 4 horas o idealmente toda la noche antes de desmoldar.",
                                        "Paso 8: Para desmoldar, pasa un cuchillo delgado por los bordes del flan. Coloca un plato grande boca abajo sobre el molde y voltea rápidamente. El caramelo derretido cubrirá el flan."
                                    }
                                }
                            }
                        }
                    },
                    new Receta
                    {
                        Titulo = "Flan de Coco",
                        Imagen = "flan_coco.jpg",
                        Categoria = "Flan",
                        Dificultad = "Fácil",
                        TiempoEstimadoMin = 180, // Requiere refrigeración
                        Descripcion = "Un flan exótico y cremoso con el dulce y distintivo sabor del coco. Una deliciosa variación del flan clásico.",
                        VariantesPorciones = new Dictionary<int, Receta.VarianteReceta>
                        {
                            {
                                6, new Receta.VarianteReceta
                                {
                                    Porciones = 6,
                                    Ingredientes = new List<Receta.Ingrediente>
                                    {
                                        new Receta.Ingrediente { Nombre = "Leche de coco", Cantidad = "1 lata (400 ml)" },
                                        new Receta.Ingrediente { Nombre = "Leche condensada", Cantidad = "1 lata (397 g)" },
                                        new Receta.Ingrediente { Nombre = "Huevos grandes", Cantidad = "4" },
                                        new Receta.Ingrediente { Nombre = "Extracto de vainilla", Cantidad = "1 cucharadita" },
                                        new Receta.Ingrediente { Nombre = "Coco rallado (sin endulzar, opcional)", Cantidad = "½ taza" },
                                        new Receta.Ingrediente { Nombre = "Azúcar (para el caramelo)", Cantidad = "½ taza" },
                                        new Receta.Ingrediente { Nombre = "Agua (para el caramelo)", Cantidad = "2 cucharadas" }
                                    },
                                    Pasos = new List<string>
                                    {
                                        "Paso 1: Para el caramelo: En un molde de 20 cm, combina el azúcar y el agua. Cocina a fuego medio-alto hasta que se forme un caramelo dorado. Cubre el fondo y los lados del molde.",
                                        "Paso 2: Precalienta el horno a 180°C (350°F).",
                                        "Paso 3: En una licuadora, combina la leche de coco, la leche condensada, los huevos y el extracto de vainilla. Licúa hasta que esté suave. Si usas coco rallado, agrégalo y mezcla.",
                                        "Paso 4: Vierte la mezcla del flan sobre el caramelo en el molde.",
                                        "Paso 5: Coloca el molde dentro de una bandeja más grande. Vierte agua caliente en la bandeja hasta la mitad de la altura del molde (baño María).",
                                        "Paso 6: Hornea durante 50-60 minutos, o hasta que el flan esté firme.",
                                        "Paso 7: Retira del baño María y deja enfriar. Refrigera por al menos 3 horas o toda la noche.",
                                        "Paso 8: Desmolda y sirve frío."
                                    }
                                }
                            }
                        }
                    },
                    // --- RECETAS DE CATEGORÍA: GALLETAS (2) ---
                    new Receta
                    {
                        Titulo = "Galletas de Chispas de Chocolate",
                        Imagen = "galletas_chispas.jpg",
                        Categoria = "Galleta",
                        Dificultad = "Fácil",
                        TiempoEstimadoMin = 40,
                        Descripcion = "Clásicas galletas suaves y masticables, con un interior tierno y repleto de chispas de chocolate derretidas. Un postre casero ideal para cualquier momento.",
                        VariantesPorciones = new Dictionary<int, Receta.VarianteReceta>
                        {
                            {
                                12, new Receta.VarianteReceta
                                {
                                    Porciones = 12,
                                    Ingredientes = new List<Receta.Ingrediente>
                                    {
                                        new Receta.Ingrediente { Nombre = "Harina de trigo", Cantidad = "1 ½ tazas" },
                                        new Receta.Ingrediente { Nombre = "Bicarbonato de sodio", Cantidad = "1 cucharadita" },
                                        new Receta.Ingrediente { Nombre = "Sal", Cantidad = "½ cucharadita" },
                                        new Receta.Ingrediente { Nombre = "Mantequilla sin sal (a temperatura ambiente)", Cantidad = "100 g" },
                                        new Receta.Ingrediente { Nombre = "Azúcar morena compacta", Cantidad = "¾ taza" },
                                        new Receta.Ingrediente { Nombre = "Azúcar granulada", Cantidad = "¼ taza" },
                                        new Receta.Ingrediente { Nombre = "Huevo grande", Cantidad = "1" },
                                        new Receta.Ingrediente { Nombre = "Extracto de vainilla", Cantidad = "1 cucharadita" },
                                        new Receta.Ingrediente { Nombre = "Chispas de chocolate semiamargo", Cantidad = "1 taza" }
                                    },
                                    Pasos = new List<string>
                                    {
                                        "Paso 1: Precalienta el horno a 175°C (350°F). Cubre una bandeja para hornear con papel pergamino.",
                                        "Paso 2: En un tazón mediano, mezcla la harina, el bicarbonato de sodio y la sal.",
                                        "Paso 3: En un tazón grande, bate la mantequilla, el azúcar morena y el azúcar granulada hasta que estén suaves y cremosos.",
                                        "Paso 4: Agrega el huevo y el extracto de vainilla a la mezcla de mantequilla, batiendo bien.",
                                        "Paso 5: Poco a poco, incorpora los ingredientes secos a la mezcla húmeda, mezclando hasta que se combinen. Luego, añade las chispas de chocolate.",
                                        "Paso 6: Forma bolas de masa de aproximadamente 2 cucharadas y colócalas en la bandeja preparada, dejando espacio entre ellas.",
                                        "Paso 7: Hornea de 9 a 11 minutos, o hasta que los bordes estén ligeramente dorados y el centro siga suave. Las galletas pueden parecer poco cocidas, pero se endurecerán al enfriarse.",
                                        "Paso 8: Deja enfriar las galletas en la bandeja durante unos minutos antes de transferirlas a una rejilla para que se enfríen completamente."
                                    }
                                }
                            }
                        }
                    },
                    new Receta
                    {
                        Titulo = "Galletas de Avena con Pasas",
                        Imagen = "galletas_pasas.jpg",
                        Categoria = "Galleta",
                        Dificultad = "Fácil",
                        TiempoEstimadoMin = 40,
                        Descripcion = "Galletas suaves y masticables con el toque rústico de la avena y la dulzura de las pasas. Un clásico reconfortante para cualquier momento del día.",
                        VariantesPorciones = new Dictionary<int, Receta.VarianteReceta>
                        {
                            {
                                15, new Receta.VarianteReceta
                                {
                                    Porciones = 15,
                                    Ingredientes = new List<Receta.Ingrediente>
                                    {
                                        new Receta.Ingrediente { Nombre = "Harina de trigo", Cantidad = "1 taza" },
                                        new Receta.Ingrediente { Nombre = "Avena en hojuelas", Cantidad = "1.5 tazas" },
                                        new Receta.Ingrediente { Nombre = "Mantequilla sin sal (suave)", Cantidad = "115 g" },
                                        new Receta.Ingrediente { Nombre = "Azúcar morena", Cantidad = "¾ taza" },
                                        new Receta.Ingrediente { Nombre = "Azúcar granulada", Cantidad = "¼ taza" },
                                        new Receta.Ingrediente { Nombre = "Huevo grande", Cantidad = "1" },
                                        new Receta.Ingrediente { Nombre = "Extracto de vainilla", Cantidad = "1 cucharadita" },
                                        new Receta.Ingrediente { Nombre = "Bicarbonato de sodio", Cantidad = "1 cucharadita" },
                                        new Receta.Ingrediente { Nombre = "Sal", Cantidad = "½ cucharadita" },
                                        new Receta.Ingrediente { Nombre = "Pasas", Cantidad = "½ taza" }
                                    },
                                    Pasos = new List<string>
                                    {
                                        "Paso 1: Precalienta el horno a 175°C (350°F). Prepara una bandeja para hornear con papel pergamino.",
                                        "Paso 2: En un tazón mediano, mezcla la harina, el bicarbonato de sodio y la sal.",
                                        "Paso 3: En un tazón grande, bate la mantequilla con el azúcar morena y el azúcar granulada hasta que esté suave y cremoso.",
                                        "Paso 4: Agrega el huevo y el extracto de vainilla y bate bien.",
                                        "Paso 5: Poco a poco, incorpora los ingredientes secos a la mezcla húmeda, mezclando hasta que se combinen. Luego, añade la avena y las pasas.",
                                        "Paso 6: Forma bolas de masa de aproximadamente 2 cucharadas y colócalas en la bandeja preparada, dejando espacio entre ellas.",
                                        "Paso 7: Hornea de 10 a 12 minutos, o hasta que los bordes estén ligeramente dorados. El centro de las galletas aún puede parecer suave.",
                                        "Paso 8: Deja enfriar las galletas en la bandeja durante unos minutos antes de transferirlas a una rejilla para que se enfríen completamente."
                                    }
                                }
                            }
                        }
                    }
                };

                foreach (var receta in recetas)
                {
                    await _database.InsertAsync(receta);
                }
            }

            if (await _database.Table<Usuario>().CountAsync() == 0) // Fix for CS0246
            {
                var defaultUser = new Usuario // Fix for CS0246
                {
                    NombreUsuario = "test",
                    Contrasena = BCrypt.Net.BCrypt.HashPassword("password"), // Fix for CS0103
                    CorreoElectronico = "test@example.com"
                };
                await _database.InsertAsync(defaultUser);
            }
        }

        public async Task<bool> AuthenticateUserAsync(string username, string password)
        {
            await InitializeAsync();
            if (_database == null) return false; // Ensure _database is not null after InitializeAsync
            var user = await _database.Table<Usuario>().Where(u => u.NombreUsuario == username).FirstOrDefaultAsync(); // Fix for CS0246
            return user != null && BCrypt.Net.BCrypt.Verify(password, user.Contrasena); // Fix for CS0103
        }

        public async Task<bool> RegisterUserAsync(Usuario user) // Fix for CS0246
        {
            await InitializeAsync();
            if (_database == null) return false; // Ensure _database is not null after InitializeAsync
            var existingUser = await _database.Table<Usuario>().Where(u => u.NombreUsuario == user.NombreUsuario || u.CorreoElectronico == user.CorreoElectronico).FirstOrDefaultAsync(); // Fix for CS0246
            if (existingUser != null)
            {
                return false;
            }

            user.Contrasena = BCrypt.Net.BCrypt.HashPassword(user.Contrasena); // Fix for CS0103
            await _database.InsertAsync(user); // Fix for CS0246
            return true;
        }

        public async Task<Usuario?> GetUserByUsernameAsync(string username) // Fix for CS0246
        {
            await InitializeAsync();
            if (_database == null) return null; // Ensure _database is not null after InitializeAsync
            return await _database.Table<Usuario>().Where(u => u.NombreUsuario == username).FirstOrDefaultAsync(); // Fix for CS0246
        }

        public async Task<List<Receta>> GetRecetasAsync()
        {
            await InitializeAsync();
            if (_database == null) return new List<Receta>(); // Ensure _database is not null after InitializeAsync
            return await _database.Table<Receta>().ToListAsync();
        }

        public async Task<Receta?> GetRecetaAsync(int id)
        {
            await InitializeAsync();
            if (_database == null) return null; // Ensure _database is not null after InitializeAsync
            return await _database.Table<Receta>().Where(r => r.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Receta> GetRecetaByIdAsync(int id)
        {
            await InitializeAsync();
            return await _database.Table<Receta>().FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}