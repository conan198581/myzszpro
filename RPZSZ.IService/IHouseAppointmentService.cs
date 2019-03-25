using RPZSZ.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPZSZ.IService
{

    /// <summary>
    /// 房子预约接口
    /// </summary>
    public interface IHouseAppointmentService:IServiceSupport
    {
        /// <summary>
        /// 新增 房子 预约记录
        /// </summary>
        /// <param name="userId">谁预约（也可以不登陆预约）</param>
        /// <param name="name">预约者姓名</param>
        /// <param name="phoneNum">预约者手机</param>
        /// <param name="houseId">预约的房子id</param>
        /// <param name="visitData">预约时间</param>
        /// <returns></returns>
        long AddNew(long? userId, string name, string phoneNum, long houseId, DateTime visitData);
        HouseAppointmentDTO GetHouseAppointmentById(long houseAppoimentId);
        HouseAppointmentDTO[] GetPageData(long cityId, string status, int startIndex, int pageSize);
        long GetTotalCount(long cityId, string status);


    }
}
