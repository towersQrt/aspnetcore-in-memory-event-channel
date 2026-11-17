using Kendal.Web.Abstractions.Events;
using Microsoft.AspNetCore.Mvc;

namespace Kendal.Web.Controllers;

public sealed class HomeController : Controller
{
    private readonly IEventPublisher _eventPublisher;

    public HomeController(IEventPublisher eventPublisher)
    {
        _eventPublisher = eventPublisher;
    }

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken ct)
    {
        await _eventPublisher.PublishAsync(new Samples.Ping.Ping.Event("Hola desde web controller"), ct);

        return View();
    }
}
