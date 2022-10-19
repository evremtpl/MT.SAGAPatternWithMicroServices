using MT.ReportService.Core.Interfaces.UnitOfWork;
using MT.ReportService.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.ReportService.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {


        private readonly AppDbContext _appDbContext;
        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }



        public void Commit()
        {
           
                try
                {
                    using (var transaction = _appDbContext.Database.BeginTransaction())
                    {
                        try
                        {
                        _appDbContext.SaveChanges();
                            transaction.Commit();
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
                catch (Exception e)
                {
                    //TODO:Logging
                }





        }

        public async Task CommitAsync()
        {

            try
            {
                using (var transaction = _appDbContext.Database.BeginTransaction())
                {
                    try
                    {
                        await _appDbContext.SaveChangesAsync();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception e)
            {
                //TODO:Logging
            }

            
        }
    }
}
