namespace TTSPrep_MVC.Repository.IRepository;

public interface IUnitOfWork
{
    IChapterRepository Chapters { get; }
    IPageRepository Pages { get; }
    IProjectRepository Projects { get; }
    IWordRepository Words { get; }
    Task<bool> SaveAsync();
}
