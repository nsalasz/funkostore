using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using funkostore.Data;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;
using funkostore.DTO;

namespace funkostore.Controllers
{
    
    public class ProformaController : Controller
    {
        private readonly ILogger<ProformaController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager; 
        public ProformaController(ApplicationDbContext context,
            ILogger<ProformaController> logger,
            UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var userIDSession = _userManager.GetUserName(User);
            //SELECT * FROM Proforma p,Producto pr WHERE p.productId=pr.Id And p.UserId=? And p.status='PENDIENTE' 

            var items = from o in _context.DataProforma select o;
            items = items.Include(p => p.Producto).
                    Where(w => w.UserID.Equals(userIDSession) &&
                     w.Status.Equals("PENDIENTE"));
            var itemsCarrito = items.ToList();
            //Fila1 1234, Shampo; Precio, Cantidad
            //Fila2 12345, Shampo3; Precio, Cantidad
            var total = itemsCarrito.Sum(c => c.Cantidad * c.Precio);

            //MEMORIA
            dynamic model = new ExpandoObject();
            model.montoTotal = total;
            model.elementosCarrito = itemsCarrito;

            //Carrito carrito = new Carrito();
            //carrito.total = total;
            //carrito.itemsCarrito = itemsCarrito;

            //return View(carrito);
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}