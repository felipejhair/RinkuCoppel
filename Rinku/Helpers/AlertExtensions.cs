using Microsoft.AspNetCore.Mvc;

namespace Rinku.Helpers
{
    public static class AlertExtensions
    {
        public static IActionResult WithAlert(this IActionResult result, string alertClass, string message)
        {
            return new AlertDecoratorResult(result, alertClass, message);
        }
    }
}
