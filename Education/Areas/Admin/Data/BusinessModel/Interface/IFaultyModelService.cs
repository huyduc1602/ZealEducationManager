using Education.Areas.Admin.Data.DataModel;
using Education.Areas.Admin.Data.ViewModel;
using Education.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Areas.Admin.Data.BusinessModel.Interface
{
    interface IFaultyModelService
    {
        Faulty ConvertFaulty(FaultyModel model);
        Faulty ConvertFaulty(FaultyModel model, Faulty faulty);
        List<FaultyView> ConvertListFaultyView(List<Faulty> list);
        FaultyView ConvertFaultyView(Faulty entity);
        User ConvertUser(FaultyModel model);
        User ConvertUser(FaultyModel model, User user);
        FaultyModel ConvertFaultyModel(Faulty faulty, User user);
    }
}
