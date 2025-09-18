using EF_WebApi_2025.Data.Models;

namespace EF_WebApi_2025.Data.Repositories
{
    public class LibroRepository : ILibroRepository
    {
        private LibrosDbContext _context; // Objeto de EF

        public LibroRepository(LibrosDbContext context) //Inyeccion de dependencia
        {
            _context = context;
        }

        public void Create(Libro libro)
        {
            _context.Libros.Add(libro);
            _context.SaveChanges(); //metodo de Unit of Work
        }

        public void Delete(int id)
        {
            var libroDeleted = GetById(id); //buscamos el objeto
            if(libroDeleted != null) // validamos porque podria ser nulo
            {
                _context.Libros.Remove(libroDeleted); //lo eliminamos
                _context.SaveChanges(); // guardamos cambios porque es una unidad de trabajo
            }
        }      

        public List<Libro> GetAll()
        {
            return _context.Libros.ToList();
        }

        public Libro? GetById(int id)
        {
            return _context.Libros.Find(id);
        }

        public void Update(Libro libro)
        {
            if (libro != null) 
            {
                _context.Libros.Update(libro);
                _context.SaveChanges(); 
            }
        }
    }
}
