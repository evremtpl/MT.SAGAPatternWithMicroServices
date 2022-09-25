
using System.Threading.Tasks;

namespace MT.ReportService.Core.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
        void Commit();
    }
}
