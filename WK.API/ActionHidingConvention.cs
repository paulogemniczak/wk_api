using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace WK.API
{
    public class ActionHidingConvention : IActionModelConvention
    {
        public void Apply(ActionModel action)
        {
            // Hide controller from Swagger
            if (action.Controller.ControllerName == "Example")
            {
                action.ApiExplorer.IsVisible = false;
            }
        }
    }
}
