using TTSPrep_MVC.Data;
using TTSPrep_MVC.Models;
using TTSPrep_MVC.Repository.IRepository;

namespace TTSPrep_MVC.Repository;

public class ChapterRepository: Repository<Chapter>, IChapterRepository
{
    private AppDbContext _context;
    public ChapterRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}
