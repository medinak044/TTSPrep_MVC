using AutoMapper;
using TTSPrep_MVC.Data;
using TTSPrep_MVC.Repository.IRepository;

namespace TTSPrep_MVC.Repository;

public class UnitOfWork: IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public UnitOfWork(
        AppDbContext context,
        IMapper mapper
        )
    {
        _context = context;
        _mapper = mapper;
    }

    public IChapterRepository Chapters => new ChapterRepository(_context);
    public IPageRepository Pages => new PageRepository(_context);
    public IProjectRepository Projects => new ProjectRepository(_context);
    public IWordRepository Words => new WordRepository(_context);

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<bool> SaveAsync()
    {
        var saved = await _context.SaveChangesAsync(); // Returns an integer
        return saved > 0 ? true : false;
    }
}
