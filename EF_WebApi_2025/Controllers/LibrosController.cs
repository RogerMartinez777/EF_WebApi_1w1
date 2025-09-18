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

        private bool isValid(Libro libro) // valida datos validos o que permiten nulos contra BD
        {
            return !string.IsNullOrEmpty(libro.Isbn) && 
                !string.IsNullOrEmpty(libro.Nombre) && 
                !string.IsNullOrEmpty(libro.FechaPublicacion) && 
                libro.Autor != 0;
        }
    }
}
