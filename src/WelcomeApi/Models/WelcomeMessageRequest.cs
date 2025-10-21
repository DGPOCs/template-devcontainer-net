using System.ComponentModel.DataAnnotations;

namespace WelcomeApi.Models;

public class WelcomeMessageRequest
{
    [Required]
    [MinLength(1)]
    [RegularExpression(@".*\S.*", ErrorMessage = "Message must contain non-whitespace characters.")]
    public string Message { get; set; } = string.Empty;
}
