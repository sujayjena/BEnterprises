using System;
using BE.Core.ViewModel;
using BE.Services.Tool;

namespace BE.Data.Entity
{
    public class bl_Entity
    {
        public bool GenerateModel(Sys_GenerateViewModel sys_GenerateViewModel)
        {
            try
            {
                GenerateModel _GenerateModel = new GenerateModel(sys_GenerateViewModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public bool GenerateViewModel(Sys_GenerateViewModel sys_GenerateViewModel)
        {
            try
            {
                GenerateViewModel _GenerateViewModel = new GenerateViewModel(sys_GenerateViewModel);
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
