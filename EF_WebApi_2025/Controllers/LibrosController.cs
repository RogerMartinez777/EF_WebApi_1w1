using EF_WebApi_2025.Data.Models;
using EF_WebApi_2025.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EF_WebApi_2025.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private ILibroRepository _repository; // necesitamos un repositorio

        public LibrosController(ILibroRepository repository) // ahora pasamos por parametro un repositorio
        {
            _repository = repository;
        }

        // GET: api/<LibrosController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_repository.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(500, "Ha ocurrido un error interno");
            }
        }

        // GET api/Libros/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var libro = _repository.GetById(id);
                if (libro == null)
                {
                    return NotFound($"Libro con el ID {id} no encontrado.");
                }
                return Ok(libro);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ha ocurrido un error interno al obtener el libro.");
            }
        }


        // POST api/<LibrosController>
        [HttpPost]
        public IActionResult Post([FromBody] Libro libro)
        {
            try
            {
                if (isValid(libro))
                {
                    _repository.Create(libro);
                    return Ok("Libro Creado!");  // 200
                }
                else
                {
                    return BadRequest("Los datos no son correctos o estan incompletos"); // 400
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Ha ocurrido un error interno"); // 500
            }
        }

        // DELETE api/<LibrosController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _repository.Delete(id);
                return Ok("Libro eliminado correctamente!"); //200
            }
            catch (Exception)
            {
                return StatusCode(500, "Ha ocurrido un error interno"); // 500
            }
        }

        // PUT api/Libros/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Libro libro)
        {
            try
            {
                // 1. Buscamos el libro existente en la base de datos.
                //    Este objeto es rastreado por el DbContext.
                var existingLibro = _repository.GetById(id);

                // Si el libro no existe, no hay nada que actualizar.
                if (existingLibro == null)
                {
                    return NotFound($"No se puede actualizar. Libro con el ID {id} no encontrado.");
                }

                // 2. Copiamos los valores actualizados del objeto 'libro' al 'existingLibro'
                existingLibro.Isbn = libro.Isbn;
                existingLibro.Nombre = libro.Nombre;
                existingLibro.FechaPublicacion = libro.FechaPublicacion;
                existingLibro.Genero = libro.Genero;
                existingLibro.Autor = libro.Autor;

                // 3. Llamamos al método Update del repositorio
                _repository.Update(existingLibro);

                return Ok("Libro actualizado correctamente.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Ha ocurrido un error interno al actualizar el libro.");
            }
        }

        private bool isValid(Libro libro) // valida datos validos o que permiten nulos contra BD
        {
            return !string.IsNullOrEmpty(libro.Isbn) &&
                !string.IsNullOrEmpty(libro.Nombre) &&
                !string.IsNullOrEmpty(libro.FechaPublicacion) &&
                libro.Autor != 0;
        }
    }
}