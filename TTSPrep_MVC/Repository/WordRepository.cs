using System.Linq;
using System.Linq.Expressions;
using TTSPrep_MVC.Data;
using TTSPrep_MVC.Models;
using TTSPrep_MVC.Repository.IRepository;

namespace TTSPrep_MVC.Repository;

public class WordRepository : Repository<Word>, IWordRepository
{
    private AppDbContext _context;
    public WordRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

}
