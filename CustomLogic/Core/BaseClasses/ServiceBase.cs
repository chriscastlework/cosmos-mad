using System;
using System.Linq;
using System.Threading.Tasks;
using CustomLogic.Core.Helpers;
using CustomLogic.Core.Interfaces;
using CustomLogic.Core.Interfaces.BL;
using CustomLogic.Core.Interfaces.DL;
using CustomLogic.Core.Interfaces.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomLogic.Core.BaseClasses
{
    /// <summary>
    /// Generic ServiceBase business logic and data logic are loaded in dynamically changing this core class may cause major refactoring please take caution  
    /// </summary>
    /// <typeparam name="T">View Model </typeparam>
    /// <typeparam name="T2">Database Domain Model</typeparam>
    public abstract class ServiceBase<T, T2> : IService<T> where T2 : class
    {

        internal readonly IUnitOfWork UnitOfWork;

        internal IQueryable<T2> DbSet;

        protected ServiceBase(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
            this.DbSet = UnitOfWork.With<T2>();
        }

        private bool RunRepoLogic(InterfaceLoader<IRepoRule<T, T2>> interfaceLoader, Response<T> result, T model, ICoreUser user)
        {
            foreach (var interfaceImplementation in interfaceLoader.InterfaceImplementations)
            {
                if (!interfaceImplementation.Run(model, ref DbSet, UnitOfWork, result, user))
                {
                    return false;
                }
            }
            return true;
        }

        #region Insert

        public async Task<Response<T>> Insert(T model, ICoreUser user)
        {
            var result = new Response<T>();
            if (!RunRepoLogic(new InterfaceLoader<IRepoRule<T, T2>>(), result, model, user))
                return result;

            if (!RunInsertBusinessLogic(new InterfaceLoader<IInsertRule<T>>(), result, model, user))
                return result;

            if (!RunInsertDataLogic(new InterfaceLoader<IInsertEvent<T>>(), result, model, user))
                return result;

            result.Success = true;
            return result;
        }


        private bool RunInsertDataLogic(InterfaceLoader<IInsertEvent<T>> interfaceLoader, Response<T> result, T model, ICoreUser user)
        {
            foreach (var interfaceImplementation in interfaceLoader.InterfaceImplementations)
            {
                try
                {
                    if (!interfaceImplementation.Run(model, UnitOfWork, result, user))
                    {
                        return false;
                    }
                }
                catch (ObjectDisposedException ex)
                {
                    result.LogError("Error with running IInsertEvent " + ex.Message.ToString());
                    return false;
                }
                catch (InvalidOperationException ex)
                {
                    result.LogError("Error with running IInsertEvent " + ex.Message.ToString());
                    return false;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    result.LogError("Error with running IInsertEvent " + ex.Message.ToString());
                    return false;
                }
                catch (DbUpdateException ex)
                {
                    result.LogError("Error with running IInsertEvent " + String.Join(Environment.NewLine, ex.GetInnerExceptions().Select(c => c.Message)));
                    return false;
                }
                catch (Exception ex)
                {
                    result.LogError("Error with running IInsertEvent " + ex.Message.ToString());
                    return false;
                }

            }
            return true;
        }
        private bool RunInsertBusinessLogic(InterfaceLoader<IInsertRule<T>> interfaceLoader, Response<T> result, T model, ICoreUser user)
        {
            foreach (var interfaceImplementation in interfaceLoader.InterfaceImplementations)
            {
                if (!interfaceImplementation.Run(model, UnitOfWork, result, user))
                {
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region Update

        public async Task<Response<T>> Update(T model, ICoreUser user)
        {
            var result = new Response<T>();
            if (!RunRepoLogic(new InterfaceLoader<IRepoRule<T, T2>>(), result, model, user))
                return result;

            if (!RunUpdateBusinessLogic(new InterfaceLoader<IUpdateRule<T, T2>>(), result, model, user))
                return result;

            if (!RunUpdateDataLogic(new InterfaceLoader<IUpdateEvent<T, T2>>(), result, model, user))
                return result;

            result.Success = true;
            return result;
        }

        private bool RunUpdateDataLogic(InterfaceLoader<IUpdateEvent<T, T2>> interfaceLoader, Response<T> result, T model, ICoreUser user)
        {
            foreach (var interfaceImplementation in interfaceLoader.InterfaceImplementations.OrderBy(c => c.priority()))
            {
                if (!interfaceImplementation.Run(model, ref DbSet, UnitOfWork, result, user))
                {
                    return false;
                }
            }
            return true;
        }

        private bool RunUpdateBusinessLogic(InterfaceLoader<IUpdateRule<T, T2>> interfaceLoader, Response<T> result, T model, ICoreUser user)
        {
            foreach (var interfaceImplementation in interfaceLoader.InterfaceImplementations)
            {
                if (!interfaceImplementation.Run(model, ref DbSet, UnitOfWork, result, user))
                {
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region Delete

        public async Task<Response<T>> Delete(T model, ICoreUser user)
        {
            var result = new Response<T>();

            if (!RunRepoLogic(new InterfaceLoader<IRepoRule<T, T2>>(), result, model, user))
                return result;

            if (!RunDeleteBusinessLogic(new InterfaceLoader<IDeleteRule<T, T2>>(), result, model, user))
                return result;

            if (!RunDeleteDataLogic(new InterfaceLoader<IDeleteEvent<T, T2>>(), result, model, user))
                return result;

            result.Success = true;
            return result;
        }

        private bool RunDeleteDataLogic(InterfaceLoader<IDeleteEvent<T, T2>> interfaceLoader, Response<T> result, T model, ICoreUser user)
        {
            foreach (var interfaceImplementation in interfaceLoader.InterfaceImplementations)
            {
                if (!interfaceImplementation.Run(model, ref DbSet, UnitOfWork, result, user))
                {
                    return false;
                }
            }
            return true;
        }

        private bool RunDeleteBusinessLogic(InterfaceLoader<IDeleteRule<T, T2>> interfaceLoader, Response<T> result, T model, ICoreUser user)
        {
            foreach (var interfaceImplementation in interfaceLoader.InterfaceImplementations)
            {
                if (!interfaceImplementation.Run(model, ref DbSet, UnitOfWork, result, user))
                {
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region View

        public async Task<Response<T>> View(T model, ICoreUser user)
        {
            var result = new Response<T>();

            if (!RunRepoLogic(new InterfaceLoader<IRepoRule<T, T2>>(), result, model, user))
                return result;

            if (!RunViewBusinessLogic(new InterfaceLoader<IViewRule<T, T2>>(), result, model, user))
                return result;

            if (!RunViewDataLogic(new InterfaceLoader<IViewEvent<T, T2>>(), result, model, user))
                return result;

            result.Success = true;
            return result;
        }

        private bool RunViewDataLogic(InterfaceLoader<IViewEvent<T, T2>> interfaceLoader, Response<T> result, T model, ICoreUser user)
        {
            foreach (var interfaceImplementation in interfaceLoader.InterfaceImplementations)
            {
                var viewResult = interfaceImplementation.Run(model, ref DbSet, UnitOfWork, result, user);
                if (!result.Success)
                {
                    return false;
                }
            }
            return true;
        }

        private bool RunViewBusinessLogic(InterfaceLoader<IViewRule<T, T2>> interfaceLoader, Response<T> result, T model, ICoreUser user)
        {
            foreach (var interfaceImplementation in interfaceLoader.InterfaceImplementations)
            {
                if (!interfaceImplementation.Run(model, ref DbSet, UnitOfWork, result, user))
                {
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region View List
        public async Task<NgTable<T>> List(NgTableParams ngTableParams, ICoreUser user)
        {
            var result = new NgTable<T>();

            //if (!RunRepoLogic(new InterfaceLoader<IRepoRule<T, T2>>(), result, model, user))
            //    return result;

            if (!RunViewListBusinessLogic(new InterfaceLoader<IViewListRule<T, T2>>(), result, ngTableParams, user))
                return result;

            if (!RunViewListDataLogic(new InterfaceLoader<IViewListEvent<T, T2>>(), result, ngTableParams, user))
                return result;

            result.Success = true;
            return result;
        }

        private bool RunViewListDataLogic(InterfaceLoader<IViewListEvent<T, T2>> interfaceLoader, NgTable<T> result, NgTableParams ngTableParams, ICoreUser user)
        {
            foreach (var interfaceImplementation in interfaceLoader.InterfaceImplementations)
            {
                if (!interfaceImplementation.Run(ngTableParams, ref DbSet, result, user, UnitOfWork))
                {
                    return false;
                }
            }
            return true;
        }

        private bool RunViewListBusinessLogic(InterfaceLoader<IViewListRule<T, T2>> interfaceLoader, NgTable<T> result, NgTableParams ngTableParams, ICoreUser user)
        {
            foreach (var interfaceImplementation in interfaceLoader.InterfaceImplementations)
            {
                if (!interfaceImplementation.Run(ngTableParams, ref DbSet, result, user, UnitOfWork))
                {
                    return false;
                }
            }
            return true;
        }

        #endregion

    }
}
