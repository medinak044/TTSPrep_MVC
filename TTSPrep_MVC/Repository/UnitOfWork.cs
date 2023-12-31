using AutoMapper;
using TTSPrep_MVC.Data;
using TTSPrep_MVC.Helpers;
using TTSPrep_MVC.Repository.IRepository;

namespace TTSPrep_MVC.Repository;

public class UnitOfWork: IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UnitOfWork(
        AppDbContext context,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor
        )
    {
        _context = context;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public IChapterRepository Chapters => new ChapterRepository(_context);
    public IPageRepository Pages => new PageRepository(_context);
    public IProjectRepository Projects => new ProjectRepository(_context);
    public IWordRepository Words => new WordRepository(_context);

    public void Dispose()
    {
        _context.Dispose();
    }

    public string GetCurrentUserId()
    {
        return _httpContextAccessor.HttpContext?.User.GetUserId(); // Gets user Id value from cookie
    }

    public async Task<bool> SaveAsync()
    {
        var saved = await _context.SaveChangesAsync(); // Returns an integer
        return saved > 0 ? true : false;
    }
}
