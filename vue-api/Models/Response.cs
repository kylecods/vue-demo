using Application.Dtos;

namespace vue_api.Models
{
    public record Response<T>(string message, List<T>? items);
}
