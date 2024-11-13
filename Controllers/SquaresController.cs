using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebApplicationAPISquare.Models;

namespace WebApplicationAPISquare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SquaresController : ControllerBase
    {
        private readonly string filePath = "Squares.json";

        [HttpGet]
        public IActionResult GetSquares()
        {
            if (!System.IO.File.Exists(filePath))
                System.IO.File.WriteAllText(filePath, "[]");

            var json = System.IO.File.ReadAllText(filePath);
            var squares = JsonSerializer.Deserialize<List<Square>>(json);
            return Ok(squares);
        }

        [HttpPost]
        public IActionResult AddSquare([FromBody] Square square)
        {
            var json = System.IO.File.ReadAllText(filePath);
            var squares = JsonSerializer.Deserialize<List<Square>>(json) ?? new List<Square>();

            squares.Add(square);

            System.IO.File.WriteAllText(filePath, JsonSerializer.Serialize(squares));
            return Ok(square);
        }
    }
}

