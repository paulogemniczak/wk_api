namespace WK.Domain
{
  public interface IUnitOfWorkTransaction : IDisposable
  {
    void Commit();
    void Rollback();
  }
}
