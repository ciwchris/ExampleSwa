using System.Net;
using System.Threading.Tasks;
using Api.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace ShoppingListApp;

public class ShoppingLists
{
    private readonly ILogger logger;
    private readonly ShoppingListsService shoppingListsService;

    public ShoppingLists(ILoggerFactory loggerFactory, ShoppingListsService shoppingListsService)
    {
        logger = loggerFactory.CreateLogger<ShoppingLists>();
        this.shoppingListsService = shoppingListsService;
    }

    [Function(nameof(ShoppingLists))]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
    {
        logger.LogInformation("Retrieving lists");

        var result = await shoppingListsService.GetShoppingLists();

        var response = req.CreateResponse(HttpStatusCode.OK);
        _ = response.WriteAsJsonAsync(result);
        return response;
    }
}
