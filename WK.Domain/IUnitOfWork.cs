namespace WK.Domain
{
  public interface IUnitOfWork
  {
    IUnitOfWorkTransaction Begin(params IRepository[] repositories);
  }
}
