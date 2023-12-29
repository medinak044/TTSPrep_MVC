using TTSPrep_MVC.Data;
using TTSPrep_MVC.Models;
using TTSPrep_MVC.Repository.IRepository;

namespace TTSPrep_MVC.Repository;

public class ProjectRepository: Repository<Project>, IProjectRepository
{
    private AppDbContext _context;
    public ProjectRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}
