using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


using funkostore.Data;
using funkostore.Models;
using funkostore.Services;

namespace funkostore.Controllers.Rest
{
    [ApiController]
    [Route("api/productoref")]
    public class ProductoRestRefController : ControllerBase
    {
        private readonly ProductoService _service;


        public ProductoRestRefController(ProductoService service){
             _service = service;
        }


        [HttpPost]
        public Task<Producto> CreateProducto(Producto producto){
            return _service.crearProducto(producto);
        }
    }
}