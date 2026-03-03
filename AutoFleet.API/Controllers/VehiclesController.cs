using Microsoft.AspNetCore.Mvc;
using AutoFleet.Application.Interfaces;
using AutoFleet.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

namespace AutoFleet.API.Controllers
{
    [Authorize] // Authentication required
    [ApiController] // Automatic validations and API behavior
    [Route("api/[controller]")] // Url: MyIP... api/vehicles
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        // Dependency Injection: Asking SERVICE as Interface, not the concretion? (O concreción)
        public VehiclesController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        /// <summary>
        /// Obtiene todos los vehículos registrados en la base de datos.
        /// </summary>
        /// <returns>Lista completa de vehículos.</returns>
        /// <response code="200">Devuelve la lista de vehículos.</response>
        /// <response code="401">No autenticado.</response>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vehicles = await _vehicleService.GetAllVehiclesAsync();
            return Ok(vehicles); // List of vehicles
        }

        /// <summary>
        /// Crea un nuevo vehículo en el inventario.
        /// </summary>
        /// <remarks>
        /// Valida los datos de entrada según las reglas de negocio (año, rango de precios, etc.).
        /// </remarks>
        /// <param name="vehicleDto">Datos del vehículo a crear.</param>
        /// <returns>El vehículo creado con su ID asignado.</returns>
        /// <response code="201">Vehículo creado exitosamente.</response>
        /// <response code="400">Datos de entrada inválidos.</response>
        /// <response code="401">No autenticado.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVehicleDto vehicleDto)
        {
            // [ApiController] Validates the DTO (Required, StringLength, etc.)
            // If it is not valid data, we return BadRequest (400) auto

            try
            {
                var createdVehicle = await _vehicleService.CreateVehicleAsync(vehicleDto);

                // Retorna 201 Created
                return CreatedAtAction(nameof(GetAll), new { }, createdVehicle);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno: " + ex.Message);
            }
        }


        /*
        External API Integration (NHTSA)
        Objective: Consume third-party REST service (Requirement of the study guide from which the project is being carried out).
        Source:      NHTSA vPIC API (Gobierno USA - Open Data).
        Doc Oficial: https://vpic.nhtsa.dot.gov/api/
        
        Integration Process:
        1. Selected endpoint: GetModelsForMake
        2. This API returns XML by default. '?format=json' must be appended
            in the QueryString for modern interoperability.
        3. Architecture: Implemented as a "Proxy Passthrough" in the controller
            for quick demonstration. In production, this would go into a service 
            'IVehicleInfoProvider' within Infrastructure.
        */
        /// <summary>
        /// [PROXY] Consults the government database of the NHTSA (USA) to obtain models.
        /// </summary>
        /// <remarks>
        /// This endpoint acts as a Gateway to the public vPIC API (Vehicle Product Information Catalog).
        /// Useful for validating brands or populating model selectors without maintaining a private database.
        /// <br/>
        /// <strong>Fuente:</strong> https://vpic.nhtsa.dot.gov/api/
        /// </remarks>
        /// <param name="make">Brand name (e.g., "tesla", "ford", "bmw").</param>
        /// <returns>Raw JSON with the list of official models.</returns>
        /// <response code="200">Returns the list of models in JSON format.</response>
        /// <response code="502">Bad Gateway. Error connecting to the external API.</response>
        [HttpGet("external-models/{make}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<IActionResult> GetModelsFromExternalApi(string make)
        {
            // 1. HttpClient (TODO, use IHttpClientFactory)
            using var httpClient = new HttpClient();
            
            // 2. Building URL
            // Tip: The API returns XML by default, force JSON with ?format=json
            var url = $"https://vpic.nhtsa.dot.gov/api/vehicles/getmodelsformake/{make}?format=json";

            try 
            {
                // 3. Async Call
                var response = await httpClient.GetAsync(url);
                
                if (!response.IsSuccessStatusCode)
                    return StatusCode(StatusCodes.Status502BadGateway, "Error connecting to NHTSA");

                var jsonString = await response.Content.ReadAsStringAsync();
                
                // Return content as is (Proxy Passthrough)
                return Content(jsonString, "application/json");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno: {ex.Message}");
            }
        }
    }
}
