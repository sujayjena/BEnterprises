using System;
using BE.Services.Tool;

namespace BE.Data.Entity
{
    public class bl_Entity
    {
        public bool GenerateModel()
        {
            try
            {
                GenerateModel _GenerateModel = new GenerateModel();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public bool GenerateViewModel()
        {
            try
            {
                GenerateViewModel _GenerateViewModel = new GenerateViewModel();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public bool GenerateUnitOfWork()
        {
            try
            {
                GenerateIUnitOfWork _GenerateIUnitOfWork = new GenerateIUnitOfWork();
                GenerateUnitOfWork _GenerateUnitOfWork = new GenerateUnitOfWork();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public bool GenerateDbContext()
        {
            try
            {
                GenerateDbContext _GenerateDbContext = new GenerateDbContext();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }
    }
}
