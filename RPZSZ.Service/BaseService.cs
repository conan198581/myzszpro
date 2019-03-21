using RPZSZ.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPZSZ.Service
{
    public class BaseService<T> where T:BaseEntity
    {
        private ZSZDbContext ctx;
        public BaseService(ZSZDbContext ctx)
        {
            this.ctx = ctx;
        }

        /// <summary>
        /// 获取所有 没有被伪删除 的 数据  iqueryable 和 ienumerable 的区别 iqueryable是在数据库中，ienumerable 是在内存中。
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            return ctx.Set<T>().Where(x => x.IsDeleted == false);
        }

        /// <summary>
        /// 获取所有的数据的总数
        /// </summary>
        /// <returns></returns>
        public long GetTotalCount()
        {
            return GetAll().LongCount();
        }


        /// <summary>
        /// 获取从第 startIndex 开始 ，取 count 条数据
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public IQueryable<T> GetPageData(int startIndex, int count)
        {
            //.net 中 skip take 似乎需要先排序 .net core中不需要;
            return GetAll().OrderBy(x=>x.CreateDateTime).Skip(startIndex).Take(count);
        }

        /// <summary>
        /// 通过id取查询 数据 ，查询到 返回 ，没有查询到 返回 null;
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(long id)
        {
            return GetAll().Where(x => x.Id == id).SingleOrDefault();
        }

        /// <summary>
        /// 软删除 一条记录
        /// </summary>
        /// <param name="id"></param>
        public void MarkDelete(long id)
        {
            T data = GetById(id);
            data.IsDeleted = true;
            ctx.SaveChanges();
        }

        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public long AddItem(T item)
        {
            ctx.Set<T>().Add(item);
            ctx.SaveChanges();
            return item.Id;
        }
        
    }
}
