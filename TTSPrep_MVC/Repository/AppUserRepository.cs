using TTSPrep_MVC.Data;
using TTSPrep_MVC.Models;
using TTSPrep_MVC.Repository.IRepository;

namespace TTSPrep_MVC.Repository;

public class AppUserRepository: Repository<AppUser>, IAppUserRepository
{
    private AppDbContext _context;
    public AppUserRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}
