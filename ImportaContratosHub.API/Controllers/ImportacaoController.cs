using Microsoft.AspNetCore.Mvc;

namespace ImportaContratosHub.API.Controllers;
public class ImportacaoController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
