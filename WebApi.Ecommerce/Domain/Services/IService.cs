using System.Threading.Tasks;
using WebApi.Ecommerce.Domain.Commands;

namespace WebApi.Ecommerce.Domain.Services
{
    public interface IService<T> where T : ICommand
    {
        Task<GenericCommandResult> Handle(T command);
    }
}
