using TTSPrep_MVC.Data;
using TTSPrep_MVC.Models;
using TTSPrep_MVC.Repository.IRepository;

namespace TTSPrep_MVC.Repository;

public class PageRepository: Repository<Page>, IPageRepository
{
    private AppDbContext _context;
    public PageRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}
